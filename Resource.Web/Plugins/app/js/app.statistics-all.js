$(function() {
    init();

});

function init() {
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
    layui.use('element', function() {
        var element = layui.element
    });
    search();
    $("#search").trigger("click");
}

function graph() {
    // $("#statistics-graph").append("<div class='col-lg-6 col-md-6 col-sm-6' id='rm'></div>");
    // $("#statistics-graph").html("<div class='col-lg-6 col-md-6 col-sm-6' id='rm'></div>");
    // alert($("#statistics-graph").outerHeight());
    // alert($("#statistics-graph").outerWidth());
    // var myChart = echarts.init(document.getElementById('statistics-graph'));
    // alert($("#rm").outerHeight());
    // alert($("#rm").outerWidth());
    // $("#statistics-graph").append("<div class='col-lg-6 col-md-6 col-sm-6' id='rm'></div>");
    $("#statistics-graph").html("<div class='col-lg-6 col-md-6 col-sm-6' id='rm'></div>");
    alert($("#statistics-graph").outerHeight());
    alert($("#statistics-graph").outerWidth());
    var myChart = echarts.init(document.getElementById('#rm'));
    var option = {
        title: {
            text: '天气情况统计',
            subtext: '虚构数据',
            left: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{b} : {c} ({d}%)"
        },
        legend: {
            // orient: 'vertical',
            // top: 'middle',
            bottom: 30,
            left: 'center',
            data: ['西凉', '益州', '兖州', '荆州', '幽州', 'aa']
        },
        series: [{
            type: 'pie',
            radius: '65%',
            center: ['50%', '50%'],
            selectedMode: 'single',
            data: [{
                    value: 1548,
                    name: '幽州',
                },
                { value: 535, name: '荆州' },
                { value: 510, name: '兖州' },
                { value: 634, name: '益州' },
                { value: 735, name: '西凉' }
            ],
            itemStyle: {
                emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        }]
    };
    myChart.setOption(option);
    $(".layui-tab-title>li:first-of-type").trigger("click");

}

function search() {
    $('#search').click(function() {
        ajax('get', $('#search').attr('data-url'), {
            park: $('#park').val(),
            beginTime: $('#stime').val(),
            endTime: $('#etime').val()
        }, 'json', function(data) {
            if (data.length > 0) {
                $(".statistics-rate>span:first-of-type").html(0);
                $(".statistics-num>span:first-of-type").html(0);
                $.each(data, function(idx, item) {
                    if (item.Kind == 1) {
                        $("#rm-total").html(item.Total);
                        $("#rm-rent").html(item.Rent);
                        $("#rm-rentRate").html(item.RentRate);
                        $("#rm-self").html(item.Self);
                        $("#rm-selfRate").html(item.SelfRate);
                        $("#rm-free").html(item.Free);
                        $("#rm-freeRate").html(item.FreeRate);
                    } else if (item.Kind == 2) {
                        $("#wp-total").html(item.Total);
                        $("#wp-rent").html(item.Rent);
                        $("#wp-rentRate").html(item.RentRate);
                        $("#wp-self").html(item.Self);
                        $("#wp-selfRate").html(item.SelfRate);
                        $("#wp-free").html(item.Free);
                        $("#wp-freeRate").html(item.FreeRate);
                    } else if (item.Kind == 3) {
                        $("#cr-total").html(item.Total);
                        $("#cr-rent").html(item.Rent);
                        $("#cr-rentRate").html(item.RentRate);
                        $("#cr-self").html(item.Self);
                        $("#cr-selfRate").html(item.SelfRate);
                        $("#cr-free").html(item.Free);
                        $("#cr-freeRate").html(item.FreeRate);
                    } else if (item.Kind == 4) {
                        $("#ad-total").html(item.Total);
                        $("#ad-rent").html(item.Rent);
                        $("#ad-rentRate").html(item.RentRate);
                        $("#ad-self").html(item.Self);
                        $("#ad-selfRate").html(item.SelfRate);
                        $("#ad-free").html(item.Free);
                        $("#ad-freeRate").html(item.FreeRate);
                    }
                });
                graph();
            } else {
                layer.msg("获取数据失败！");
            }
        });
    });
}