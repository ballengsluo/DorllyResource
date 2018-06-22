$(function () {
    // 添加
    $("#add").on("click", function () {
        $(".form-header").html("添加区域");
        $("#commit").off("click").on("click", function () {
            ajax_form("/Region/Create", "POST", new FormData($("form")[0]), "html", function () {
                ajax("/Region/Details", "GET", {}, "html", 2, function (resdata) {
                    $(".data-action").html(resdata);
                    layer.msg("刷新成功");
                });
            });
        });
        layer.open({
            type: 1,            //页面层
            closeBtn: 2,
            content: $(".form-edit-horizontal"),
            area: ["400px"],
            title: false,
            scrollbar: false,
            end: function () {
                $("form").get(0).reset();
                $("#edit-city").selectpicker('render');
                tr_select_id = "";
                $(".table tr").removeClass("active");
                table_bund();
            }
        });
    });
 
    // 编辑
    $("#edit").on("click", function () {
        if (!tr_select_id || tr_select_id == "") {
            layer.msg("请选择！");
        } else {
            $(".form-header").html("更新区域");
            ajax("/Region/Edit", "GET", { id: tr_select_id }, "json", 2, function (resdata) {
                $("#edit-code").val(resdata[0].region.Code);
                $("#edit-name").val(resdata[0].region.Name);
                $("#edit-city").selectpicker('val', resdata[0].city.Code);
            });
            $("#commit").off("click").on("click", function () {
                var formdata = new FormData($("form")[0]);
                ajax_form("/Region/Edit/" + tr_select_id, "POST", formdata, "html", function () {
                    ajax("/Region/Details", "GET", {}, "html", 2, function (repData) {
                        $(".data-action").html(repData);
                        layer.msg("刷新成功");
                    });
                });
            });
            layer.open({
                type: 1,            //页面层
                closeBtn: 2,
                content: $(".form-edit-horizontal"),
                area: ["400px"],
                title: false,
                scrollbar: false,
                end: function () {
                    $("form").get(0).reset();
                    $("#edit-city").selectpicker('render');
                    tr_select_id = "";
                    $(".table tr").removeClass("active");
                    table_bund();
                }
            });
        }
    });

    //删除
    $("#del").on("click", function () {
        if (!tr_select_id || tr_select_id == "") {
            layer.msg("请选择城市！");
        } else {
            layer.confirm("是否删除？", function () {
                ajax("/Region/Delete", "GET", { id: tr_select_id }, "html", 1, function (resdata) {
                    ajax("/Region/Details", "GET", {}, "html", 2, function (repData) {
                        $(".data-action").html(repData);
                        layer.msg("刷新成功");
                    });
                });
            });
        }
    });

    //查询
    $("#select").on("click", function () {
        ajax("/Region/Select", "GET", { cityCode: $("#select-city").val() }, "html", 2, function (resdata) {
            $(".data-action").html(resdata);
            layer.msg("查询成功");
        });
    });
});






