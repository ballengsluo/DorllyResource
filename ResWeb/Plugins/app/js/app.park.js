$(function () {
    // 添加
    $("#add").on("click", function () {
        $(".form-header").html("添加园区");
        $("#commit").off("click").on("click", function () {
            ajax_form("/Park/Create", "POST", new FormData($("form")[0]), "html", function () {
                ajax("/Park/Details", "GET", {}, "html", 2, function (repData) {
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
            $(".form-header").html("更新园区");
            ajax("/Park/Edit", "GET", { id: tr_select_id }, "json", 2, function (resdata) {
                // console.log(resdata);
                $("#edit-code").val(resdata[0].park.Code);
                $("#edit-name").val(resdata[0].park.Name);
                $("#edit-x").val(resdata[0].park.GisX);
                $("#edit-y").val(resdata[0].park.GisY);
                $("#edit-addr").val(resdata[0].park.Addr);
                $("#edit-city").selectpicker('val', resdata[0].city.Code);
                $("#edit-region").selectpicker('val', resdata[0].region.Code);
            });
            $("#commit").off("click").on("click", function () {
                var formdata = new FormData($("form")[0]);
                ajax_form("/Park/Edit/" + tr_select_id, "POST", formdata, "html", function () {
                    ajax("/Park/Details", "GET", {}, "html", 2, function (repData) {
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
            layer.msg("请选择要删除的数据！");
        } else {
            layer.confirm("是否删除？", function () {
                ajax("/Park/Delete", "GET", { id: tr_select_id }, "html", 1, function (resdata) {
                    ajax("/Park/Details", "GET", {}, "html", 2, function (repData) {
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
        ajax("/Park/Select", "GET",
            {
                cityCode: $("#select-city").val(),
                regionCode: $("#select-region").val()
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
        });
    });

});






