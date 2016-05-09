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

windows server 2012:

.. code:: bat
    
    c:\Windows\Microsoft.Net\Framework64\v4.0.30319\csc.exe .\windowList.cs