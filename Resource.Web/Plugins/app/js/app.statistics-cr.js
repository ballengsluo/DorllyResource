$(function() {
    init();
    searchDay();
    $('#searchDay').trigger("click");
});

function searchDay() {
    $('#searchDay').click(function() {
        ajax('get', $(this).attr('data-url'), {
            beginTime: $('#stime').val(),
            endTime: $('#etime').val()
        }, 'json', function(data) {
            console.log(data);
            dealData(data);
        });
    });
}

function dealData(parkList) {
    if (parkList.Flag == 1) {
        var parkList;
        if ($("#park").val().length > 0) {
            parkList = $.grep(parkList.Part1, function(obj, i) {
                return obj.ParkID == $("#park").val();
            });
        } else {
            parkList = parkList.Part1;
        }
        if (parkList.length <= 0) {
            $("#tb").html("<td colspan='10' style='text-align:center;'>暂无数据！</td>");
            return false;
        }

        var html = template('tableTpl', { data: parkList });
        $("#tb").html(html);
    } else {
        $("#tb").html("<td colspan='10' style='text-align:center;'>数据异常！</td>");
    }

}

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