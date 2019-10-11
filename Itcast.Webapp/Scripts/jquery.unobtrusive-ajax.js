/*!
** Unobtrusive Ajax support library for jQuery
** Copyright (C) Microsoft Corporation. All rights reserved.
*/

/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, newcap: true, immed: true, strict: false */
/*global window: false, jQuery: false */
/*
data-ajax=true //开启绑定
 
data-ajax-mode//更新的形式 BEFORE插入到对象之前 AFTER插入到对象之后 为空就是覆盖
data-ajax-update//更新的对象
data-ajax-confirm//设置一个确定取消弹出框的文字，没有则不设置
data-ajax-loading//显示loading的对象
data-ajax-loading-duration//持续时间 默认是0
data-ajax-method//提交方式
data-ajax-url//提交url
data-ajax-begin//ajax前触发的函数或者一段程序
data-ajax-complete//完成后，此时还没有加载返回的数据，请求成功或失败时均调用
data-ajax-success//成功，加载完成的数据
data-ajax-failure//失败
 
*/

(function ($) {
    var data_click = "unobtrusiveAjaxClick",
        data_validation = "unobtrusiveValidation";
    //第二核心，判断是否函数，不是则构造一个匿名函数
    function getFunction(code, argNames) {
        var fn = window, parts = (code || "").split(".");
        while (fn && parts.length) {
            fn = fn[parts.shift()];
        }//查找函数名有时候是命名空间比如xxx.xxx
        if (typeof (fn) === "function") {
            return fn;
        }
        argNames.push(code);
        //如果不是函数对象则自己构造一个并返回，吊！
        return Function.constructor.apply(null, argNames);
    }

    function isMethodProxySafe(method) {
        return method === "GET" || method === "POST";
    }
    //可以添加各种提交方式，应该是为Web Api做的补充
    function asyncOnBeforeSend(xhr, method) {
        if (!isMethodProxySafe(method)) {
            xhr.setRequestHeader("X-HTTP-Method-Override", method);
        }
        //注：X-HTTP-Method-Override是一个非标准的HTTP报头。
        //这是为不能发送某些HTTP请求类型（如PUT或DELETE）的客户端而设计的
    }
    //完成后的
    function asyncOnSuccess(element, data, contentType) {
        var mode;

        if (contentType.indexOf("application/x-javascript") !== -1) {  // jQuery already executes JavaScript for us
            return;
        }

        mode = (element.getAttribute("data-ajax-mode") || "").toUpperCase();
        $(element.getAttribute("data-ajax-update")).each(function (i, update) {
            var top;

            switch (mode) {
                case "BEFORE":
                    top = update.firstChild;
                    $("<div />").html(data).contents().each(function () {
                        update.insertBefore(this, top);
                    });
                    break;
                case "AFTER":
                    $("<div />").html(data).contents().each(function () {
                        update.appendChild(this);
                    });
                    break;
                default:
                    $(update).html(data);
                    break;
            }
        });
    }
    //主要函数
    //绑定的对象和参数
    function asyncRequest(element, options) {
        var confirm, loading, method, duration;

        confirm = element.getAttribute("data-ajax-confirm");
        if (confirm && !window.confirm(confirm)) {
            return;
        }

        loading = $(element.getAttribute("data-ajax-loading"));//
        duration = element.getAttribute("data-ajax-loading-duration") || 0;//默认是0

        $.extend(options, {
            type: element.getAttribute("data-ajax-method") || undefined,
            url: element.getAttribute("data-ajax-url") || undefined,
            beforeSend: function (xhr) {//ajax前触发，此处的xhr将在下面用apply传递出去
                var result;
                asyncOnBeforeSend(xhr, method);//判断是否添加特种的提交方式
                result = getFunction(element.getAttribute("data-ajax-begin"), ["xhr"]).apply(this, arguments);//argument：替换函数对象的其中一个属性对象，存储参数。这里是将原先的参数传递出去，吊！
                if (result !== false) {
                    loading.show(duration);
                }
                return result;
            },
            complete: function () {
                loading.hide(duration);
                getFunction(element.getAttribute("data-ajax-complete"), ["xhr", "status"]).apply(this, arguments);
            },
            success: function (data, status, xhr) {
                asyncOnSuccess(element, data, xhr.getResponseHeader("Content-Type") || "text/html");
                getFunction(element.getAttribute("data-ajax-success"), ["data", "status", "xhr"]).apply(this, arguments);
            },
            error: getFunction(element.getAttribute("data-ajax-failure"), ["xhr", "status", "error"])
        });

        options.data.push({ name: "X-Requested-With", value: "XMLHttpRequest" });

        method = options.type.toUpperCase();//大写
        if (!isMethodProxySafe(method)) {
            options.type = "POST";
            options.data.push({ name: "X-HTTP-Method-Override", value: method });
        }
        //最后都是调用jquery的ajax
        $.ajax(options);
    }

    function validate(form) {
        //可以取消验证
        var validationInfo = $(form).data(data_validation);
        return !validationInfo || !validationInfo.validate || validationInfo.validate();
    }

    $(document).on("click", "a[data-ajax=true]", function (evt) {
        evt.preventDefault();
        asyncRequest(this, {
            url: this.href,
            type: "GET",
            data: []
        });
    });

    $(document).on("click", "form[data-ajax=true] input[type=image]", function (evt) {//这个不常用
        var name = evt.target.name,
            $target = $(evt.target),
            form = $target.parents("form")[0],
            offset = $target.offset();

        $(form).data(data_click, [
            { name: name + ".x", value: Math.round(evt.pageX - offset.left) },
            { name: name + ".y", value: Math.round(evt.pageY - offset.top) }
        ]);

        setTimeout(function () {
            $(form).removeData(data_click);
        }, 0);
    });

    $(document).on("click", "form[data-ajax=true] :submit", function (evt) {
        var name = evt.target.name,
            form = $(evt.target).parents("form")[0];

        $(form).data(data_click, name ? [{ name: name, value: evt.target.value }] : []);

        setTimeout(function () {
            $(form).removeData(data_click);
        }, 0);
    });

    $(document).on("submit", "form[data-ajax=true]", function (evt) {
        var clickInfo = $(this).data(data_click) || [];
        evt.preventDefault();
        if (!validate(this)) {
            return;
        }
        asyncRequest(this, {
            url: this.action,
            type: this.method || "GET",
            data: clickInfo.concat($(this).serializeArray())//写得好，序列化表单并拼接，以后的ajax都可以这样，方便啊
        });
    });
}(jQuery));