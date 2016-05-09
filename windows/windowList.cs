using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Text;

// TODO:
//      1.  Capture Screen
//      2.  Record the screen (Full-Screen)
//      3.  Record screen by window

// 录像代码参考:
//      http://stackoverflow.com/questions/4068414/how-to-capture-screen-to-be-video-using-c-sharp-net


public class user32{
    public struct RECT{
        public int Left;       // Specifies the x-coordinate of the upper-left corner of the rectangle.
        public int Top;        // Specifies the y-coordinate of the upper-left corner of the rectangle.
        public int Right;      // Specifies the x-coordinate of the lower-right corner of the rectangle.
        public int Bottom;     // Specifies the y-coordinate of the lower-right corner of the rectangle.
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO{
        public uint cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;

        public WINDOWINFO(Boolean? filler) : this() {
            cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
        }

    }

    public delegate bool EnumDelegate(IntPtr hWnd, int lParam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", EntryPoint = "GetWindowText",
    ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

    // [DllImport("user32.dll", SetLastError = true)]
    // public static extern bool GetWindowInfo(IntPtr hWnd, out WINDOWINFO pwi);
    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("user32.dll",SetLastError = true)]
    public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);


    [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
    ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

    static void Main(){
        var collection = new List<string>();
        user32.EnumDelegate filter  = delegate(IntPtr hWnd, int lParam){
            StringBuilder strbTitle = new StringBuilder(255);
            int    nLength  = user32.GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
            string strTitle = strbTitle.ToString();

            if (user32.IsWindowVisible(hWnd) && string.IsNullOrEmpty(strTitle) == false){
                // Get Window Info
                // WINDOWINFO structure: https://msdn.microsoft.com/en-us/library/ms632610(VS.85).aspx
                // RECT structure      : https://msdn.microsoft.com/en-us/library/dd162897(v=vs.85).aspx
                WINDOWINFO info = new WINDOWINFO();
                info.cbSize = (uint)Marshal.SizeOf(info);
                user32.GetWindowInfo(hWnd, ref info);

                Console.WriteLine("rcWindow.left: {0}, rcWindow.right: {0} ", info.rcWindow.Left, info.rcWindow.Right);
                Console.WriteLine("rcWindow.top: {0}, rcWindow.bottom: {0} ", info.rcWindow.Top, info.rcWindow.Bottom);

                Console.WriteLine("rcClient.left: {0}, rcClient.right: {0} ", info.rcClient.Left, info.rcClient.Right);
                Console.WriteLine("rcClient.top: {0}, rcClient.bottom: {0} ", info.rcClient.Top, info.rcClient.Bottom);

                collection.Add(strTitle);
            }
            return true;
        };

        if (user32.EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero)){
            foreach (var item in collection){
                Console.WriteLine(item);
            }
        }
        Console.Read();
    }
}