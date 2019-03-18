layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;

    //任务列表
    var tableIns = table.render({
        elem: '#taskList',
        url: '/TaskInfo/LoadData/',
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 10,
        id: "taskTable",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: 'Id', minWidth: 30, align: "center" },
            { field: 'Name', title: '名称', minWidth: 50, align: "center" },
            { field: 'Group', title: '分组', minWidth: 50, align: "center" },
            { field: 'Cron', title: 'Cron', minWidth: 80, align: "center" },
            { field: 'Status', title: '状态', minWidth: 100, fixed: "right", align: "center", templet: '#IsRun' },
            { title: '操作', minWidth: 80, templet: '#taskBar', fixed: "right", align: "center" }
        ]]
    });

    $(".search_btn").on("click", function () {
        if ($(".searchVal").val() !== '') {
            table.reload("taskTable", {
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

    //添加任务
    function addTask(edit) {
        var tit = "添加任务";
        if (edit) {
            tit = "编辑任务";
        }
        var index = layui.layer.open({
            title: tit,
            type: 2,
            anim: 1,
            area: ['600px', '80%'],
            content: "/TaskInfo/AddOrModify/",
            success: function (layero, index) {
                var body = layui.layer.getChildFrame('body', index);
                if (edit) {
                    body.find("#Id").val(edit.Id);
                    body.find(".Name").val(edit.Name);
                    body.find(".Group").val(edit.Group);
                    body.find(".Assembly").val(edit.Assembly);
                    body.find(".ClassName").val(edit.ClassName);
                    body.find(".Cron").val(edit.Cron);
                    body.find(".StartTime").val(edit.StartTime);
                    body.find(".EndTime").val(edit.EndTime);
                    body.find(".Status").val(edit.Status);
                    body.find(".Description").text(edit.Description);
                    form.render();

                }
            }
        });
    }
    $(".addTask_btn").click(function () {
        addTask();
    });


    //列表操作
    table.on('tool(taskList)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        if (layEvent === 'edit') { //编辑
            addTask(data);
        } else if (layEvent === 'del') { //删除
            layer.confirm('确定删除此任务？', { icon: 3, title: '提示信息' }, function (index) {
                del(data.Id);
            });
        }
    });

    //批量暂停
    $(".stopAll_btn").click(function () {
        var checkStatus = table.checkStatus('taskTable'),
            data = checkStatus.data,
            ids = [];
        if (data.length > 0) {
            for (var i in data) {
                ids.push(data[i].Id);
            }
            layer.confirm('确定停止选中的任务吗？', { icon: 3, title: '提示信息' }, function (index) {
                //获取防伪标记
                goStop(ids);
            });
        } else {
            layer.msg("请选择需要停止的任务");
        }
    });

    function goStop(ids) {
        $.ajax({
            type: 'POST',
            url: '/TaskInfo/Stop/',
            data: { Ids: ids },
            dataType: "json",
            headers: {
                "X-CSRF-TOKEN-yilezhu": $("input[name='AntiforgeryKey_yilezhu']").val()
            },
            success: function (data) {//res为相应体,function为回调函数
                layer.msg(data.ResultMsg, {
                    time: 2000 //20s后自动关闭
                }, function () {
                    tableIns.reload();
                    });
                layer.close(index);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
                layer.close(index);
            }
        });
    }

    //批量启动
    $(".startAll_btn").click(function () {
        var checkStatus = table.checkStatus('taskTable'),
            data = checkStatus.data,
            ids = [];
        if (data.length > 0) {
            for (var i in data) {
                ids.push(data[i].Id);
            }
            layer.confirm('确定启动选中的任务吗？', { icon: 3, title: '提示信息' }, function (index) {
                //获取防伪标记
                goStart(ids);

            });
        } else {
            layer.msg("请选择需要启动的任务");
        }
    });

    function goStart(ids) {
        $.ajax({
            type: 'POST',
            url: '/TaskInfo/Start/',
            data: { Ids: ids },
            dataType: "json",
            headers: {
                "X-CSRF-TOKEN-yilezhu": $("input[name='AntiforgeryKey_yilezhu']").val()
            },
            success: function (data) {//res为相应体,function为回调函数
                layer.msg(data.ResultMsg, {
                    time: 2000 //20s后自动关闭
                }, function () {
                    tableIns.reload();
                    });
                layer.close(index);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
                layer.close(index);
            }
        });
    }

    function del(id) {
        $.ajax({
            type: 'POST',
            url: '/TaskInfo/Delete/',
            data: { Id: id },
            dataType: "json",
            headers: {
                "X-CSRF-TOKEN-yilezhu": $("input[name='AntiforgeryKey_yilezhu']").val()
            },
            success: function (data) {//res为相应体,function为回调函数
                layer.msg(data.ResultMsg, {
                    time: 2000 //20s后自动关闭
                }, function () {
                    tableIns.reload();
                    });
                layer.close(index);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
                layer.close(index);
            }
        });
    }

    form.on('switch(IsRun)', function (data) {
        var tipText = '确定启动当前任务吗？';
        if (!data.elem.checked) {
            tipText = '确定停止当前任务吗？';
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
            changeStatus(data.value, data.elem.checked);
            layer.close(index);
        }, function (index) {
            data.elem.checked = !data.elem.checked;
            form.render();
            layer.close(index);
        });
    });

    function changeStatus(id, status) {
        $.ajax({
            type: 'POST',
            url: '/TaskInfo/ChangeStatus/',
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
