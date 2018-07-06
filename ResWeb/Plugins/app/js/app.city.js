$(function () {
    // 添加
    $("#add").on("click", function () {
        ceOpen("Region");
    });

    // 编辑
    $("#edit").on("click", function () {
        if (!choose || choose == "") {
            layer.msg("请选择数据！");
        } else {
            ceOpen("/City/Edit?id=" + choose);
        }
    });

    //删除
    $("#del").on("click", function () {
        if (!choose || choose == "") {
            layer.msg("请选择数据！");
        } else {
            layer.confirm("是否删除？", function () {
                $.post("/City/Delete", { id: choose }, function (data) {
                    layer.msg(data.msg, { icon: data.result })
                    if(data.result=1){
                        choose="";
                        refresh();
                    }
                    
                }, "json");
            });
        }
    });

    //查询
    $("#Check").on("click", function () {
        refresh($("#Name").val());
    });
    refresh();


});

function ceOpen (url) {
    var index = layer.open({
        type: 2,
        content: url,
         title:' '        
    });
    layer.full(index);
}

function tableDblClick() {
    $(".table tr").dblclick(function () {
        ceOpen("/City/Edit?id=" + $(this).find("#key").html());
    })
}

function refresh(param){
    $.get("/City/Details", {name:param},function (data) {
        $(".data-action").html(data);
        tableClick();
        tableDblClick();
    });
}

