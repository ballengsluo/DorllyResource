$(function() {
    layui.use('laydate', function() {
        var laydate = layui.laydate;
        //执行一个laydate实例
        laydate.render({
            elem: '#etime' //指定元素
        });
    });

    search({ Park: $("#park").val(), Etime: $("#etime").val() });
})

function search(params) {
    // var begin = Date.parse(new Date($("#etime").val() + " 09:30:00"));
    // var end = Date.parse(new Date($("#etime").val() + " 10:00:00"));
    // console.log(begin);
    // console.log(end - begin);
    // console.log(Math.round((end - begin) / (60 * 1000 * 30)));
    ajax("get", $('#search').attr('data-url'), params, "json", function(data) {
        // console.log(data);
        $("tbody").html(data.table);
        $(".detailContainer").html(data.detail);
    });
    hoverShow();
    // $("tbody").on("hover", "[data-status]", function() {
    //     alert("aa");
    // }, function() {
    //     alert("bb");
    // });
    // setTimeout(function() {
    //         $("[data-status]").hover(function() {
    //             alert("aaa");
    //             $(this).after("<td style='display:none;' id='" + $(this).attr("data-id") + "'></td>");
    //         }, function() {
    //             $("#" + $(this).attr("data-id")).remove();
    //         });
    //     }, 1000)
    // $("[data-status]").hover(function() {
    //     alert("aaa");
    //     $(this).after("<td style='display:none;' id='" + $(this).attr("data-id") + "'></td>");
    // }, function() {
    //     $("#" + $(this).attr("data-id")).remove();
    // });
};

function hoverShow() {
    setTimeout(function() {
        $("[data-status]").hover(function() {
            // $(this).after("<td style='display:none;' id='" + $(this).attr("data-id") + "'></td>");
            layer.tips($("[data-pid='" + $(this).attr("data-id") + "']").html(),
                "#" + $(this).attr("id"), {
                    time: 60000
                });
            $(".layui-layer-tips").width('220px');
        }, function() {
            layer.close(layer.index);
            // $("#" + $(this).attr("data-id")).remove();
        });
    }, 1000)

}