$(function () {
    cityChange();
    setBund();
    $("#Check").on("click", function () {
        $.get("/Public/Check", {
            cityCode: $("#City").val(),
            regionCode: $("#Region").val(),
            parkCode: $("#Park").val(),
            stageCode: $("#Stage").val(),
            buildingCode: $("#Building").val(),
            floorCode: $("#Floor").val(),
            typeId: $("#RType").val(),
            resourceId: $("#ResourceCode").val()
        }, function (data) {
            $(".data-action").html(data);
            table_bund();
        });
    });
})
var cityChange = function () {
    $("#City").off("change").on("change", function () {
        $.get("/Region/GetDropData", { parentCode: $(this).val() }, function (data) {
            $("#Region").parent().after(data).remove();
            $("#Region").selectpicker("refresh");
            regionChange();
        });
        $("#Park").attr("disabled", true)[0].options.selectedIndex = 0;
        $("#Park").selectpicker("refresh");
        $("#Stage").attr("disabled", true)[0].options.selectedIndex = 0;
        $("#Stage").selectpicker("refresh");
        $("#Building").attr("disabled", true)[0].options.selectedIndex = 0;
        $("#Building").selectpicker("refresh");
        $("#Floor").attr("disabled", true)[0].options.selectedIndex = 0;
        $("#Floor").selectpicker("refresh");
    });
}
var regionChange = function () {
    $("#Region").change(function () {
        $.get("/Park/GetDropData", { parentCode: $(this).val() }, function (data) {
            $("#Park").parent().after(data).remove();
            $("#Park").selectpicker("refresh");
            parkChange();
        });
        $("#Stage").attr("disabled", true)[0].options.selectedIndex = 0;
        $("#Stage").selectpicker("refresh");
        $("#Building").attr("disabled", true)[0].options.selectedIndex = 0;
        $("#Building").selectpicker("refresh");
        $("#Floor").attr("disabled", true)[0].options.selectedIndex = 0;
        $("#Floor").selectpicker("refresh");
    });
}
var parkChange = function () {
    $("#Park").change(function () {
        $.get("/Stage/GetDropData", { parentCode: $(this).val() }, function (data) {
            $("#Stage").parent().after(data).remove();
            $("#Stage").selectpicker("refresh");
            stageChange();
        });
        $("#Building").attr("disabled", true)[0].options.selectedIndex = 0;
        $("#Building").selectpicker("refresh");
        $("#Floor").attr("disabled", true)[0].options.selectedIndex = 0;
        $("#Floor").selectpicker("refresh");
    });
}
var stageChange = function () {
    $("#Stage").change(function () {
        $.get("/Building/GetDropData", { parentCode: $(this).val() }, function (data) {
            $("#Building").parent().after(data).remove();
            $("#Building").selectpicker("refresh");
            buildingChange();
        });
        $("#Floor").attr("disabled", true)[0].options.selectedIndex = 0;
        $("#Floor").selectpicker("refresh");
    });
}
var buildingChange = function () {
    $("#Building").change(function () {
        $.get("/Floor/GetDropData", { parentCode: $(this).val() }, function (data) {
            $("#Floor").parent().after(data).remove();
            $("#Floor").selectpicker("refresh");
        });
    });
}
var setBund = function () {
    $(".table tr").click(function(){
        $(".table tr").each(function () {
            $(this).removeClass("active");
        });
        $(this).addClass("active");
    }).dblclick(function () {
        var code = $(this).find("td").eq(0).html();
        public(code);
    });
    $(".public").click(function(){
        var code = $(this).parent().parent().find("td").eq(0).html();
        // alert(code);
        public(code);
    });
}
var public = function (data) {
    var index = layer.open({
        type: 2,
        content: '/Public/Public?resourceCode=' + data
    });
    layer.full(index);
}


