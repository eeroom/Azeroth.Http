using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azeroth.WebApi
{
    public class JwtData
    {
        int max = 7;//7天内有效
        int slide = 1;//超过2分钟就更新token
        public int jwtdata { get; set; }
        public Guid Id { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// jwtdata是否过期
        /// true-已过期，false-未过期
        /// </summary>
        /// <returns></returns>
        public bool ValidateTimeout()
        {
            return DateTime.Now>this.CreateDateTime.AddDays(max);
        }

        public bool TokenIsOld()
        {
            return DateTime.Now > this.CreateDateTime.AddMinutes(slide);
        }

        public JwtData SlidingTimeout()
        {
            this.CreateDateTime = DateTime.Now;
            return this;
        }
    }
}