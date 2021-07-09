<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bootstrap-table.aspx.cs" Inherits="ITCast.UI.bootstrap_table" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 上述3个meta标签*必须*放在最前面，任何其他内容都*必须*跟随其后！ -->
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../../favicon.ico">

    <title>bootstrap-table</title>

    <!-- Bootstrap core CSS -->
    <link href="/Bootstrap3.3.7/css/bootstrap.css" rel="stylesheet" />
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <link href="/css/ie10-viewport-bug-workaround.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="/css/layout-fluid.css" rel="stylesheet" />
    <!-- Just for debugging purposes. Don't actually copy these 2 lines! -->
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
        <script src="/js/ie8-responsive-file-warning.js"></script>
        <script src="/js/html5shiv.min.js"></script>
        <script src="/js/respond.min.js"></script>
    <![endif]-->
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="/jQuey/jquery-1.11.3.js"></script>

    <script src="/Bootstrap3.3.7/js/bootstrap.js"></script>

    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="/js/ie10-viewport-bug-workaround.js"></script>
    <link href="bootstrap-table-1.11.1/bootstrap-table.css" rel="stylesheet" />
    <script src="bootstrap-table-1.11.1/bootstrap-table.js"></script>
    <script src="bootstrap-table-1.11.1/locale/bootstrap-table-zh-CN.js"></script>
    <style type="text/css">
        .nav-sidebar {
            margin-bottom: 0;
        }

            .nav-sidebar > li > a {
                font-weight: 700;
            }

        .nav-sub > li > a > span {
            padding-left: 15px;
        }

        .nav-sub > li > a:hover {
            color: #fff;
            background-color: #428bca;
        }

        .active > a {
            color: #fff;
            background-color: #428bca;
        }

        .navbar-nav > li > a:hover {
            border-bottom: 2px solid #ff0000;
        }

        .navbar-nav > .active > a {
            border-bottom: 2px solid #ff0000;
        }
        .btn-row-delete{
            margin-left:10px;
        }
    </style>
    <script type="text/javascript">
        $(function () {

            $(".nav-sidebar>li>a").click(function () {
                var brothers = $(this).parents("ul")//所在ul
                    .siblings("ul")//所有兄弟的ul
                    .find("li>ul")
                    .collapse("hide");
            });

            $(".sidebar .nav-sub").on("show.bs.collapse", function (event) {
                var el = $(event.target).parent().prev();
                el.find(".menu-xiangyou").hide();
                el.find(".menu-xiangxia").show();


            });

            $(".sidebar .nav-sub").on("hide.bs.collapse", function (event) {
                var el = $(event.target).parent().prev();
                el.find(".menu-xiangyou").show();
                el.find(".menu-xiangxia").hide();
            });

        });

        $(function () {
            //所有选项都定义在  jQuery.fn.bootstrapTable.defaults
            $("#tbFilelst").bootstrapTable({
                toolbar: "#tbToolbar"
                , striped: true                           //是否显示行间隔色,默认为false
                , showRefresh: true                  //是否显示刷新按钮,默认为false
                , showToggle: true                   //是否显示详细视图和列表视图的切换按钮
                , clickToSelect: true                //是否启用点击选中行
                , showColumns:true      //允许选择要展示的列，默认为false
                , cache: false                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                , pagination: true                   //是否分页
                , queryParamsType: '' //默认值为 'limit' ,传给服务端的参数为：offset,limit,sort。"",传给服务端的参数为:pageSize,pageNumber
                , sidePagination: "server"       //分页模式：client or server
                , sortName: "Id"                     //首次加载排序的字段，默认无值
                , sortOrder: "desc"                  //首次加载的排序方式
                , pageList: [10, 25, 50, 100]        //可供选择的每页的行数（*）
                , url: " "                      //请求后台的URL（*），" "这样表示当前页面地址
                , method: "POST"                      //请求方式（*）
                , contentType: "application/x-www-form-urlencoded"
                , queryParams: function (parameters) {
                    console.log(parameters);
                    parameters["cmd"] = "GetFileEntities";
                    return parameters;
                }
            });

            var mm = jQuery.fn.bootstrapTable.defaults;

            $("#tbFilelst").on("click", ".btn-row-edit", function () {
                alert("要修改的数据，Id=" + $(this).data("id"));
            });
            $("#tbFilelst").on("click", ".btn-row-delete", function () {
                alert("要删除的数据，Id=" + $(this).data("id"));
            });
            $("form").submit(function () {

            });
        });

        function handlerColumnFormatter(value, row, index) {
            return `<a class="btn btn-xs btn-default btn-row-edit" data-id="${row.Id}">修改</a>`
                        + `<a class="btn btn-xs btn-default btn-row-delete" data-id="${row.Id}">删除</a>`;
        }
    </script>

</head>

<body>
    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    
                </button>
                <a class="navbar-brand" href="#">千针石林</a>
            </div>
            <div id="navbar" class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-left">
                    <li class="active"><a href="#">dashboard</a></li>
                    <li><a href="#">逆变器</a></li>
                    <li><a href="#">镇流器</a></li>
                </ul>
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="#">设置</a></li>
                    <li><a href="#">帮助</a></li>
                    <li><a href="#">eeroom</a></li>
                </ul>
            </div>
        </div>
    </nav>

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-3 sidebar">
                <ul class="nav nav-sidebar">
                    <li>
                        <a data-toggle="collapse" href="#collapseExample1">
                            <span>研究型大学</span>
                            <span class="menu-xiangyou pull-right" style="display: none">
                                <span class="glyphicon glyphicon-chevron-right"></span>
                            </span>
                            <span class="menu-xiangxia pull-right" style="">
                                <span class="glyphicon glyphicon-chevron-down"></span>
                            </span>
                        </a>
                    </li>
                    <li>
                        <ul id="collapseExample1" class="nav in nav-sub">
                            <li class="active"><a href="#"><span>Reports</span></a></li>
                            <li><a href="#"><span>Reports2</span></a></li>
                            <li><a href="#"><span>Reports3</span></a></li>
                        </ul>
                    </li>
                </ul>
                <ul class="nav nav-sidebar">
                    <li>
                        <a data-toggle="collapse" href="#collapseExample2">
                            <span>二次加工</span>
                            <span class="menu-xiangyou pull-right" style="">
                                <span class="glyphicon glyphicon-chevron-right"></span>
                            </span>
                            <span class="menu-xiangxia pull-right" style="display: none">
                                <span class="glyphicon glyphicon-chevron-down"></span>
                            </span>
                        </a>
                    </li>
                    <li>
                        <ul id="collapseExample2" class="nav collapse nav-sub">
                            <li><a href="#"><span>加工</span></a></li>
                            <li><a href="#"><span>加工三次</span></a></li>
                            <li><a href="#"><span>加工很多次</span></a></li>
                            <li><a href="#"><span>加工很多次哦</span></a></li>
                            <li><a href="#"><span>加工很多次哦啊</span></a></li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div class="col-md-21 col-md-offset-3 main">
                <div id="tbToolbar">
                    <form class="form-inline">
                        <div class="btn-group" role="group" aria-label="...">
                             <button class="btn btn-default  btn-row-add" type="button">新增</button>
                            <button class="btn btn-default  btn-row-delete2" type="button">删除</button>
                        </div>
                       
                        <div class="form-group">
                            <div class="input-group">
                                <div class="input-group-addon">名称</div>
                                <input type="text" class="form-control" name="Name" >
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <div class="input-group-addon">检查结果</div>
                                <select class="form-control" name="CkResult">
                                    <option value="0">---</option>
                                    <option value="1">一级</option>
                                    <option value="2">二级</option>
                                </select>
                            </div>
                        </div>
                        <button type="submit" class="btn btn-primary">查询</button>
                    </form>
                </div>
                <table id="tbFilelst">
                    <thead>
                        <tr>
                             <th data-checkbox="true" ></th>
                             <%-- <th data-radio="true" ></th>--%>
                            <th data-field="Name" data-sortable="true">名称</th>
                            <th data-field="Size" data-sortable="true">大小</th>
                            <th data-field="CkResult" data-sortable="true">检查结果</th>
                            <th data-field="LastModifyTime" data-sortable="true">修改时间</th>
                            <th data-formatter="handlerColumnFormatter">操作</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>

    <div class="col-md-offset-3 navbar-fixed-bottom">
        <p class="text-center" style="margin: 0 0"><small>版权所有&copy;丢了光影 2016-2020</small></p>
    </div>
</body>
</html>

