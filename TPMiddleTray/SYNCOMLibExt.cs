using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace SYNCOMLib {
  // Constants and things not exposed in the com API type library.

  public enum ConnectionType : int {
    ANY = 0,
    COM = 1,
    PS2 = 2,
    USB = 3,
  }

  public enum DeviceType : int {
    Any = 0,
    Mouse,
    TouchPad,
    WheelMouse,
    IBMCompatibleStick,
    Styk,
    FiveButtonWheelMouse,
    cPad,
  }

  public enum APIProperty : int {
    First = 0x10000000,
    Version,
    MaxDevices,
    DevicesPresent,
    DriverVersion,
    RequiredDriverVersion,
    Last = First + 0x100,
  }

  public enum DeviceProperty : int {
    First = APIProperty.Last,
    Handle,
    DeviceType,
    ConnectionType,

    FWVersion,
    Geometry,
    SensorType,
    ProductType,
    ASICType = ProductType + 2,
    ReportRate = ASICType + 4,
    Gestures,
    SecondaryGestures,
    EdgeMotionOptions,
    EdgeMotionSpeed,
    MotionRotationAngle,

    XDPI,
    YDPI,
    XLoSensor,
    YLoSensor,
    XHiSensor,
    YHiSensor,
    XLoRim,
    YLoRim,
    XHiRim,
    YHiRim,
    XLoBorder,
    YLoBorder,
    XHiBorder,
    YHiBorder,
    YLoBorderVScroll,
    YHiBorderVScroll,
    XLoWideBorder,
    YLoWideBorder,
    XHiWideBorder,
    YHiWideBorder,
    ZMaximum,
    ZTouchThreshold,

    TopLeftCornerWidth,
    TopLeftCornerHeight,
    TopRightCornerWidth,
    TopRightCornerHeight,
    BottomRightCornerWidth,
    BottomRightCornerHeight,
    BottomLeftCornerWidth,
    BottomLeftCornerHeight,

    TopLeftCornerAction,
    TopRightCornerAction,
    BottomRightCornerAction,
    BottomLeftCornerAction,

    LeftButtonAction,
    RightButtonAction,
    BothButtonAction,
    MiddleButtonAction,
    UpButtonAction,
    DownButtonAction,
    Ex1ButtonAction,
    Ex2ButtonAction,
    Ex3ButtonAction,
    Ex4ButtonAction,
    Ex5ButtonAction,
    Ex6ButtonAction,
    Ex7ButtonAction,
    Ex8ButtonAction,

    ExtendedButtons,

    // Boolean properties.
    HasMiddleButton,
    HasUpDownButtons,

    IsMultiFingerCapable,
    IsPenCapable,
    IsVScroll,
    IsHScroll,
    IsWEMode,
    IsLowReportRate,
    IsHighReportRate,

    IsTapEnabled,
    IsDragEnabled,
    IsDragLockEnabled,
    IsCornerTapEnabled,
    IsEdgeMotionEnabled,
    IsEdgeMotionDragEnabled,
    IsEdgeMotionMoveEnabled,

    IsReleaseToSelectEnabled,
    IsMiddleTapToHelpEnabled,
    IsMiddleButtonBlockEnabled,
    IsPressureDragEnabled,
    Is3ButtonEnabled,
    IsPressureEdgeMotionEnabled,
    IsMiddleButtonLock,

    // Button 4 & 5 support
    Button4Action,
    Button5Action,

    // Somewhat vetted enhancements app properties.
    VerticalScrollingFlags = Button5Action + 17,
    HorizontalScrollingFlags,

    // New COM specific properties
    DisplayFlags,

    // Newer properties.
    ModelId,
    DisableState,

    Last = First + 0x200
  }

  public enum PacketProperty : int {
    First = DeviceProperty.Last,
    AssociatedDeviceHandle,
    SequenceNumber,
    TimeStamp,
    XRaw,
    ZXRaw = XRaw,
    YRaw,
    ZRaw,
    ZYRaw,
    W,
    X,
    Y,
    Z,
    XDelta,
    YDelta,
    ZDelta,
    XMickeys,
    YMickeys,
    AnachronisticState,
    FingerState,
    ExtendedState,
    ButtonState,
    ExtraFingerState,
    Last = First + 0x100
  }

  public enum DisplayProperty {
    First = PacketProperty.Last,
    BackLightState,
    DisplayRows,
    DisplayColumns,
    DisplayOwned,
    BackLightOnOffOnce,
    Last = First + 0x100
  };

  public enum APIStringProperty {
    First = DisplayProperty.Last,
    VersionString,
    Last = First + 0x100
  };

  public enum DeviceStringProperty {
    First = APIStringProperty.Last,
    ModelString,
    PnPID,
    ShortName,
    Last = First + 0x100,
  };

  [Flags]
  public enum SynButtonFlags : int {
    ButtonLeft = 0x00000001,
    ButtonRight = 0x00000002,
    ButtonMiddle = 0x00000004,
    ButtonUp = 0x00000010,
    ButtonDown = 0x00000020,
    Button4 = 0x00000040,
    Button5 = 0x00000080,
    ButtonExtended1 = 0x00000100,
    ButtonExtended2 = 0x00000200,
    ButtonExtended3 = 0x00000400,
    ButtonExtended4 = 0x00000800,
    ButtonExtended5 = 0x00001000,
    ButtonExtended6 = 0x00002000,
    ButtonExtended7 = 0x00004000,
    ButtonExtended8 = 0x00008000,
    ButtonReportedLeft = 0x00010000,
    ButtonReportedRight = 0x00020000,
    ButtonReportedMiddle = 0x00040000,
    ButtonReported4 = 0x00080000,
    ButtonReported5 = 0x01000000,
    ButtonVirtualLeft = 0x00100000,
    ButtonVirtualRight = 0x00200000,
    ButtonVirtualMiddle = 0x00400000,
    ButtonVirtual4 = 0x00800000,
    ButtonVirtual5 = 0x02000000,
    ButtonAnyVirtual = 0x02f00000,
    ButtonAnyReported = 0x010f0000,
    ButtonAnyPhysical = 0x0000ffff,
    ButtonAny = 0x03ffffff,
  }

  [Flags]
  public enum SynActions {
    None = 0,
    Primary = 1,
    Secondary = 2,
    Auxiliary = 4,
  }

  public static class Constants {
    public const int E_FAIL = unchecked((int)0x80004005);
  }

  static class PacketExt {
    public static int GetProperty(this ISynPacket packet, PacketProperty property) {
      packet.GetProperty((int)property, out int value);
      return value;
    }

    public static SynButtonFlags GetButtonState(this ISynPacket packet) {
      return (SynButtonFlags)GetProperty(packet, PacketProperty.ButtonState);
    }

    public static (int, int, int) GetDelta(this ISynPacket packet) {
      int x = GetProperty(packet, PacketProperty.XDelta);
      int y = GetProperty(packet, PacketProperty.YDelta);
      int z = GetProperty(packet, PacketProperty.ZDelta);
      return (x, y, z);
    }
  }

  static class Ext {
    public static IEnumerable<SynDevice> FindDevices(this ISynAPI synApi, ConnectionType connectionType, DeviceType deviceType) {
      int handle = 0;
      var devices = new List<SynDevice>();
      while (true) {
        try {
          synApi.FindDevice((int)connectionType, (int)deviceType, ref handle);
        } catch (FileNotFoundException) {
          return devices;
        }
        synApi.CreateDevice(handle, out SynDevice device);
        devices.Add(device);
      }
    }

    public static int GetProperty(this ISynDevice device, DeviceProperty property) {
      device.GetProperty((int)property, out int value);
      return value;
    }


    public static string GetStringProperty(
        this ISynDevice device,
        DeviceStringProperty property) {
      int len = 256;
      var buf = Marshal.AllocHGlobal(len);
      try {
        unsafe {
          device.GetStringProperty((int)property, ref *(byte*)buf.ToPointer(), len);
        }
        return Marshal.PtrToStringAnsi(buf);
      } finally {
        Marshal.FreeHGlobal(buf);
      }
    }
  }
}