layui.use(['form', 'layer', 'jquery'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery;

    $(".loginBody .seraph").click(function () {
        layer.msg("这只是做个样式，功能暂未实现！", {
            time: 2000
        });
    });

    //登录按钮
    form.on("submit(login)", function (data) {
        //console.log(data);
        var obj = $(this);
        obj.text("登录中...").attr("disabled", "disabled").addClass("layui-disabled");
        $.ajax({
            type: 'POST', 
            url: '/Account/SignIn/',
            data: data.field,
            dataType: "json",
            headers: {
                "X-CSRF-TOKEN-yilezhu": $("input[name='AntiforgeryKey_yilezhu']").val()
            },
            success: function (res) {//res为相应体,function为回调函数
                if (res.ResultCode === 0) {
                    window.location.href = "/";
                } else {
                    layer.alert(res.ResultMsg, { icon: 5 });
                    d = new Date();
                    $("#Password").val('');
                    $("#CaptchaCodeImg").attr("src", "/Account/GetCaptchaImage?" + d.getTime());
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
            },
            complete: function () {
                obj.text("登录").removeAttr("disabled").removeClass("layui-disabled");

            }
        });
        return false;
    });

    form.verify({
        userName: function (value, item) { //value：表单的值、item：表单的DOM对象
            if (!new RegExp("^[a-zA-Z0-9_\u4e00-\u9fa5\\s·]+$").test(value)) {
                return '登录名不能有特殊字符';
            }
            if (/(^\_)|(\__)|(\_+$)/.test(value)) {
                return '登录名首尾不能出现下划线\'_\'';
            }
            if (/^\d+\d+\d$/.test(value)) {
                return '登录名不能全为数字';
            }
            if (value.length > 32 || value.length < 4) {
                return '登录名长度必须符合规则';
            }
        },
        captchaCode: function (value, item) { //value：表单的值、item：表单的DOM对象
            if (!new RegExp("^[a-zA-Z0-9_\u4e00-\u9fa5\\s·]+$").test(value)) {
                return '验证码不能有特殊字符';
            }
            if (/(^\_)|(\__)|(\_+$)/.test(value)) {
                return '验证码首尾不能出现下划线\'_\'';
            }
            if (value.length !== 4) {
                return '验证码长度必须符合规则';
            }
        },
        password: function (value, item) { //value：表单的值、item：表单的DOM对象
            if (/(^\_)|(\__)|(\_+$)/.test(value)) {
                return '密码首尾不能出现下划线\'_\'';
            }
            if (value.length > 32 || value.length < 4) {
                return '验证码长度必须符合规则';
            }
        }
    });

    $("#CaptchaCodeImg").click(function () {
        d = new Date();
        $("#CaptchaCodeImg").attr("src", "/Account/GetCaptchaImage?" + d.getTime());
    });


    //表单输入效果
    $(".loginBody .input-item").click(function (e) {
        e.stopPropagation();
        $(this).addClass("layui-input-focus").find(".layui-input").focus();
    });
    $(".loginBody .layui-form-item .layui-input").focus(function () {
        $(this).parent().addClass("layui-input-focus");
    });
    $(".loginBody .layui-form-item .layui-input").blur(function () {
        $(this).parent().removeClass("layui-input-focus");
        if ($(this).val() !== '') {
            $(this).parent().addClass("layui-input-active");
        } else {
            $(this).parent().removeClass("layui-input-active");
        }
    });
});
