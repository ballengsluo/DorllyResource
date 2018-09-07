$(function() {
    $.post("/s_cb/search", '', function(data) {
        console.log(data);
        execrm(data);
    }, 'json')

});

function execrm(data) {
    var park = "";
    // 房间数据处理
    $.each(data, function(idx, cbtype) {
        var typebox = $("<div class='row cbtype'><h3>" + cbtype.TName + "</h3>");
        $.each(cbtype.RM, function(idx, rm) {
            var box = $("<div class='cbbox'><h4>" + rm.RName + "</h4></div>");
            var cbbox;
            if (cbtype.TName.indexOf("4") >= 0) {
                cbbox = $("<div class='cb fourcb'></div>");
            } else if (cbtype.TName.indexOf("6") >= 0) {
                cbbox = $("<div class='cb sixcb'></div>");
            } else if (cbtype.TName.indexOf("8") >= 0) {
                cbbox = $("<div class='cb eightcb'></div>");
            } else {
                cbbox = $("<div class='cb eightcb'></div>");
            }
            $.each(rm.CB, function(idx, cb) {
                park = cb.Loc1;
                cbbox.append("<div data-status='" + cb.Status +
                    "' data-id='" + cb.ID + "'></div>");
                var dtbox = $("<div class='detail' data-pid='" + cb.ID + "'>" +
                    "<p><span>资源编号：</span><span>" + cb.ID + "</span></p>" +
                    "<p><span>公司(个人)：</span><span>" + cb.CustShortName + "</span></p>" +
                    "<p><span>联系电话：</span><span>" + cb.CustTel + "</span></p>" +
                    "<p><span>开始日期：</span><span>" + cb.RentBeginTime + "</span></p>" +
                    "<p><span>结束日期：</span><span>" + cb.RentEndTime + "</span></p>" +
                    "</div>");
                $(".detailContainer").append(dtbox);

            });
            box.append(cbbox);
            typebox.append(box);
        });
        $(".resourceContainer").append(typebox);
    });
    clickShow();
    $("#park").selectpicker("val", park);
}



function clickShow() {
    $(".cb>div").click(function(e) {
        e.stopPropagation();
        $(".cb>div").removeClass("cbclick");
        $(this).addClass("cbclick");
        layer.tips($("div[data-pid='" + $(this).attr("data-id") + "']").html(),
            "div[data-id='" + $(this).attr("data-id") + "']", {
                time: 60000
            });
        $(".layui-layer-tips").width('260px');
    });
    $(document).click(function() {
        layer.close(layer.index);
        $(".cb>div").removeClass("cbclick");
    });
}