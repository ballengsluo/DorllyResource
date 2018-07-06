$(function () {
        
    //发布时间初始化
    layui.use('laydate', function () {
        var layerDate = layui.laydate;
        layerDate.render({
            elem: '#PublicTime',
            range: '  ~  '
        });
    });
    $("form").on("submit", function (event) {
        // event.preventDefault();
        return false;
    })
    // 价格模式变化
    $("#PriceModel").change(function () {
        if ($(this).val() == 1) {
            $("#NormalModel").show();
            $("#IntervalModel").hide();
        } else if ($(this).val() == 2) {
            $("#NormalModel").hide();
            $("#IntervalModel").show();
        }
        // $("#normalModel")
    });
    //图片数据加载
    $.get("/Image/GetImgByrResCode",{resourceCode:$("#ResourceCode").val()},function(data){
        $(".img-box").remove();
        $("#ImgContainer").prepend(data);
        imgChange();
    });
    //价格数据加载
    $.getJSON("/Price/GetPriceByResCode",{resourceCode:$("#ResourceCode").val()},function(data){
        // console.log(data);        
        $("#PriceModel").selectpicker('val',data.Model);
        // $("#PriceModel").selectpicker('refresh');
        $("#Unit").selectpicker('val',data.UnitCode);
        if(data.Model==1){
            $("#Price").val(data.Price);
            $("#NormalModel").show();
            $("#IntervalModel").hide();
        }else if(data.Model==2){
            $("#MinPrice").val(data.MinPrice);
            $("#MaxPrice").val(data.MaxPrice);
            $("#NormalModel").hide();
            $("#IntervalModel").show();
        }        
    });
    //发布数据加载
    $.getJSON("/Public/GetPublicByResCode",{resourceCode:$("#ResourceCode").val()},function(data){
        $("#PriceModel").val();
        $("#PriceModel").val();
        $("#PriceModel").val();
        
        console.log(data);        
        $("#PriceModel").selectpicker('val',data.Model);
        // $("#PriceModel").selectpicker('refresh');
        $("#Unit").selectpicker('val',data.UnitCode);
        if(data.Model==1){
            $("#Price").val(data.Price);
            $("#NormalModel").show();
            $("#IntervalModel").hide();
        }else if(data.Model==2){
            $("#MinPrice").val(data.MinPrice);
            $("#MaxPrice").val(data.MaxPrice);
            $("#NormalModel").hide();
            $("#IntervalModel").show();
        }        
    });
    //图片上传
    $("#ImgUpload").change(function () {
        var form = new FormData();
        form.append("product", $("#ImgUpload").attr("data-id"));
        form.append("imgUpload", $(this).get(0).files[0]);
        // new FormData($(".img-container")[0])
        ajax_form("/Image/SaveResImg", "POST", form, "json", function (data) {
            if (data.result == 1) {
                $("#ImgContainer").prepend("<div class='img-box'>"
                    + "<span class='close-box'  data-id='" + data.id + "'></span>"
                    + "<img src='" + data.path + "'/>"
                    + "<div class='select-box'>"
                    + "<span class='radio-box'>"
                    + "<input type='radio' name='isCover' id='" + data.tempid + "' data-id='" + data.id + "' />"
                    + "<label for='" + data.tempid + "'></label>"
                    + "</span></div>"
                    + "</div>"
                );
                imgChange();
            }
            layer.msg(data.msg, { icon: data.result });
        });
    });
    $("#Commit").click(function(){
        var form = new FormData($("form")[0]);
        ajaxForm("/Public/Public","post",form,"json",function(data){
            layer.msg(data.msg,{icon:data.result})
            if(data.result==1)
                parent.layer.closeAll();
            
        });
    });


    $("#Close").click(function(){
        parent.layer.closeAll();
    });

})

// var initResource = function (data) {
//     $("#ResourceCode").val(data.ResourceCode);
//     $("#ResourceName").val(data.ResourceName);
//     $("#ShortAddr").val(data.ShortAddr);
//     $("#FullAddr").val(data.FullAddr);
//     $("#RentArea").val(data.RentArea);
//     $("#PersonNum").val(data.PersonNum);
//     $("#Size").val(data.Size);
//     $("#Deposit").val(data.Deposit);
//     $("#SProvider").attr("disabled",true).selectpicker('refresh').selectpicker('val', data.SProviderCode);
//     $("#RType").selectpicker('val', data.RTypeID).attr("disabled",true).selectpicker('refresh');
//     $("#RGroup").selectpicker('val', data.RGroupCode);
//     $("#Park").attr("disabled",true).selectpicker('refresh').selectpicker('val', data.ParkCode);
//     $.get("/Image/GetResourceImgListById",{resourceCode:data.ResourceCode},function(data){
//         $(".img-box").remove();
//         $("#ImgContainer").prepend(data);
//     });
//     $("#ImgUpload").attr("data-id",data.ResourceCode)
//     // $("#").val();
//     // $("#").val();
//     // $("#").val();@Model
// }
var imgChange = function () {
    $(".close-box").off("click").on("click", function () {
        $this = $(this);
        ajax("/Image/DeleteResImg", "POST", { id: $this.attr("data-id") }, "json", 2, function (data) {
            if (data.result == 1) {
                $this.parent().remove();
            }
            layer.msg(data.msg, { icon: data.result });
        });

    });
    $("input[name=isCover]").off("change").on("change", function () {
        ajax("/Image/UpdateResImg", "GET", { id: $(this).attr("data-id") }, "json", 2, function (data) {
            layer.msg(data.msg, { icon: data.result });
        });
    });
}