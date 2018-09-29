$(function() {
    init();
    searchDay();
    searchMonth();
    $('#searchDay').trigger("click");
    $('#searchMonth').trigger("click");
});

function init() {
    layui.use('element', function() {
        var element = layui.element
    });
    if ($('#stime').length > 0) {
        layui.use('laydate', function() {
            var laydate = layui.laydate
            laydate.render({
                elem: '#stime'
            });
        });
    }
    if ($('#etime').length > 0) {
        layui.use('laydate', function() {
            var laydate = layui.laydate
            laydate.render({
                elem: '#etime'
            });
        });
    }
    if ($('#stime1').length > 0) {
        layui.use('laydate', function() {
            var laydate = layui.laydate
            laydate.render({
                elem: '#stime1',
                type: 'month'
            });
        });
    }
    if ($('#etime1').length > 0) {
        layui.use('laydate', function() {
            var laydate = layui.laydate
            laydate.render({
                elem: '#etime1',
                type: 'month'
            });
        });
    }
}

function searchDay() {
    $('#searchDay').click(function() {
        ajax('get', $(this).attr('data-url'), {
            beginTime: $('#stime').val(),
            endTime: $('#etime').val()
        }, 'json', function(data) {

            park(data);
        });
    });
}

function searchMonth() {
    $('#searchMonth').click(function() {
        ajax('get', $(this).attr('data-url'), {
            beginTime: $('#stime1').val(),
            endTime: $('#etime1').val()
        }, 'json', function(data) {
            console.log(data);
            if (data.Flag == 1) {
                var html = template('tableTpl', { park: data.park, month: data.month });
                $("#tb").html(html);
            }
        });
    });
}

function park(data) {
    if (data.Flag == 1) {
        var parkList;
        if ($("#park").val().length > 0) {
            parkList = $.grep(data.Part1, function(obj, i) {
                return obj.ParkID == $("#park").val();
            });
        } else {
            parkList = data.Part1;
        }
        if (parkList.length <= 0) {
            $("#statistics-panel").html("<div style='text-align:center;line-height:300px;'>暂无数据！</div>");
            return false;
        }
        $("#statistics-graph").html("");
        var parkContainer = $("<div class='statistics-panel'></div>");
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
                var buildContainer = $("<div class='statistics-panel' style='display:none;' data-pid='" + park.ParkID + "'></div>");
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
            var len = parkList.length;
            var title = park.ParkName;
            var graphData = new Array();
            graphData.push({ name: "客户租赁", value: park.RentRate });
            graphData.push({ name: "内部使用", value: park.SelfRate });
            graphData.push({ name: "空置", value: park.FreeRate });
            graph(park.ParkID, len, park.ParkName, graphData);
        });
        $("#statistics-panel").html(parkContainer);
        checkBuild();
    } else {
        $("#statistics-panel").html(" <div style='text-align:center;line-height:300px;'>数据异常！</div>");
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


function graph(id, len, title, data) {
    if (len == 1) {
        $("#statistics-graph").append("<div class='col-lg-6 col-md-6 col-sm-6'><div id='" + id + "' style='width:" + $(".container").width() + "px;height:400px;'></div></div>");
    } else {
        $("#statistics-graph").append("<div class='col-lg-6 col-md-6 col-sm-6'><div id='" + id + "' style='width:" + $(".container").width() / 2 + "px;height:300px;'></div></div>");
    }
    var myChart = echarts.init(document.getElementById(id));
    var option = {
        title: {
            text: title,
            left: '0'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{b} : {c} %"
        },

        legend: {
            orient: 'vertical',
            top: '10%',
            bottom: 10,
            left: '0',
            data: ['客户租赁', '内部使用', '空置']
        },
        series: [{
            type: 'pie',
            radius: '70%',
            center: ['50%', '50%'],
            selectedMode: 'single',
            label: {
                position: 'outside',
                formatter: '{b}: {c}%'
            },
            labelLine: {
                length2: 0,
                smooth: true,
                lineStyle: {
                    type: 'dotted'
                }
            },
            itemStyle: {
                emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            },
            data: data
        }]
    };
    myChart.setOption(option);
    $("#" + id).removeAttr("style");
}