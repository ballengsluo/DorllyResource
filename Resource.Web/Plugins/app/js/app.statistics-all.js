$(function() {
    if ($("#stime").length > 0) {
        layui.use('laydate', function() {
            var laydate = layui.laydate;
            laydate.render({
                elem: '#stime'
            });
        });
    }
    if ($("#etime").length > 0) {
        layui.use('laydate', function() {
            var laydate = layui.laydate;
            laydate.render({
                elem: '#etime'
            });
        });
    }
    layui.use('element', function() {
        var element = layui.element;
    });
})