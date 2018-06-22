var layer;
$(function () {
    layui.use('layer', function () {
        layer = layui.layer;
    });
    r_bundle();//绑定表格动作
    r_create();
    r_update();
    r_delete();
    r_commit();
});

var r_bundle = function () {
    $(".table tr").click(function () {
        $(".table tr").each(function () {
            $(this).removeClass("active");
        });
        $("#chooseID").val($(this).find("td").eq(1).html());
        $(this).addClass("active");
    });
};

var r_create = function () {
    $("#r_create").click(function () {
        $("#r_commit").attr("data-role", "create");
        r_operate(1);
    });
}
var r_update = function () {
    $("#r_update").click(function () {
        $("#r_commit").attr("data-role", "update");
        r_operate(2);
    });
}
var r_delete = function () {
    $("#r_delete").click(function () {
        if ($("#chooseID").val() == "") {
            layer.msg("请选择角色！");
        } else {
            layer.confirm('是否删除', { btn: ['确认', '取消'] }, function () {
                ajaxRole("/Role/Delete", "POST", { id: $("#chooseID").val() }, "html", 1);
            });

        }
    });
}
var r_commit = function () {
    $("#r_commit").click(function () {
        if ($("#r_commit").attr("data-role") == "create") {
            ajaxRole("/Role/Create", "POST", { roleName: $("#r_name").val(), roleDesc: $("#r_desc").val() }, "html", 1);
        } else {
            ajaxRole("/Role/Edit", "POST", { id: $("#chooseID").val(), roleName: $("#r_name").val(), roleDesc: $("#r_desc").val() }, "html", 1);
        }
    });
}
var r_operate = function (type) {
    if (type == 1) {
        $("#r_title").html("添加角色");
        $("#r_commit").val("提交");
        layerOpen();
    } else if (type == 2) {
        console.log($("#chooseID").val());
        if ($("#chooseID").val() == "" || !$("#chooseID").val()) {
            layer.msg("请选择角色！");
        } else {
            $("#r_title").html("更新角色");
            $("#r_commit").val("更新");

            ajaxRole("/Role/Edit", "GET", { id: $("#chooseID").val() }, "json", 2);
            layerOpen();
        }

    } else return;
}
var layerOpen = function () {
    layer.open({
        type: 1,            //页面层
        closeBtn: 2,
        content: $(".rolediv"),
        area: ["390px", "230"],
        title: false,
        scrollbar: false,
        resize: false,
        success: function () {
            // calcRoleDiv();
        },
        end: function () {
            $("#r_form").get(0).reset();
        }
    });
}
var ajaxRole = function (reqUrl, reqType, reqData, resData, r_type) {
    $.ajax({
        type: reqType,
        url: reqUrl,
        data: reqData,
        dataType: resData,
        async: false,
        beforeSend: function (XMLHttpRequest) {
            loadIndex = layer.load(0, { shade: 0.1 });
        },
        complete: function (XMLHttpRequest, textStatus) {
            layer.close(loadIndex);
        },
        success: function (responseData) {
            if (r_type == 1) {
                layer.alert(responseData.split(':')[1], { icon: responseData.split(':')[0], }, function () {
                    parent.layer.closeAll();
                    $("#r_form").get(0).reset();
                    ajaxRole("/Role/Details", "GET", {}, "html", 4);
                });
            } else if (r_type == 2) {
                console.log(responseData);
                $.each(responseData, function (idx, item) {
                    $("#r_name").val(item.RoleName);
                    $("#r_desc").val(item.RoleDesc);
                });

            } else {
                $(".data-action").html(responseData);
                r_bundleData();
                layer.msg("刷新成功！");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            layer.msg("通讯故障，请求失败！");
            console.log("请求失败！");
        }
    });
};