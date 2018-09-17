$(function() {
    initResourceData();
    initOrderData();
    initTransaction();
});

function initResourceData() {
    // $("#rm").height($("#ad").outerHeight() + $("#cb").outerHeight() + $("#mr").outerHeight() - 1);
    // $("#park").change(function() {
    //     $.get("/Admin/GetResourceData", { park: $(this).val() }, function(data) {
    //         $("#resourceData").html(data);
    //         $("#rm").height($("#ad").outerHeight() + $("#cb").outerHeight() + $("#mr").outerHeight() - 1);
    //     });
    // });
    $("#park option:first").attr("selected", true).trigger("change");
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