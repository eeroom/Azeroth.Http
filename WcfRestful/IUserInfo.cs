using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfRestful
{
    [System.ServiceModel.ServiceContract]
    public interface IUserInfo:ICrossDomainEnable
    {
       
        [System.ServiceModel.OperationContract]
        [System.ComponentModel.Description("新增员工信息")]
        Model.UserInfo Add(Model.UserInfo model);

        [System.ServiceModel.OperationContract]
        [System.ComponentModel.Description("获取所有员工信息")]
        List<Model.UserInfo> GetList(Model.UserInfo predicate);
    }
}
