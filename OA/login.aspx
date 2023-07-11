<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="login.aspx.cs" Inherits="OA.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登陆</title>
    <!-- Bootstrap core CSS -->
    <link href="/Bootstrap3.3.7/css/bootstrap.css" rel="stylesheet" />
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <link href="/css/ie10-viewport-bug-workaround.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <%--<link href="/css/layout-fluid.css" rel="stylesheet" />--%>
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
        body {
            padding-top: 40px;
            padding-bottom: 40px;
            background-color: #eee;
        }

        .form-signin {
            max-width: 330px;
            padding: 15px;
            margin: 0 auto;
        }

            .form-signin .form-signin-heading,
            .form-signin .checkbox {
                margin-bottom: 10px;
            }

            .form-signin .checkbox {
                font-weight: 400;
            }

            .form-signin .form-control {
                position: relative;
                -webkit-box-sizing: border-box;
                -moz-box-sizing: border-box;
                box-sizing: border-box;
                height: auto;
                padding: 10px;
                font-size: 16px;
            }

                .form-signin .form-control:focus {
                    z-index: 2;
                }

            .form-signin input[type="text"] {
                margin-bottom: -1px;
                border-bottom-right-radius: 0;
                border-bottom-left-radius: 0;
            }

            .form-signin input[type="password"] {
                margin-bottom: 10px;
                border-top-left-radius: 0;
                border-top-right-radius: 0;
            }
    </style>
</head>
<body>
    <div class="container">

        <form class="form-signin" method="post" enctype="application/x-www-form-urlencoded"  action="login.aspx">
            <h2 class="form-signin-heading">请登录</h2>
            <label for="inputEmail" class="sr-only">用户名</label>
            <input name="userName" type="text" id="inputEmail" class="form-control" placeholder="请输入用户名" required autofocus value="<%=this.UserName %>" />
            <label for="inputPassword" class="sr-only">密码</label>
            <input name="pwd" type="password" id="inputPassword" class="form-control" placeholder="请输入密码" required value="<%=this.Pwd %>" />
            <div class="checkbox">
                <label>
                    <input type="checkbox" value="remember-me" />
                    记住我
                </label>
            </div>
            <button class="btn btn-lg btn-primary btn-block" type="submit">确定</button>
        </form>

    </div>
    <!-- /container -->
</body>
</html>
