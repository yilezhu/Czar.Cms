layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;

    //角色列表
    var tableIns = table.render({
        elem: '#managerList',
        url: '/Manager/LoadData/',
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 10,
        id: "managerListTable",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: 'Id', width: 50, align: "center" },
            { field: 'UserName', title: '登陆ID', minWidth: 50, align: "center" },
            { field: 'NickName', title: '用户昵称', minWidth: 50, align: "center" },
            { field: 'Mobile', title: '手机号码', minWidth: 80, align: "center" },
            { field: 'Email', title: '邮箱地址', minWidth: 100, align: "center" },
            { field: 'RoleName', title: '所属角色', minWidth: 80, align: 'center' },
            { field: 'Remark', title: '备注', align: 'center' },
            { field: 'IsLock', title: '是否锁定', minWidth: 100, fixed: "right", align: "center", templet: '#IsLock' },
            { title: '操作', minWidth: 80, templet: '#managerListBar', fixed: "right", align: "center" }
        ]]
    });

    //搜索【此功能需要后台配合，所以暂时没有动态效果演示】
    $(".search_btn").on("click", function () {
        if ($(".searchVal").val() !== '') {
            table.reload("managerListTable", {
                page: {
                    curr: 1 //重新从第 1 页开始
                },
                where: {
                    key: $(".searchVal").val()  //搜索的关键字
                }
            });
        } else {
            layer.msg("请输入搜索的内容");
        }
    });

    //添加用户
    function addManager(edit) {
        var tit = "添加用户";
        if (edit) {
            tit = "编辑用户";
        }
        var index = layui.layer.open({
            title: tit,
            type: 2,
            anim: 1,
            area: ['500px', '90%'],
            content: "/Manager/AddOrModify/",
            success: function (layero, index) {
                var body = layui.layer.getChildFrame('body', index);
                if (edit) {
                    body.find("#Id").val(edit.Id);
                    body.find(".UserName").val(edit.UserName);
                    body.find(".NickName").val(edit.NickName);
                    body.find(".RoleId").val(edit.RoleId);
                    body.find(".Mobile").val(edit.Mobile);
                    body.find(".Email").val(edit.Email);
                    body.find("input:checkbox[name='IsLock']").prop("checked", edit.IsLock);
                    body.find(".Remark").text(edit.Remark);
                    form.render();
                }
            }
        });
    }
    $(".addManager_btn").click(function () {
        addManager();
    });

    //批量删除
    $(".delAll_btn").click(function () {
        var checkStatus = table.checkStatus('managerListTable'),
            data = checkStatus.data,
            managerId = [];
        if (data.length > 0) {
            for (var i in data) {
                managerId.push(data[i].Id);
            }
            layer.confirm('确定删除选中的用户？', { icon: 3, title: '提示信息' }, function (index) {
                //获取防伪标记
                del(managerId);
            });
        } else {
            layer.msg("请选择需要删除的用户");
        }
    });

    //列表操作
    table.on('tool(managerList)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        if (layEvent === 'edit') { //编辑
            addManager(data);
        } else if (layEvent === 'del') { //删除
            layer.confirm('确定删除此用户？', { icon: 3, title: '提示信息' }, function (index) {
                del(data.Id);
            });
        }
    });

    form.on('switch(IsLock)', function (data) {
        var tipText = '确定锁定当前用户吗？';
        if (!data.elem.checked) {
            tipText = '确定启用当前用户吗？';
        }
        layer.confirm(tipText, {
            icon: 3,
            title: '系统提示',
            cancel: function (index) {
                data.elem.checked = !data.elem.checked;
                form.render();
                layer.close(index);
            }
        }, function (index) {
            changeLockStatus(data.value, data.elem.checked);
            layer.close(index);
        }, function (index) {
            data.elem.checked = !data.elem.checked;
            form.render();
            layer.close(index);
        });
    });

    function del(managerId) {
        $.ajax({
            type: 'POST',
            url: '/Manager/Delete/',
            data: { managerId: managerId },
            dataType: "json",
            headers: {
                "X-CSRF-TOKEN-yilezhu": $("input[name='AntiforgeryKey_yilezhu']").val()
            },
            success: function (data) {//res为相应体,function为回调函数
                layer.msg(data.ResultMsg, {
                    time: 2000 //20s后自动关闭
                }, function () {
                    tableIns.reload();
                    layer.close(index);
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
            }
        });
    }

    function changeLockStatus(managerId,status) {
        $.ajax({
            type: 'POST',
            url: '/Manager/ChangeLockStatus/',
            data: { Id: managerId, Status: status },
            dataType: "json",
            headers: {
                "X-CSRF-TOKEN-yilezhu": $("input[name='AntiforgeryKey_yilezhu']").val()
            },
            success: function (data) {//res为相应体,function为回调函数
                layer.msg(data.ResultMsg, {
                    time: 2000 //2s后自动关闭
                }, function () {
                    tableIns.reload();
                    layer.close(index);
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
            }
        });
    }

});
