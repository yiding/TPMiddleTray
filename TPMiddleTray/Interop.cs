using System;
using System.Runtime.InteropServices;

namespace TPMiddleTray {
  public static class Interop {
    [DllImport("user32.dll")]
    public static extern uint SendInput(
    uint nInputs,
    [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs,
    int cbSize);
  }

  public enum InputType : uint {
    MOUSE = 0,
    KEYBOARD = 1,
    HARDWARE = 2,
  }

  [Flags]
  public enum MOUSEEVENTF : uint {
    MIDDLEDOWN = 0x0020,
    MIDDLEUP = 0x0040,
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct INPUT {
    internal InputType type;
    internal InputUnion U;
    internal static int Size {
      get { return Marshal.SizeOf(typeof(INPUT)); }
    }

    public static implicit operator INPUT(MOUSEINPUT mi) {
      return new INPUT() {
        type = InputType.MOUSE,
        U = new InputUnion {
          mi = mi
        }
      };
    }
  }

  [StructLayout(LayoutKind.Explicit)]
  internal struct InputUnion {
    [FieldOffset(0)]
    internal MOUSEINPUT mi;
    [FieldOffset(0)]
    internal KEYBDINPUT ki;
    [FieldOffset(0)]
    internal HARDWAREINPUT hi;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct MOUSEINPUT {
    internal int dx;
    internal int dy;
    internal int mouseData;
    internal MOUSEEVENTF dwFlags;
    internal uint time;
    internal UIntPtr dwExtraInfo;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct KEYBDINPUT {
    internal ushort wVk;
    internal ushort wScan;
    internal uint dwFlags;
    internal uint time;
    internal UIntPtr dwExtraInfo;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct HARDWAREINPUT {
    internal uint uMsg;
    internal ushort wParamL;
    internal ushort wParamH;
  }
}
