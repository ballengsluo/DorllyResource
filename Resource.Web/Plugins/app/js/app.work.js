$(function() {
    initResourceData();
    initOrderData();
    initTransaction();
});

function initResourceData() {
    // $("#rm").height($("#ad").outerHeight() + $("#cb").outerHeight() + $("#mr").outerHeight() - 1);
    $("#park").change(function() {
        ajax('get', "/Admin/GetResourceData", { park: $(this).val() }, 'json', function(data) {
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
        });
    });
    $("#park").trigger("change");
    // $("#park option:first").attr("selected", true).trigger("change");
}

function initTransaction() {
    if ($("#transaction").length > 0) {
        ajax("get", "/Admin/GetTransactionData", {}, "json", function(data) {
            var html = template('transTpl', data);
            $("#trans-table").html(html);
            $("#trans-table a").click(function() {
                var url = $(this).attr("data-url")
                var index = layer.open({
                    type: 2,
                    content: url,
                    title: ' ',
                    end: function() {
                        initTransaction();
                    }
                });
                layer.full(index);
            });
        });
    }
}

function initOrderData() {
    if ($("#order-graph").length > 0) {
        if ($("#stime").length > 0) {
            layui.use('laydate', function() {
                var laydate = layui.laydate;
                laydate.render({
                    elem: '#stime'
                });
            });
        }
        if ($("#etime").length > 0) {
            layui.use('laydate', function() {
                var laydate = layui.laydate;
                laydate.render({
                    elem: '#etime'
                });
            });
        }
        graph();
        $("#search").click(function() {
            graph();
        });
    }
}

function graph() {
    $.getJSON("/Admin/GetOrderData", {
        stime: $("#stime").val(),
        etime: $("#etime").val()
    }, function(data) {
        console.log(data);
        var title = new Array();
        var count = new Array();
        $.each(data, function(idx, item) {
            $("[data-kind='" + item.RID + "']").html(item.RCount);
            title.push(item.RName);
            count.push(item.RCount);
        });
        graphData(title, count);
    });
}

function graphData(title, count) {
    // 基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('order-graph'));
    // 指定图表的配置项和数据
    var option = {
        color: ['blue'],
        title: {
            text: ''
        },
        tooltip: {},
        legend: {
            data: ['销量']
        },
        xAxis: {
            data: title
        },
        yAxis: {},
        series: [{
            type: 'bar',
            data: count
        }]
    };
    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
}