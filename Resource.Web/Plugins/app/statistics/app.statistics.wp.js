$(function() {
    $('#searchDay').trigger("click");
    $('#searchMonth').trigger("click");
});


function dealDayData(data) {
    var $container = $("#statistics-list-normal");
    if (data.Flag == 1) {
        var parkList;
        //刷选园区数据
        if ($("#park").val().length > 0) {
            parkList = $.grep(data.Part1, function(obj, i) {
                return obj.ParkID == $("#park").val();
            });
        } else {
            parkList = data.Part1;
        }
        if (parkList.length <= 0) {
            $("#statistics-panel").html(" <div style='text-align:center;line-height:300px;'>暂无数据！</div>");
            return false;
        }
        var parkContainer = $("#statistics-panel");
        $("#statistics-graph-pie").html(""); //清除图表数据
        var parkContainer = $("<div class='statistics-list'></div>");
        $.each(parkList, function(idx, park) {
            var parkHtml = $("<div class='row'></div>");
            parkHtml.append("<div class='col-lg-1 col-md-1 statistics-title'>" + park.ParkName + "</div>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-total'><div class='statistics-name'>工位总数</div><div class='statistics-num'><span>" + park.Total + "</span><span>个</span></div></div>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-rent'><div class='statistics-name'>客户租赁个数</div><div class='statistics-rate'><span>" + park.RentRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Rent + "</span><span>个</span></div></div>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-self'><div class='statistics-name'>内部使用个数</div><div class='statistics-rate'><span>" + park.SelfRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Self + "</span><span>个</span></div></div>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-free'><div class='statistics-name'>空置个数</div><div class='statistics-rate'><span>" + park.FreeRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Free + "</span><span>个</span></div></div>");
            parkContainer.append(parkHtml);
            // 图表数据处理
            var graphData = new Array();
            graphData.push({ name: "客户租赁", value: park.RentRate });
            graphData.push({ name: "内部使用", value: park.SelfRate });
            graphData.push({ name: "空置", value: park.FreeRate });
            graphPie(park.ParkID, parkList.length, park.ParkName, graphData);
        });
        $container.html(parkContainer);
    } else {
        layer.msg(data.Msg);
        console.log(data.ExMsg);
    }
}

function dealMonthData(data) {

    if (data.Flag == 1) {
        // 表格数据处理
        var html = template('tableTpl', { park: data.park, month: data.monthData });
        $("#statistics-list-table").html(html);
        // 图表数据处理
        graphLine("工位租用率趋势图", data.park, data.month, lineSeries(data.parkData));
    } else {
        layer.msg(data.Msg);
        console.log(data.ExMsg);
    }

}