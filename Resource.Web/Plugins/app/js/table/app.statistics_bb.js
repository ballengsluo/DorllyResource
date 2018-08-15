$(function() {
    operation();
    $("#park").val("01");

    search(paramsSet());
    search1(paramsSet());
    $("#chartsdiv").hide(1);
});

function operation() {
    $('#search').click(function() {
        search1(paramsSet());
    });
    $("#park").change(function() {
        search2(paramsSet());
    });
};

function search(params) {
    ajax("get", $('#search').attr('data-url'), params, "json", function(data) {
        //var html = template('tableTpl', data);
        //$("#tb").html(html);

        $("#TotBuildSize").text("");
        $("#TotBuildRate").text("");
        $("#OccupancySize").text("");
        $("#OccupancyRate").text("");
        $("#RemainderSize").text("");
        $("#RemainderRate").text("");
        $("#DisableSize").text("");
        $("#DisableRate").text("");


        if (tab == "list") $("#chartsdiv").show();

        var chartsdata = [];
        $.each(data["data"], function(index, value) {
            $("#TotBuildSize").text(value.TotBuildSize);
            $("#TotBuildRate").text(value.TotBuildRate);
            $("#OccupancySize").text(value.OccupancySize);
            $("#OccupancyRate").text(value.OccupancyRate);
            $("#RemainderSize").text(value.RemainderSize);
            $("#RemainderRate").text(value.RemainderRate);
            $("#DisableSize").text(value.DisableSize);
            $("#DisableRate").text(value.DisableRate);

            var row = {};
            row.label = "已租个数";
            row.data = value.OccupancySize;
            chartsdata.push(row);

            row = {};
            row.label = "空置个数";
            row.data = value.RemainderSize;
            chartsdata.push(row);
        });

        var placeholder = $("#placeholder");
        placeholder.unbind();
        $.plot(placeholder, chartsdata, {
            series: {
                pie: {
                    show: true,
                    radius: 1,
                    label: {
                        show: true,
                        radius: 3 / 4,
                        formatter: labelFormatter,
                        background: {
                            opacity: 0.5
                        }
                    }
                }
            }
        });
        if (tab == "list") $("#chartsdiv").hide(300);

    });
};

function search1(params) {
    ajax("get", $('#search').attr('data-url'), params, "json", function(data) {
        id = "";
        var html = template('tableTpl', data);
        $("#tb").html(html);
    });
};

function search2(params) {
    ajax("get", "/StatisticsList_WP/Search1", params, "json", function(data) {

        $("#TotBuildSize").text("");
        $("#TotBuildRate").text("");
        $("#OccupancySize").text("");
        $("#OccupancyRate").text("");
        $("#RemainderSize").text("");
        $("#RemainderRate").text("");
        $("#DisableSize").text("");
        $("#DisableRate").text("");

        $.each(data["data"], function(index, value) {
            $("#TotBuildSize").text(value.TotBuildSize);
            $("#TotBuildRate").text(value.TotBuildRate);
            $("#OccupancySize").text(value.OccupancySize);
            $("#OccupancyRate").text(value.OccupancyRate);
            $("#RemainderSize").text(value.RemainderSize);
            $("#RemainderRate").text(value.RemainderRate);
            $("#DisableSize").text(value.DisableSize);
            $("#DisableRate").text(value.DisableRate);
        });
    });

    search1(params);
};

function labelFormatter(label, series) {
    return "<div style='font-size:8pt; text-align:center; padding:2px; color:white;'>" + label + "<br/>" + Math.round(series.percent) + "%</div>";
}