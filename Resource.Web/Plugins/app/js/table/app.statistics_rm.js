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
        search2(paramsSet());
    });
};

function search(params) {
    ajax("get", $('#search').attr('data-url'), params, "json", function (data) {
        var html = template('tableTpl', data);
        $("#tb").html(html);

        var html1 = template('tableTpl1', data);
        $("#tb1").html(html1);

        $("#TotBuildSize").text("");
        $("#TotBuildRate").text("");
        $("#OccupancySize").text("");
        $("#OccupancyRate").text("");
        $("#ReserveSize").text("");
        $("#ReserveRate").text("");
        $("#RemainderSize").text("");
        $("#RemainderRate").text("");
        $("#DisableSize").text("");
        $("#DisableRate").text("");

        $.each(data["data1"], function (index, value) {
            $("#TotBuildSize").text(value.TotBuildSize);
            $("#TotBuildRate").text(value.TotBuildRate);
            $("#OccupancySize").text(value.OccupancySize);
            $("#OccupancyRate").text(value.OccupancyRate);
            $("#ReserveSize").text(value.ReserveSize);
            $("#ReserveRate").text(value.ReserveRate);
            $("#RemainderSize").text(value.RemainderSize);
            $("#RemainderRate").text(value.RemainderRate);
            $("#DisableSize").text(value.DisableSize);
            $("#DisableRate").text(value.DisableRate);
        });
    });

    ajax("get", "/StatisticsList_RM/Search2", {}, "json", function (data) {
        if (tab == "list") $("#chartsdiv").show();
        var chartsdata = [];
        $.each(data["data"], function (index, value) {
            var row = {};
            row[0] = value.StDate;
            row[1] = value.OccupancySize;
            chartsdata.push(row);
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
        if (tab == "list") $("#chartsdiv").hide(1);
    });
    
    ajax("get", "/StatisticsList_RM/Search3", {}, "json", function (data) {
        if (tab == "list") $("#chartsdiv").show();
        var chartsdata = [];
        var rows = [];
        var rows1 = [];
        $.each(data["data"], function (index, value) {
            rows.push([value.Months, value.OccupancyRate]);
            rows1.push([value.Months, value.RemainderRate]);
        });
        
        var list = { };
        list.label = "总出租率";
        list.data = rows;
        chartsdata.push(list);

        list = {};
        list.label = "总空置率";
        list.data = rows1;
        chartsdata.push(list);
        
        var options = {
            series: {
                lines: {
                    show: true
                },
                points: {
                    show: true
                }
            },
            legend: {
                noColumns: 2
            },
            xaxis: {
                tickDecimals: 0
            },
            yaxis: {
                min: 0
            },
            selection: {
                mode: "x"
            }
        };

        var placeholder = $("#placeholder1");
        var plot = $.plot(placeholder, chartsdata, options);
        if (tab == "list") $("#chartsdiv").hide(1);
    });
};

function search1(params) {
    ajax("get", $('#search').attr('data-url'), params, "json", function (data) {
        id = "";
        var html = template('tableTpl', data);
        $("#tb").html(html);
    });
};

function search2(params) {
    ajax("get", "/StatisticsList_RM/Search1", params, "json", function (data) {
        var html1 = template('tableTpl1', data);
        $("#tb1").html(html1);

        $("#TotBuildSize").text("");
        $("#TotBuildRate").text("");
        $("#OccupancySize").text("");
        $("#OccupancyRate").text("");
        $("#ReserveSize").text("");
        $("#ReserveRate").text("");
        $("#RemainderSize").text("");
        $("#RemainderRate").text("");
        $("#DisableSize").text("");
        $("#DisableRate").text("");
        
        $.each(data["data"], function (index, value) {
            $("#TotBuildSize").text(value.TotBuildSize);
            $("#TotBuildRate").text(value.TotBuildRate);
            $("#OccupancySize").text(value.OccupancySize);
            $("#OccupancyRate").text(value.OccupancyRate);
            $("#ReserveSize").text(value.ReserveSize);
            $("#ReserveRate").text(value.ReserveRate);
            $("#RemainderSize").text(value.RemainderSize);
            $("#RemainderRate").text(value.RemainderRate);
            $("#DisableSize").text(value.DisableSize);
            $("#DisableRate").text(value.DisableRate);
        });


        ajax("get", "/StatisticsList_RM/Search2", params, "json", function (data) {
            if (tab == "list") $("#chartsdiv").show();
            var chartsdata = [];
            $.each(data["data"], function (index, value) {
                var row = {};
                row[0] = value.StDate;
                row[1] = value.OccupancySize;
                chartsdata.push(row);
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
            if (tab == "list") $("#chartsdiv").hide(1);
        });

        ajax("get", "/StatisticsList_RM/Search3", params, "json", function (data) {
            if (tab == "list") $("#chartsdiv").show();
            var chartsdata = [];
            var rows = [];
            var rows1 = [];
            $.each(data["data"], function (index, value) {
                rows.push([value.Months, value.OccupancyRate]);
                rows1.push([value.Months, value.RemainderRate]);
            });

            var list = {};
            list.label = "总出租率";
            list.data = rows;
            chartsdata.push(list);

            list = {};
            list.label = "总空置率";
            list.data = rows1;
            chartsdata.push(list);

            var options = {
                series: {
                    lines: {
                        show: true
                    },
                    points: {
                        show: true
                    }
                },
                legend: {
                    noColumns: 2
                },
                xaxis: {
                    tickDecimals: 0
                },
                yaxis: {
                    min: 0
                },
                selection: {
                    mode: "x"
                }
            };

            var placeholder = $("#placeholder1");
            var plot = $.plot(placeholder, chartsdata, options);
            if (tab == "list") $("#chartsdiv").hide(1);
        });
    });

    search1(params);
};
