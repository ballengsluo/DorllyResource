$(function () {
    // 添加
    $("#add").on("click", function () {
        $(".form-header").html("添加楼层");
        $("#commit").off("click").on("click", function () {
            ajax_form("/Floor/Create", "POST", new FormData($("form")[0]), "html", function () {
                ajax("/Floor/Details", "GET", {}, "html", 2, function (repData) {
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
        
        if (!tr_select_id || tr_select_id == "") {
            layer.msg("请选择楼层！");
        } else {
            $(".form-header").html("更新楼层");
            ajax("/Floor/Edit", "GET", { id: tr_select_id }, "json", 2, function (resdata) {
                $("#edit-code").val(resdata[0].floor.Code);
                $("#edit-name").val(resdata[0].floor.Name);
                $("#edit-city").selectpicker('val', resdata[0].city.Code);
                $("#edit-region").selectpicker('val', resdata[0].region.Code);
                $("#edit-park").selectpicker('val', resdata[0].park.Code);
                $("#edit-stage").selectpicker('val', resdata[0].stage.Code);
                $("#edit-build").selectpicker('val', resdata[0].build.Code);
            });
            $("#commit").off("click").on("click", function () {
                var formdata = new FormData($("form")[0]);
                ajax_form("/Floor/Edit/" + tr_select_id, "POST", formdata, "html", function () {
                    ajax("/Floor/Details", "GET", {}, "html", 2, function (repData) {
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
                    $("#edit-stage").selectpicker('render');
                    $("#edit-build").selectpicker('render');
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
                ajax("/Floor/Delete", "GET", { id: tr_select_id }, "html", 1, function (resdata) {
                    ajax("/Floor/Details", "GET", {}, "html", 2, function (repData) {
                        $(".data-action").html(repData);
                        layer.msg("刷新成功");
                    });
                });
            });
        }
    });

    //查询
    $("#select").on("click", function () {
        ajax("/Floor/Select", "GET",
            {
                cityCode: $("#select-city").val(),
                regionCode: $("#select-region").val(),
                parkCode: $("#select-park").val(),
                stageCode: $("#select-stage").val(),
                buildingCode: $("#select-build").val()
            }, "html", 2, function (resdata) {
                $(".data-action").html(resdata);
                layer.msg("查询成功");
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
            // $("#select-park").selectpicker("render").selectpicker("refresh").attr("disabled",true);
            $("#select-park").attr("disabled", true)[0].options.selectedIndex = 0;
            $("#select-park").selectpicker("refresh");
            $("#select-stage").attr("disabled", true)[0].options.selectedIndex = 0;
            $("#select-stage").selectpicker("refresh");
            $("#select-build").attr("disabled", true)[0].options.selectedIndex = 0;
            $("#select-build").selectpicker("refresh");
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
            $("#select-stage").attr("disabled", true)[0].options.selectedIndex = 0;
            $("#select-stage").selectpicker("refresh");
            $("#select-build").attr("disabled", true)[0].options.selectedIndex = 0;
            $("#select-build").selectpicker("refresh");
        });
    });
    //园区联动
    $("#select-park").on("change", function () {
        ajax("/Floor/GetStage", "GET", { pcode: $("#select-park").val() }, "json", 2, function (resdata) {
            var html = "<option value=''>全部</option>";
            for (var i = 0; i < resdata.length; i++) {
                html += "<option value='" + resdata[i].Code + "'>" + resdata[i].Name + "</option>";
            }
            $("#select-stage").removeAttr("disabled").html(html).selectpicker("refresh");
            $("#select-build").attr("disabled", true)[0].options.selectedIndex = 0;
            $("#select-build").selectpicker("refresh");
        });
    });
    //建设期联动
    $("#select-stage").on("change", function () {
        ajax("/Floor/GetBuilding", "GET", { pcode: $("#select-stage").val() }, "json", 2, function (resdata) {
            var html = "<option value=''>全部</option>";
            for (var i = 0; i < resdata.length; i++) {
                html += "<option value='" + resdata[i].Code + "'>" + resdata[i].Name + "</option>";
            }
            $("#select-build").removeAttr("disabled").html(html).selectpicker("refresh");
        });
    });
});






