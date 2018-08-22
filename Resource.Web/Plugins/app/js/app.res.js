var ue = UE.getEditor('Content', {
    initialFrameHeight: 200,
    allowDivTransToP: false,
    initialFrameWidth: '100%'
});

$(function() {
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
        $(this).prop("disabled", true);
        var form = new FormData($("form")[0]);
        if ($('input[type=submit]').attr("data-url") == '' || !$('input[type=submit]').attr("data-url")) { return false; }
        ajaxForm("post", $('input[type=submit]').attr("data-url"), form, "json", function(data) {
            layer.msg(data.msg, { icon: data.result })
            if (data.result == 1) {
                window.parent.refresh = true;
                setTimeout(function() { window.parent.layer.closeAll() }, 1000);
            } else {
                $(this).prop("disabled", false);
            }
        });
        return false;
    });
    dropChange(2);
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
            layer.msg(data.msg, { icon: data.result })
            if (data.result == 1) {
                $this.parents(".imgbox").remove();
            }
        });
    });
});