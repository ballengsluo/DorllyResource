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
    // $("#statistics-graph").html("<div class='col-lg-6 col-md-6 col-sm-6' id='rm'></div>");
    // alert($("#statistics-graph").outerHeight());
    // alert($("#statistics-graph").outerWidth());
    // alert($("#graph1").outerHeight());
    // alert($("#graph1").outerWidth());
    var myChart = echarts.init(document.getElementById('graph1'));
    var option = {
        title: {
            text: '房屋使用率',
            left: '10%'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{b} : {c} ({d}%)"
        },

        legend: {
            orient: 'vertical',
            top: '10%',
            bottom: 10,
            left: '10%',
            data: ['客户租赁', '内部使用', '空置']
        },
        series: [{
            type: 'pie',
            radius: '70%',
            center: ['60%', '50%'],
            selectedMode: 'single',
            label: { position: 'outside' },
            data: [
                { value: 68527.67, name: '客户租赁' },
                { value: 330, name: '内部使用' },
                { value: 59489.66, name: '空置' }
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
    $("#statistics-graph").append($("#graph>div"));
    $("#graph>div").remove();
}

function search() {
    $('#search').click(function() {
        ajax('get', $('#search').attr('data-url'), {
            park: $('#park').val(),
            beginTime: $('#stime').val(),
            endTime: $('#etime').val()
        }, 'json', function(data) {
            console.log(data);
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
                    var data = new Array();
                    data.push({ name: "客户租赁", num: item.Rent, rate: item.RentRate });
                    data.push({ name: "内部使用", num: item.Self, rate: item.SelfRate });
                    data.push({ name: "空置", num: item.Free, rate: item.FreeRate });
                    g2("grapy" + idx, data);
                });

            } else {
                layer.msg("获取数据失败！");
            }
        });
    });
}

function g2(id, data) {
    $("#statistics-graph").append("<div class='col-lg-6 col-md-6 col-sm-6'><h4>dddd</h4><div id='" + id + "' style='width:" + $(".container").width() / 2 + "px;'></div></div>");
    var chart = new G2.Chart({
        container: id,
        height: 300,
        forceFit: true
    });
    chart.source(data
        // , {
        // percent: {
        //     formatter: function formatter(val) {
        //         val = val * 100 + '%';
        //         return val;
        //     }
        //     }
        // }
    );
    chart.coord('theta', {
        radius: 1
    });
    chart.tooltip({
        showTitle: false,
        itemTpl: '<li>{name}: {value}</li>'
    });
    chart.legend("name", {
        position: 'left-middle',
        itemGap: 40
            // title: {
            //     text: 'ad',
            //     textAlign: 'start', // 文本对齐方向，可取值为： start middle end
            //     fill: '#404040', // 文本的颜色
            //     fontSize: '12', // 文本大小
            //     fontWeight: 'bold', // 文本粗细
            //     rotate: 30, // 文本旋转角度，以角度为单位，仅当 autoRotate 为 false 时生效
            //     textBaseline: 'top' // 文本基准线，可取 top middle bottom，默认为middle
            // }
    });
    chart.intervalStack().position('rate').color('name').label('rate', {
        offset: 5,
        formatter: (val, item) => {
            return item.point.name + ': ' + val + '%';
        }
    }).tooltip('name*rate', function(item, percent) {
        percent = percent + '%';
        return {
            name: item,
            value: percent
        };
    }).style({
        lineWidth: 1,
        stroke: '#fff'
    });
    chart.render();
    $("#statistics-graph>div>div").css("width", "");
}