$(function () {
  if ($('#stime').length > 0) {
    layui.use('laydate', function () {
      var laydate = layui.laydate
      laydate.render({
        elem: '#stime'
      })
    })
  }
  if ($('#etime').length > 0) {
    layui.use('laydate', function () {
      var laydate = layui.laydate
      laydate.render({
        elem: '#etime'
      })
    })
  }
  layui.use('element', function () {
    var element = layui.element
  })
  search();
  $("#search").trigger("click");
})

function search() {
  $('#search').click(function () {
    ajax('get', $('#search').attr('data-url'), {
      park: $('#park').val(),
      beginTime: $('#stime').val(),
      endTime: $('#etime').val()
    }, 'json', function (data) {
      if (data.length > 0) {
        $(".statistics-rate>span:first-of-type").html(0);
        $(".statistics-num>span:first-of-type").html(0);
        $.each(data, function (idx, item) {
          if (item.Kind == 1) {
            $("#rm-total").html(item.Total);
            $("#rm-rent").html(item.Rent);
            $("#rm-rentRate").html(item.RentRate);
            $("#rm-self").html(item.Self);
            $("#rm-selfRate").html(item.SelfRate);
            $("#rm-free").html(item.Free);
            $("#rm-freeRate").html(item.FreeRate);
          } else if (item.Kind == 2) {
            $("#wp-total").html(item.Total);
            $("#wp-rent").html(item.Rent);
            $("#wp-rentRate").html(item.RentRate);
            $("#wp-self").html(item.Self);
            $("#wp-selfRate").html(item.SelfRate);
            $("#wp-free").html(item.Free);
            $("#wp-freeRate").html(item.FreeRate);
          } else if (item.Kind == 3) {
            $("#cr-total").html(item.Total);
            $("#cr-rent").html(item.Rent);
            $("#cr-rentRate").html(item.RentRate);
            $("#cr-self").html(item.Self);
            $("#cr-selfRate").html(item.SelfRate);
            $("#cr-free").html(item.Free);
            $("#cr-freeRate").html(item.FreeRate);
          } else if (item.Kind == 4) {
            $("#ad-total").html(item.Total);
            $("#ad-rent").html(item.Rent);
            $("#ad-rentRate").html(item.RentRate);
            $("#ad-self").html(item.Self);
            $("#ad-selfRate").html(item.SelfRate);
            $("#ad-free").html(item.Free);
            $("#ad-freeRate").html(item.FreeRate);
          }
        })
      } else {
        layer.msg("获取数据失败！");
      }
    })
  })
}