$(function () {
    // 添加
    $("#add").on("click", function () {
        $(".form-header").html("添加建设期");
        $("#commit").off("click").on("click", function () {
            ajax_form("/Stage/Create", "POST", new FormData($("form")[0]), "html", function () {
                ajax("/Stage/Details", "GET", {}, "html", 2, function (repData) {
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
                $("#edit-region").selectpicker('render');
                $("#edit-park").selectpicker('render');
                tr_select_id = "";
                $(".table tr").removeClass("active");
                table_bund();
            }
        });
    });

    // 编辑
    $("#edit").on("click", function () {
        if (!tr_select_id || tr_select_id == "") {
            layer.msg("请选择城市！");
        } else {
            $(".form-header").html("更新建设期");
            ajax("/Stage/Edit", "GET", { id: tr_select_id }, "json", 2, function (resdata) {
                // console.log(resdata);
                $("#edit-code").val(resdata[0].stage.Code);
                $("#edit-name").val(resdata[0].stage.Name);
                $("#edit-city").selectpicker('val', resdata[0].city.Code);
                $("#edit-region").selectpicker('val', resdata[0].region.Code);
                $("#edit-park").selectpicker('val', resdata[0].park.Code);
            });
            $("#commit").off("click").on("click", function () {
                var formdata = new FormData($("form")[0]);
                ajax_form("/Stage/Edit/" + tr_select_id, "POST", formdata, "html", function () {
                    ajax("/Stage/Details", "GET", {}, "html", 2, function (repData) {
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
                    $("#edit-region").selectpicker('render');
                    $("#edit-park").selectpicker('render');
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
            layer.msg("请选择！");
        } else {
            layer.confirm("是否删除？", function () {
                ajax("/Stage/Delete", "GET", { id: tr_select_id }, "html", 1, function (resdata) {
                    ajax("/Stage/Details", "GET", {}, "html", 2, function (repData) {
                        $(".data-action").html(repData);
                        layer.msg("刷新成功");
                        table_bund();
                    });
                });
            });
        }
    });

    //查询
    $("#select").on("click", function () {
        ajax("/Stage/Select", "GET",
            {
                cityCode: $("#select-city").val(),
                regionCode: $("#select-region").val(),
                parkCode: $("#select-park").val()
            }, "html", 2, function (resdata) {
                $(".data-action").html(resdata);
                layer.msg("查询成功");
                table_bund();
            });
    });

    //城市联动
    $("#select-city").on("change", function () {
        ajax("/Floor/GetRegion", "GET", { pcode: $("#select-city").val() }, "json", 2, function (resdata) {
            var html = "<option value=''>全部</option>";
            for (var i = 0; i < resdata.length; i++) {
                html += "<option value='" + resdata[i].Code + "'>" + resdata[i].Name + "</option>";
            }
            $("#select-region").removeAttr("disabled").html(html).selectpicker("refresh");
            $("#select-park").attr("disabled", true)[0].options.selectedIndex = 0;
            $("#select-park").selectpicker("refresh");


        });
    });
    //区域联动
    $("#select-region").on("change", function () {
        ajax("/Floor/GetPark", "GET", { pcode: $("#select-region").val() }, "json", 2, function (resdata) {
            var html = "<option value=''>全部</option>";
            for (var i = 0; i < resdata.length; i++) {
                html += "<option value='" + resdata[i].Code + "'>" + resdata[i].Name + "</option>";
            }
            $("#select-park").removeAttr("disabled").html(html).selectpicker("refresh");
        });
    });

});






