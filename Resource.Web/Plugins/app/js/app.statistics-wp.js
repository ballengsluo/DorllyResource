$(function() {
    init();
    searchDay();
    $('#searchDay').trigger("click");
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
    if ($('#etime2').length > 0) {
        layui.use('laydate', function() {
            var laydate = layui.laydate
            laydate.render({
                elem: '#etime2',
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
            console.log(data);
            park(data);
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
            $("#statistics-panel").html(" <div style='text-align:center;line-height:300px;'>暂无数据！</div>");
            return false;
        }
        var parkContainer = $("#statistics-panel");
        parkContainer.html(""); //清空平面内容
        $("#statistics-graph").html(""); //清空图表内容
        // var parkContainer = $("<div class='statistics-panel'></div>");
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
            graph(park.ParkID, parkList.length, park.ParkName, graphData);
        });
        // $("#statistics-panel").html(parkContainer);
    } else {
        $("#statistics-panel").html(" <div style='text-align:center;line-height:300px;'>数据异常！</div>");
    }

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