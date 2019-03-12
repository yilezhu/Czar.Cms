var form, $, areaData;
layui.config({
    base: "../../js/"
}).extend({
    "address": "address"
});
layui.use(['form', 'layer', 'upload', 'laydate', "address"], function () {
    form = layui.form;
    $ = layui.jquery;
    var layer = parent.layer === undefined ? layui.layer : top.layer,
        upload = layui.upload,
        laydate = layui.laydate,
        address = layui.address;

    //上传头像
    upload.render({
        elem: '.userFaceBtn',
        url: '/File/UploadImage/',
        size: 1024,
        method: "post", 
        done: function (res, index, upload) {
            if (res.code === 0) {
                $('#userFace').attr('src', res.data.src);
                window.sessionStorage.setItem('userFace', res.data.src);
                $("#Avatar").val(res.data.src);
            }
            else {
                layer.msg(res.msg);
            }
           
        }
    });

    //添加验证规则
    form.verify({
        userBirthday: function (value) {
            if (!/^(\d{4})[\u4e00-\u9fa5]|[-\/](\d{1}|0\d{1}|1[0-2])([\u4e00-\u9fa5]|[-\/](\d{1}|0\d{1}|[1-2][0-9]|3[0-1]))*$/.test(value)) {
                return "出生日期格式不正确！";
            }
        }
    });
    //选择出生日期
    laydate.render({
        elem: '.userBirthday',
        format: 'yyyy年MM月dd日',
        trigger: 'click',
        max: 0,
        mark: { "0-09-05": "生日" },
        done: function (value, date) {
            if (date.month === 9 && date.date === 5) { //点击每年9月9日，弹出提示语
                layer.msg('今天是依乐祝的生日，也是CzarCms1.0的发布日，快来送上祝福吧！');
            }
        }
    });

    //获取省信息
    address.provinces();

    //提交个人资料
    form.on("submit(changeUser)", function (data) {
        var index = layer.msg('提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        //将填写的用户信息存到session以便下次调取
        var key, userInfoHtml = '';
        userInfoHtml = {
            'realName': $(".realName").val(),
            'sex': data.field.sex,
            'userPhone': $(".userPhone").val(),
            'userBirthday': $(".userBirthday").val(),
            'province': data.field.province,
            'city': data.field.city,
            'area': data.field.area,
            'userEmail': $(".userEmail").val(),
            'myself': $(".myself").val()
        };
        for (key in data.field) {
            if (key.indexOf("like") !== -1) {
                userInfoHtml[key] = "on";
            }
        }
        window.sessionStorage.setItem("userInfo", JSON.stringify(userInfoHtml));
        setTimeout(function () {
            layer.close(index);
            layer.msg("提交成功！");
        }, 2000);
        return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
    });

});