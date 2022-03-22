using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsServerHostWcf
{
    /// <summary>
    /// 这里嵌套了class只是为了避免类继承System.ServiceProcess.ServiceBase后，导致vs默认打开设计器
    /// </summary>
    public class P1Service
    {
        public class P1ServiceImpl:System.ServiceProcess.ServiceBase
        {
            public static string KeyServiceName = "P1Service:ServiceName";
            public static string KeyDescription = "P1Service:Description";
            System.ServiceModel.Web.WebServiceHost hs { set; get; }
            public P1ServiceImpl() {
                this.ServiceName = System.Configuration.ConfigurationManager.AppSettings[KeyServiceName];
            }

            protected override void OnStart(string[] args)
            {
                this.hs = new System.ServiceModel.Web.WebServiceHost(typeof(Home));
                this.hs.Open();
            }

            protected override void OnStop()
            {
                this.hs.Close();
            }
        }
    }

    /// <summary>
    /// 这个类是给InstalUtil用的，
    /// this.p1Installer1.ServiceName = P1ServiceImpl.MyServiceName和运行的serverName产生关联
    /// </summary>
    [System.ComponentModel.RunInstaller(true)]
    public class AkInstaller : System.Configuration.Install.Installer {
        private System.ServiceProcess.ServiceProcessInstaller processInstaller;
        private System.ServiceProcess.ServiceInstaller p1Installer1;
        public AkInstaller() {
            this.processInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.p1Installer1 = new System.ServiceProcess.ServiceInstaller();

            this.processInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.processInstaller.Password = null;
            this.processInstaller.Username = null;

            this.p1Installer1.ServiceName = System.Configuration.ConfigurationManager.AppSettings[P1Service.P1ServiceImpl.KeyServiceName];
            this.p1Installer1.Description = System.Configuration.ConfigurationManager.AppSettings[P1Service.P1ServiceImpl.KeyDescription];
            this.Installers.Add(this.processInstaller);
            this.Installers.Add(this.p1Installer1);
        }
    }
}
