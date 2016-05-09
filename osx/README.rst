OS X系统屏幕录像
====================

.. contents::


日志
---------

1.  [DONE] 获取窗口列表。
2.  [DONE] 根据窗口编号获取窗口详细信息，比如坐标点，宽高，标题。
3.  [TODO] 根据指定的窗口进行截图和录像（录像即为 每秒截图25张（25FPS））
4.  [DONE] 根据坐标点进行截图和录像（全屏和某块区域）

编译
-------

使用 Make 命令:

.. code:: bash

    make
    # make clean


手动使用 Clang:

.. code:: bash

    clang -fobjc-arc -Wall -framework Foundation -framework AVFoundation -framework ApplicationServices -framework CoreVideo -framework CoreMedia -framework AppKit record-screen.m -o record-screen



Python 调用 OC
-------------------

.. code:: python
    
    #!/usr/bin/env python
    #-*- coding:utf-8 -*-

    import sys
    from Quartz import CGWindowListCopyWindowInfo, kCGWindowListExcludeDesktopElements, kCGNullWindowID
    # from Foundation import NSSet, NSMutableSet

    reload(sys)
    sys.setdefaultencoding('utf8')

    def get_windows():

        """
        {
            kCGWindowAlpha = 1;
            kCGWindowBackingLocationVideoMemory = 1;
            kCGWindowBounds =     {
                Height = 22;
                Width = 1440;
                X = 0;
                Y = 0;
            };
            kCGWindowIsOnscreen = 1;
            kCGWindowLayer = 24;
            kCGWindowMemoryUsage = 128160;
            kCGWindowName = Menubar;
            kCGWindowNumber = 3;
            kCGWindowOwnerName = "Window Server";
            kCGWindowOwnerPID = 217;
            kCGWindowSharingState = 1;
            kCGWindowStoreType = 2;
        }
        """
        windows = list(CGWindowListCopyWindowInfo(kCGWindowListExcludeDesktopElements, kCGNullWindowID))
        result  = []
        for window in windows:
            window['kCGWindowBounds'] = dict(window['kCGWindowBounds'])
            result.append(dict(window))
            
        windows = result

        for window in windows:
                if window['kCGWindowLayer'] == 0:
                    for k,v in window.items():
                        print k,"\t",v
                    print "="*30




参考
--------

*   `Objective-C record-screen <https://github.com/atebits/record-screen>`_
*   `Python-objc record-screencap <https://gist.github.com/timsutton/0c6439eb6eb1621a5964>`_
*   `Front most window using CGWindowListCopyWindowInfo <http://stackoverflow.com/questions/5286274/front-most-window-using-cgwindowlistcopywindowinfo>`_
*   `How to identify which process is running which window in Mac OS X? <http://superuser.com/questions/902869/how-to-identify-which-process-is-running-which-window-in-mac-os-x>`_
*   `What process owns a certain window (Mac OS X) <http://blog.loudhush.ro/2014/04/what-process-owns-certain-window-mac-os.html>`_


