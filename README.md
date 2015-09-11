# Task.EntityFramwork
请使用VS2013及以上版本打开，调试前请更新自动更新dll文件。

注意：使用多个库的事物操作的时会遇到以下异常：

异常标题：基础提供程序在 Open 上失败。

异常描述：服务器 ’xxx‘上的 MSDTC 不可用

解决方案如下：
服务器×××上的MSDTC不可用解决办法

MSDTC(分布式交易协调器)，协调跨多个数据库、消息队列、文件系统等资源管理器的事务。该服务的进程名为Msdtc.exe,该进程调用系统Microsoft Personal Web Server和Microsoft SQL Server。该服务用于管理多个服务器 .

位置：控制面板－－管理工具－－服务－－Distributed Transaction Coordinator

依存关系：Remote Procedure Call(RPC)和Security Accounts Manager 

建议：一般家用计算机涉及不到，除非你启用Message Queuing服务，可以停止。

解决办法: 1. 在windows控制面版-->管理工具-->服务-->Distributed Transaction Coordinator-->属性-->启动

        2.在CMD下运行"net start msdtc"开启服务后正常。
        
注：如果在第1步Distributed Transaction Coordinator

无法启动，则是因为丢失了日志文件,重新创建日志文件,再启动就行了。重新创建 MSDTC 日志,并重新启动服务的步骤如下：

(1) 单击"开始",单击"运行",输入 cmd 后按"确定"。

(2) 输入:msdtc -resetlog (注意运行此命令时,不要执行挂起的事务)

(3) 最后输入:net start msdtc 回车,搞定!
