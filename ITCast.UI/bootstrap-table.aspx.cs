using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCast.UI {
    public partial class bootstrap_table : CmdPage<bootstrap_table>,IAnonymousPage {
        
        public ListWrapper<UploadFileInfo> GetFileEntities(HttpContext context) {
            var lst = System.Linq.Enumerable.Range(0, 199).Select(x => new UploadFileInfo() {
                Id = x,
                Name = System.IO.Path.GetRandomFileName(),
                LastModifyTime = DateTime.Now.AddHours(-1 * x)
            }).ToList();
            var sortName = context.Request["sortName"];
            var parameterExp = System.Linq.Expressions.Expression.Parameter(typeof(UploadFileInfo), "mq");
            var getpropValueExp = System.Linq.Expressions.Expression.PropertyOrField(parameterExp, sortName);
            var getpropObjectValueExp = System.Linq.Expressions.Expression.Convert(getpropValueExp, typeof(object));
            var lex = System.Linq.Expressions.Expression.Lambda<Func<UploadFileInfo, object>>(getpropObjectValueExp,parameterExp);

            var sortOrder = context.Request["sortOrder"];
            if (sortOrder == "asc")
                lst = lst.OrderBy(lex.Compile()).ToList();
            else
                lst = lst.OrderByDescending(lex.Compile()).ToList();
            var pageSize = int.Parse(context.Request["pageSize"]);
            var pageNumber = int.Parse(context.Request["pageNumber"]);
            var lstRT= lst.Skip(pageNumber * pageSize - pageSize).Take(pageSize).ToList();
            var rt = new ListWrapper<UploadFileInfo>() {
                 rows=lstRT,
                  total=lst.Count
            };
            return rt;
        }
    }

    public class UploadFileInfo {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime LastModifyTime { get; set; }


    }

    public class ListWrapper<T> {
        public int total { get; set; }

        public List<T> rows { get; set; }
    }
}