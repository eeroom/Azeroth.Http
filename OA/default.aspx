<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OA._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的待办</title>
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
                <a class="navbar-brand" href="#">我的网盘</a>
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
                    <li><a href="#"><%=this.Context.User.Identity.Name %></a></li>
                </ul>
            </div>

        </div>
    </nav>


    <div class="col-md-offset-3 ">
        <p class="text-center" style="margin: 0 0"><small>版权所有&copy;丢了光影 2016-2020</small></p>
    </div>
</body>
</html>
