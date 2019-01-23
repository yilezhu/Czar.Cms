layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;

    //角色列表
    var tableIns = table.render({
        elem: '#roleList',
        url: '/ManagerRole/LoadData/',
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 10,
        id: "roleListTable",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: 'Id', width: 50, align: "center" },
            { field: 'RoleName', title: '角色名称', minWidth: 100, align: "center" },
            {
                field: 'RoleType', title: '角色类型', minWidth: 150, align: 'center', templet: function (d) {
                    if (d.RoleType === 1) {
                        return "超级管理员";
                    } else if (d.RoleType === 2) {
                        return "系统管理员";
                    } else {
                        return "未知";
                    }
                }
            },
            {
                field: 'IsSystem', title: '系统默认', minWidth: 100, align: 'center', templet: function (d) {
                    return d.IsSystem === true ? "是" : "否";
                }
            },
            { field: 'Remark', title: '备注', align: 'center' },
            { field: 'AddTime', title: '添加时间', align: 'center', minWidth: 150 },
            { title: '操作', minWidth: 175, templet: '#roleListBar', fixed: "right", align: "center" }
        ]]
    });

    //搜索【此功能需要后台配合，所以暂时没有动态效果演示】
    $(".search_btn").on("click", function () {
        if ($(".searchVal").val() !== '') {
            table.reload("roleListTable", {
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
    function addRole(edit) {
        var tit = "添加角色";
        if (edit) {
            tit = "编辑角色";
        }
        var index = layui.layer.open({
            title: tit,
            type: 2,
            anim: 1,
            area: ['600px', '70%'],
            content: "/ManagerRole/AddOrModify/" + edit.Id,
            success: function (layero, index) {
                var body = layui.layer.getChildFrame('body', index);
                if (edit) {
                    body.find("#Id").val(edit.Id);  //主键
                    body.find(".RoleName").val(edit.RoleName);  //角色名
                    body.find(".RoleType").val(edit.RoleType);  //会员等级
                    if (edit.IsSystem === true) {
                        body.find(".IsSystem input[value=1]").prop("checked", "checked");  //是否系统默认
                    }
                    else {
                        body.find(".IsSystem input[value=0]").prop("checked", "checked");   //是否系统默认

                    }
                    body.find(".Remark").text(edit.Remark);    //角色备注
                    form.render();

                }
            }
        });
    }
    $(".addRoles_btn").click(function () {
        addRole();
    });

    //批量删除
    $(".delAll_btn").click(function () {
        var checkStatus = table.checkStatus('roleListTable'),
            data = checkStatus.data,
            roleId = [];
        if (data.length > 0) {
            for (var i in data) {
                roleId.push(data[i].Id);
            }
            layer.confirm('确定删除选中的角色？', { icon: 3, title: '提示信息' }, function (index) {
                //获取防伪标记
                del(roleId);
            });
        } else {
            layer.msg("请选择需要删除的角色");
        }
    });

    //列表操作
    table.on('tool(roleList)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        if (layEvent === 'edit') { //编辑
            addRole(data);
        } else if (layEvent === 'del') { //删除
            layer.confirm('确定删除此角色？', { icon: 3, title: '提示信息' }, function (index) {
                del(data.Id);
            });
        }
    });

    function del(roleId) {
        $.ajax({
            type: 'POST',
            url: '/ManagerRole/Delete/',
            data: { roleId: roleId },
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

});
