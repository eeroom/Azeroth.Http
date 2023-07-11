using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA{
    public enum FindApproverMode {
        所在节点,
        上级节点,
        上上级节点,
        上三级节点,
        上四级节点,
        上五级节点,
        唯一角色,
        指定节点,
        指定审批人,
    }
}
