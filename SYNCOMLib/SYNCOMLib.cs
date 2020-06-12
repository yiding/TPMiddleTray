// SYNCOMLib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// Global type: <Module>
// Architecture: AnyCPU (64-bit preferred)
// Runtime: v4.0.30319
// Hash algorithm: SHA1

using Microsoft.Win32.SafeHandles;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: TypeLibVersion(2, 0)]
[assembly: Guid("E2489565-2CE5-4690-9111-76E79A9F6CCD")]
[assembly: ImportedFromTypeLib("SYNCOMLib")]
[assembly: AssemblyVersion("2.0.0.0")]
namespace SYNCOMLib
{
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("41320763-F0EC-4B7F-9A2E-B4DA92C80FE7")]
	public interface ISynAPI
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Initialize();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void FindDevice(int lConnectionType, int lDeviceType, ref int ulHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateDevice(int lHandle, [MarshalAs(UnmanagedType.Interface)] out SynDevice ppDevice);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetProperty(int lSpecifier, out int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetStringProperty(int lSpecifier, ref byte pBuffer, ref int ulBufLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetProperty(int lSpecifier, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetEventNotification(SafeWaitHandle hEvent);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetEventParameter(ref int lParameter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void PersistState(int lStateFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void RestoreState(int lStateFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void HardwareBroadcast(int lAction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetNotifyService();
	}
	[ComImport]
	[Guid("E7D5F8AC-866C-4C8C-AF5F-F28DE4918647")]
	[CoClass(typeof(SynDeviceClass))]
	public interface SynDevice : ISynDevice
	{
	}
	[ComImport]
	[CoClass(typeof(SynAPIClass))]
	[Guid("41320763-F0EC-4B7F-9A2E-B4DA92C80FE7")]
	public interface SynAPI : ISynAPI
	{
	}
	[ComImport]
	[Guid("9C042297-D1CD-4F0D-B1AB-9F48AD6A6DFF")]
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[ClassInterface(ClassInterfaceType.None)]
	public class SynAPIClass : ISynAPI, SynAPI
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void Initialize();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void FindDevice(int lConnectionType, int lDeviceType, ref int ulHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void CreateDevice(int lHandle, [MarshalAs(UnmanagedType.Interface)] out SynDevice ppDevice);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetProperty(int lSpecifier, out int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetStringProperty(int lSpecifier, ref byte pBuffer, ref int ulBufLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetProperty(int lSpecifier, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetEventNotification(SafeWaitHandle hEvent);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetEventParameter(ref int lParameter);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void PersistState(int lStateFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void RestoreState(int lStateFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void HardwareBroadcast(int lAction);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetNotifyService();
	}

	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("E7D5F8AC-866C-4C8C-AF5F-F28DE4918647")]
	public interface ISynDevice
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetProperty(int lSpecifier, out int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetBooleanProperty(int lSpecifier, ref int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetStringProperty(int lSpecifier, ref byte pBuffer, ref int ulBufLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetProperty(int lSpecifier, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetEventNotification(SafeWaitHandle hEvent);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreatePacket([MarshalAs(UnmanagedType.Interface)] ref SynPacket ppPacket);

		/**
		 * Retrieve a pending packet.
		 * 
		 * Returns E_FAIL (0x80004005) when there's no more packets.
		 */
		[MethodImpl(MethodImplOptions.InternalCall)]
		[PreserveSig]
		int LoadPacket([MarshalAs(UnmanagedType.Interface)] SynPacket pPacket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void ForceMotion(int lDeltaX, int lDeltaY, int lButtonState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void ForcePacket([MarshalAs(UnmanagedType.Interface)] SynPacket pPacket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Acquire(int lFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Unacquire();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateDisplay([MarshalAs(UnmanagedType.Interface)] ref SynDisplay ppDisplay);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Select(int lHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void PeekPacket(ref int plSequence);
	}
	[ComImport]
	[Guid("BF9D398B-F631-44B4-8EC0-D3FB3E388B62")]
	[CoClass(typeof(SynPacketClass))]
	public interface SynPacket : ISynPacket
	{
	}
	[ComImport]
	[CoClass(typeof(SynDisplayClass))]
	[Guid("A398ED6B-A2CC-471D-96F7-959610870AE0")]
	public interface SynDisplay : ISynDisplay
	{
	}
	[ComImport]
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("9345312C-D098-4BB1-B2B2-D529EB995173")]
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	public class SynDeviceClass : ISynDevice, SynDevice
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetProperty(int lSpecifier, out int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetBooleanProperty(int lSpecifier, ref int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetStringProperty(int lSpecifier, ref byte pBuffer, ref int ulBufLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetProperty(int lSpecifier, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetEventNotification(SafeWaitHandle hEvent);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void CreatePacket([MarshalAs(UnmanagedType.Interface)] ref SynPacket ppPacket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[PreserveSig]
		public virtual extern int LoadPacket([MarshalAs(UnmanagedType.Interface)] SynPacket pPacket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void ForceMotion(int lDeltaX, int lDeltaY, int lButtonState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void ForcePacket([MarshalAs(UnmanagedType.Interface)] SynPacket pPacket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void Acquire(int lFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void Unacquire();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void CreateDisplay([MarshalAs(UnmanagedType.Interface)] ref SynDisplay ppDisplay);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void Select(int lHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void PeekPacket(ref int plSequence);
	}
	[ComImport]
	[Guid("BF9D398B-F631-44B4-8EC0-D3FB3E388B62")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ISynPacket
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetProperty(int lSpecifier, out int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetProperty(int lSpecifier, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetStringProperty(int lSpecifier, ref byte pBuffer, ref int ulBufLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Copy([MarshalAs(UnmanagedType.Interface)] SynPacket pFrom);
	}
	[ComImport]
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("E0C6335D-27F8-424B-A5C2-561291A902A0")]
	public class SynPacketClass : ISynPacket, SynPacket
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetProperty(int lSpecifier, out int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetProperty(int lSpecifier, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetStringProperty(int lSpecifier, ref byte pBuffer, ref int ulBufLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void Copy([MarshalAs(UnmanagedType.Interface)] SynPacket pFrom);
	}
	[ComImport]
	[Guid("289531F4-DE95-416E-BBCE-971A75B880DE")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ISynGroup
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetProperty(int lSpecifier, out int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetProperty(int lSpecifier, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPropertyByIndex(int lSpecifier, int lIndex, ref int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetPropertyByIndex(int lSpecifier, int lIndex, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPacketByIndex(int lIndex, [MarshalAs(UnmanagedType.Interface)] SynPacket pPacket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetPacketByIndex(int lIndex, [MarshalAs(UnmanagedType.Interface)] SynPacket pPacket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPacketPointer(int lIndex, [MarshalAs(UnmanagedType.Interface)] ref SynPacket ppPacket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Copy([MarshalAs(UnmanagedType.Interface)] SynGroup pFrom);
	}
	[ComImport]
	[CoClass(typeof(SynGroupClass))]
	[Guid("289531F4-DE95-416E-BBCE-971A75B880DE")]
	public interface SynGroup : ISynGroup
	{
	}
	[ComImport]
	[ClassInterface(ClassInterfaceType.None)]
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[Guid("FB9298BC-209C-46B8-827A-533F46A05220")]
	public class SynGroupClass : ISynGroup, SynGroup
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetProperty(int lSpecifier, out int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetProperty(int lSpecifier, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetPropertyByIndex(int lSpecifier, int lIndex, ref int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetPropertyByIndex(int lSpecifier, int lIndex, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetPacketByIndex(int lIndex, [MarshalAs(UnmanagedType.Interface)] SynPacket pPacket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetPacketByIndex(int lIndex, [MarshalAs(UnmanagedType.Interface)] SynPacket pPacket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetPacketPointer(int lIndex, [MarshalAs(UnmanagedType.Interface)] ref SynPacket ppPacket);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void Copy([MarshalAs(UnmanagedType.Interface)] SynGroup pFrom);
	}
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("A398ED6B-A2CC-471D-96F7-959610870AE0")]
	public interface ISynDisplay
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetProperty(int lSpecifier, out int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetProperty(int lSpecifier, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void PixelToTouch(int PixelX, int PixelY, ref int pTouchX, ref int pTouchY);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void TouchToPixel(int TouchX, int TouchY, ref int pPixelX, ref int pPixelY);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDC([ComAliasName("SYNCOMLib.wireHDC")] IntPtr pHDC);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void FlushDC(int lFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Acquire(int lDisplayAcquisitionMethod);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Unacquire();

		[MethodImpl(MethodImplOptions.InternalCall)]
		void Select(int lDeviceHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetEventNotification(IntPtr hEvent);

		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetEventParameter(ref int lParameter);
	}

	[ComImport]
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("248AFB1A-27C4-4A30-BF45-6544146648BC")]
	public class SynDisplayClass : ISynDisplay, SynDisplay
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetProperty(int lSpecifier, out int pValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetProperty(int lSpecifier, int lValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void PixelToTouch(int PixelX, int PixelY, ref int pTouchX, ref int pTouchY);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void TouchToPixel(int TouchX, int TouchY, ref int pPixelX, ref int pPixelY);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetDC([ComAliasName("SYNCOMLib.wireHDC")] IntPtr pHDC);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void FlushDC(int lFlags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void Acquire(int lDisplayAcquisitionMethod);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void Unacquire();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void Select(int lDeviceHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void SetEventNotification(IntPtr hEvent);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void GetEventParameter(ref int lParameter);
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct _RemotableHandle
	{
		public int fContext;

		public __MIDL_IWinTypes_0009 u;
	}
	[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 4)]
	public struct __MIDL_IWinTypes_0009
	{
		[FieldOffset(0)]
		public int hInproc;

		[FieldOffset(0)]
		public int hRemote;
	}
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct _userHBITMAP
	{
		public int fContext;

		public __MIDL_IWinTypes_0007 u;
	}
	[StructLayout(LayoutKind.Explicit, Pack = 8, Size = 8)]
	[ComConversionLoss]
	public struct __MIDL_IWinTypes_0007
	{
		[FieldOffset(0)]
		public int hInproc;

		[FieldOffset(0)]
		public long hInproc64;
	}
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	[ComConversionLoss]
	public struct _userBITMAP
	{
		public int bmType;

		public int bmWidth;

		public int bmHeight;

		public int bmWidthBytes;

		public ushort bmPlanes;

		public ushort bmBitsPixel;

		public uint cbSize;

		[ComConversionLoss]
		public IntPtr pBuffer;
	}
}
