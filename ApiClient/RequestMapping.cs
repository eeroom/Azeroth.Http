using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiClient
{
    public class RequestMappingAttribute : Attribute
    {
        static RequestMappingAttribute defaultInstance = new RequestMappingAttribute() { Consumes = MediaType.Json, Produces = MediaType.Json, Method = HttpMetod.POST };
        public static RequestMappingAttribute Default { get { return defaultInstance; } }
        /// <summary>
        /// 对应的api请求地址为：baseurl+Action;
        /// Action为空的时候，对应的api请求地址为：baseurl+接口类/方法名
        /// </summary>
        public string Action { get; set; }

        public HttpMetod Method { get; set; }

        /// <summary>
        /// 请求参数格式
        /// </summary>
        public MediaType Consumes { get; set; }

        /// <summary>
        /// 响应结果格式
        /// </summary>
        public MediaType Produces { get; set; }

    }

    public enum HttpMetod
    {
        POST,
        GET
    }

    public enum MediaType
    {
        Json,
        /// <summary>
        /// 普通表单形式 a=1&b=3
        /// </summary>
        FormUrlEncoded,
        Xml,
        /// <summary>
        /// 表单形式，涉及上传文件
        /// </summary>
        FormData,
        /// <summary>
        /// 纯文本形式，对应text/plain
        /// </summary>
        Text
    }
}
