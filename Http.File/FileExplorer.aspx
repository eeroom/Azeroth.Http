<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileExplorer.aspx.cs" Inherits="Http.File.FileExplorer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>文件管理</title>
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

        .btn-input-file {
            position: relative;
        }

            .btn-input-file input {
                position: absolute;
                left: 0;
                top: 0;
                bottom: 0;
                right: 0;
                width: 100%;
                opacity: 0;
            }

        .panel-lstfile {
            min-height: 600px;
            max-height: 800px;
        }

        .panel-lst-filetask {
            position: absolute;
            right: 20px;
            top: 70px;
            max-height: 600px;
            overflow-y: auto;
            overflow-x: hidden;
            opacity:0.9;
        }
        .panel-lst-filetask .progress{
            margin-bottom:2px;
        }

        .ellipsis {
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .container .breadcrumb{
            margin-top:10px;
            margin-bottom:1px;
        }
        .container .panel{
            margin-top:10px;
        }
    </style>
    <script type="text/javascript">
        $(function () {



        });

    </script>

</head>

<body>
    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
              </button>
                <a class="navbar-brand" href="#">文件管理</a>
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

    <div class="container">
        <div class="row">
            <div class="col-sm-18">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title" style="display:inline">文件列表</h3>
                        <div class="pull-right">总数量:100;总大小:20GB</div>
                    </div>
                    <div class="panel-body panel-lstfile">
                        <a class="btn btn-default  btn-input-file" href="#" role="button">上传文件
                            <input type="file" name="myfile" multiple="multiple" />
                        </a>
                        <a class="btn btn-default  btn-input-file" href="#" role="button">上传文件夹
                            <input type="file" name="myfile" multiple="multiple" webkitdirectory />
                        </a>
                        <a class="btn btn-default" href="#" role="button">删除</a>
                          <a class="btn btn-default" href="#" role="button">下载</a>
                        <ol class="breadcrumb">
                            <li><a href="#">配置文件</a></li>
                            <li><a href="#">idea</a></li>
                            <li class="active">maven</li>
                        </ol>
                        <table class="table table-hover">
                            <tr>
                                <th>文件名称</th>
                                <th>文件大小</th>
                                <th>最后修改时间</th>
                                <th>文件SHA256</th>
                                <th>操作</th>
                            </tr>
                            <tr>
                                <td><a title="笑傲江笑傲江湖001笑傲江湖001笑傲江湖001笑傲江湖001笑傲江湖001湖001.rmvb">笑傲江笑傲江湖001..</a></td>
                                <td>115MB</td>
                                <td>2019年6月11日 12:22:11</td>
                                <td>mawwwwwwwwwwwwwwwww</td>
                                <td><button type="button" class="btn btn-default btn-xs">删除</button>
  <button type="button" class="btn btn-default btn-xs">下载</button></td>
                            </tr>
                        </table>
                        <ul class="pagination pull-right">
        <li class="disabled"><a href="#" aria-label="Previous"><span aria-hidden="true">«</span></a></li>
        <li class="active"><a href="#">1 <span class="sr-only">(current)</span></a></li>
        <li><a href="#">2</a></li>
        <li><a href="#">3</a></li>
        <li><a href="#">4</a></li>
        <li><a href="#">5</a></li>
        <li><a href="#" aria-label="Next"><span aria-hidden="true">»</span></a></li>
     </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6 hidden-xs panel-lst-filetask">
                <div class="panel panel-default ">
            <!-- Default panel contents -->
            <div class="panel-heading">上传任务
                <div class="pull-right">速度:5MB/秒;剩余大小:200MB</div>
            </div>
            <!-- List group -->
            <ul class="list-group">
                <li class="list-group-item">
                    <div class="row">
                        <div class="col-md-18 ellipsis">
                            <a title="文件名特长的文件水水水水水水水水水水水水水水水水水水水哇哇哇哇哇.bin">文件名特长的文件水水水水水水水水水水水水水水水水水水水哇哇哇哇哇.bin</a>
                        </div>
                        <div class="col-md-6">
                            <div class="btn-group btn-group-xs">
                                <button type="button" class="btn btn-default">暂停</button>
                                <button type="button" class="btn btn-default">删除</button>
                            </div>
                        </div>
                    </div>
                    <div class="progress">
                        <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100" style="width: 45%">
                            <span>45%</span>
                        </div>
                    </div>
                </li>
                <li class="list-group-item">
                    <div class="row">
                        <div class="col-md-18 ellipsis">
                            <a title="amp.exe">amp.exe</a>
                        </div>
                        <div class="col-md-6">
                            <div class="btn-group btn-group-xs">
                                <button type="button" class="btn btn-default">暂停</button>
                                <button type="button" class="btn btn-default">删除</button>
                            </div>
                        </div>
                    </div>
                    <div class="progress">
                        <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100" style="width: 16%">
                            <span>16%</span>
                        </div>
                    </div>
                </li>

            </ul>
        </div>
            </div>
        </div>
        

    </div>

    <div class="col-md-offset-3 navbar-fixed-bottom">
        <p class="text-center" style="margin: 0 0"><small>版权所有&copy;丢了光影 2016-2020</small></p>
    </div>
</body>
</html>
