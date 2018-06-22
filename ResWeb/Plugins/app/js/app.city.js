$(function () {
    // 添加
    $("#add").on("click", function () {
        $(".form-header").html("添加城市");
        $("#commit").off("click").on("click", function () {
            ajax_form("/City/Create", "POST", new FormData($("form")[0]), "html", function () {
                ajax("/City/Details", "GET", {}, "html", 2, function (repData) {
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
                tr_select_id = "";
                $(".table tr").removeClass("active");
                table_bund();
            }
        });
    });

    // 编辑
    $("#edit").on("click", function () {
        $(".form-header").html("更新城市");
        if (!tr_select_id || tr_select_id == "") {
            layer.msg("请选择城市！");
        } else {
            $(".form-header").html("添加城市");
            ajax("/City/Edit", "GET", { id: tr_select_id }, "json", 2, function (resdata) {
                $("#edit-code").val(resdata.Code);
                $("#edit-name").val(resdata.Name);
            });
            $("#commit").off("click").on("click", function () {
                var formdata = new FormData($("form")[0]);
                ajax_form("/City/Edit/" + tr_select_id, "POST", formdata, "html", function () {
                    ajax("/City/Details", "GET", {}, "html", 2, function (repData) {
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
                ajax("/City/Delete", "GET", { id: tr_select_id }, "html", 1, function (resdata) {
                    ajax("/City/Details", "GET", {}, "html", 2, function (repData) {
                        $(".data-action").html(repData);
                        layer.msg("刷新成功");
                    });
                });
            });
        }
    });

    //查询
    $("#select").on("click", function () {
        ajax("/City/Select", "GET", { name: $("#select-city") }, "html", 2, function (resdata) {
            $(".data-action").html(resdata);
            layer.msg("查询成功");
        });
    });
});






