using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticsearchClient
{
    public class ZlInfomation
    {

        public int Print()
        {
            Console.WriteLine("hellow owr");

            return 3;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public String title { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// 专题名称
        /// </summary>
        public string[] topicname { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string coverurl { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string[] tags { get; set; }

        /// <summary>
        /// 文档类别
        /// </summary>
        public Documents docType { get; set; }

        public DateTime datetime { get; set; }

    }

    public enum Documents
    {
        资讯 = 0x1,
        视频 = 0x2,
        专家洞见 = 0x4,
        问题 = 0x5,
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] iaa = null;
            var iaaa4 = iaa?[7];


            string[] a1 = null;



            var aa1 = string.Join(";", "a", null, "b");

            var zl = new ZlInfomation() { docType = Documents.视频, datetime = DateTime.Now, id = Guid.NewGuid() };
            var ddd = default(Guid);
            var m222 = zl?.Print();
            zl = null;
            var m2223 = zl?.Print();

            var patarmeter = Newtonsoft.Json.JsonConvert.SerializeObject(zl, new Newtonsoft.Json.JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm:ss" });

            int ba = 0;
            if (System.Threading.Interlocked.Exchange(ref ba, 1) == 1)
                Console.Write("正在重建索引");
            else
                Console.Write("开始重建索引");

            if (System.Threading.Interlocked.Exchange(ref ba, 1) == 1)
                Console.Write("正在重建索引");
            else
                Console.Write("开始重建索引");

            ElasticSearchConfig config = new ElasticSearchConfig("http://111.230.87.237:9200/", "articleindex");
            ElasticSearchConfig config2 = new ElasticSearchConfig("http://111.230.87.237:9200/", "htttags");
            var mappings = new
            {
                contentinfo = new
                {
                    dynamic = true,
                    properties = new
                    {
                        id = new
                        {
                            type = "text",
                            index = false
                        },
                        title = new
                        {
                            type = "text",
                            analyzer = "ik_smart"
                        },
                        content = new
                        {
                            type = "text",
                            analyzer = "ik_smart"
                        },
                        tags = new
                        {
                            type = "text",
                            analyzer = "ik_smart"
                        }
                    }
                }
            };
            // string rtcreate = CreateIndex(config2, new { mappings });

            string rtanalyze = Analyze(config, new { analyzer = "ik_smart", text = "饿了就吃饿了吗" });

            //string mapInfo= GetMapping(config);
            //Article article = new Article() { id=Guid.NewGuid(), author="智库+平台", content="2018年8月20日的郑鄂文", issueTime=DateTime.Now.AddYears(-10), title="星巴克牵手阿里布局新林寿生态，是迷奸还是砒霜"};
            //string rtadd=  AddDocument(config,article.id.ToString(),article);
            string[][] tags = new string[3][];
            tags[0] = new string[] { "支付宝升级延时到账：遇到诈骗可“一键”撤回(2/1016) ", "美国" };
            tags[1] = new string[] { "超30亿条用户数据被窃取 BAT无一幸免(1/154) ", "美国" };
            tags[2] = new string[] { "说说入职两日的感受(14/830) ", "美国" };

            var lstcontent = System.Linq.Enumerable.Range(0, 20).Select(x => new contentinfo
            {
                id = Guid.NewGuid(),
                content = "8月2日，星巴克与阿里巴巴宣布在新零售方面进行深度战略。饿了么将承接星巴克店面的咖啡、食品外送业务。有业内人士将其视为星巴克开始全面布局互联网咖啡",
                tags = tags[x % 3],
                title = "今日头条" + x
            }).ToList();

            //   lstcontent.ForEach(x=>AddDocument(config2,x.id.ToString(),x));

            string rtsearchtags = Search(config2, new
            {
                @from = 2,
                size = 2,
                query = new
                {
                    @bool = new
                    {
                        should = new object[] {
                            new {match=new { tags="你喜欢美国吗"} }
                        }
                    }
                }
            });


            string rtsearch = Search(config, new
            {
                query = new
                {
                    @bool = new
                    {
                        should = new object[]{
                            new {match=new { title="星巴克"} },
                            new {match=new { content="股权的作用"} },
                        }
                    }
                },
                highlight = new
                {
                    tags_schema = "styled",
                    fields = new
                    {
                        title = new { },
                        content = new { }
                    }
                }
            });

            //string rtdel = DeleteDocument<Article>(config,article.id);


            //新增指定document（类型名=索引名称）

            //修改document

            //删除docuemnt

            //查询document

        }

        private static string Analyze(ElasticSearchConfig config, object parameter)
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = config.BaseAddress;
                string cmdstr = "_analyze?pretty";
                System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, cmdstr);
                request.Content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var rt = client.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return rt;
            }
        }

        private static string CreateIndex(ElasticSearchConfig config, object parameter)
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = config.BaseAddress;
                string cmdstr = string.Format("{0}?pretty", config.Indexs.First());
                System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Put, cmdstr);
                request.Content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var rt = client.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return rt;
            }
        }

        /// <summary>
        /// 删除document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static string DeleteDocument<T>(ElasticSearchConfig config, string id)
        {
            string className = typeof(T).Name.ToLower();
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = config.BaseAddress;
                System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Delete, string.Format("{0}/{1}/{2}", config.Indexs.First()
                    , className
                    , id));
                var rt = client.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return rt;
            }
        }

        /// <summary>
        /// 查询document
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static string Search(ElasticSearchConfig config, object parameter)
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = config.BaseAddress;
                System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, string.Format("{0}/_search/", string.Join(",", config.Indexs)));
                string parameterstr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);
                request.Content = new System.Net.Http.StringContent(parameterstr);
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var rt = client.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return rt;
            }
        }

        /// <summary>
        /// 新增document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="id"></param>
        /// <param name="docInfo"></param>
        /// <returns></returns>
        private static string AddDocument<T>(ElasticSearchConfig config, string id, T docInfo)
        {
            var className = typeof(T).Name.ToLower();
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = config.BaseAddress;
                string cmdstr = string.IsNullOrEmpty(id) ? string.Format("{0}/{1}", config.Indexs.First(), className) : string.Format("{0}/{1}/{2}", config.Indexs.First(), className, id);
                System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Put, cmdstr);
                request.Content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(docInfo));
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var rt = client.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return rt;
            }
        }

        /// <summary>
        /// 修改document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="id"></param>
        /// <param name="docInfo"></param>
        /// <returns></returns>
        private static string EditDocument<T>(ElasticSearchConfig config, string id, T docInfo)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("必须指定要修改的document的id");
            var className = typeof(T).Name.ToLower();
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = config.BaseAddress;
                string cmdstr = string.Format("{0}/{1}/{2}", config.Indexs.First(), className, id);
                System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Put, cmdstr);
                request.Content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(docInfo));
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var rt = client.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return rt;
            }
        }




        /// <summary>
        /// 获取索引的map结构
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private static string GetMapping(ElasticSearchConfig config)
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = config.BaseAddress;
                System.Net.Http.HttpRequestMessage request =
                    new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, string.Format("{0}/_mapping?pretty", config.Indexs.First()));
                var rt = client.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
                return rt;
            }
        }


    }

    /// <summary>
    /// 约定和索引的Type名称一致
    /// </summary>
    public class Article
    {
        public string author { get; set; }
        public string content { get; set; }
        public Guid id { get; set; }
        public DateTime issueTime { get; set; }
        public string title { get; set; }
    }

    public class ElasticSearchConfig
    {
        public ElasticSearchConfig(string url, params string[] indexs)
        {
            this.BaseAddress = new Uri(url);
            this.Indexs = indexs;
        }
        public Uri BaseAddress { get; set; }
        public string[] Indexs { get; set; }
    }

    public class contentinfo
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string[] tags { get; set; }
    }
}
