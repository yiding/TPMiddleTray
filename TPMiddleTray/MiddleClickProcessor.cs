using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using SYNCOMLib;

namespace TPMiddleTray {

  sealed class DeviceHandler : IDisposable {
    private readonly ISynDevice device;
    private bool middleButtonDown = false;
    private Stopwatch middleButtonDownStopwatch = new Stopwatch();
    public AutoResetEvent Event { get; }

    private DeviceHandler(ISynDevice device, AutoResetEvent waitHandle) {
      this.device = device;
      Event = waitHandle;
    }

    public static DeviceHandler Create(ISynDevice device) {
      var waitHandle = new AutoResetEvent(false);
      device.SetEventNotification(waitHandle.SafeWaitHandle);
      return new DeviceHandler(device, waitHandle);
    }

    public void HandlePackets() {
      SynPacket packet = new SynPacket();
      while (true) {
        int result = device.LoadPacket(packet);
        if (result == Constants.E_FAIL) {
          break;
        } else if (result != 0) {
          throw new COMException("Failed in LoadPacket", result);
        }
        HandlePacket(packet);
      }
    }

    private void HandlePacket(ISynPacket packet) {
      var (x, y, z) = packet.GetDelta();
      if (x != 0 || y != 0 || z != 0) {
        // If movement is detected, disarm the button state so we don't trigger
        // middle click events when button is released.
        middleButtonDown = false;
      } else {
        var buttonState = packet.GetButtonState();

        // When set to scrolling, reports ButtonVirtualMiddle | ButtonMiddle
        // When set to middle-click, reports ButtonReportedMiddle | ButtonMiddle

        // Checking no motion between down and up when in scrolling mode, and 
        // emit a middle click event on release.
        bool currentMiddleDown = (buttonState & SynButtonFlags.ButtonVirtualMiddle) != 0;
        if (middleButtonDown && !currentMiddleDown) {
          // middle button was released with no other movement.
          middleButtonDown = false;
          if (middleButtonDownStopwatch.ElapsedMilliseconds < 250) {
            SendMiddleClick();
          }
        } else if (!middleButtonDown && currentMiddleDown) {
          middleButtonDown = true;
          middleButtonDownStopwatch.Restart();
        }
      }
    }

    private static void SendMiddleClick() {
      var down = new MOUSEINPUT() {
        dwFlags = MOUSEEVENTF.MIDDLEDOWN,
      };
      var up = new MOUSEINPUT() {
        dwFlags = MOUSEEVENTF.MIDDLEUP,
      };
      Interop.SendInput(2, new INPUT[] { down, up }, INPUT.Size);
    }

    public void Dispose() {
      Event.Dispose();
    }
  }

  public delegate void DevicesEnumerated(int nDevices);

  public class MiddleClickProcessor : IDisposable {
    private ISynAPI synapi = null;
    private readonly AutoResetEvent apiEvent;
    public event DevicesEnumerated DevicesEnumerated;

    public MiddleClickProcessor() {
      apiEvent = new AutoResetEvent(false);
    }

    public void Dispose() {
      apiEvent.Dispose();
    }

    private IEnumerable<ISynDevice> EnumerateDevices() {
      var allDevices = new List<ISynDevice>();
      foreach (ConnectionType connection in Enum.GetValues(typeof(ConnectionType))) {
        if (connection == ConnectionType.ANY) {
          continue;
        }
        foreach (DeviceType type in Enum.GetValues(typeof(DeviceType))) {
          if (type == DeviceType.Any) {
            continue;
          }
          var devices = synapi.FindDevices(connection, type);
          foreach (var device in devices) {
            if (device != null) {
              var model = device.GetStringProperty(DeviceStringProperty.ModelString);
              var shortName = device.GetStringProperty(DeviceStringProperty.ShortName);
              Console.WriteLine($"Detected: {model} {shortName} {connection} {type}");

              // use-for-scrolling = 0
              // use-for-middle-click = 4
              var middleButtonAction = (SynActions)device.GetProperty(DeviceProperty.MiddleButtonAction);
              Console.WriteLine($"MiddleButtonAction: {middleButtonAction}");
              allDevices.Add(device);
            }
          }
        }
      }
      return allDevices;
    }

    public void WorkLoop() {
      synapi = new SynAPI();
      synapi.Initialize();
      synapi.SetEventNotification(apiEvent.SafeWaitHandle);

      while (true) {
        var deviceHandlers = EnumerateDevices()
          .Select(DeviceHandler.Create)
          .ToList();

        DevicesEnumerated?.Invoke(deviceHandlers.Count);

        var waitHandles = deviceHandlers
          .Select(handler => handler.Event)
          .Append(apiEvent)
          .ToArray();

        while (true) {
          var handleIdx = WaitHandle.WaitAny(waitHandles);
          if (handleIdx < deviceHandlers.Count) {
            // Handle packets from this device.
            deviceHandlers[handleIdx].HandlePackets();
          } else {
            // Reinitialize devices.
            Console.WriteLine("Reinitializing devices.");
            break;
          }
        }
        deviceHandlers.ForEach(device => device.Dispose());
      }
    }
  }
}
