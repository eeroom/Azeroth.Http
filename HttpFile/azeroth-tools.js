/// <reference path="D:\Code\Azeroth.Http\HttpFile\jQuey/jquery-1.11.3.js" />
//options:{maxTaskCount,url,formadataHandler, chunkSize,completeHandler,errorHandler,uploadingHandler}
//formadataHandler=(formdata,opt)=>void
//completeHandler,errorHandler,uploadingHandler=(opt, resdata, options)=>void
function klzUploader(options) {
    options.maxTaskCount = options.maxTaskCount || 1;
    var runingTaskCount = 0;
    var lstFileWrapper = [];
    var uploader = this;

    function uploadFileByChunk(opt) {
        opt.fileWrapper.position = opt.fileWrapper.position || 0;
        var buffer = opt.fileWrapper.file.slice(opt.fileWrapper.position, opt.fileWrapper.position + opt.chunkSize);
        var formdata = new FormData();
        formdata.append("FileEntity", buffer, opt.fileWrapper.file.name);
        formdata.append("Position", opt.fileWrapper.position);
        formdata.append("FileSize", opt.fileWrapper.file.size);
        formdata.append("WebkitRelativePath", opt.fileWrapper.file.webkitRelativePath||"");
        opt.formadataHandler && opt.formadataHandler(formdata, opt);
        $.ajax({
            "method": "POST",
            "url": opt.url,
            "data": formdata,
            "processData": false,
            "contentType": false,
            success: function (resdata) {
                opt.fileWrapper.position = opt.fileWrapper.position + opt.chunkSize;
                if (opt.fileWrapper.position >= opt.fileWrapper.file.size) {
                    opt.completeHandler(opt, resdata);
                    return;
                }
                if (!!opt.fileWrapper.statusflag) {
                    opt.statusHandler(opt, resdata)
                } else {
                    opt.uploadingHandler(opt, resdata);
                    uploadFileByChunk(opt);
                }
            },
            error: function (resdata) {
                opt.errorHandler(opt, resdata);
            }
        });
    }

    //fileWrapper:{file,startPosition}
    //appendMode,top加在数组头部，默认添加到数组尾部
    this.send = function (fileWrapper, appendMode) {
        if (runingTaskCount >= options.maxTaskCount) {
            if (appendMode == "top") {
                lstFileWrapper.unshift(fileWrapper);
            } else {
                lstFileWrapper.push(fileWrapper)
            }
            return;
        }
        runingTaskCount++;
        uploadFileByChunk({
            fileWrapper: fileWrapper,
            url: options.url,
            formadataHandler: options.formadataHandler,
            chunkSize: options.chunkSize,
            completeHandler: function (opt, resdata) {
                runingTaskCount--;
                options.completeHandler(opt, resdata, options);
                var tmp = lstFileWrapper.shift();
                if (tmp)
                    uploader.send(tmp);
            },
            errorHandler: function (opt, resdata) {
                runingTaskCount--;
                options.errorHandler(opt, resdata, options);
                var tmp = lstFileWrapper.shift();
                if (tmp)
                    uploader.send(tmp);
            },
            uploadingHandler: function (opt, resdata) {
                options.uploadingHandler(opt, resdata, options);
            },
            statusHandler: function (opt, resdata) {
                //上传中，暂停，删除
                runingTaskCount--;
                var tmp = lstFileWrapper.shift();
                if (tmp)
                    uploader.send(tmp);
                options.statusHandler && options.statusHandler(opt, resdata, options)
            }
        });

    };
}

$.fn.serializeObject = function () {
    var arry = $(this).serializeArray();
    var obj = {};
    $.each(arry, function (index, el) {
        if (!obj[el.name]) {
            obj[el.name] = el.value;
        } else {
            obj[el.name] = [obj[el.name]];
            obj[el.name].push(el.value);
        }
    });
    return obj;
};