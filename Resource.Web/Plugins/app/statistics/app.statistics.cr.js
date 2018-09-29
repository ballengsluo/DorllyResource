$(function() {
    $('#searchDay').trigger("click");
    $('#searchMonth').trigger("click");
});


function dealDayData(data) {
    if (data.Flag == 1) {
        var data;
        if ($("#park").val().length > 0) {
            data = $.grep(data.Part1, function(obj, i) {
                return obj.ParkID == $("#park").val();
            });
        } else {
            data = data.Part1;
        }
        if (data.length <= 0) {
            $("#statistics-list-normal").html("<td colspan='10' style='text-align:center;'>暂无数据！</td>");
            return false;
        }
        var html = template('listTpl', { data: data });
        $("#statistics-list-normal").html(html);
    } else {
        layer.msg(data.Msg);
        console.log(data.ExMsg);
    }

}

function dealMonthData(data) {
    console.log(data);
    if (data.Flag == 1) {
        // 表格数据处理
        var html = template('tableTpl', { title: data.table.title, rows: data.table.rows });
        $("#statistics-list-table").html(html);
        // 图表数据处理
        graphLine("会议室租用率趋势图", data.graph.legend, data.graph.xAxis, lineSeries(data.graph.series));
    } else {
        layer.msg(data.Msg);
        console.log(data.ExMsg);
    }

}