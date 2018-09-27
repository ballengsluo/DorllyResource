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
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-totalNum'><div class='statistics-name'>房间数</div><div class='statistics-num'><span>" + park.TotalNum + "</span><span>间</span></div></div>");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-total'><div class='statistics-name'>总面积</div><div class='statistics-num'><span>" + park.Total + "个");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-rent'><div class='statistics-name'>客户租赁面积</div><div class='statistics-rate'><span>" + park.RentRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Rent + "个");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-self'><div class='statistics-name'>内部使用面积</div><div class='statistics-rate'><span>" + park.SelfRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Self + "个");
            parkHtml.append("<div class='col-lg-2 col-md-2 statistics-free'><div class='statistics-name'>空置面积</div><div class='statistics-rate'><span>" + park.FreeRate + "</span><span>%</span></div><div class='statistics-num'><span>" + park.Free + "个");
            var buildList = $.grep(data.Part2, function(obj, idx) {
                return obj.ParkID == park.ParkID;
            });
            if (buildList.length > 0) {
                parkHtml.append("<div class='col-lg-1 col-md-1 statistics-detail'><div data-id='" + park.ParkID + "'>查看楼栋<i class='glyphicon glyphicon-menu-down'></i></div></div>");
                var buildContainer = $("<div class='statistics-panel' style='display:none;' data-pid='" + park.ParkID + "'></div>");
                $.each(buildList, function(idx, build) {
                    var buildHtml = $("<div class='row'></div>");
                    buildHtml.append("<div class='col-lg-offset-1 col-md-offset-1 col-lg-1 col-md-1 statistics-title'>" + build.Name + "</div>");
                    buildHtml.append("<div class='col-lg-2 col-md-2 statistics-totalNum'><div class='statistics-name'>房间数</div><div class='statistics-num'><span>" + build.TotalNum + "</span><span>间</span></div></div>");
                    buildHtml.append("<div class='col-lg-2 col-md-2 statistics-total'><div class='statistics-name'>总面积</div><div class='statistics-num'><span>" + build.Total + "个");
                    buildHtml.append("<div class='col-lg-2 col-md-2 statistics-rent'><div class='statistics-name'>客户租赁面积</div><div class='statistics-rate'><span>" + build.RentRate + "</span><span>%</span></div><div class='statistics-num'><span>" + build.Rent + "个");
                    buildHtml.append("<div class='col-lg-2 col-md-2 statistics-self'><div class='statistics-name'>内部使用面积</div><div class='statistics-rate'><span>" + build.SelfRate + "</span><span>%</span></div><div class='statistics-num'><span>" + build.Self + "个");
                    buildHtml.append("<div class='col-lg-2 col-md-2 statistics-free'><div class='statistics-name'>空置面积</div><div class='statistics-rate'><span>" + build.FreeRate + "</span><span>%</span></div><div class='statistics-num'><span>" + build.Free + "个");
                    buildContainer.append(buildHtml);
                });
            }
            parkContainer.append(parkHtml);
            parkContainer.append(buildContainer);
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