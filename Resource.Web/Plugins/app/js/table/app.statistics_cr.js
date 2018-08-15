$(function() {
    operation();
    $("#park").val("01");

    search(paramsSet());
    $("#chartsdiv").hide(1);
});

function operation() {
    $('#search').click(function () {
        search1(paramsSet());
    });
    $("#park").change(function () {
        search(paramsSet());
    });
};

function search(params) {
    ajax("get", $('#search').attr('data-url'), params, "json", function (data) {

        $("#TotBuildSize").text("");
        $("#TotBuildSize").text("");
        $("#TotBuildRate").text("");
        $("#OccupancySize").text("");
        $("#OccupancyRate").text("");
        $("#RemainderSize").text("");
        $("#RemainderRate").text("");

        $.each(data["data"], function (index, value) {
            $("#TotBuildSize").text(value.DisableSize);
            $("#TotBuildSize").text(value.TotBuildSize);
            $("#TotBuildRate").text(value.TotBuildRate);
            $("#OccupancySize").text(value.OccupancySize);
            $("#OccupancyRate").text(value.OccupancyRate);
            $("#RemainderSize").text(value.RemainderSize);
            $("#RemainderRate").text(value.RemainderRate);
        });
    });

    search1(params);
    
    ajax("get", "/StatisticsList_CR/Search2", params, "json", function (data) {
        if (tab == "list") $("#chartsdiv").show();

        var chartsdata = [];
        var chartsdata1 = [];
        $.each(data["data"], function (index, value) {
            var row = {};
            row[0] = value.CRName;
            row[1] = value.TotRentSize;
            chartsdata.push(row);

            row = {};
            row[0] = value.CRName;
            row[1] = value.OccupancyMi;
            chartsdata1.push(row);
        });

        $.plot("#placeholder", [chartsdata], {
            series: {
                bars: {
                    show: true,
                    barWidth: 0.6,
                    align: "center"
                }
            },
            xaxis: {
                mode: "categories",
                tickLength: 0
            }
        });


        $.plot("#placeholder1", [chartsdata1], {
            series: {
                bars: {
                    show: true,
                    barWidth: 0.6,
                    align: "center"
                }
            },
            xaxis: {
                mode: "categories",
                tickLength: 0
            }
        });


        if (tab == "list") $("#chartsdiv").hide(1);
    });
};

function search1(params) {
    ajax("get", "/StatisticsList_CR/Search3", params, "json", function (data) {
        id = "";
        var html = template('tableTpl', data);
        $("#tb").html(html);
    });
};

