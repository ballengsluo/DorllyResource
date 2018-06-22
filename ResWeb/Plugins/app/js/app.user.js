

/*
 * jQuery resize event - v1.1 - 3/14/2010
 * http://benalman.com/projects/jquery-resize-plugin/
 * 
 * Copyright (c) 2010 "Cowboy" Ben Alman
 * Dual licensed under the MIT and GPL licenses.
 * http://benalman.com/about/license/
 */
!(function ($, h, c) { var a = $([]), e = $.resize = $.extend($.resize, {}), i, k = "setTimeout", j = "resize", d = j + "-special-event", b = "delay", f = "throttleWindow"; e[b] = 250; e[f] = true; $.event.special[j] = { setup: function () { if (!e[f] && this[k]) { return false } var l = $(this); a = a.add(l); $.data(this, d, { w: l.width(), h: l.height() }); if (a.length === 1) { g() } }, teardown: function () { if (!e[f] && this[k]) { return false } var l = $(this); a = a.not(l); l.removeData(d); if (!a.length) { clearTimeout(i) } }, add: function (l) { if (!e[f] && this[k]) { return false } var n; function m(s, o, p) { var q = $(this), r = $.data(this, d); r.w = o !== c ? o : q.width(); r.h = p !== c ? p : q.height(); n.apply(this, arguments) } if ($.isFunction(l)) { n = l; return m } else { n = l.handler; l.handler = m } } }; function g() { i = h[k](function () { a.each(function () { var n = $(this), m = n.width(), l = n.height(), o = $.data(this, d); if (m !== o.w || l !== o.h) { n.trigger(j, [o.w = m, o.h = l]) } }); g() }, e[b]) } })(jQuery, this);
var layer;
var user_reflash = false;
$(function () {
    layui.use('layer', function () {
        layer = layui.layer;
    });
    layui.use('laydate', function () {
        var laydate = layui.laydate;
        laydate.render({
            elem: '#createDate',
            range: '~',
        });
    });
    dataCheck();    //数据检查
    selectUser();   //查询
    openUser();     //启用
    closeUser();    //停用
    deleteUser();   //删除
    resetPwd();     //重置密码
    resetCommit();  //提交重置密码数据
    regOpen();      //注册
    editOpen();     //编辑
    bundleData();   //绑定表格动作
});

var regOpen = function () {
    $("#insert").click(function () {
        var loadIndex;
        layer.open({
            type: 2,
            content: ['/User/Create'],
            title: false,
            // title: ['用户注册', 'font-size:20px;letter-spacing:2px;'],
            area: ['510px', '558px'],
            anim: 5,
            closeBtn: 2,
            success: function () {
                calcIframe();
            },
            end: function () {
                if (user_reflash) {
                    var url = "/User/Details";
                    var data = {};
                    ajaxReq(url, "GET", data, "刷新成功！");
                }
            }
        });
    });
};

var editOpen = function () {
    $("#edit").click(function () {
        if ($("#chooseID").val() == "") {
            layer.msg("请选择用户！");
        } else {
            layer.open({
                type: 2,
                content: ['/User/Edit/' + $("#chooseID").val()],
                title: ['编辑用户', 'font-size:20px;letter-spacing:2px;'],
                area: ['510px', '558px'],
                anim: 5,
                closeBtn: 2,
                end: function () {
                    if (user_reflash) {
                        var url = "/User/Details";
                        var data = {};
                        ajaxReq(url, "GET", data, "刷新成功！");
                    }
                }
            });

        }
    });
};

var selectUser = function () {

    $("#select").click(function () {
        var uName = $("#userName").val();
        var time = $("#createDate").val();
        var url = "/User/Select"
        var data = { userName: uName, createDate: time };
        ajaxReq(url, "GET", data, "查询成功！");
    });

};

var openUser = function () {
    $("#open").click(function () {
        if ($("#chooseID").val() == "") {
            layer.msg("请选择用户！");
        } else {
            var url = "/User/CloseOrOpenUser";
            var data = { id: $("#chooseID").val(), type: "open" };
            ajaxReq(url, "GET", data, "启用成功！");
        }
    });

};

var closeUser = function () {
    $("#close").click(function () {
        if ($("#chooseID").val() == "") {
            layer.msg("请选择用户！");
        } else {
            var url = "/User/CloseOrOpenUser";
            var data = { id: $("#chooseID").val(), type: "close" };
            ajaxReq(url, "GET", data, "停用成功！");
        }
    });
};

var deleteUser = function () {
    $("#delete").click(function () {
        if ($("#chooseID").val() == "") {
            layer.msg("请选择用户！");
        } else {
            layer.confirm('是否删除', { btn: ['确认', '取消'] }, function () {
                var url = "/User/Delete";
                var data = { id: $("#chooseID").val() };
                ajaxReq(url, "GET", data, "删除成功！");
            });
        }
    });
};

var resetPwd = function () {
    $("#reset").click(function () {
        if ($("#chooseID").val() == "") {
            layer.msg("请选择用户！");
        } else {
            layer.open({
                type: 1,
                closeBtn: 2,
                content: $(".resetdiv"),
                area: ["390px", "230"],
                title: false,
                scrollbar: false,
                resize: false,
                success: function () {
                    calcRestDiv();
                },
                end: function () {
                    $("#resetPwd").get(0).reset();
                    $("#resetPwd").data('bootstrapValidator').destroy();
                    dataCheck();
                }
            });
        }

    });
};
var resetCommit = function () {
    $("#resetCommit").click(function () {
        //获取表单对象
        var bootstrapValidator = $("#resetPwd").data('bootstrapValidator').validate();
        if (bootstrapValidator.isValid()) {
            //表单提交的方法、比如ajax提交
            $.ajax({
                type: "POST",
                url: "/User/ResetPwd",
                data: { id: $("#chooseID").val(), pwd: $("#pwd").val() },
                async: false,
                beforeSend: function (XMLHttpRequest) {
                    loadIndex = layer.load(0, { shade: 0.1 });
                },
                complete: function (XMLHttpRequest, textStatus) {
                    layer.close(loadIndex);
                },
                success: function (responseData) {
                    layer.alert(responseData.split(':')[1], { icon: responseData.split(':')[0], }, function () {
                        parent.layer.closeAll();
                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.msg("通讯故障，请求失败！");
                    console.log("请求失败！");
                }
            });
        }
    });
};
var bundleData = function () {
    $(".table tr").click(function () {
        $(".table tr").each(function () {
            $(this).removeClass("active");
        });
        $("#chooseID").val($(this).find("td").eq(1).html());
        $(this).addClass("active");
        var text = $(this).find("td").find("span").eq(0);
        if (text.hasClass("status-disable")) {
            $("#open").show();
            $("#close").hide();
        } else {
            $("#open").hide();
            $("#close").show();

        }
    });
};

var ajaxReq = function (reqUrl, reqType, reqData, msg) {
    $.ajax({
        type: reqType,
        url: reqUrl,
        data: reqData,
        dataType: "html",
        async: false,
        beforeSend: function (XMLHttpRequest) {
            loadIndex = layer.load(0, { shade: 0.1 });
        },
        complete: function (XMLHttpRequest, textStatus) {
            layer.close(loadIndex);
        },
        success: function (responseData) {
            $(".data-action").html(responseData);
            bundleData();
            layer.msg(msg);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            layer.msg("通讯故障，请求失败！");
            console.log("请求失败！");
        }
    });
};
var dataCheck = function () {
    $("form").bootstrapValidator({

        message: '数据校验错误',
        group: '.form-group',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },

        fields: {
            userName: {
                validators: {
                    notEmpty: {
                        message: '账号不能为空'
                    }
                }
            },
            pwd: {
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    // identical: {
                    //     field: 'ConfirmPwd',
                    //     message: '密码不一致！'
                    //   },
                    stringLength: {
                        min: 6,
                        max: 18,
                        message: '密码长度必须在6到18位之间'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_]+$/,
                        message: '用户名只能包含大写、小写、数字和下划线'
                    }
                }
            },
            confirmPwd: {
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    identical: {
                        field: 'pwd',
                        message: '密码不一致！'
                    },
                    stringLength: {
                        min: 6,
                        max: 18,
                        message: '密码长度必须在6到18位之间'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_]+$/,
                        message: '用户名只能包含大写、小写、数字和下划线'
                    }
                }
            },
            phone: {
                validators: {
                    regexp: {
                        regexp: /^1(3|5|7|8|9)\d{9}$/,
                        message: '手机号码格式有误'
                    }
                }
            },
            email: {
                validators: {
                    regexp: {
                        regexp: /^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$/,
                        message: '手机号码格式有误'
                    }
                }
            },
            addr: {
                validators: {
                    stringLength: {
                        min: 3,
                        max: 50,
                        message: '地址必须在4到100位之间'
                    },
                }
            },
            role: {
                validators: {
                    notEmpty: {
                        message: '角色不能为空'
                    }
                }
            }
            // submitHandler: function (validator, form, submitButton) {
            //     validator.defaultSubmit();
            // }
        }
    }).on('error.validator.bv', function (e, data) {
        //这个方法是让错误信息只显示最新的一个（有时会出现多个错误信息同时显示用这个方法解决）
        data.element
            .data('bv.messages')
            // Hide all the messages
            .find('.help-block[data-bv-for="' + data.field + '"]').hide()
            // Show only message associated with current validator
            .filter('[data-bv-validator="' + data.validator + '"]').show();
    }).on("success.form.bv", function (e) {
        // if (againSubmit) {
        //     return;
        // }
        // againSubmit = true;
        e.preventDefault();
        // var form = $(e.target);
        // alert(form);
        if ($("#userID").val() === "") {
            urlStr = "/User/Create";

        } else {
            urlStr = "/User/Edit";
        }
        var formData = new FormData($("form")[0]);
        $.ajax({
            url: urlStr,
            type: "POST",
            data: formData,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function (XMLHttpRequest) {
                loadIndex = layer.load(0, { shade: 0.1 });
            },
            complete: function (XMLHttpRequest, textStatus) {
                layer.close(loadIndex);
            },
            success: function (responseData) {
                layer.alert(responseData.split(':')[1], { icon: responseData.split(':')[0], }, function () {
                    user_reflash = true;
                    parent.layer.closeAll();
                });

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("通讯故障，请求失败！");
            }
        });
    });
    $("#resetPwd").bootstrapValidator({
        message: '数据校验错误',
        group: '.form-group',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },

        fields: {
            pwd: {
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    // identical: {
                    //     field: 'ConfirmPwd',
                    //     message: '密码不一致！'
                    //   },
                    stringLength: {
                        min: 6,
                        max: 18,
                        message: '密码长度必须在6到18位之间'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_]+$/,
                        message: '用户名只能包含大写、小写、数字和下划线'
                    }
                }
            },
            confirmPwd: {
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    identical: {
                        field: 'pwd',
                        message: '密码不一致！'
                    },
                    stringLength: {
                        min: 6,
                        max: 18,
                        message: '密码长度必须在6到18位之间'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_]+$/,
                        message: '用户名只能包含大写、小写、数字和下划线'
                    }
                }
            }
        }
    }).on('error.validator.bv', function (e, data) {
        //这个方法是让错误信息只显示最新的一个（有时会出现多个错误信息同时显示用这个方法解决）
        data.element
            .data('bv.messages')
            // Hide all the messages
            .find('.help-block[data-bv-for="' + data.field + '"]').hide()
            // Show only message associated with current validator
            .filter('[data-bv-validator="' + data.validator + '"]').show();
    });
};
var datepicker = function () {

    $('#reservationtime').daterangepicker(
        {
            timePicker: true,
            timePickerIncrement: 30,
            showDropdowns: true,
            timePicker: true,
            //singleDatePicker:true,
            opens: "left",
            "locale": {
                format: 'YYYY-MM-DD',
                separator: '  至  ',
                applyLabel: "应用",
                cancelLabel: "取消",
                resetLabel: "重置"
            }
        }
    );
};

var calcRestDiv = function () {
    $(".resetdiv").resize(function () {
        // alert($(this).outerHeight(true));
        $("div[type='page']>div").css("height", $(this).outerHeight(true) + "px");
        $("div[type='page']").css("height", $(this).outerHeight(true) + "px");
    });
};

var calcIframe = function () {
    var $ele = $("iframe").contents().find("body");
    $ele.resize(function () {
        // alert($ele.outerHeight(true));
        var eleheight = $ele.outerHeight(true);
        $("div[type='iframe']>div>iframe").css("height", eleheight + "px");
        $("div[type='iframe']").css("height", eleheight + "px");
    });

};