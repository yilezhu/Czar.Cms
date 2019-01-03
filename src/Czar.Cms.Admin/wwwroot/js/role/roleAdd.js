layui.use(['form', 'layer'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery;

    form.on("submit(addRole)", function (data) {
        //获取防伪标记
        $.ajax({
            type: 'POST',
            url: '/ManagerRole/AddOrModify/',
            data: {
                Id: $("#Id").val(),  //主键
                RoleName: $(".RoleName").val(),  //角色名称
                RoleType: $(".RoleType").val(),  //角色类型
                IsSystem: $("input[name='IsSystem']:checked").val() === "0" ? false : true,  //是否系统默认
                Remark: $(".Remark").val()  //用户简介
            },
            dataType: "json",
            headers: {
                "X-CSRF-TOKEN-yilezhu": $("input[name='AntiforgeryKey_yilezhu']").val()
            },
            success: function (res) {//res为相应体,function为回调函数
                if (res.ResultCode === 0) {
                    var alertIndex = layer.alert(res.ResultMsg, { icon: 1 }, function () {
                        layer.closeAll("iframe");
                        //刷新父页面
                        parent.location.reload();
                        top.layer.close(alertIndex);
                    });
                    //$("#res").click();//调用重置按钮将表单数据清空
                } else if (res.ResultCode === 102) {
                    layer.alert(res.ResultMsg, { icon: 5 }, function () {
                        layer.closeAll("iframe");
                        //刷新父页面
                        parent.location.reload();
                        top.layer.close(alertIndex);
                    });
                }
                else {
                    layer.alert(res.ResultMsg, { icon: 5 });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
            }
        });
        return false;
    });
});