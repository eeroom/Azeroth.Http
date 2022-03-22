# .net官方的实现windows服务关键点
```
1、一个windows服务对应一个继承于System.ServiceProcess.ServiceBase的类
	需要明确指定ServiceName的值，后续的安装部分也会涉及到这个ServiceName，
	两个地方需要保持一致，才能关联起来，后续才能正常启动
2、需要一个继承于System.Configuration.Install.Installer的类型，
	这个是给安装服务的执行InstallUtil的时候用到的
	需要包含一个System.ServiceProcess.ServiceProcessInstaller的实例，用于指定服务运行的账号
	需要包含一个System.ServiceProcess.ServiceInstaller，通过指定ServiceName关联上具体的windows服务，
3、InstallUtil.exe 工具，.net会自动这个工具，需要以管理员权限运行，才能正常完成安装
4、安装服务：InstallUtil.exe 服务程序
5、卸载服务：InstallUtil.exe /u 服务程序
6、InstallUtil.exe的本质就是调用System.Configuration.Install.ManagedInstallerClass.InstallHelper()方法，
	把安装和卸载的逻辑在自己程序中，也可以直接控制台运行，方便调试
	安装：WindowsServerHostWcf.exe /install
	卸载：WindowsServerHostWcf.exe /uninstall
```

# windows服务启动UI
```
xp系统的用户和window service运行在一个session下，
在xp以后，windows系统改变了用户会话管理的策略，
window service独立运行在session0下，
依次给后续的登录用户分配sessionX(X =1,2,3...)，
session0没有权限运行UI。
所以在window xp以后的系统下，window service调用有UI的application时只能看到程序进程但不能运行程序的UI
```