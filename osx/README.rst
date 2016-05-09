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

*   获取 Window List With Window Info ( 携带窗口位置和标题信息的窗口列表)

.. code:: python
    
    #!/usr/bin/env python
    #-*- coding:utf-8 -*-

    import sys
    from Quartz import CGWindowListCopyWindowInfo, kCGWindowListExcludeDesktopElements, kCGNullWindowID
    from Quartz import CGGetDisplaysWithRect, CGRect
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
        # windows = list(CGWindowListCopyWindowInfo(kCGWindowListExcludeDesktopElements, kCGNullWindowID))
        windows = filter(
            lambda window: window['kCGWindowLayer'] == 0 and window['kCGWindowStoreType'] == 1, 
            list(CGWindowListCopyWindowInfo(kCGWindowListExcludeDesktopElements, kCGNullWindowID))
        )
        for i in range(len(windows)):
            windows[i]['kCGWindowBounds'] = dict(windows[i]['kCGWindowBounds'])
            # windowRect = windows[i]['kCGWindowBounds']
            # rect = (
            #     str(int(windowRect['X'])), str(int(windowRect['Y'])), 
            #     str(int(windowRect['Width'])), str(int(windowRect['Height'])),
            # )
            # Take one picture at window rect
            # import subprocess, time
            # for n in range(25*15):
            #     cmd = ["screencapture", "-R", ",".join(rect),"-t", "png", "tmp%d.png"%n]
            #     r = subprocess.Popen(cmd, stdout=subprocess.PIPE, shell=False, stderr=None)
            #     time.sleep(0.04)

        for window in windows:
            if window['kCGWindowLayer'] == 0:
                for k,v in window.items():
                    print k,"\t",v
                print "="*30
            else:
                pass


*   Python 调用 OS X AVFoundation 库对指定区域进行录像

.. code:: python

    #!/usr/bin/python
    
    import time
    import Quartz, AVFoundation as AVF

    from Foundation import NSObject, NSURL
    from Quartz import NSRect

    def main():
        # Full-Desktop
        display_id = Quartz.CGMainDisplayID()

        # display_id = 34207

        session = AVF.AVCaptureSession.alloc().init()
        # cropRect
        screen_input = AVF.AVCaptureScreenInput.alloc()
        screen_input.cropRect = NSRect((200, 200), (1000, 500))
        screen_input.initWithDisplayID_(display_id)
        
        file_output = AVF.AVCaptureMovieFileOutput.alloc().init()

        session.addInput_(screen_input)
        session.addOutput_(file_output)
        session.startRunning()

        file_url = NSURL.fileURLWithPath_('foo.mov')
        file_url = file_output.startRecordingToOutputFileURL_recordingDelegate_(file_url, NSObject.alloc().init())
        time.sleep(10)
        session.stopRunning()

    if __name__ == '__main__':
        main()


参考
--------

*   `Objective-C record-screen <https://github.com/atebits/record-screen>`_
*   `Python-objc record-screencap <https://gist.github.com/timsutton/0c6439eb6eb1621a5964>`_
*   `Front most window using CGWindowListCopyWindowInfo <http://stackoverflow.com/questions/5286274/front-most-window-using-cgwindowlistcopywindowinfo>`_
*   `How to identify which process is running which window in Mac OS X? <http://superuser.com/questions/902869/how-to-identify-which-process-is-running-which-window-in-mac-os-x>`_
*   `What process owns a certain window (Mac OS X) <http://blog.loudhush.ro/2014/04/what-process-owns-certain-window-mac-os.html>`_


