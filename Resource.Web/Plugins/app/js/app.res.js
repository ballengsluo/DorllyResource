var ue = UE.getEditor('Content', {
    initialFrameHeight: 200,
    allowDivTransToP: false,
    initialFrameWidth: '100%'
});

$(function() {

    dropChange(2);
    changeNodeName();

    $("input[type='file']").change(function() {
        var $file = $(this);
        console.log($file[0].files.length);
        var fileObj = $file[0];
        $(this).parents(".imgContainer").find("div[data-insert]").remove();
        for (var i = 0; i < fileObj.files.length; i++) {
            var windowURL = window.URL || window.webkitURL;
            var dataURL = windowURL.createObjectURL(fileObj.files[i]);
            // $(this).parents(".imgContainer").prepend("<div class='imgbox' data-insert='1'> <img src='" + dataURL + "' /></div>");
            $(this).parents(".addbox").before("<div class='imgbox' data-insert='1'> <img src='" + dataURL + "' /></div>");
        }
    });


    $("form").submit(function() {
        $(this).attr("disabled", "disabled");
        $("#price input").each(function() {

        });
        var form = new FormData($("form")[0]);
        if ($('input[type=submit]').attr("data-url") == '' || !$('input[type=submit]').attr("data-url")) { return false; }
        ajaxForm("post", $('input[type=submit]').attr("data-url"), form, "json", function(data) {
            layer.msg(data.Msg)
            console.log(data.ExMsg);
            if (data.Flag == 1) {
                window.parent.refresh = true;
                setTimeout(function() { window.parent.layer.closeAll() }, 1000);
            } else {
                $(this).prop("disabled", false);
            }
        });
        return false;
    });

    // $("input[type='submit']").click(function() {
    //     $("form").valid();
    //     $(this).prop("disabled", true);
    //     var form = new FormData($("form")[0]);
    //     if ($(this).attr("data-url") == '' || !$(this).attr("data-url")) { return false; }
    //     ajaxForm("post", $(this).attr("data-url"), form, "json", function(data) {
    //         layer.msg(data.msg, { icon: data.result })
    //         if (data.result == 1) {
    //             window.parent.refresh = true;
    //             setTimeout(function() { window.parent.layer.closeAll() }, 1000);
    //         } else {
    //             $(this).prop("disabled", false);
    //         }
    //     });
    // });
    $(".imgdel").click(function() {
        var $this = $(this);
        var id = $this.attr("data-id");
        if (id == "" || !id) { return false; }
        ajax("post", "/public/delimgaction", { id: id }, "json", function(data) {
            layer.msg(data.Msg);
            Console.log(data.ExMsg);
            if (data.Flag == 1) {
                $this.parents(".imgbox").remove();
            }
        });
    });

    $(".imgbox[data-covert='True']").css("border", "1px solid green").find(".imgset").append("<i class='glyphicon glyphicon-ok'></i>");
    $("input[name='covert']").change(function() {
        var $this = $(this);
        var id = $this.attr("id");
        var rid = $this.attr("data-rid");
        if (id == "" || !id) { return false; }
        ajax("post", "/base/setimgaction", { id: id, rid: rid }, "json", function(data) {
            layer.msg(data.Msg);
            console.log(data.ExMsg);
            if (data.Flag == 1) {
                $(".imgbox").css("border", "none");
                $(".imgset i").remove();
                $this.parent().append("<i class='glyphicon glyphicon-ok'></i>");
                $this.parents(".imgbox").css("border", "1px solid green");
            }
        });
    });
});

function changeNodeName() {
    if ($("#park").length > 0) {
        $("#park").attr("name", "Loc1");
    }
    if ($("#stage").length > 0) {
        $("#stage").attr("name", "Loc2");
    }
    if ($("#build").length > 0) {
        $("#build").attr("name", "Loc3");
    }
    if ($("#floor").length > 0) {
        $("#floor").attr("name", "Loc4");
    }
    if ($("#room").length > 0) {
        $("#room").attr("name", "Loc5");
    }
}

function setPrice(max, min) {
    $("#MaxPrice").val(max);
    $("#MinPrice").val(min);
}