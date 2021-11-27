<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pan.aspx.cs" Inherits="Http.File.Pan" %>

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
    <link href="bootstrap-table-1.11.1/bootstrap-table.css" rel="stylesheet" />
    <script src="bootstrap-table-1.11.1/bootstrap-table.js"></script>
    <script src="bootstrap-table-1.11.1/locale/bootstrap-table-zh-CN.js"></script>
    <script src="azeroth-lib.js"></script>
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
        }

        .panel-lst-filetask {
            position: absolute;
            right: 20px;
            top: 70px;
            max-height: 600px;
            overflow-y: auto;
            overflow-x: hidden;
            opacity: 0.9;
        }

            .panel-lst-filetask .progress {
                margin-bottom: 2px;
            }

        .ellipsis {
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .container .breadcrumb {
            margin-top: 10px;
            margin-bottom: 1px;
        }

        .container .panel {
            margin-top: 10px;
        }

        .span-margin {
            margin-left: 6px;
        }

        .bg-yellow {
            color: #ffd800;
        }

        .bg-red {
            color: #ed0c0c;
        }
    </style>
    <script type="text/javascript">
        function handlerColumnFormatter(value, row, index) {
            return `<a class="btn btn-xs btn-default btn-row-delete" data-id="${row.Id}"><span class ="glyphicon glyphicon-remove-sign"></span></a>`;
        }
        function handlerNameFormatter(value, row, index) {
            if (row.CC == "dir")
                return `<span class ="glyphicon glyphicon-folder-open bg-yellow"></span><span class="span-margin">${row.FullName}</span>`
            return `<span class ="glyphicon glyphicon-file bg-red"></span><span class="span-margin">${row.FullName}</span>`;
        }

        $(function () {
            //所有选项都定义在  jQuery.fn.bootstrapTable.defaults
            window.btable = btable = $("#tbFilelst").bootstrapTable({
                toolbar: "#tbToolbar"
                 , striped: true                           //是否显示行间隔色,默认为false
                 , showRefresh: true                  //是否显示刷新按钮,默认为false
                 , showToggle: true                   //是否显示详细视图和列表视图的切换按钮
                 , clickToSelect: false                //是否启用点击选中行
                 , showColumns: true      //允许选择要展示的列，默认为false
                 , cache: false                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                 , pagination: false                   //是否分页
                 , queryParamsType: '' //默认值为 'limit' ,传给服务端的参数为：offset,limit,sort。"",传给服务端的参数为:pageSize,pageNumber
                 , sidePagination: "server"       //分页模式：client or server
                 , sortName: "Id"                     //首次加载排序的字段，默认无值
                 , sortOrder: "desc"                  //首次加载的排序方式
                 , pageList: [10, 25, 50, 100]        //可供选择的每页的行数（*）
                 , url: " "                      //请求后台的URL（*），" "这样表示当前页面地址
                 , method: "POST"                      //请求方式（*）
                 , contentType: "application/x-www-form-urlencoded"
                 , queryParams: function (parameters) {
                     var formdata = $("form").serializeObject();
                     //比对本次和上一次表单的值有没有修改过
                     if (JSON.stringify(formdata) != JSON.stringify(this.myformdata || {})) {
                         this.pageNumber = 1;
                         parameters["pageNumber"] = 1;
                         this.myformdata = formdata;
                     }
                     $.extend(parameters, formdata);
                     parameters["cmd"] = "GetFileEntities";
                     return parameters;
                 }
                , onDblClickRow: function (item, $element) {
                    if (item.CC != "dir")
                        return;
                    $("#filepath").val(item.Path)
                    btable.bootstrapTable("refresh", {
                        pageNumber: 1
                    })
                    return false;
                }
            });

            var mm = jQuery.fn.bootstrapTable.defaults;
            $("#tbFilelst").on("click", ".btn-row-delete", function () {
                var id = $(this).data("id");
                if (id == "2147483647") {
                    alert("不能直接删除文件夹");
                    return
                }

                var lstId = [id];
                $.ajax({
                    url: "?cmd=Delete",
                    type: "post",
                    contentType: "application/json",
                    data: JSON.stringify({ lstId}),
                            processData: false,
                            success: function () {
                        btable.bootstrapTable("refresh", { pageNumber: 1 })
                    }
                    })
                    });
            $("form").submit(function () {
                btable.bootstrapTable("refresh", {
                        pageNumber: 1
                    });
                return false;
            });
            $("#tbToolbar").on("click", ".btn-row-delete2", function () {
                var data = btable.bootstrapTable("getSelections");
                var lstId = data.map(x=>x.Id)
                if (lstId.find(x=>x == "2147483647")) {
                    alert("不能直接删除文件夹");
                    return
                }

                $.ajax({
                    url: "?cmd=Delete",
                    type: "post",
                    contentType: "application/json",
                    data: JSON.stringify({ lstId}),
                            processData: false,
                            success: function () {
                        btable.bootstrapTable("refresh", { pageNumber: 1 })
                    }
                    })
                    });
                    });

    </script>
    <script type="text/javascript">
        //正在上传的列表数据
        let lstuplodingfilewrapper = [];
        function removeUplodingItem(fileWrapper) {
            lstuplodingfilewrapper.splice(lstuplodingfilewrapper.findIndex(x=>x == fileWrapper), 1)
            $(fileWrapper.uploadingElement).remove();
            if (lstuplodingfilewrapper.length < 1)
                $(".panel-lst-filetask").hide();
        }

        function addUplodingItem(fileWrapper) {
            lstuplodingfilewrapper.push(fileWrapper);
            $(".panel-lst-filetask").show();
        }

        var uploader = new klzUploader({
            maxTaskCount: 3,
            url: "?cmd=Upload",
            chunkSize: 10 * 1024,
            completeHandler: function (opt, resdata, options) {
                //$("#" + opt.fileWrapper.elUploadingId).empty();
                $.post("?cmd=Complete", { fileid: opt.fileWrapper.fileid || resdata.Id }, function () {
                    removeUplodingItem(opt.fileWrapper)
                    //刷新列表
                    window.btable.bootstrapTable("refresh", {
                        pageNumber: 1
                    })
                })
            },
            uploadingHandler: function (opt, resdata, options) {
                $(opt.fileWrapper.uploadingElement).find(".lstjd-msg").empty()
                $(opt.fileWrapper.uploadingElement).find(".lstjd-info").hide()
                var jd = parseInt(100.0 * opt.fileWrapper.position / opt.fileWrapper.file.size);
                $(opt.fileWrapper.uploadingElement).find(".lstjd-div").css("width", jd + "%")
                $(opt.fileWrapper.uploadingElement).find(".lstjd-span").html(jd + "%")
                if (!opt.fileWrapper.hasfileid) {
                    opt.fileWrapper.fileid = resdata.Id
                    opt.fileWrapper.hasfileid = true;
                }
            },
            errorHandler: function (opt, resdata, options) {
                $(opt.fileWrapper.uploadingElement).find(".lstjd-msg").html((resdata.responseJSON && resdata.responseJSON.msg) || "服务器发送错误")
                $(opt.fileWrapper.uploadingElement).find(".lstjd-fuc-btn-stop").trigger("click");
            },
            statusHandler: function (opt) {

            },
            formadataHandler: function (formdata, opt) {
                formdata.append("fileId", opt.fileWrapper.fileid)
            }
        });

        function createNewTaskHtml(file) {
            //file是从file的input直接选文件添加上传任务，fileEnity是从api取得数据添加续传任务
            var fullname = file.webkitRelativePath || file.name, filename = file.name
            var jdvalue = 0
            var tipclass = "hidden", ctclass = "hidden", stopclass = ""
            var htmlstr = getTaskHtml(fullname, filename, jdvalue, tipclass, ctclass, stopclass)
            var uploadingElement = $(htmlstr);
            uploadingElement.appendTo("#lstuploading");
            var fileWrapper = { file: file, uploadingElement, hasfileid: false, fullname, filename, position: 0, fileid: -1};
            addUplodingItem(fileWrapper)
            uploader.send(fileWrapper);
            }

        function createCCTaskHtml(fileEntity) {
            //file是从file的input直接选文件添加上传任务，fileEnity是从api取得数据添加续传任务
            var fullname = fileEntity.FullName, filename = fileEntity.Name
            var jdvalue = parseInt(100.0 * fileEntity.Position / fileEntity.Size)
            var tipclass = "", ctclass="", stopclass="hidden"
            var htmlstr = getTaskHtml(fullname, filename, jdvalue, tipclass, ctclass, stopclass)
            var uploadingElement = $(htmlstr);
            uploadingElement.appendTo("#lstuploading");
            var fileWrapper = { file: null, uploadingElement, hasfileid: true, fullname, filename, position: fileEntity.Position, fileid: fileEntity.Id, size: fileEntity.Size};
            addUplodingItem(fileWrapper)
            }

        function getTaskHtml(fullname, filename, jdvalue, tipclass, ctclass, stopclass) {
            var htmlstr = `<li class="list-group-item">
                            <div class="row">
                                <div class="col-md-16 ellipsis">
                                    <a title="${filename}"><span class ="glyphicon glyphicon-exclamation-sign lstjd-info ${tipclass}" style="color:red"></span> ${filename
            }</a>
                                </div>
                                <div class="col-md-8">
                                    <div class ="btn-group btn-group-xs">
                                        <button type="button" class ="btn btn-default btn-input-file ${ctclass}"><span class ="glyphicon glyphicon-step-forward"></span>续传
                                            <input class ="lstjd-fuc-file-ct" type="file" name="ctmyfile" data-fullname="${fullname}" data-togtarget="lstjd-fuc-btn-stop" /></button>
                                        <button type="button" class ="btn btn-default lstjd-fuc-btn lstjd-fuc-btn-stop ${stopclass}" data-fullname="${fullname}" data-statusflag="stop"
                                             data-togtarget="lstjd-fuc-btn-cc"><span class ="glyphicon glyphicon-pause"></span>暂停</button>
                                        <button type="button" class ="btn btn-default lstjd-fuc-btn lstjd-fuc-btn-cc hidden" data-fullname="${fullname}"
                                                data-statusflag="" data-togtarget="lstjd-fuc-btn-stop"><span class ="glyphicon glyphicon-play"></span>继续</button>
                                        <button type="button" class ="btn btn-default lstjd-fuc-btn" data-fullname="${fullname}" data-statusflag="delete">
                                        <span class ="glyphicon glyphicon-remove-sign"></span>删除</button>
                                    </div>
                                </div>
                            </div>
                            <div class="progress">
                                <div class ="progress-bar progress-bar-striped active lstjd-div" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"
                                style="width: ${jdvalue}%">
                                    <span class ="lstjd-span">${jdvalue}%</span>
                                </div>
                            </div>
                            <div class ="lstjd-msg" style="color:red"></div>
                        </li>`
            return htmlstr;
        }

        $(function () {
            $("input[name='myfile']").change(function (sender) {
                $.each(this.files, function (index, file) {
                    createNewTaskHtml(file)
                });
                $(this).val("")
            });

            $(document.body).on("click", ".lstjd-fuc-btn", function () {
                var fullname = $(this).data("fullname");
                var statusflag = $(this).data("statusflag")
                var fileWrapper = lstuplodingfilewrapper.find(x=>x.fullname == fullname);
                $(fileWrapper.uploadingElement).find(".lstjd-info").show()
                fileWrapper.statusflag = statusflag
                if (!fileWrapper.statusflag) {
                    uploader.send(fileWrapper);
                }
                if (fileWrapper.statusflag == "delete") {
                    removeUplodingItem(fileWrapper);
                }
                var togtarget = $(this).data("togtarget")
                if (!togtarget)
                    return
                $(this).addClass("hidden")
                $(this).parent().find("." + togtarget).removeClass("hidden")

            })
            $(document.body).on("change", ".lstjd-fuc-file-ct", function () {
                var file = this.files[0];
                var fullname = $(this).data("fullname")
                var fileWrapper = lstuplodingfilewrapper.find(x=>x.fullname == fullname);
                if (file.name != fileWrapper.fullname || file.size != fileWrapper.size) {
                    alert("请选择同一个文件进行续传");
                    return;
                }
                var togtarget = $(this).data("togtarget")
                $(this).parent().addClass("hidden")
                $(this).parents(".btn-group-xs").find("." + togtarget).removeClass("hidden")
                fileWrapper.file = file;
                uploader.send(fileWrapper)
                $(this).val("")
            })
            $.post("?cmd=GetUploadingFileEntities", {}, function (data) {
                $.each(data, function (ind, fileEntity) {
                    createCCTaskHtml(fileEntity)
                })
            })
        })
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
                        <h3 class="panel-title" style="display: inline">我的文件</h3>
                        <div class="pull-right">空间容量：2048GB</div>
                    </div>
                    <div class="panel-body panel-lstfile">
                        <div id="tbToolbar">
                            <form class="form-inline">
                                <input type="hidden" name="path" id="filepath" />
                                <div class="btn-group" role="group" aria-label="...">
                                    <a class="btn btn-default  btn-input-file" href="#" role="button">上传文件
                            <input type="file" name="myfile" multiple="multiple" />
                                    </a>
                                    <a class="btn btn-default  btn-input-file" href="#" role="button">上传文件夹
                            <input type="file" name="myfile" multiple="multiple" webkitdirectory />
                                    </a>
                                    <button class="btn btn-default  btn-row-delete2" type="button">删除</button>
                                </div>

                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-addon">名称</div>
                                        <input type="text" class="form-control" name="Name" />
                                    </div>
                                </div>
                                <button type="submit" class="btn btn-primary">查询</button>
                            </form>
                        </div>
                        <table id="tbFilelst">
                            <thead>
                                <tr>
                                    <th data-checkbox="true"></th>
                                    <%-- <th data-radio="true" ></th>--%>
                                    <th data-field="FullName" data-sortable="true" data-formatter="handlerNameFormatter">名称</th>
                                    <th data-field="Size" data-sortable="true">大小</th>
                                    <th data-formatter="handlerColumnFormatter">操作</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6 hidden-xs panel-lst-filetask" style="display: none">
                <div class="panel panel-default ">
                    <div class="panel-heading">
                        上传任务
                <div class="pull-right">速度:5MB/秒;剩余大小:200MB</div>
                    </div>
                    <ul class="list-group" id="lstuploading">
                        <%--<li class="list-group-item">
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
                        </li>--%>
                    </ul>
                </div>
            </div>
        </div>


    </div>

    <div class="col-md-offset-3 ">
        <p class="text-center" style="margin: 0 0"><small>版权所有&copy;丢了光影 2016-2020</small></p>
    </div>
</body>
</html>
