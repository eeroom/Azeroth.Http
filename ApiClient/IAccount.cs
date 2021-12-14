using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiClient
{
    public interface IAccount
    {
        ApiResult<bool> Login(LoginDTO parameter);
    }

    public class LoginDTO
    {
        public string Name { get; set; }

        public string Pwd { get; set; }
    }

    public class ApiResult<T>
    {
        public int code { get; set; }

        public string msg { get; set; }

        public T data { get; set; }
    }
}
