$(function() {
    $('#searchDay').trigger("click");
    $('#searchMonth').trigger("click");
});

function dealDayData(data) {
    // console.log(data);
    var $container = $("#statistics-list-normal");
    if (data.Flag == 1) {
        var parkList;
        //筛选园区数据
        if ($("#park").val().length > 0) {
            parkList = $.grep(data.Part1, function(obj, i) {
                return obj.ParkID == $("#park").val();
            });
        } else {
            parkList = data.Part1;
        }

        if (parkList.length <= 0) {
            $container.html("<div style='text-align:center;line-height:300px;'>暂无数据！</div>");
            return false;
        }
        //组装数据
        $("#statistics-graph-pie").html(""); //清除图表数据
        var parkContainer = $("<div class='statistics-list'></div>");
        $.each(parkList, function(idx, park) {
            var parkHtml = $("<div class='row'></div>");
            parkHtml.append("<div class='col-lg-1 col-md-1 statistics-title'>" + park.ParkName + "</div>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-totalNum'><div class='statistics-name'>房间数</div><div class='statistics-num'><span>" + park.TotalNum + "</span><span>间</span></div></div>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-total'><div class='statistics-name'>总面积</div><div class='statistics-num'><span>" + park.Total + "</span><span>㎡</span>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-rent'><div class='statistics-name'>客户租赁面积</div><div class='statistics-rate'><span>" + park.RentRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Rent + "</span><span>㎡</span>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-self'><div class='statistics-name'>内部使用面积</div><div class='statistics-rate'><span>" + park.SelfRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Self + "</span><span>㎡</span>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-free'><div class='statistics-name'>空置面积</div><div class='statistics-rate'><span>" + park.FreeRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Free + "</span><span>㎡</span>");
            var buildList = $.grep(data.Part2, function(obj, idx) {
                return obj.ParkID == park.ParkID;
            });
            // 楼栋数据处理
            if (buildList.length > 0) {
                parkHtml.append("<div class='col-lg-1 col-md-1 statistics-detail'><div data-id='" + park.ParkID + "'>查看楼栋<i class='glyphicon glyphicon-menu-down'></i></div></div>");
                var buildContainer = $("<div class='statistics-list' style='display:none;' data-pid='" + park.ParkID + "'></div>");
                $.each(buildList, function(idx, build) {
                    var buildHtml = $("<div class='row'></div>");
                    buildHtml.append("<div class='col-lg-offset-1 col-md-offset-1 col-lg-1 col-md-1 statistics-title'>" + build.Name + "</div>");
                    buildHtml.append("<div class='col-lg-2 col-md-2 statistics-totalNum'><div class='statistics-name'>房间数</div><div class='statistics-num'><span>" + build.TotalNum + "</span><span>间</span></div></div>");
                    buildHtml.append("<div class='col-lg-2 col-md-2 statistics-total'><div class='statistics-name'>总面积</div><div class='statistics-num'><span>" + build.Total + "</span><span>㎡</span>");
                    buildHtml.append("<div class='col-lg-2 col-md-2 statistics-rent'><div class='statistics-name'>客户租赁面积</div><div class='statistics-rate'><span>" + build.RentRate + "</span><span>%</span></div><div class='statistics-num'><span>" + build.Rent + "</span><span>㎡</span>");
                    buildHtml.append("<div class='col-lg-2 col-md-2 statistics-self'><div class='statistics-name'>内部使用面积</div><div class='statistics-rate'><span>" + build.SelfRate + "</span><span>%</span></div><div class='statistics-num'><span>" + build.Self + "</span><span>㎡</span>");
                    buildHtml.append("<div class='col-lg-2 col-md-2 statistics-free'><div class='statistics-name'>空置面积</div><div class='statistics-rate'><span>" + build.FreeRate + "</span><span>%</span></div><div class='statistics-num'><span>" + build.Free + "</span><span>㎡</span>");
                    buildContainer.append(buildHtml);
                });
            }
            parkContainer.append(parkHtml);
            parkContainer.append(buildContainer);
            // 图表处理
            var graphData = new Array();
            graphData.push({ name: "客户租赁", value: park.RentRate });
            graphData.push({ name: "内部使用", value: park.SelfRate });
            graphData.push({ name: "空置", value: park.FreeRate });
            graphPie(park.ParkID, parkList.length, park.ParkName, graphData);
        });
        $container.html(parkContainer);
        checkBuild(); //查看楼栋事件绑定
    } else {
        layer.msg(data.Msg);
        console.log(data.ExMsg);
    }

}

function checkBuild() {
    $(".statistics-detail>div").click(function() {
        if ($(this).find("i").hasClass("glyphicon-menu-up")) {
            $(this).find("i").removeClass("glyphicon-menu-up").addClass("glyphicon-menu-down");
            $("div[data-pid='" + $(this).attr("data-id") + "']").slideUp();
        } else {
            $(".statistics-detail>div>i").removeClass("glyphicon-menu-up").addClass("glyphicon-menu-down");
            $("div[data-pid]").hide();
            $(this).find("i").addClass("glyphicon-menu-up");
            $("div[data-pid='" + $(this).attr("data-id") + "']").slideDown();
        }

    });
}

function dealMonthData(data) {

    if (data.Flag == 1) {
        // 表格数据处理
        var html = template('tableTpl', { title: data.table.title, rows: data.table.rows });
        $("#statistics-list-table").html(html);
        // 图表数据处理
        graphLine("房屋租用率趋势图", data.graph.legend, data.graph.xAxis, lineSeries(data.graph.series));
    } else {
        layer.msg(data.Msg);
        console.log(data.ExMsg);
    }

}