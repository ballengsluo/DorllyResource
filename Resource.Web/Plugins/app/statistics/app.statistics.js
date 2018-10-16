$(function() {
    initModule();
});

function initModule() {
    // 组件初始化
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
    // if ($('#park>option:nth-child(2)').length <= 0) {
    //     $.getJSON("/Drop/ParkDropList", {}, function(data) {

    //         var html = "";
    //         html += "<option value=''>全部</option>";
    //         for (var i = 0; i < data.length; i++) {
    //             html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
    //         }
    //         $("#park").html(html).selectpicker("refresh");
    //     });
    // }
    if ($('#parkMonth').length > 0) {
        $.getJSON("/Drop/ParkDropList", {}, function(data) {
            var html = "";
            html += "<option value=''>全部</option>";
            for (var i = 0; i < data.length; i++) {
                html += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
            }
            $("#parkMonth").html(html).selectpicker("refresh");
        });
    }
    // 事件初始化
    if ($('#searchDay').length > 0) {
        $('#searchDay').click(function() {
            var start = $('#stime').val();
            var end = $('#etime').val();
            if (start.length > 0 || end.length > 0) {
                if (new Date(start.replace("-", "/").replace("-", "/")) > new Date(end.replace("-", "/").replace("-", "/"))) {
                    layer.msg("结束时间比开始时间小！");
                    return false;
                }
            }
            ajax('get', $(this).attr('data-url'), {
                beginTime: $('#stime').val(),
                endTime: $('#etime').val()
            }, 'json', function(data) {
                dealDayData(data);
            });
        });
    }
    if ($('#searchMonth').length > 0) {
        $('#searchMonth').click(function() {
            var start = $('#stime1').val();
            var end = $('#etime1').val();
            if (start.length > 0 || end.length > 0) {
                if (new Date(start.replace("-", "/").replace("-", "/")) > new Date(end.replace("-", "/").replace("-", "/"))) {
                    layer.msg("结束时间比开始时间小！");
                    return false;
                }
            }
            ajax('get', $(this).attr('data-url'), {
                park: $('#parkMonth').val(),
                beginTime: $('#stime1').val(),
                endTime: $('#etime1').val()
            }, 'json', function(data) {
                dealMonthData(data);
            });
        });
    }
    if ($('#importDay').length > 0) {
        $('#importDay').click(function() {
            var start = $('#stime').val();
            var end = $('#etime').val();
            if (start.length > 0 || end.length > 0) {
                if (new Date(start.replace("-", "/").replace("-", "/")) > new Date(end.replace("-", "/").replace("-", "/"))) {
                    layer.msg("结束时间比开始时间小！");
                    return false;
                }
            }
            var url = $(this).attr('data-url') + "?park=" + $("#park").val() + "&&" + "stime=" + $("#stime").val() + "&&" + "etime=" + $("#etime").val() + "&&" + "model=1";
            window.location.href = url;
        });
    }
    if ($('#importMonth').length > 0) {
        $('#importMonth').click(function() {
            var start = $('#stime1').val();
            var end = $('#etime1').val();
            if (start.length > 0 || end.length > 0) {
                if (new Date(start.replace("-", "/").replace("-", "/")) > new Date(end.replace("-", "/").replace("-", "/"))) {
                    layer.msg("结束时间比开始时间小！");
                    return false;
                }
            }
            var url = $(this).attr('data-url') + "?park=" + $("#parkMonth").val() + "&&" + "stime=" + $("#stime1").val() + "&&" + "etime=" + $("#etime1").val() + "&&" + "model=2";
            window.location.href = url;
        });
    }
}

function graphPie(id, len, title, data) {
    if (len == 1) {
        $("#statistics-graph-pie").append("<div class='col-lg-6 col-md-6 col-sm-6'><div id='" + id + "' style='width:" + $(".container").width() + "px;height:400px;'></div></div>");
    } else {
        $("#statistics-graph-pie").append("<div class='col-lg-6 col-md-6 col-sm-6'><div id='" + id + "' style='width:" + $(".container").width() / 2 + "px;height:300px;'></div></div>");
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
        toolbox: {
            show: true,
            right: 40,
            feature: {
                saveAsImage: {
                    pixelRatio: 2
                }
            }
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



function graphLine(title, legend, xAxis, series) {
    $("#statistics-graph-line").css("width", $(".container").width());
    var myChart = echarts.init(document.getElementById('statistics-graph-line'));
    var option = {
        title: {
            text: title,
            left: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: '{b} : {c}%'
        },
        legend: {
            bottom: 0,
            data: legend
        },
        toolbox: {
            show: true,
            right: 40,
            feature: {
                saveAsImage: {
                    pixelRatio: 2
                }
            }
        },
        xAxis: {
            type: 'category',
            name: '月份',
            splitLine: { show: false },
            nameGap: 5,
            data: xAxis
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '10%',
            containLabel: true
        },
        yAxis: {
            type: 'value',
            name: '租用率',
            nameGap: 12,
            max: 100
        },
        series: series
    }
    myChart.setOption(option);
    $("#statistics-graph-line").removeAttr("style");
}
var symbolArray = ['emptyCircle', 'rect', 'circle', 'triangle', 'diamond'];

function lineSeries(data) {
    var series = new Array();
    var index = 0;
    $.each(data, function(idx, obj) {
        series.push({
            name: obj.name,
            type: 'line',
            symbol: symbolArray[index],
            symbolSize: 10,
            data: obj.data
        });
        if (index <= 5) {
            index++;
        } else {
            index = 0;
        }
    });
    return series;
}