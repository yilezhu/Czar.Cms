
layui.config({
    base: "/js/"
}).extend({
    "authtree": "authtree"
});
layui.use(['form', 'layer', 'authtree'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery, authtree = layui.authtree;
    form.on("submit(addMenu)", function (data) {
        //获取防伪标记
        $.ajax({
            type: 'POST',
            url: '/Menu/AddOrModify/',
            data: {
                Id: $("#Id").val(),  //主键
                Name: $(".Name").val(),
                DisplayName: $(".DisplayName").val(),
                IconUrl: $(".IconUrl").val(),
                LinkUrl: $(".LinkUrl").val(),
                Sort: $(".Sort").val(),
                ParentId: $(".ParentId").val(),
                IsSystem: $("input[name='IsSystem']:checked").val() === "0" ? false : true,
                IsDisplay: $("input[name='IsDisplay']:checked").val() === "0" ? false : true
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

    $.ajax({
        url: "/Menu/LoadData/",
        dataType: 'json',
        success: function (res) {
            console.log(res.data);

            // 支持自定义递归字段、数组权限判断等
            // 深坑注意：如果API返回的数据是字符串，那么 startPid 的数据类型也需要是字符串
            var trees = authtree.listConvert(res.data, {
                primaryKey: 'Id'
                , startPid: 0
                , parentKey: 'ParentId'
                , nameKey: 'DisplayName'
                , valueKey: 'Id'
            });

            console.log(trees);
            // 渲染单选框
            var html = '<option value="0">无上级菜单</option>';
            layui.each(trees, function (index, item) {
                html = html + '<option value="' + item.value + '" '
                    + (item.checked ? 'selected' : '' + ' ')
                    + (item.disabled ? 'disabled' : '' + '>')
                    + item.name + '</option>';
            });

            console.log(html);

            $('.ParentId').html(html);
            form.render('select');
            form.on('select(ParentId)', function (data) {
                console.log('选中信息', data);
            });

        },
        error: function (xml, errstr, err) {
            layer.alert(errstr + '，获取样例数据失败，请检查是否部署在本地服务器中！');
        }
    });
});