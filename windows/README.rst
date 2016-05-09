Windows屏幕录像
==================


.. contents::


日志
---------
1.  [DONE] 获取窗口列表。
2.  [DONE] 根据窗口编号获取窗口详细信息，比如坐标点，宽高，标题。
3.  [TODO] 根据指定的窗口进行截图和录像（录像即为 每秒截图25张（25FPS））
4.  [TODO] 根据坐标点进行截图和录像（全屏和某块区域）


编译
---------

Windows Server 2012:

.. code:: bat
    
    c:\Windows\Microsoft.Net\Framework64\v4.0.30319\csc.exe .\windowList.cs



参考
----------

*Stackoverflow*:

*   `How to capture screen to be video using C# .Net? <http://stackoverflow.com/questions/4068414/how-to-capture-screen-to-be-video-using-c-sharp-net>`_


*Pinvoke*:

*   `EnumDesktopWindows (user32) <http://pinvoke.net/default.aspx/user32.EnumDesktopWindows>`_
*   `GetWindowInfo (user32) <http://pinvoke.net/default.aspx/user32.GetWindowInfo>`_
*   `GetWindowText (user32) <http://pinvoke.net/default.aspx/user32.GetWindowText>`_
*   `GetWindowRect (user32) <http://pinvoke.net/default.aspx/user32.GetWindowRect>`_

*   `WINDOWINFO (Structures) <http://www.pinvoke.net/default.aspx/Structures/WINDOWINFO.html>`_
*   `RECT (Structures) <http://www.pinvoke.net/default.aspx/Structures/RECT.html>`_


*MSDN*:

*   `EnumDesktopWindows function <https://msdn.microsoft.com/en-us/library/windows/desktop/ms682615(v=vs.85).aspx>`_
*   `GetWindowInfo function <https://msdn.microsoft.com/en-us/library/windows/desktop/ms633516(v=vs.85).aspx>`_
*   `GetWindowText function <https://msdn.microsoft.com/en-us/library/windows/desktop/ms633520(v=vs.85).aspx>`_
*   `GetWindowRect function <https://msdn.microsoft.com/en-us/library/windows/desktop/ms633519(v=vs.85).aspx>`_

*   `WINDOWINFO structure <https://msdn.microsoft.com/en-us/library/windows/desktop/ms632610(v=vs.85).aspx>`_
*   `RECT structure <https://msdn.microsoft.com/en-us/library/windows/desktop/dd162897(v=vs.85).aspx>`_


