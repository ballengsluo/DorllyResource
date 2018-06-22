var pro = "FTYQ-1-01-001-K/101";
$(function () {
    setImg();
    $("#img-upload").change(function () {
        if ($("#product").val() == "" || !$("#product").val()) {
            layer.msg("请选择资源！");
        } else {
            ajax_form("/Upload/SaveImage", "POST", new FormData($(".img-container")[0]), "json", function (data) {
                if (data.result == 1) {
                    $(".img-container").prepend("<div class='img-box'>"
                        + "<span class='close-box'  data-id='" + data.id + "'></span>"
                        + "<img src='" + data.path + "'/>"
                        + "<div class='select-box'>"
                        + "<span class='radio-box'>"
                        + "<input type='radio' name='isCover' id='" + data.tempid + "' data-id='" + data.id + "' />"
                        + "<label for='" + data.tempid + "'></label>"
                        + "</span></div>"
                        + "</div>"
                    );
                    setImg();
                }
                layer.msg(data.msg, { icon: data.result });
            });
        }

    });

});
var setImg = function () {
    $(".close-box").off("click").on("click", function () {
        $this = $(this);
        ajax("/Upload/DeleteImage", "POST", { id: $this.attr("data-id") }, "json", 2, function (data) {
            if (data.result == 1) {
                $this.parent().remove();
            }
            layer.msg(data.msg, { icon: data.result });
        });

    });
    $("input[name=isCover]").off("change").on("change", function () {
        ajax("/Upload/UpdateImage", "GET", { id: $(this).attr("data-id") }, "json", 2, function (data) {
            layer.msg(data.msg, { icon: data.result });
        });
    });
}
function getObjectURL(file) {
    var url = null;
    if (window.createObjectURL != undefined) { // basic
        url = window.createObjectURL(file);
    } else if (window.URL != undefined) { // mozilla(firefox)
        url = window.URL.createObjectURL(file);
    } else if (window.webkitURL != undefined) { // webkit or chrome
        url = window.webkitURL.createObjectURL(file);
    }
    return url;
}