var layer;
$(function () {
    layui.use('layer', function () {
        layer = layui.layer;
    });
    table_bund();
    // 添加
    $("#city-create").on("click", function () {
        $(".form-header").html("添加城市");
        $("#city-submit").off("click").on("click", function () {
            ajax("/Area/AddCity", "POST", { code: $("#city-code").val(), name: $("#city-name").val() }, "html", 1, function () {
                ajax("/Area/CityTable", "GET", {}, "html", 2, function (repData) {
                    $("div[data-type='city'] .data-action").html(repData);
                    layer.msg("刷新成功");
                });
            });
        });
        layer.open({
            type: 1,            //页面层
            closeBtn: 2,
            content: $(".div-city"),
            area: ["400px"],
            title: false,
            scrollbar: false,
            end: function () {
                $(".div-city form").get(0).reset();

            }
        });
    });
    // 编辑
    $("#city-edit").on("click", function () {
        $(".form-header").html("更新城市");
        if (!tr_select_id || tr_select_id == "") {
            layer.msg("请选择城市！");
        } else {
            $(".form-header").html("添加城市");
            
            $("#city-submit").off("click").on("click", function () {
                ajax("/Area/AddCity", "POST", { code: $("#city-code").val(), name: $("#city-name").val() }, "html", 1, function () {
                    ajax("/Area/CityTable", "GET", {}, "html", 2, function (repData) {
                        $("div[data-type='city'] .data-action").html(repData);
                        layer.msg("刷新成功");
                    });
                });
            });
            layer.open({
                type: 1,            //页面层
                closeBtn: 2,
                content: $(".div-city"),
                area: ["400px"],
                title: false,
                scrollbar: false,
                end: function () {
                    $(".div-city form").get(0).reset();

                }
            });
        }
    });
});






