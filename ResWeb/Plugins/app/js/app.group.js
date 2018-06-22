var layer;
var submitFlag;
$(function () {
    layui.use('layer', function () {
        layer = layui.layer;
    });
    g_bundle();
    g_select();
    g_close();
    g_open();
    g_delete();
    g_create();
    g_edit();
    g_submit();
});
var g_create = function () {
    $("#create").click(function () {
        $("#op_title").html("添加分组");
        $("#submit").val("添加");
        submitFlag = 1;
        layerOpen();
    });
}
var g_edit = function () {
    $("#edit").click(function () {
        if ($("#chooseID").val() == "" || !$("#chooseID").val()) {
            layer.msg("请选择分组！");
        } else {
            $("#op_title").html("数据更新操作");
            $("#submit").val("更新");
            $("#groupid").val($("#chooseID").val());
            ajax("/Group/Edit", "GET", { id: $("#chooseID").val() }, "json", 2, g_edit_set);
            layerOpen();
        }
    });
}
var g_edit_set = function (data) {
    $.each(data, function (idx, obj) {
        $("#groupCode").val(obj.RGroupCode);
        $("#groupName").val(obj.RGroupName);
        $("#f_type").selectpicker('val', obj.RTypeID);
        $("#f_park").selectpicker('val', obj.ParkID);
    });
    submitFlag = 2;
}
var g_submit = function () {
    $("#submit").click(function () {
        var formdata = new FormData($("form").get(0));
        if (submitFlag == 1) {
            ajaxForm("/Group/Create", "POST", formdata, "html", 1);
        } else if (submitFlag == 2) {
            ajaxForm("/Group/Edit", "POST", formdata, "html", 1);
        } else return;
    });
}
var g_select = function () {
    $("#select").click(function () {
        ajax("/Group/Select", "GET", {
            typeId: $("#type").val(),
            parkId: !$("#park").val() ,
            status: !$("#status").val()
        }, "html", 4);

    });
}

var g_close = function () {
    $("#close").click(function () {
        if (!$("#chooseID").val() || $("#chooseID").val() != "") {
            ajax("/Group/CloseOrOpen", "GET", { id: $("#chooseID").val(), type: 1 }, "html", 1);
        } else {
            layer.msg("请选择分组！");
        }

    });
}
var g_open = function () {
    $("#open").click(function () {
        if (!$("#chooseID").val() || $("#chooseID").val() != "") {
            ajax("/Group/CloseOrOpen", "GET", { id: $("#chooseID").val(), type: 2 }, "html", 1);
        } else {
            layer.msg("请选择分组！");
        }
    });
}
var g_delete = function () {
    $("#delete").click(function () {
        if (!$("#chooseID").val() || $("#chooseID").val() != "") {
            ajax("/Group/Delete", "GET", { id: $("#chooseID").val() }, "html", 1);
        } else {
            layer.msg("请选择分组！");
        }
    });
}
var layerOpen = function () {
    layer.open({
        type: 1,            //页面层
        closeBtn: 2,
        content: $(".groupdiv"),
        area: ["400px"],
        title: false,
        scrollbar: false,
        // resize: false,
        end: function () {
            $("#g_form").get(0).reset();
            $("#g_form select.selectpicker").selectpicker("refresh");
            // $("select.selectpicker").selectpicker("render");
        }
    });
}
var ajax = function (reqUrl, reqType, reqData, reqDataType, commitType,func) {
    $.ajax({
        type: reqType,
        url: reqUrl,
        data: reqData,
        dataType: reqDataType,
        async: false,
        beforeSend: function (XMLHttpRequest) {
            loadIndex = layer.load(0, { shade: 0.1 });
        },
        complete: function (XMLHttpRequest, textStatus) {
            layer.close(loadIndex);
        },
        success: function (responseData) {
            if (commitType == 1) {
                layer.alert(responseData.split(':')[1], { icon: responseData.split(':')[0], }, function () {
                    ajax("/Group/Details", "GET", {}, "html", 4);
                    parent.layer.closeAll();
                });
            } else if (commitType == 2) {
                func(responseData);
            } else {
                $(".data-action").html(responseData);
                g_bundle();
                layer.msg("刷新成功！");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            layer.msg("通讯故障，请求失败！");
        }
    });
};
var ajaxForm = function (reqUrl, reqType, reqData, reqDataType) {
    $.ajax({
        type: reqType,
        url: reqUrl,
        data: reqData,
        dataType: reqDataType,
        async: false,
        processData: false,
        contentType: false,
        beforeSend: function (XMLHttpRequest) {
            loadIndex = layer.load(0, { shade: 0.1 });
        },
        complete: function (XMLHttpRequest, textStatus) {
            layer.close(loadIndex);
        },
        success: function (responseData) {
            layer.alert(responseData.split(':')[1], { icon: responseData.split(':')[0], }, function () {
                ajax("/Group/Details", "GET", {}, "html", 4);
                parent.layer.closeAll();
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            layer.msg("通讯故障，请求失败！");
        }
    });
};
var g_bundle = function () {
    $(".table tr").click(function () {
        $(".table tr").each(function () {
            $(this).removeClass("active");
        });
        $("#chooseID").val($(this).find("td").eq(1).html());
        $(this).addClass("active");
        var text = $(this).find("td").find("span").eq(0);
        if (text.hasClass("status-disable")) {
            $("#open").show();
            $("#close").hide();
        } else {
            $("#open").hide();
            $("#close").show();
        }
    });
};