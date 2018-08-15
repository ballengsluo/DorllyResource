$(function() {
    $.post("/s_rm/search", { park: $("#park").val() }, function(data) {
        console.log(data);
        execrm(data);
    }, 'json');
    dropChange(2);
    $("#rent").click(function() {
        search()
    });
    $("#free").click(function() {
        search();
    });
    $("#rmno").keyup(function() {
        search();
    });
    $("#cust").keyup(function() {
        search();
    });

});

function execrm(data) {
    var buildul = $("<ul></ul>");

    // 园区数据处理
    // var parkSelect = "";
    // $.each(data.park, function(idx, park) {
    //     parkSelect = parkSelect + "<option value='" + park.ID + "'>" + park.Name + "</option>";
    // });
    // $("#park").html(parkSelect).selectpicker("refresh");

    // 房间数据处理
    $.each(data, function(idx, build) {
        var rmc = $("<div data-build='" + build.BID + "'></div>");
        buildul.append("<li data-id='" + build.BID + "'>" + build.BName + "</li>");
        $.each(build.Floor, function(idx, floor) {
            rmc.append("<div class='row'><h3>" + floor.FName + "</h3><hr /></div>");
            var rmbox = $("<div class='row'></div>")
            $.each(floor.RM, function(idx, rm) {
                rmbox.append("<div class='rmbox' style='' data-status='" + rm.Status +
                    "' data-id='" + rm.ID +
                    "' data-rmbuild='" + build.BID +
                    "' data-rmname='" + rm.Name +
                    "' data-rmcust='" + rm.CustName +
                    "'>" +
                    "<div class='rmno'>" + rm.Name + "</div>" +
                    "<div class='cust'>" + rm.CustName + "</div>" +
                    "<div class='area'>" + rm.RentArea + "<span>㎡</span></div>" +
                    "</div>");
                var dtbox = $("<div class='detail' data-rid='" + rm.ID + "'>" +
                    "<p><span>资源编号：</span><span>" + rm.ID + "</span></p>" +
                    "<p><span>资源面积：</span><span>" + rm.RentArea + "㎡</span></p>" +
                    "<p><span>公司(个人)：</span><span>" + rm.CustName + "</span></p>" +
                    "<p><span>联系电话：</span><span>" + rm.CustPhone + "</span></p>" +
                    "<p><span>租赁日期：</span><span>" + rm.RentBeginTime + " - " + rm.RentEndTime + "</span></p>" +
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
    $("div[data-status='1']").addClass("rmrent");
    $("div[data-status='2']").addClass("rmfree");
    hoverShow();
}

function changeBuild() {
    $(".building li").click(function() {
        $(".building li").removeClass("buildingClick");
        var $this = $(this);
        $this.addClass("buildingClick");
        $("div[data-build]").hide();
        $("div[data-build='" + $this.attr("data-id") + "']").show();
        // $("div[data-build='" + $this.attr("data-id") + "'] div").show();
        // $("input[type='checkbox']").prop("checked", "true");
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

function search() {
    if ($("#rent").prop("checked")) {
        $("[data-status='1']").show();
    } else {
        $("[data-status='1']").hide();
    }
    if ($("#free").prop("checked")) {
        $("[data-status='2']").show();
    } else {
        $("[data-status='2']").hide();
    }
    if ($("#rmno").val() != "" || !!$("#rmno").val()) {
        // console.log($("#rmno").val());
        // $("div[data-rmname]:not([data-rmname*='" + $("#rmno").val() + "']):visible").hide();
        $(".rmbox:not([style]:not([style*='display: none'])[data-rmname*='" + $("#rmno").val() + "'])").hide();
    }
    if ($("#cust").val() != "" || !!$("#cust").val()) {
        $(".rmbox:not([style]:not([style*='display: none'])[data-rmcust*='" + $("#cust").val() + "'])").hide();
        // $("[data-rmcust]:not([data-rmcust*='" + $("#cust").val() + "'])&&[style]:not([style*='display: none'])").hide();
        // $("div[data-rmcust*='" + $("#cust").val() + "']:visible").show();
    }
}