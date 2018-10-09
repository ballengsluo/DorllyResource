$(function() {
    $('#search').click(function() {
        search(paramsSet());
    });
    search(paramsSet());
    // $.post("/s_rm/search", { park: 'FTYQ' }, function(data) {
    //     console.log(data);
    //     execrm(data);
    // }, 'json');

    // $("#rent").click(function() {
    //     search()
    // });
    // $("#free").click(function() {
    //     search();
    // });
    // $("#rmno").keyup(function() {
    //     search();
    // });
    // $("#cust").keyup(function() {
    //     search();
    // });
});


function search(params) {
    ajax("get", $('#search').attr('data-url'), params, "json", function(data) {
        // console.log(data);
        execData(data);
    });
};

function paramsSet() {
    return obj = {
        park: $("#park").val(),
        name: $("#name").val(),
        status: $("#status").val(),
        cust: $("#cust").val()
    };
};

function execData(data) {
    //清空数据
    $("#build").html("");
    $(".resourceContainer").html("");
    $(".detailContainer").html("");
    $("#park").selectpicker('val', data.park);
    //检索数据
    $.each(data.blist, function(bx, build) { //楼栋
        $("#build").append("<button type='button' class='btn btn-default' data-id='" + build.ID + "'>" + build.Name + "</button>");
        var bbox = $("<div data-build='" + build.ID + "' style='display:none;'></div>");;
        // if (bx == 0) {
        //     bbox = $("<div data-build='" + build.ID + "'></div>");
        // } else {
        //     bbox = $("<div data-build='" + build.ID + "' style='display:none;'></div>");
        // }

        //楼层数据
        var flist = $.grep(data.flist, function(obj, i) {
            return build.ID == obj.BID;
        });
        $.each(flist, function(fx, floor) { //楼层
            var fbox = $("<div data-floor='" + floor.ID + "' class='row'></div>");
            fbox.append("<h3>" + floor.Name + "</h3>");
            //房间数据
            var rmlist = $.grep(data.rmlist, function(obj, i) {
                return floor.ID == obj.Loc4;
            });
            $.each(rmlist, function(rx, rm) { //房间
                fbox.append("<div class='rmbox' data-status='" + rm.Status +
                    "' data-id='" + rm.ID +
                    "'>" +
                    "<div class='rmno'>" + rm.Name + "</div>" +
                    "<div class='cust'>" + rm.CustShortName + "</div>" +
                    "<div class='area'>" + rm.RentArea + "<span>㎡</span></div>" +
                    "</div>");
                $(".detailContainer").append("<div class='detail' data-rid='" + rm.ID + "'>" +
                    "<p><span>资源编号：</span><span>" + rm.ID + "</span></p>" +
                    "<p><span>资源面积：</span><span>" + rm.RentArea + "㎡</span></p>" +
                    "<p><span>公司(个人)：</span><span>" + rm.CustShortName + "</span></p>" +
                    "<p><span>联系电话：</span><span>" + rm.CustTel + "</span></p>" +
                    "<p><span>开始日期：</span><span>" + rm.RentBeginTime + "</span></p>" +
                    "<p><span>结束日期：</span><span>" + rm.RentEndTime + "</span></p>" +
                    "</div>");
            });
            bbox.append(fbox);

        });
        $(".resourceContainer").append(bbox);
    });
    //绑定事件
    selectBuild();
    hoverShow();
    $("#build>button:first-of-type").css("background-color", "#e6e6e6").trigger("click");
}

function selectBuild() {
    $("#build>button").click(function() {
        $("#build>button").css("background-color", "");
        $(this).css("background-color", "#e6e6e6");
        $("div[data-build]").hide();
        $("div[data-build='" + $(this).attr("data-id") + "']").show();
    });
}


function execrm(data) {
    var buildul = $("<ul></ul>");
    console.log(data);
    $.each(data, function(idx, build) {

        var rmc = $("<div data-build='" + build.BID + "'></div>");
        buildul.append("<li data-id='" + build.BID + "'>" + build.BName + "</li>");
        $.each(build.Floor, function(idx, floor) {
            rmc.append("<div class='row'><h3>" + floor.FName + "</h3><hr /></div>");
            var rmbox = $("<div class='row'></div>")
            $.each(floor.RM, function(idx, rm) {
                rmbox.append("<div class='rmbox' style='' data-status='" + rm.RentStatus +
                    "' data-id='" + rm.ID +
                    "' data-rmbuild='" + build.BID +
                    "' data-rmname='" + rm.Name +
                    "' data-rmcust='" + rm.CustShortName +
                    "'>" +
                    "<div class='rmno'>" + rm.Name + "</div>" +
                    "<div class='cust'>" + rm.CustShortName + "</div>" +
                    "<div class='area'>" + rm.RentArea + "<span>㎡</span></div>" +
                    "</div>");
                var dtbox = $("<div class='detail' data-rid='" + rm.ID + "'>" +
                    "<p><span>资源编号：</span><span>" + rm.ID + "</span></p>" +
                    "<p><span>资源面积：</span><span>" + rm.RentArea + "㎡</span></p>" +
                    "<p><span>公司(个人)：</span><span>" + rm.CustShortName + "</span></p>" +
                    "<p><span>联系电话：</span><span>" + rm.CustTel + "</span></p>" +
                    "<p><span>开始日期：</span><span>" + rm.RentBeginTime + "</span></p>" +
                    "<p><span>结束日期：</span><span>" + rm.RentEndTime + "</span></p>" +
                    "</div>");
                $(".detailContainer").append(dtbox);

            });
            rmc.append(rmbox);
        });
        $(".resourceContainer").append(rmc);
    });
    $(".building").html(buildul);
    changeBuild();
    $(".building li:first-of-type").click();
    hoverShow();
}

function changeBuild() {
    $(".building li").click(function() {
        $(".building li").removeClass("buildingClick");
        var $this = $(this);
        $this.addClass("buildingClick");
        $("div[data-build]").hide();
        $("div[data-build='" + $this.attr("data-id") + "']").show();
    });
}

function hoverShow() {
    $(".rmbox").hover(function() {
        layer.tips($("div[data-rid='" + $(this).attr("data-id") + "']").html(),
            "div[data-id='" + $(this).attr("data-id") + "']", {
                time: 60000
            });
        $(".layui-layer-tips").width('220px');
    }, function() {
        layer.close(layer.index);
    });
}

// function search() {
//     if ($("#rent").prop("checked")) {
//         $("[data-status='1']").show();
//     } else {
//         $("[data-status='1']").hide();
//     }
//     if ($("#free").prop("checked")) {
//         $("[data-status='3']").show();
//     } else {
//         $("[data-status='3']").hide();
//     }
//     if ($("#rmno").val() != "" || !!$("#rmno").val()) {
//         // console.log($("#rmno").val());
//         // $("div[data-rmname]:not([data-rmname*='" + $("#rmno").val() + "']):visible").hide();
//         $(".rmbox:not([style]:not([style*='display: none'])[data-rmname*='" + $("#rmno").val() + "'])").hide();
//     }
//     if ($("#cust").val() != "" || !!$("#cust").val()) {
//         $(".rmbox:not([style]:not([style*='display: none'])[data-rmcust*='" + $("#cust").val() + "'])").hide();
//         // $("[data-rmcust]:not([data-rmcust*='" + $("#cust").val() + "'])&&[style]:not([style*='display: none'])").hide();
//         // $("div[data-rmcust*='" + $("#cust").val() + "']:visible").show();
//     }
// }