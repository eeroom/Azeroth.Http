//options:{maxTaskCount,url,formadataHandler, chunkSize,completeHandler,errorHandler,uploadingHandler}
//formadataHandler=(formdata,opt)=>void
//completeHandler,errorHandler,uploadingHandler=(opt, resdata, options)=>void
function klzUploader(options) {
    options.maxTaskCount = options.maxTaskCount || 1;
    var runingTaskCount = 0;
    var lstFileWrapper = [];
    var uploader = this;

    function uploadFileByChunk(opt) {
        opt.position = opt.position || 0;
        var buffer = opt.fileWrapper.file.slice(opt.position, opt.position + opt.chunkSize);
        var formdata = new FormData();
        formdata.append("FileEntity", buffer, opt.fileWrapper.file.name);
        formdata.append("Position", opt.position);
        formdata.append("FileSize", opt.fileWrapper.file.size);
        formdata.append("WebkitRelativePath", opt.fileWrapper.file.webkitRelativePath);
        opt.formadataHandler && opt.formadataHandler(formdata, opt);
        $.ajax({
            "method": "POST",
            "url": opt.url,
            "data": formdata,
            "processData": false,
            "contentType": false
        }).then(function (resdata) {
            opt.position = opt.position + opt.chunkSize;
            if (opt.position >= opt.fileWrapper.file.size) {
                opt.completeHandler(opt, resdata);
                return;
            }
            opt.uploadingHandler(opt, resdata);
            uploadFileByChunk(opt);
        }).fail(function (resdata) {
            opt.errorHandler(opt, resdata);
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
        uploadFileByChunk({
            fileWrapper,
                url: options.url,
            formadataHandler: options.formadataHandler,
            chunkSize: options.chunkSize,
            completeHandler: function (opt, resdata) {
                options.completeHandler(opt, resdata, options);
                var tmp = lstFileWrapper.shift();
                if (tmp)
                    uploader.send(tmp);
            },
            errorHandler: function (opt, resdata) {
                options.errorHandler(opt, resdata, options);
                var tmp = lstFileWrapper.shift();
                if (tmp)
                    uploader.send(tmp);
            },
            uploadingHandler: function (opt, resdata) {
                options.uploadingHandler(opt, resdata, options);
            }
        });

    };
}