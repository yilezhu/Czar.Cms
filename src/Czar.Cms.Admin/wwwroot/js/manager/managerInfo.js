var form, $, areaData;
layui.config({
    base: "../../js/"
}).extend({
    "address": "address"
});
layui.use(['form', 'layer', 'upload'], function () {
    form = layui.form;
    $ = layui.jquery;
    var layer = parent.layer === undefined ? layui.layer : top.layer,
        upload = layui.upload;

    //上传头像
    upload.render({
        elem: '.userFaceBtn',
        url: '/File/UploadImage/',
        size: 1024,
        method: "post",
        before: function (obj) {
            layer.load(); //上传loading
        },
        done: function (res, index, upload) {
            if (res.code === 0) {
                $('#userFace').attr('src', res.data.src);
                window.sessionStorage.setItem('userFace', res.data.src);
                $("#Avatar").val(res.data.src);
            }
            else {
                layer.msg(res.msg);
            }
            layer.closeAll('loading'); //关闭loading
        },
        error: function (index, upload) {
            layer.closeAll('loading'); //关闭loading
        }
    });

    form.on("submit(changeManager)", function (data) {
        var obj = $(this);
        obj.text("提交中...").attr("disabled", "disabled").addClass("layui-disabled");
        //获取防伪标记
        $.ajax({
            type: 'POST',
            url: '/Manager/ManagerInfo/',
            data: data.field,
            dataType: "json",
            headers: {
                "X-CSRF-TOKEN-yilezhu": $("input[name='AntiforgeryKey_yilezhu']").val()
            },
            success: function (res) {//res为相应体,function为回调函数
                var alertIndex;
                if (res.ResultCode === 0) {
                    alertIndex=layer.alert(res.ResultMsg, { icon: 1 }, function (index) {
                        layer.close(index);
                        parent.parent.location.reload();
                        top.layer.close(alertIndex);
                    });

                } else {
                    alertIndex=layer.alert(res.ResultMsg, { icon: 5 }, function (index) {
                        layer.close(index);
                        location.reload();
                        top.layer.close(alertIndex);
                    });
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
            },
            complete: function () {
                obj.text("立即提交").removeAttr("disabled").removeClass("layui-disabled");

            }
        });
        return false;
    });

});