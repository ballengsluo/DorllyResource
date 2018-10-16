$(function() {
    search();
    imp();
    $("#search").trigger("click");
});

function search() {
    $('#search').click(function() {
        var start = $('#stime').val();
        var end = $('#etime').val();
        if (start.length > 0 || end.length > 0) {
            if (new Date(start.replace("-", "/").replace("-", "/")) > new Date(end.replace("-", "/").replace("-", "/"))) {
                layer.msg("结束时间比开始时间小！");
                return false;
            }
        }
        ajax('get', $('#search').attr('data-url'), {
            park: $('#park').val(),
            beginTime: $('#stime').val(),
            endTime: $('#etime').val()
        }, 'json', function(data) {
            if (data.length > 0) {
                var title = "";
                $(".statistics-rate>span:first-of-type").html(0);
                $(".statistics-num>span:first-of-type").html(0);
                $("#statistics-graph-pie").html("");
                $.each(data, function(idx, item) {
                    if (item.Kind == 1) {
                        $("#rm-total").html(item.Total);
                        $("#rm-rent").html(item.Rent);
                        $("#rm-rentRate").html(item.RentRate);
                        $("#rm-self").html(item.Self);
                        $("#rm-selfRate").html(item.SelfRate);
                        $("#rm-free").html(item.Free);
                        $("#rm-freeRate").html(item.FreeRate);
                        title = "房屋使用率";
                    } else if (item.Kind == 2) {
                        $("#wp-total").html(item.Total);
                        $("#wp-rent").html(item.Rent);
                        $("#wp-rentRate").html(item.RentRate);
                        $("#wp-self").html(item.Self);
                        $("#wp-selfRate").html(item.SelfRate);
                        $("#wp-free").html(item.Free);
                        $("#wp-freeRate").html(item.FreeRate);
                        title = "工位使用率";
                    } else if (item.Kind == 3) {
                        $("#cr-total").html(item.Total);
                        $("#cr-rent").html(item.Rent);
                        $("#cr-rentRate").html(item.RentRate);
                        $("#cr-self").html(item.Self);
                        $("#cr-selfRate").html(item.SelfRate);
                        $("#cr-free").html(item.Free);
                        $("#cr-freeRate").html(item.FreeRate);
                        title = "会议室使用率";
                    } else if (item.Kind == 4) {
                        $("#ad-total").html(item.Total);
                        $("#ad-rent").html(item.Rent);
                        $("#ad-rentRate").html(item.RentRate);
                        $("#ad-self").html(item.Self);
                        $("#ad-selfRate").html(item.SelfRate);
                        $("#ad-free").html(item.Free);
                        $("#ad-freeRate").html(item.FreeRate);
                        title = "广告位使用率";
                    }
                    var data = new Array();
                    data.push({ name: "客户租赁", value: item.RentRate });
                    data.push({ name: "内部使用", value: item.SelfRate });
                    data.push({ name: "空置", value: item.FreeRate });
                    graphPie("graph-" + idx, 2, title, data);
                });

            } else {
                layer.msg("获取数据失败！");
            }
        });
    });
}

function imp() {
    $('#import').click(function() {
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