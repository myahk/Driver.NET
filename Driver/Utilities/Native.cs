﻿namespace Driver.Utilities
{
    using System;
    using System.IO;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;
    using System.Security;

    using Microsoft.Win32.SafeHandles;

    internal class Native
    {
        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr OpenSCManager(string MachineName, string DatabaseName, uint DesiredAccess);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateService(
            IntPtr              ServiceManager,
            string              ServiceName,
            string              DisplayName,
            uint                DesiredAccess,
            uint                ServiceType,
            uint                StartType,
            uint                ErrorControl,
            string              BinaryPathName,
            string              LoadOrderGroup,
            string              TagId,
            string              Dependencies,
            string              ServiceStartName,
            string              Password
        );

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr OpenService(IntPtr ServiceManager, string ServiceName, uint DesiredAccess);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteService(IntPtr Service);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseServiceHandle(IntPtr Handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr Handle);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern SafeFileHandle CreateFile(
                                            string              FileName,
            [MarshalAs(UnmanagedType.U4)]   FileAccess          FileAccess,
            [MarshalAs(UnmanagedType.U4)]   FileShare           FileShare,
                                            IntPtr              SecurityAttributes,
            [MarshalAs(UnmanagedType.U4)]   FileMode            CreationDisposition,
            [MarshalAs(UnmanagedType.U4)]   uint                FlagsAndAttributes,
                                            IntPtr              Template);

        internal static uint CtlCode(uint DeviceType, uint Function, uint Method, uint Access)
        {
            return (((DeviceType) << 16) | ((Access) << 14) | ((Function) << 2) | (Method));
        } 
    }
}
