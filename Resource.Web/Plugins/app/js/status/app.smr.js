$(function() {
    layui.use('laydate', function() {
        var laydate = layui.laydate;
        //执行一个laydate实例
        laydate.render({
            elem: '#stime' //指定元素
        });
    });
    $('#search').click(function() {
        search({ Park: $("#park").val(), stime: $("#stime").val() });
    });
    search({ Park: $("#park").val(), stime: $("#stime").val() });
})

function search(params) {
    ajax("get", $('#search').attr('data-url'), params, "json", function(data) {
        if (data.Flag == 1) {
            layer.msg("数据加载成功");
            $("tbody").html(data.table);
            $(".detailContainer").html(data.detail);
            hoverShow();
        } else {
            layer.msg("加载失败...");
        }
    });

};

function hoverShow() {
    setTimeout(function() {
        $("[data-status]").hover(function() {
            layer.tips($("[data-id='" + $(this).attr("data-pid") + "']").html(),
                "#" + $(this).attr("id"), {
                    time: 60000
                });
            $(".layui-layer-tips").width('220px');
        }, function() {
            layer.close(layer.index);
        });
    }, 1000)

}