layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;

    //角色列表
    var tableIns = table.render({
        elem: '#articleList',
        url: '/Article/LoadData/',
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 10,
        id: "articleListTable",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: 'Id', width: 50, align: "center" },
            { field: 'Title', title: '标题', minWidth: 50, align: "center" },
            { field: 'Content', title: '内容', minWidth: 50, align: "center" },
            { field: 'Author', title: '作者', minWidth: 100, align: "center" },
            { field: 'AddTime', title: '发布时间', minWidth: 80, align: 'center' },
            { field: 'ViewCount', title: '浏览次数', align: 'center' },
            { field: 'Sort', title: '排序', minWidth: 80, align: "center" },
            { field: 'IsPublish', title: '是否发布', minWidth: 100, fixed: "right", align: "center", templet: '#IsPublish' },
            { title: '操作', minWidth: 80, templet: '#articleListBar', fixed: "right", align: "center" }
        ]]
    });

    //搜索【此功能需要后台配合，所以暂时没有动态效果演示】
    $(".search_btn").on("click", function () {
        if ($(".searchVal").val() !== '') {
            table.reload("articleListTable", {
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

    //添加文章
    function addArticle(edit) {
        var tit = "添加文章";
        if (edit) {
            tit = "编辑文章";
        }
        var index = layui.layer.open({
            title: tit,
            type: 2,
            anim: 1,
            content: "/Article/AddOrModify/",
            success: function (layero, index) {
                var body = layui.layer.getChildFrame('body', index);
                if (edit) {
                    body.find("#Id").val(edit.Id);
                    //body.find(".UserName").val(edit.UserName);
                    //body.find(".NickName").val(edit.NickName);
                    //body.find(".RoleId").val(edit.RoleId);
                    //body.find(".Mobile").val(edit.Mobile);
                    //body.find(".Email").val(edit.Email);
                    //body.find("input:checkbox[name='IsLock']").prop("checked", edit.IsLock);
                    //body.find(".Remark").text(edit.Remark);
                    form.render();
                }
            }
        });
        layui.layer.full(index);
        //改变窗口大小时，重置弹窗的宽高，防止超出可视区域（如F12调出debug的操作）
        $(window).on("resize", function () {
            layui.layer.full(index);
        })
    }
    $(".addArticle_btn").click(function () {
        addArticle();
    });

    //批量删除
    $(".delAll_btn").click(function () {
        var checkStatus = table.checkStatus('articleListTable'),
            data = checkStatus.data,
            Ids = [];
        if (data.length > 0) {
            for (var i in data) {
                Ids.push(data[i].Id);
            }
            layer.confirm('确定删除选中的文章？', { icon: 3, title: '提示信息' }, function (index) {
                del(Ids);
            });
        } else {
            layer.msg("请选择需要删除的文章");
        }
    });

    //列表操作
    table.on('tool(articleList)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        if (layEvent === 'edit') { //编辑
            addArticle(data);
        } else if (layEvent === 'del') { //删除
            layer.confirm('确定删除此文章？', { icon: 3, title: '提示信息' }, function (index) {
                del(data.Id);
            });
        }
    });

    form.on('switch(IsPublish)', function (data) {
        var tipText = '确定锁定当前文章吗？';
        if (!data.elem.checked) {
            tipText = '确定启用当前文章吗？';
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
            changePublishStatus(data.value, data.elem.checked);
            layer.close(index);
        }, function (index) {
            data.elem.checked = !data.elem.checked;
            form.render();
            layer.close(index);
        });
    });

    function del(Ids) {
        $.ajax({
            type: 'POST',
            url: '/Article/Delete/',
            data: { Ids: Ids },
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

    function changePublishStatus(id,status) {
        $.ajax({
            type: 'POST',
            url: '/Article/ChangePublishStatus/',
            data: { Id: id, Status: status },
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
