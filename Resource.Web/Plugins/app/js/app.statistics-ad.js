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
        var parkContainer = $("<div class='statistics-panel'></div>");
        $.each(parkList, function(idx, park) {
            var parkHtml = $("<div class='row'></div>");
            parkHtml.append("<div class='col-lg-1 col-md-1 statistics-title'>" + park.ParkName + "</div>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-total'><div class='statistics-name'>广告位总数</div><div class='statistics-num'><span>" + park.Total + "</span><span>个</span></div></div>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-rent'><div class='statistics-name'>客户租赁个数</div><div class='statistics-rate'><span>" + park.RentRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Rent + "</span><span>个</span></div></div>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-self'><div class='statistics-name'>内部使用个数</div><div class='statistics-rate'><span>" + park.SelfRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Self + "</span><span>个</span></div></div>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-free'><div class='statistics-name'>空置个数</div><div class='statistics-rate'><span>" + park.FreeRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Free + "</span><span>个</span></div></div>");
            parkContainer.append(parkHtml);
        });
        $("#statistics-panel").html(parkContainer);
    } else {
        $("#statistics-panel").html(" <div style='text-align:center;line-height:300px;'>数据异常！</div>");
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