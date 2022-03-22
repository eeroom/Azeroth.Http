using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WindowsServerHostWcf
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ServiceBase> lstsrv = new List<ServiceBase>();
            lstsrv.Add(new P1Service.P1ServiceImpl());
            if(System.Diagnostics.Process.GetCurrentProcess().SessionId == 0)
            {
                System.ServiceProcess.ServiceBase.Run(lstsrv.ToArray());
            }
            else
            {
                if (args.Length < 1)
                {
                    var startHandler = (Action<ServiceBase, string[]>)Delegate.CreateDelegate(typeof(Action<ServiceBase, string[]>), 
                        typeof(ServiceBase).GetMethod("OnStart", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic));
                    lstsrv.ForEach(x => startHandler(x, null));
                    Console.WriteLine("任意键退出");
                    Console.ReadKey();
                    var stopHandler = (Action<ServiceBase>)Delegate.CreateDelegate(typeof(Action<ServiceBase>), 
                        typeof(ServiceBase).GetMethod("OnStop", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic));
                    lstsrv.ForEach(x => stopHandler(x));

                }
                else
                {//安装、卸载服务，对应的参数 /install /uninstall ,详细可参看installutil.exe的参数说明，
                    //这里只是把installutil.exe的代码逻辑搬到程序自己内部
                    var location=  System.Reflection.Assembly.GetExecutingAssembly().Location;
                    Console.WriteLine(location);
                    var lstarg= args.ToList();
                    lstarg.Add(location);
                    System.Configuration.Install.ManagedInstallerClass.InstallHelper(lstarg.ToArray());
                }
            }
        }
    }
}
