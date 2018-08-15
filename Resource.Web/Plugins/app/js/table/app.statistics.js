$(function() {
    // layui.use('layer', function() {
    //     layer = layui.layer;
    // });

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
        $("#TotBuildSize_WP").text("");
        $("#TotBuildRate_WP").text("");
        $("#OccupancySize_WP").text("");
        $("#OccupancyRate_WP").text("");
        $("#RemainderSize_WP").text("");
        $("#RemainderRate_WP").text("");
        $("#DisableSize_WP").text("");
        $("#DisableRate_WP").text("");
        $("#TotBuildSize_CR").text("");
        $("#TotBuildRate_CR").text("");
        $("#OccupancySize_CR").text("");
        $("#OccupancyRate_CR").text("");
        $("#ReserveSize_CR").text("");
        $("#ReserveRate_CR").text("");
        $("#DisableSize_CR").text("");
        $("#DisableRate_CR").text("");
        $("#TotBuildSize_BB").text("");
        $("#TotBuildRate_BB").text("");
        $("#OccupancySize_BB").text("");
        $("#OccupancyRate_BB").text("");
        $("#RemainderSize_BB").text("");
        $("#RemainderRate_BB").text("");
        $("#DisableSize_BB").text("");
        $("#DisableRate_BB").text("");

        if (tab == "list") $("#chartsdiv").show();

        var chartsdata = [];
        var chartsdata1 = [];
        var chartsdata2 = [];
        var chartsdata3 = [];
        $.each(data["data"], function (index, value) {
            if (value.ResType == "1") {
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

                var row = {};
                row.label = "入住面积";
                row.data = value.OccupancySize;
                chartsdata.push(row);

                row = {};
                row.label = "招商面积";
                row.data = 0;
                chartsdata.push(row);

                row = {};
                row.label = "剩余面积";
                row.data = value.RemainderSize;
                chartsdata.push(row);
            }
            else if (value.ResType == "2") {
                $("#TotBuildSize_WP").text(value.TotBuildSize);
                $("#TotBuildRate_WP").text(value.TotBuildRate);
                $("#OccupancySize_WP").text(value.OccupancySize);
                $("#OccupancyRate_WP").text(value.OccupancyRate);
                $("#RemainderSize_WP").text(value.RemainderSize);
                $("#RemainderRate_WP").text(value.RemainderRate);
                $("#DisableSize_WP").text(value.DisableSize);
                $("#DisableRate_WP").text(value.DisableRate);

                var row = {};
                row.label = "租赁总个数";
                row.data = value.TotRentSize;
                chartsdata1.push(row);

                row = {};
                row.label = "已租个数";
                row.data = value.OccupancySize;
                chartsdata1.push(row);

                row = {};
                row.label = "空置个数";
                row.data = value.RemainderSize;
                chartsdata1.push(row);
            }
            else if (value.ResType == "3") {
                $("#TotBuildSize_CR").text(value.TotBuildSize);
                $("#TotBuildRate_CR").text(value.TotBuildRate);
                $("#OccupancySize_CR").text(value.OccupancySize);
                $("#OccupancyRate_CR").text(value.OccupancyRate);
                $("#ReserveSize_CR").text(value.ReserveSize);
                $("#ReserveRate_CR").text(value.ReserveRate);
                $("#DisableSize_CR").text(value.DisableSize);
                $("#DisableRate_CR").text(value.DisableRate);

                var row = {};
                row.label = "租赁总次数";
                row.data = value.TotRentSize;
                chartsdata2.push(row);

                row = {};
                row.label = "已租次数";
                row.data = value.OccupancySize;
                chartsdata2.push(row);

                row = {};
                row.label = "内部使用次数";
                row.data = value.ReserveSize;
                chartsdata2.push(row);
            }
            else if (value.ResType == "4") {
                $("#TotBuildSize_BB").text(value.TotBuildSize);
                $("#TotBuildRate_BB").text(value.TotBuildRate);
                $("#OccupancySize_BB").text(value.OccupancySize);
                $("#OccupancyRate_BB").text(value.OccupancyRate);
                $("#RemainderSize_BB").text(value.RemainderSize);
                $("#RemainderRate_BB").text(value.RemainderRate);
                $("#DisableSize_BB").text(value.DisableSize);
                $("#DisableRate_BB").text(value.DisableRate);

                var row = {};
                row.label = "租赁总个数";
                row.data = value.TotRentSize;
                chartsdata3.push(row);

                row = {};
                row.label = "已租个数";
                row.data = value.OccupancySize;
                chartsdata3.push(row);

                row = {};
                row.label = "空置个数";
                row.data = value.RemainderSize;
                chartsdata3.push(row);
            }
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

        var placeholder1 = $("#placeholder1");
        placeholder1.unbind();
        $.plot(placeholder1, chartsdata1, {
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

        var placeholder2 = $("#placeholder2");
        placeholder2.unbind();
        $.plot(placeholder2, chartsdata2, {
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

        var placeholder3 = $("#placeholder3");
        placeholder3.unbind();
        $.plot(placeholder3, chartsdata3, {
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
    ajax("get", "/StatisticsList/Search1", params, "json", function (data) {
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
        $("#TotBuildSize_WP").text("");
        $("#TotBuildRate_WP").text("");
        $("#OccupancySize_WP").text("");
        $("#OccupancyRate_WP").text("");
        $("#RemainderSize_WP").text("");
        $("#RemainderRate_WP").text("");
        $("#DisableSize_WP").text("");
        $("#DisableRate_WP").text("");
        $("#TotBuildSize_CR").text("");
        $("#TotBuildRate_CR").text("");
        $("#OccupancySize_CR").text("");
        $("#OccupancyRate_CR").text("");
        $("#ReserveSize_CR").text("");
        $("#ReserveRate_CR").text("");
        $("#DisableSize_CR").text("");
        $("#DisableRate_CR").text("");
        $("#TotBuildSize_BB").text("");
        $("#TotBuildRate_BB").text("");
        $("#OccupancySize_BB").text("");
        $("#OccupancyRate_BB").text("");
        $("#RemainderSize_BB").text("");
        $("#RemainderRate_BB").text("");
        $("#DisableSize_BB").text("");
        $("#DisableRate_BB").text("");

        if (tab == "list") $("#chartsdiv").show();

        var chartsdata = [];
        var chartsdata1 = [];
        var chartsdata2 = [];
        var chartsdata3 = [];
        $.each(data["data"], function (index, value) {
            if (value.ResType == "1") {
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

                var row = {};
                row.label = "入住面积";
                row.data = value.OccupancySize;
                chartsdata.push(row);

                row = {};
                row.label = "招商面积";
                row.data = 0;
                chartsdata.push(row);

                row = {};
                row.label = "剩余面积";
                row.data = value.RemainderSize;
                chartsdata.push(row);
            }
            else if (value.ResType == "2") {
                $("#TotBuildSize_WP").text(value.TotBuildSize);
                $("#TotBuildRate_WP").text(value.TotBuildRate);
                $("#OccupancySize_WP").text(value.OccupancySize);
                $("#OccupancyRate_WP").text(value.OccupancyRate);
                $("#RemainderSize_WP").text(value.RemainderSize);
                $("#RemainderRate_WP").text(value.RemainderRate);
                $("#DisableSize_WP").text(value.DisableSize);
                $("#DisableRate_WP").text(value.DisableRate);

                var row = {};
                row.label = "租赁总个数";
                row.data = value.TotRentSize;
                chartsdata1.push(row);

                row = {};
                row.label = "已租个数";
                row.data = value.OccupancySize;
                chartsdata1.push(row);

                row = {};
                row.label = "空置个数";
                row.data = value.RemainderSize;
                chartsdata1.push(row);
            }
            else if (value.ResType == "3") {
                $("#TotBuildSize_CR").text(value.TotBuildSize);
                $("#TotBuildRate_CR").text(value.TotBuildRate);
                $("#OccupancySize_CR").text(value.OccupancySize);
                $("#OccupancyRate_CR").text(value.OccupancyRate);
                $("#ReserveSize_CR").text(value.ReserveSize);
                $("#ReserveRate_CR").text(value.ReserveRate);
                $("#DisableSize_CR").text(value.DisableSize);
                $("#DisableRate_CR").text(value.DisableRate);

                var row = {};
                row.label = "租赁总次数";
                row.data = value.TotRentSize;
                chartsdata2.push(row);

                row = {};
                row.label = "已租次数";
                row.data = value.OccupancySize;
                chartsdata2.push(row);

                row = {};
                row.label = "内部使用次数";
                row.data = value.ReserveSize;
                chartsdata2.push(row);
            }
            else if (value.ResType == "4") {
                $("#TotBuildSize_BB").text(value.TotBuildSize);
                $("#TotBuildRate_BB").text(value.TotBuildRate);
                $("#OccupancySize_BB").text(value.OccupancySize);
                $("#OccupancyRate_BB").text(value.OccupancyRate);
                $("#RemainderSize_BB").text(value.RemainderSize);
                $("#RemainderRate_BB").text(value.RemainderRate);
                $("#DisableSize_BB").text(value.DisableSize);
                $("#DisableRate_BB").text(value.DisableRate);

                var row = {};
                row.label = "租赁总个数";
                row.data = value.TotRentSize;
                chartsdata3.push(row);

                row = {};
                row.label = "已租个数";
                row.data = value.OccupancySize;
                chartsdata3.push(row);

                row = {};
                row.label = "空置个数";
                row.data = value.RemainderSize;
                chartsdata3.push(row);
            }
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

        var placeholder1 = $("#placeholder1");
        placeholder1.unbind();
        $.plot(placeholder1, chartsdata1, {
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

        var placeholder2 = $("#placeholder2");
        placeholder2.unbind();
        $.plot(placeholder2, chartsdata2, {
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

        var placeholder3 = $("#placeholder3");
        placeholder3.unbind();
        $.plot(placeholder3, chartsdata3, {
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


        if (tab == "list") $("#chartsdiv").hide(1);
    });

    search1(params);
};

function labelFormatter(label, series) {
    return "<div style='font-size:8pt; text-align:center; padding:2px; color:white;'>" + label + "<br/>" + Math.round(series.percent) + "%</div>";
}