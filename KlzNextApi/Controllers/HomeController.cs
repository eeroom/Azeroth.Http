using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace KlzNextApi.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider;

        public HomeController(Microsoft.AspNetCore.Mvc.ApiExplorer.IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
        {
            this.apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
        }
        [HttpGet]
        public List<int> GetEntities()
        {
            var lstApi = this.apiDescriptionGroupCollectionProvider.ApiDescriptionGroups;
            var lst = System.Linq.Enumerable.Range(0, 10).ToList();
            return lst;
        }

        [HttpGet]
        public List<TreeNode> EasyuiPath()
        {
            string root = @"D:\Code\zl.gutop\Gutop\Assets\jquery-easyui-1.5.2\demo\";
            var lstdir = new System.Collections.Generic.Stack<Tuple<string, string>>();
            lstdir.Push(Tuple.Create(root, string.Empty));
            var lstnodeTmp = new List<TreeNode>();
            while (lstdir.Count > 0)
            {
                var wrapperPath = lstdir.Pop();
                var path = wrapperPath.Item1;
                var parentPath = wrapperPath.Item2;
                var node = new TreeNode()
                {
                    id = path,
                    pid = parentPath,
                    children = new List<TreeNode>(),
                    text = System.IO.Path.GetFileNameWithoutExtension(path),
                    state = "closed"
                };
                lstnodeTmp.Add(node);
                var lstFileNode = System.IO.Directory.GetFiles(path, "*.html").Select(x => new TreeNode()
                {
                    id = x,
                    pid = path,
                    children = new List<TreeNode>(),
                    text = System.IO.Path.GetFileNameWithoutExtension(x)
                }).ToList();
                lstnodeTmp.AddRange(lstFileNode);
                foreach (var item in System.IO.Directory.GetDirectories(path))
                {
                    lstdir.Push(Tuple.Create(item, path));
                }
            }
            var lstnode = lstnodeTmp.Where(x => !x.text.StartsWith("_")).OrderBy(x => x.id).ToList();
            var dictNode = lstnode.ToDictionary(x => x.id, x => x);
            var lstNodeNoRoot = lstnode.Where(x => !string.IsNullOrEmpty(x.pid)).ToList();
            lstNodeNoRoot.ForEach(x => dictNode[x.pid].children.Add(x));
            lstNodeNoRoot.ForEach(x =>
            {
                x.children = x.children.Count == 0 ? null : x.children;
                x.id = x.id.Replace(root, string.Empty).Replace("\\", "/");
                x.text = System.IO.Path.GetFileNameWithoutExtension(x.id);
            });

            var rootNode = lstnode.First(x => string.IsNullOrEmpty(x.pid));
            return rootNode.children;
        }
    }
}
