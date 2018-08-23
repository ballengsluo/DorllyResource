$(function() {
    // layui.use('layer', function() {
    //     layer = layui.layer;
    // });
    search({ pageIndex: currentNum, pageSize: limitCount });
    operation();
    dropChange(1);
});

function operation() {
    //查询
    $('#search').click(function() {
        search(paramsSet());
    });
    //添加
    $('#add').click(function() {
        var url = $(this).attr('data-url');
        layerOpen(url);
    });
    //编辑
    $('#edit').click(function() {
        if (!id || id == "") { layer.msg('请选择数据！'); return false; }
        var url = $(this).attr('data-url') + '?id=' + id;
        layerOpen(url);
    });
    //编辑
    $('#check').click(function() {
        if (!id || id == "") { layer.msg('请选择数据！'); return false; }
        var url = $(this).attr('data-url') + '?id=' + id;
        layerOpen(url);
    });

    //重置密码
    $('#reset').click(function() {
        // mission("是否重置密码？", $(this));
        if (!id || id == "") { layer.msg("请选择数据！"); return false; }
        var url = $(this).attr('data-url') + "?id=" + id;
        layer.confirm("是否重置密码？", function() {
            layerOpen(url);
        });
    });
    //绑定角色权限
    $('#func').click(function() {
        if (!id || id == "") { layer.msg("请选择数据！"); return false; }
        var url = $(this).attr('data-url') + "?id=" + id;
        layerOpen(url);
    });
    //删除
    $('#del').click(function() {
        mission("最高级别警告，一旦删除数据将不可恢复，是否继续？", $(this));
    });
    //启用
    $('#open').click(function() {
        mission("温馨提示，是否启用？", $(this));
    });
    //停用
    $('#close').click(function() {
        mission("温馨提示，是否停用？", $(this));
    });

    //审核通过
    $('#pass').click(function() {
        mission("温馨提示，是否审核通过？", $(this));
    });
    //取消审核
    $('#notpass').click(function() {
        mission("温馨提示，是否审核不通过？", $(this));
    });
    //绑定角色
    $('#role').click(function() {
        var url = $(this).attr('data-url') + "?id=" + id;
        layerOpen(url);
    });
    //发布
    $('#pub').click(function() {
        mission("郑重提醒,是否发布此资源？", $(this));
    });
    //取消发布
    $('#unpub').click(function() {
        mission("严重警告,是否下架此资源？", $(this));
    });
    //作废
    $('#off').click(function() {
        mission("严厉警告,作废将不可使用,是否继续？", $(this));
    });
};

function pub() {
    //发布
    $('.public').click(function() {
        parent.layer.closeAll();
        var url = $(this).attr('data-url') + "?id=" + $(this).parents("tr").find(".key").html()
        parent.layerOpen(url);
    });
}

function mission(msg, obj) {
    if (!id || id == "") { layer.msg("请选择数据！"); return false; }
    layer.confirm(msg, function() {
        // var url = $(this).attr('data-url');
        var url = obj.attr('data-url');
        ajax("post", url, { id: id }, 'json', function(data) {
            layer.msg(data.msg);
            if (data.result == 1) { search(paramsSet()); }
        });
    });
}

function layerOpen(url) {
    var index = layer.open({
        type: 2,
        content: url,
        title: ' ',
        end: function() {
            if (refresh == true) {
                refresh = false;
                search(paramsSet());
            }
        }
    });
    layer.full(index);
}


function search(params) {
    ajax("get", $('#search').attr('data-url'), params, "json", function(data) {
        id = "";
        totalcount = data.count;
        var html = template('tableTpl', data);
        $("#tb").html(html);
        if ($('.public').get(0) != '{}' && typeof($('.public').get(0)) != "undefined") {
            pub();
        }
        table();
        page();
    });
};

currentNum = 1;
limitCount = 20;

function page() {
    layui.use('laypage', function() {
        laypage = layui.laypage;
        laypage.render({
            elem: 'page',
            count: totalcount,
            curr: currentNum,
            limit: limitCount,
            groups: 12,
            layout: ['count', 'prev', 'page', 'next', 'limit', 'skip'],
            jump: function(obj, first) {
                if (!first) {
                    currentNum = obj.curr;
                    limitCount = obj.limit;
                    search(paramsSet());
                }
            }
        })
    });
};

function table() {
    $(".table tr").click(function() {
        $(".table tr").removeClass("success");
        $(this).addClass("success");
        id = $(this).find(".key").html();
        showenable($(this).find(".enable").attr("data-enable"));
        // showstatus($(this).find(".status").attr("data-status"));
    });
}

// function showstatus(value) {
//     if (value == 1) { //待上架 待审核
//         $('#pass').prop("disabled", false);
//         $('#notpass').prop("disabled", true);
//         $('#pub').prop("disabled", false);
//         $('#unpub').prop("disabled", true);
//         $("#edit").prop("disabled", false);
//         $("#off").prop("disabled", false);
//         $('#del').prop("disabled", false);
//     } else if (value == 2) { //审核通过
//         $('#pub').show();
//         $('#unpub').show();
//         $('#pass').hide();
//         $('#notpass').hide();
//         $("#edit").show();
//         $("#off").show();
//     } else if (value == 3) { //审核不通过
//         $('#pub').hide();
//         $('#unpub').hide();
//         $('#pass').prop("disabled", false);
//         $('#notpass').prop("disabled", false);
//         $("#edit").show();
//         $("#off").show();
//     } else if (value == 4) { //上架
//         $('#pub').prop("disabled", true);
//         $('#unpub').prop("disabled", false);
//         $("#edit").prop("disabled", true);
//         $("#off").prop("disabled", true);
//         $('#del').prop("disabled", true);
//     } else if (value == 5) { //下架
//         $('#pub').prop("disabled", false);
//         $('#unpub').prop("disabled", true);
//         $("#edit").prop("disabled", false);
//         $("#off").prop("disabled", false);
//         $('#del').prop("disabled", false);
//     } else if (value == 6) { //作废
//         $('#del').prop("disabled", false);
//         $('#pub').prop("disabled", true);
//         $('#unpub').prop("disabled", true);
//         $("#edit").prop("disabled", true);
//         $("#off").prop("disabled", true);
//     }
// }

function showstatusbak(value) {
    if (value == 1) { //待审核
        $('#pub').show();
        $('#unpub').hide();
        $('#pass').show();
        $('#notpass').show();
        $("#edit").show();
        $("#off").show();
    } else if (value == 2) { //审核通过
        $('#pub').show();
        $('#unpub').show();
        $('#pass').hide();
        $('#notpass').hide();
        $("#edit").show();
        $("#off").show();
    } else if (value == 3) { //审核不通过
        $('#pub').hide();
        $('#unpub').hide();
        $('#pass').hide();
        $('#notpass').hide();
        $("#edit").show();
        $("#off").show();
    } else if (value == 4) { //上架
        $('#pass').hide();
        $('#notpass').hide();
        $('#pub').hide();
        $('#unpub').show();
        $("#edit").show();
        $("#off").show();
    } else if (value == 5) { //下架
        $('#pass').hide();
        $('#notpass').hide();
        $('#pub').show();
        $('#unpub').hide();
        $("#edit").show();
        $("#off").show();
    } else if (value == 6) { //下架
        $('#pass').hide();
        $('#notpass').hide();
        $('#pub').hide();
        $('#unpub').hide();
        $("#edit").hide();
        $("#off").hide();
    }
}

function showenable(value) {
    if (value == "true") { //启用
        $('#open').hide();
        $('#close').show();


    } else if (value == "false") { //停用
        $('#open').show();
        $('#close').hide();
    }
}