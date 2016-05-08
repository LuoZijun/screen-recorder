using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Text;

/// <summary>
/// EnumDesktopWindows Demo - shows the caption of all desktop windows.
/// Authors: Svetlin Nakov, Martin Kulov 
/// Bulgarian Association of Software Developers - http://www.devbg.org/en/
/// </summary>
public class user32
{
    /// <summary>
    /// filter function
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    public delegate bool EnumDelegate(IntPtr hWnd, int lParam);

    /// <summary>
    /// check if windows visible
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    /// <summary>
    /// return windows text
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="lpWindowText"></param>
    /// <param name="nMaxCount"></param>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "GetWindowText",
    ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

    /// <summary>
    /// enumarator on all desktop windows
    /// </summary>
    /// <param name="hDesktop"></param>
    /// <param name="lpEnumCallbackFunction"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
    ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

    /// <summary>
    /// entry point of the program
    /// </summary>
    static void Main()
    {
    var collection = new List<string>();
    user32.EnumDelegate filter = delegate(IntPtr hWnd, int lParam)
    {
        StringBuilder strbTitle = new StringBuilder(255);
        int nLength = user32.GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
        string strTitle = strbTitle.ToString();

        if (user32.IsWindowVisible(hWnd) && string.IsNullOrEmpty(strTitle) == false)
        {
        collection.Add(strTitle);
        }
        return true;
    };

    if (user32.EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero))
    {
        foreach (var item in collection)
        {
        Console.WriteLine(item);
        }
    }
    Console.Read();
    }
}