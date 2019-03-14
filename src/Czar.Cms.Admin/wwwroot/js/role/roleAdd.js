
layui.config({
    base: "/js/"
}).extend({
    "authtree": "authtree"
});
layui.use(['form', 'layer', 'authtree'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery, authtree = layui.authtree;

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
                MenuIds: authtree.getChecked('#yilezhu-auth-tree'),
                Remark: $(".Remark").val()  //用户简介
            },
            dataType: "json",
            headers: {
                "X-CSRF-TOKEN-yilezhu": $("input[name='AntiforgeryKey_yilezhu']").val()
            },
            success: function (res) {//res为相应体,function为回调函数
                var alertIndex;
                if (res.ResultCode === 0) {
                    alertIndex = layer.alert(res.ResultMsg, { icon: 1 }, function () {
                        layer.closeAll("iframe");
                        //刷新父页面
                        parent.location.reload();
                        top.layer.close(alertIndex);

                    });
                    //$("#res").click();//调用重置按钮将表单数据清空
                } else if (res.ResultCode === 102) {
                   alertIndex=layer.alert(res.ResultMsg, { icon: 5 }, function () {
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

    // 初始化
    $.ajax({
        url: '/Menu/LoadDataWithParentId/',
        dataType: 'json',
        success: function (data) {
            // 渲染时传入渲染目标ID，树形结构数据（具体结构看样例，checked表示默认选中），以及input表单的名字
            var trees = authtree.listConvert(data, {
                primaryKey: 'Id'
                , startPid: 0
                , parentKey: 'ParentId'
                , nameKey: 'DisplayName'
                , valueKey: 'Id'
                , checkedKey: strToIntArr($('#MenuIdsInit').val())
            });
            authtree.render('#yilezhu-auth-tree', trees, {
                inputname: 'ids[]'
                , layfilter: 'yilezhu-check-auth'
                , autowidth: true
            });

            authtree.on('change(yilezhu-check-auth)', function (data) {
                console.log('监听 authtree 触发事件数据', data);
            });
            authtree.on('dblclick(yilezhu-check-auth)', function (data) {
                console.log('监听到双击事件', data);
            });
        },
        error: function (xml, errstr, err) {
            layer.alert(errstr + '，系统异常！');
        }
    });

    function strToIntArr(str) {
        if (str) {
            var strArr = str.split(',');
            var dataIntArr = [];//保存转换后的整型字符串
            //方法一
            strArr.forEach(function (data, index, arr) {
                dataIntArr.push(+data);
            });
            return dataIntArr;
        }
    }
});