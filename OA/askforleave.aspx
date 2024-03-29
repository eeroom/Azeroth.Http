﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="askforleave.aspx.cs" Inherits="OA.askforleave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 上述3个meta标签*必须*放在最前面，任何其他内容都*必须*跟随其后！ -->
    <meta name="description" content="">
    <meta name="author" content="">


    <title>请假申请</title>

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
    <script src="/jQuery/jquery-1.11.3.js"></script>

    <script src="/Bootstrap3.3.7/js/bootstrap.js"></script>

    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="/js/ie10-viewport-bug-workaround.js"></script>
    <script src="/js/menu.js"></script>
    <link href="bootstrap-datepicker-1.9.0-dist/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.js"></script>
    <script src="bootstrap-datepicker-1.9.0-dist/locales/bootstrap-datepicker.zh-CN.min.js"></script>
    <style type="text/css">
        .btn-quit {
            display: block;
            width: 100%;
            background: transparent;
            border: none;
            text-align: left;
        }
        .form-md input,.form-md select{
            width:250px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var opt = $.fn.datepicker.defaults;
            $('#startTime').datepicker({
                format: 'yyyy-mm-dd'
                , language: "zh-CN"
                , autoclose: true
                //,clearBtn: true
                ,todayBtn: true
                , todayHighlight: true
                , orientation: "bottom right"
                , minViewMode: 0//天选择
                , maxViewMode: 2//年,这个往下一级是10年，世纪等
            });
            $('#endTime').datepicker({
                format: 'yyyy-mm-dd'
                , language: "zh-CN"
                , autoclose: true
                //,clearBtn: true
                , todayBtn: true
                , todayHighlight: true
                , orientation: "bottom right"
                , minViewMode: 0//天选择
                , maxViewMode: 2//年,这个往下一级是10年，世纪等
            });
            $("#btnsubmitleave").click(function () {
                console.log("formLeave", $("#formLeave"))
                var parameters = $("#formLeave").serializeArray();
                var parameter = {};
                for (var i = 0; i < parameters.length; i++) {
                    if (parameters[i].name == "formdata")
                        continue;
                    parameter[parameters[i].name] = parameters[i].value;
                }
                $("#formdata").val(JSON.stringify(parameter));
                return true;
            })
        });
    </script>

</head>

<body>
    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar"
                    aria-expanded="false" aria-controls="navbar">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">千针石林</a>
            </div>
            <div id="navbar" class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-left">
                    <li class="active"><a href="./bootstrap-table.html">html</a></li>
                    <li id="README"><a href="../index.html?tp=README.md">devops</a></li>
                    <li id='net'><a href="../index.html?tp=net.md">.net</a></li>
                    <li id="java"><a href="../index.html?tp=java.md">java</a></li>
                    <li id="cpp"><a href="../index.html?tp=cpp.md">c/c++</a></li>
                    <li id="nodejs"><a href="../index.html?tp=nodejs.md">nodejs</a></li>
                    <li id="Algorithm"><a href="../index.html?tp=Algorithm.md">Algorithm</a></li>
                    <li><a href="../hzfoundation/index.html" target="_blank">hz</a></li>

                </ul>
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="#">帮助</a></li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><%=this.Context.User.Identity.Name %> <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li>
                                <a href="#">
                                    <form method="post" enctype="application/x-www-form-urlencoded" action="logout.ashx">
                                        <input class="btn-quit" type="submit" value="退出" />
                                    </form>
                                </a>
                            </li>
                            <li><a href="#">个人中心</a></li>
                        </ul>
                    </li>
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
                            <span>jQuery</span>
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
                            <li class="active"><a href="./bootstrap-layout-fluid.html"><span>layout</span></a></li>
                            <li><a href="./bootstrap-table.html"><span>table</span></a></li>
                            <li><a href="./bootstrap-drawer.html"><span>drawer</span></a></li>
                            <li><a href="./bootstrap-datepicker.html"><span>datepicker</span></a></li>
                            <li><a href="./bootstrap-select.html"><span>select</span></a></li>
                            <li><a href="./easyui-demo.html"><span>easyui1.5</span></a></li>
                        </ul>
                    </li>
                </ul>
                <ul class="nav nav-sidebar">
                    <li class="menu-l1">
                        <a target="_blank" href="./react.html"><span>react</span></a>
                    </li>
                </ul>
                <ul class="nav nav-sidebar">
                    <li class="menu-l1">
                        <a target="_blank" href="./vue.html"><span>vue</span></a>
                    </li>
                </ul>
            </div>
            <div class="col-md-21 col-md-offset-3 main">
                <blockquote>
                    <p class="lead">请假申请</p>
                </blockquote>
                <form id="formLeave" class="form-md" method="post" action="workflowStart.ashx" enctype="application/x-www-form-urlencoded">
                    <input type="hidden" name="Category" value="请假申请" />
                    <input type="hidden" name="WorkFlowXaml" value="请假流程.xaml" />
                    <input type="hidden" name="formdata" id="formdata" />
                    <input type="hidden" name="TagField"  value="LeavType" />
                    <div class="form-group">
                        <label >类别</label>
                        <select class="form-control" name="LeavType">
                            <option selected="selected" value="年假">年假</option>
                            <option value="事假">事假</option>
                            <option value="病假">病假</option>
                            <option value="陪产假">陪产假</option>
                            <option value="育儿假">育儿假</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="startTime">开始时间</label>
                        <input id="startTime" type="text" class="form-control"  name="StartTime" />
                    </div>
                     <div class="form-group">
                        <label for="endTime">结束时间</label>
                        <input id="endTime" type="text" class="form-control"  name="EndTime" />
                    </div>
                    <div class="form-group">
                        <label for="reason">原因</label>
                        <textarea class="form-control" rows="3" name="Reason" id="reason" ></textarea>
                    </div>
                    <button id="btnsubmitleave" type="submit" class="btn btn-default">提交</button>
                </form>
            </div>
        </div>
    </div>

    <div class="col-md-offset-3 ">
        <p class="text-center" style="margin: 0 0"><small>版权所有&copy;丢了光影 2016-2020</small></p>
    </div>
</body>

</html>
