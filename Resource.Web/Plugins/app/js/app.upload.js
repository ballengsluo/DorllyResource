$(function() {
    imginit();
    upload();
});

function imginit() {
    if (!$("#id").val() || $("#id").val() == "") {
        return false;
    }
    $.get("/Image/ResourceImgList", { resourceID: $("#id").val() }, function(data) {
        $("#upload").prepend(data);
        setbind();
    });
}

function upload() {
    $("#upbtn").click(function() {
        if (!$("#id").val() || $("#id").val() == "") {
            layer.msg("资源编号不能为空！");
            return false;
        }
    });
    $("#upbtn").change(function() {
        if (!$("#id").val() || $("#id").val() == "") {
            layer.msg("资源编号为空，上传失败！");
            return false;
        }
        var form = new FormData();
        form.append("upimg", $(this).get(0).files[0]);
        $.ajax({
            url: "/Image/SaveResourceImg?resourceID=" + $("#id").val(),
            type: "POST",
            data: form,
            dataType: "json",
            processData: false,
            contentType: false,
            success: function(data) {
                if (data.result == 1) {
                    var html = "<div class='imgbox'><img src='" + data.img.ImgUrl +
                        "' /><br /><a class='imgset' data-id='" + data.img.ID +
                        "' data-pid='" + data.img.ResourceID +
                        "'>设为封面</a><a class='imgdel' data-id='" + data.img.ResourceID +
                        "'>删除</a></div>";
                    $("#upload").prepend(html);
                    setbind();
                } else {
                    layer.msg(data.msg, { icon: data.result });
                }
            }
        });
    });
}

function setbind() {
    $(".imgdel").off("click").on("click", function() {
        $this = $(this);
        $.post("/Image/DelResourceImg", { imgID: $this.attr("data-id") }, function(data) {
            if (data.result == 1) {
                $this.parent().remove();
            } else {
                layer.msg(data.msg, { icon: data.result });
            }
        });
    });
    $(".imgset").off("click").on("click", function() {
        $this = $(this);
        $.post("/Image/SetResourceCovert", { imgID: $this.attr("data-id"), resourceID: $this.attr("data-pid") }, function(data) {
            if (data.result == 1) {
                $(".imgbox img").css("border", "");
                $this.parent().find("img").css("border", "3px solid #fc720d");
            } else {
                layer.msg(data.msg, { icon: data.result });
            }
        });
    });
}