layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;

    //角色列表
    var tableIns = table.render({
        elem: '#menuList',
        url: '/Menu/LoadData/',
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 10,
        id: "menuListTable",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: 'Id', minWidth: 30, align: "center" },
            { field: 'Name', title: '调用别名', minWidth: 50, align: "center" },
            { field: 'DisplayName', title: '显示名称', minWidth: 50, align: "center" },
            { field: 'LinkUrl', title: '链接地址', minWidth: 80, align: "center" },
            { field: 'Sort', title: '排序数字', minWidth: 80, align: 'center' },
            { field: 'IsDisplay', title: '是否显示', minWidth: 100, fixed: "right", align: "center", templet: '#IsDisplay' },
            { title: '操作', minWidth: 80, templet: '#menuListBar', fixed: "right", align: "center" }
        ]]
    });

    //搜索【此功能需要后台配合，所以暂时没有动态效果演示】
    $(".search_btn").on("click", function () {
        if ($(".searchVal").val() !== '') {
            table.reload("menuListTable", {
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
    function addMenu(edit) {
        var tit = "添加菜单";
        if (edit) {
            tit = "编辑菜单";
        }
        var index = layui.layer.open({
            title: tit,
            type: 2,
            anim: 1,
            area: ['600px', '80%'],
            content: "/Menu/AddOrModify/",
            success: function (layero, index) {
                var body = layui.layer.getChildFrame('body', index);
                if (edit) {
                    body.find("#Id").val(edit.Id);
                    body.find(".Name").val(edit.Name);
                    body.find(".DisplayName").val(edit.DisplayName);
                    body.find(".IconUrl").val(edit.IconUrl);
                    body.find(".LinkUrl").val(edit.LinkUrl);
                    body.find(".Sort").val(edit.Sort);
                    body.find(".DisplayName").val(edit.DisplayName);
                    if (edit.IsSystem === true) {
                        body.find(".IsSystem input[value=1]").prop("checked", "checked");
                    }
                    else {
                        body.find(".IsSystem input[value=0]").prop("checked", "checked");

                    }
                    if (edit.IsDisplay === true) {
                        body.find(".IsDisplay input[value=1]").prop("checked", "checked");
                    }
                    else {
                        body.find(".IsDisplay input[value=0]").prop("checked", "checked");

                    }
                    body.find(".ParentId").val(edit.ParentId);
                    body.find(".Remark").text(edit.Remark);    //角色备注
                    form.render();

                }
            }
        });
    }
    $(".addMenu_btn").click(function () {
        addMenu();
    });

    //批量删除
    $(".delAll_btn").click(function () {
        var checkStatus = table.checkStatus('menuListTable'),
            data = checkStatus.data,
            roleId = [];
        if (data.length > 0) {
            for (var i in data) {
                roleId.push(data[i].Id);
            }
            layer.confirm('确定删除选中的菜单？', { icon: 3, title: '提示信息' }, function (index) {
                //获取防伪标记
                del(roleId);
            });
        } else {
            layer.msg("请选择需要删除的菜单");
        }
    });

    //列表操作
    table.on('tool(menuList)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        if (layEvent === 'edit') { //编辑
            addMenu(data);
        } else if (layEvent === 'del') { //删除
            layer.confirm('确定删除此菜单？', { icon: 3, title: '提示信息' }, function (index) {
                del(data.Id);
            });
        }
    });

    function del(menuId) {
        $.ajax({
            type: 'POST',
            url: '/Menu/Delete/',
            data: { menuId: menuId },
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

    form.on('switch(IsDisplay)', function (data) {
        var tipText = '确定显示当前菜单吗？';
        if (!data.elem.checked) {
            tipText = '确定关闭当前菜单吗？';
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
            changeDisplayStatus(data.value, data.elem.checked);
            layer.close(index);
        }, function (index) {
            data.elem.checked = !data.elem.checked;
            form.render();
            layer.close(index);
        });
    });

    function changeDisplayStatus(menuId, status) {
        $.ajax({
            type: 'POST',
            url: '/Menu/ChangeDisplayStatus/',
            data: { Id: menuId, Status: status },
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
