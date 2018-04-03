var common = {};

/**
* 获取Cookie中的数据
*/
common.getCookie = function (c_name) {
    if (document.cookie.length > 0) {
        var c_start = document.cookie.indexOf(c_name + "=");

        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) {
                c_end = document.cookie.length
            }
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return "";
}

/**
* 设置Cookie
*/
common.setCookie = function (c_name, c_value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(c_value) + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString())
}

/**
* 格式化字符串
* 用法: utils.formatString("{0}-{1}","a","b");
*/
common.formatString = function () {
    for (var i = 1; i < arguments.length; i++) {
        var exp = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        arguments[0] = arguments[0].replace(exp, arguments[i]);
    }
    return arguments[0];
}

/**  
* 格式化数字显示方式   
* returnType 返回类型 false string。true number
* 用法  
* formatNumber(12345.999,'#,##0.00');  
* formatNumber(12345.999,'#,##0.##');  
* formatNumber(123,'000000');
*/
common.formatNumber = function (v, pattern, returnType) {
    returnType = returnType || false;
    if (!v)
        return v;
    if (parseFloat(v) == NaN)
        return v;
    if (!pattern)
        return v;

    var patrn = /^(-)?\d+(\.\d+)?$/;
    if (patrn.exec(v) == null || v == "") {
        return v;
    }
    var strarr = v ? v.toString().split('.') : ['0'];
    var fmtarr = pattern ? pattern.split('.') : [''];
    var retstr = '';
    // 整数部分   
    var str = strarr[0];
    var fmt = fmtarr[0];
    var i = str.length - 1;

    var comma = false;
    for (var f = fmt.length - 1; f >= 0; f--) {
        switch (fmt.substr(f, 1)) {
            case '#':
                if (i >= 0) retstr = str.substr(i--, 1) + retstr;
                break;
            case '0':
                if (i >= 0) retstr = str.substr(i--, 1) + retstr;
                else retstr = '0' + retstr;
                break;
            case ',':
                comma = true;
                retstr = ',' + retstr;
                break;
        }
    }
    if (i >= 0) {
        if (comma) {
            var l = str.length;
            for (; i >= 0; i--) {
                retstr = str.substr(i, 1) + retstr;
                if (i > 0 && ((l - i) % 3) == 0) retstr = ',' + retstr;
            }
        }
        else retstr = str.substr(0, i + 1) + retstr;
    }
    retstr = retstr + '.';
    // 处理小数部分   
    str = strarr.length > 1 ? strarr[1] : '';
    fmt = fmtarr.length > 1 ? fmtarr[1] : '';
    i = 0;
    for (var f = 0; f < fmt.length; f++) {
        switch (fmt.substr(f, 1)) {
            case '#':
                if (i < str.length) retstr += str.substr(i++, 1);
                break;
            case '0':
                if (i < str.length) retstr += str.substr(i++, 1);
                else retstr += '0';
                break;
        }
    }
    if (!returnType)
        return retstr.replace(/^,+/, '').replace(/\.$/, '').replace('-,', '-');
    else
        return Number(retstr.replaceAll(',', ''));
}

//生成GUID(有横线36位)
common.guid = (function () {
    var a = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".split("");
    return function (b, f) {
        var h = a, e = [], d = Math.random; f = f || h.length;
        if (b) {
            for (var c = 0; c < b; c++) {
                e[c] = h[0 | d() * f];
            }
        } else {
            var g; e[8] = e[13] = e[18] = e[23] = "-"; e[14] = "4";
            for (var c = 0; c < 36; c++) {
                if (!e[c]) {
                    g = 0 | d() * 16; e[c] = h[(c == 19) ? (g & 3) | 8 : g & 15];
                }
            }
        } return e.join("").toLowerCase();
    };
})();

//生成GUID(没有横线32位)
common.guidNoline = (function () {
    var a = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".split("");
    return function (b, f) {
        var h = a, e = [], d = Math.random; f = f || h.length;
        if (b) {
            for (var c = 0; c < b; c++) {
                e[c] = h[0 | d() * f];
            }
        } else {
            var g; e[14] = "4";
            for (var c = 0; c < 32; c++) {
                if (!e[c]) {
                    g = 0 | d() * 16; e[c] = h[(c == 19) ? (g & 3) | 8 : g & 15];
                }
            }
        } return e.join("").toLowerCase();
    };
})();

/*
 * 功能 ：时间格式化
 * 说明 : 时间格式化  format:"yyyy-MM-dd hh:mm:ss" 
*/
Date.prototype.format = function (format) {
    if (!format) {
        format = "yyyy-MM-dd hh:mm:ss";
    }
    var o = {
        "M+": this.getMonth() + 1, // month
        "d+": this.getDate(), // day
        "h+": this.getHours(), // hour
        "m+": this.getMinutes(), // minute
        "s+": this.getSeconds(), // second
        "q+": Math.floor((this.getMonth() + 3) / 3), // quarter
        "S": this.getMilliseconds()
    };

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};

/*
   * 功能 ：string类型 全文替换
   * 参数 ：reallyDo原字段 replaceWith新字段 ignoreCase 全局并忽略大小匹配、全局并大小匹配
*/
String.prototype.replaceAll = function (reallyDo, replaceWith, ignoreCase) {
    if (!RegExp.prototype.isPrototypeOf(reallyDo)) {
        return this.replace(new RegExp(reallyDo, (ignoreCase ? "gi" : "g")), replaceWith);
    } else {
        return this.replace(reallyDo, replaceWith);
    }
}

/*
 * 功能 ：通过元素删除
 * 说明 : 通过元素删除
*/
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};