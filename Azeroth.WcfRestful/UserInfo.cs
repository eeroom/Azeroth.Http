using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azeroth.WcfRestful
{
    [StormServiceBehavior]
    public class UserInfo:CrossDomainEnable,IUserInfo
    {
        public Model.UserInfo Add(Model.UserInfo model)
        {
            model.Id = Guid.NewGuid();
            return model;
        }


        public List<Model.UserInfo> GetList(Model.UserInfo predicate)
        {
            predicate.Id = Guid.NewGuid();
            List<Model.UserInfo> lst = new List<Model.UserInfo>() { predicate, predicate, predicate };
            return lst;
        }
    }
}
