$(function () {
    // 位置加载
    $.getJSON("/HomePage/GetHPType", { type: 1 }, function (data) {
        var html = " <option value=''>全部</option>"
        for (let idx = 0; idx < data.length; idx++) {
            html = html + "<option value='" + data[idx].ID + "'>" + data[idx].PositionName + "</option>";
        }
        $("#Position").html(html).selectpicker("refresh");
    });
    $("#Check").click(function () {
        $.get("/HomePage/Check",
            {
                cityCode: $("#City").val(),
                positionId: $("#Position").val(),
                status: $("#Status").val()
            }, function (data) {
                $(".data-action").html(data);
            });
    });

})