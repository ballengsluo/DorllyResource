var layer
$(function () {
    layui.use('layer', function () {
        layer = layui.layer;
    });
    table_bund();
});
var ajax = function (reqUrl, reqType, reqData, resType, dealType, callback) {
    $.ajax({
        type: reqType,
        url: reqUrl,
        data: reqData,
        dataType: resType,
        async: true,
        beforeSend: function (XMLHttpRequest) {
            loadIndex = layer.load(0, { shade: 0.1 });
        },
        complete: function (XMLHttpRequest, textStatus) {
            layer.close(loadIndex);
        },
        success: function (responseData) {
            if (dealType == 1) {
                layer.alert(responseData.split(':')[1], { icon: responseData.split(':')[0], }, function () {
                    parent.layer.closeAll();
                    callback();
                });
            } else if (dealType == 2) {
                callback(responseData);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            layer.msg("请求失败！");
        }
    });
};
var ajax_form = function (reqUrl, reqType, reqData, resType, callback) {
    $.ajax({
        type: reqType,
        url: reqUrl,
        data: reqData,
        dataType: resType,
        async: true,
        processData: false,
        contentType: false,
        beforeSend: function (XMLHttpRequest) {
            loadIndex = layer.load(0, { shade: 0.1 });
        },
        complete: function (XMLHttpRequest, textStatus) {
            layer.close(loadIndex);
        },
        success: function (data) {
            callback(data)
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            layer.msg("请求失败！");
        }
    });
};
var tr_select_id;
var table_bund = function () {
    $(".table tr").click(function () {
        $(".table tr").each(function () {
            $(this).removeClass("active");
        });
        $(this).addClass("active");
        // console.log($(this).find(".pkey").eq(0).html());
        tr_select_id = $(this).find(".pkey").eq(0).html();

    });
}