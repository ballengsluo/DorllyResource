$(function() {
    layui.use('layer', function() {
        layer = layui.layer;
    });
    // if ($("#park").length > 0) {
    //     $("#park option:first").attr("selected", true);
    // }
});

function ajax(reqtype, requrl, reqdata, datatype, callback) {
    $.ajax({
        type: reqtype,
        url: requrl,
        data: reqdata,
        dataType: datatype,
        success: function(data) {
            callback(data);
        },
        error: function(xhr, ts, et) {
            cmtFailed(xhr, ts, et);
        }
    });
}

function ajaxForm(reqtype, requrl, reqdata, datatype, callback) {
    $.ajax({
        type: reqtype,
        url: requrl,
        data: reqdata,
        dataType: datatype,
        processData: false,
        contentType: false,
        success: function(data) {
            callback(data);
        },
        error: function(xhr, ts, et) {
            cmtFailed(xhr, ts, et);
        }
    });
}

function ajaxForm(reqtype, requrl, reqdata, datatype, callback) {
    $.ajax({
        type: reqtype,
        url: requrl,
        data: reqdata,
        dataType: datatype,
        processData: false,
        contentType: false,
        success: function(data) {
            callback(data);
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            cmtFailed(XMLHttpRequest, textStatus, errorThrown);
        }
    });
}

var refresh = false;

function cmtSuccess(data) {
    layer.msg(data.Msg)
    if (data.Flag == 1) {
        window.parent.refresh = true;
        setTimeout(function() { window.parent.layer.closeAll() }, 1000);
    }
}

function cmtFailed(xhr, ts, et) {
    if (ts === 401) {
        layer.msg("登录失效，请重新登录！");
    } else {
        layer.msg("请求失败！");
    }
}

function dropChange(model) {

    $("#city").change(function() {
        if ($("#region")) {
            $("#region").html("").selectpicker("refresh");
            $("#park").html("").selectpicker("refresh");
            $("#stage").html("").selectpicker("refresh");
            $("#build").html("").selectpicker("refresh");
            $("#floor").html("").selectpicker("refresh");
            $("#room").html("").selectpicker("refresh");
            if ($(this).val()) {
                $.getJSON("/Drop/RegionDropList", { pid: $(this).val() }, function(data) {
                    console.log(data);
                    var html = "";
                    if (model == 1) html += "<option value=''>全部</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                    }
                    $("#region").html(html).selectpicker("refresh");
                });
            }
        }
    });
    $("#region").change(function() {
        if ($("#park")) {
            $("#park").html("").selectpicker("refresh");
            $("#stage").html("").selectpicker("refresh");
            $("#build").html("").selectpicker("refresh");
            $("#floor").html("").selectpicker("refresh");
            $("#room").html("").selectpicker("refresh");
            if ($(this).val()) {
                $.getJSON("/Drop/ParkDropList", { pid: $(this).val() }, function(data) {
                    console.log(data);
                    var html = "";
                    if (model == 1) html += "<option value=''>全部</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                    }
                    $("#park").html(html).selectpicker("refresh");
                });
            }
        }
    });
    $("#park").change(function() {
        if ($("#stage")) {
            $("#stage").html("").selectpicker("refresh");
            $("#build").html("").selectpicker("refresh");
            $("#floor").html("").selectpicker("refresh");
            $("#room").html("").selectpicker("refresh");
            if ($(this).val()) {
                $.getJSON("/Drop/StageDropList", { pid: $(this).val() }, function(data) {
                    console.log(data);
                    var html = "";
                    if (model == 1) html += "<option value=''>全部</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                    }
                    $("#stage").html(html).selectpicker("refresh");
                });
            }
        }
    });
    $("#stage").change(function() {
        if ($("#build")) {
            $("#build").html("").selectpicker("refresh");
            $("#floor").html("").selectpicker("refresh");
            $("#room").html("").selectpicker("refresh");
            if ($(this).val()) {
                $.getJSON("/Drop/BuildingDropList", { pid: $(this).val() }, function(data) {
                    console.log(data);
                    var html = "";
                    if (model == 1) html += "<option value=''>全部</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                    }
                    $("#build").html(html).selectpicker("refresh");
                });
            }
        }
    });
    $("#build").change(function() {
        if ($("#floor")) {
            $("#floor").html("").selectpicker("refresh");
            $("#room").html("").selectpicker("refresh");
            if ($(this).val()) {
                $.getJSON("/Drop/FloorDropList", { pid: $(this).val() }, function(data) {
                    console.log(data);
                    var html = "";
                    if (model == 1) html += "<option value=''>全部</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                    }
                    $("#floor").html(html).selectpicker("refresh");
                });
            }
        }
    });
    $("#floor").change(function() {
        if ($("#room")) {
            $("#room").html("").selectpicker("refresh");
            if ($(this).val()) {
                $.getJSON("/Drop/RMDropList", { pid: $(this).val() }, function(data) {
                    console.log(data);
                    var html = "";
                    if (model == 1) html += "<option value=''>全部</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                    }
                    $("#room").html(html).selectpicker("refresh");
                });
            }
        }
    });
    $("#kind").change(function() {
        if ($("#type")) {
            $("#type").html("").selectpicker("refresh");
            if ($(this).val()) {
                $.getJSON("/Drop/ResourceTypeDropList", { kind: $(this).val() }, function(data) {
                    console.log(data);
                    var html = "";
                    if (model == 1) html += "<option value=''>全部</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                    }
                    $("#type").html(html).selectpicker("refresh");
                });
            }
        }
        if ($("#group")) {
            $("#group").html("").selectpicker("refresh");
            if ($(this).val()) {
                $.getJSON("/Drop/GroupDropList", { kind: $(this).val() }, function(data) {
                    console.log(data);
                    var html = "";
                    if (model == 1) html += "<option value=''>全部</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                    }
                    $("#group").html(html).selectpicker("refresh");
                });
            }
        }
    });
}

function dropChangeBak(model) {

    $("#city").change(function() {
        if (!$("#region")) { return false; }
        $("#region").html("").selectpicker("refresh");
        $("#park").html("").selectpicker("refresh");
        $("#stage").html("").selectpicker("refresh");
        $("build").html("").selectpicker("refresh");
        $("#floor").html("").selectpicker("refresh");
        $("#room").html("").selectpicker("refresh");
        if (!$(this).val() || $(this).val() == "") {
            return false;
        } else {
            $.getJSON("/A_Region/GetList", { pid: $(this).val() }, function(data) {
                console.log(data);
                var html = "";
                if (model == 1) html += "<option value=''>全部</option>";
                for (var i = 0; i < data.length; i++) {
                    html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                }
                $("#region").html(html).selectpicker("refresh");
            });
        }
    });
    $("#region").change(function() {
        if (!$("#park")) { return false; }
        $("#park").html("").selectpicker("refresh");
        $("#stage").html("").selectpicker("refresh");
        $("build").html("").selectpicker("refresh");
        $("#floor").html("").selectpicker("refresh");
        $("#room").html("").selectpicker("refresh");
        if (!$(this).val() || $(this).val() == "") {
            return false;
        } else {
            $.getJSON("/A_Park/GetList", { pid: $(this).val() }, function(data) {
                console.log(data);
                var html = "";
                if (model == 1) html += "<option value=''>全部</option>";
                for (var i = 0; i < data.length; i++) {
                    html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                }
                $("#park").html(html).selectpicker("refresh");
            });
        }
    });
    $("#park").change(function() {
        if (!$("#stage")) { return false; }
        $("#stage").html("").selectpicker("refresh");
        $("build").html("").selectpicker("refresh");
        $("#floor").html("").selectpicker("refresh");
        $("#room").html("").selectpicker("refresh");
        if (!$(this).val() || $(this).val() == "") {
            return false;
        } else {
            $.getJSON("/A_Stage/GetList", { pid: $(this).val() }, function(data) {
                console.log(data);
                var html = "";
                if (model == 1) html += "<option value=''>全部</option>";
                for (var i = 0; i < data.length; i++) {
                    html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                }
                $("#stage").html(html).selectpicker("refresh");
            });
        }
    });
    $("#stage").change(function() {
        if (!$("build")) { return false; }
        $("build").html("").selectpicker("refresh");
        $("#floor").html("").selectpicker("refresh");
        $("#room").html("").selectpicker("refresh");
        if (!$(this).val() || $(this).val() == "") {
            return false;
        } else {
            $.getJSON("/A_Building/GetList", { pid: $(this).val() }, function(data) {
                console.log(data);
                var html = "";
                if (model == 1) html += "<option value=''>全部</option>";
                for (var i = 0; i < data.length; i++) {
                    html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                }
                $("build").html(html).selectpicker("refresh");
            });
        }
    });
    $("build").change(function() {
        if (!$("#floor")) { return false; }
        $("#floor").html("").selectpicker("refresh");
        $("#room").html("").selectpicker("refresh");
        if (!$(this).val() || $(this).val() == "") {
            return false;
        } else {
            $.getJSON("/A_Floor/GetList", { pid: $(this).val() }, function(data) {
                console.log(data);
                var html = "";
                if (model == 1) html += "<option value=''>全部</option>";
                for (var i = 0; i < data.length; i++) {
                    html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                }
                $("#floor").html(html).selectpicker("refresh");
            });
        }
    });
    $("#floor").change(function() {
        if (!$("#room")) { return false; }
        $("#room").html("").selectpicker("refresh");
        if (!$(this).val() || $(this).val() == "") {
            return false;
        } else {
            $.getJSON("/R_RM/GetList", { pid: $(this).val() }, function(data) {
                console.log(data);
                var html = "";
                if (model == 1) html += "<option value=''>全部</option>";
                for (var i = 0; i < data.length; i++) {
                    html += "<option value='" + data[i].ID + "'>" + data[i].ID + "</option>";
                }
                $("#room").html(html).selectpicker("refresh");
            });
        }
    });

}