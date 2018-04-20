


$.extend($.fn.textbox.defaults.rules, {
    minLength: {  //最小长度
        validator: function (value, param) {
            return value.length >= param[0];
        },
        message: '请输入最小{0}位字符.'
    },
    maxLength: {  //最大长度
        validator: function (value, param) {
            return param[0] >= value.length;
        },
        message: '请输入最大{0}位字符.'
    },
    length: {  //长度限制
        validator: function (value, strparam) {
            var param = strparam.split('-');
            return value.length >= param[0] && param[1] >= value.length;
        },
        message: '请输入{0}位字符.'
    },
    equals: { //两个控件值比较
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: '两次输入不一致.'
    },
    web: {  //URL 验证
        validator: function (value) {
            var regex = /^(http[s]{0,1}|ftp):\/\//i;
            return regex.test($.trim(value));
        },
        message: '网址格式错误.'
    },
    mobile: {  //手机号码及座机验证
        validator: function (value) {
            var isPhone = /^([0-9]{3,4}-)?[0-9]{7,8}$/;
            var isMob = /^((\+?86)|(\(\+86\)))?(13[012356789][0-9]{8}|15[012356789][0-9]{8}|18[02356789][0-9]{8}|147[0-9]{8}|1349[0-9]{7})$/;
            var arrValue = value.split(',');
            if (arrValue.length <= 1) arrValue[1] = arrValue[0];
            if ((isMob.test(arrValue[0]) || isPhone.test(arrValue[0])) && (isMob.test(arrValue[1]) || isPhone.test(arrValue[1]))) {
                return true;
            } else {
                return false;
            }
        },
        message: '电话号码格式错误.'
    },
    fax: {  //传真号码验证
        validator: function (value) {
            var isPhone = /^([0-9]{3,4}-)?[0-9]{7,8}$/;
            var isMob = /^((\+?86)|(\(\+86\)))?(13[012356789][0-9]{8}|15[012356789][0-9]{8}|18[02356789][0-9]{8}|147[0-9]{8}|1349[0-9]{7})$/;
            var arrValue = value.split(',');
            if (arrValue.length <= 1) arrValue[1] = arrValue[0];
            if ((isMob.test(arrValue[0]) || isPhone.test(arrValue[0])) && (isMob.test(arrValue[1]) || isPhone.test(arrValue[1]))) {
                return true;
            } else {
                return false;
            }
        },
        message: '传真号码格式错误.'
    },
    email: {  //邮箱验证
        validator: function (value) {
            var regex = /^[a-zA-Z0-9_+.-]+\@([a-zA-Z0-9-]+\.)+[a-zA-Z0-9]{2,4}$/i
            return regex.test($.trim(value));
        },
        message: '电子邮箱格式错误.'
    },
    date: {  //日期验证
        validator: function (value) {
            var regex = /^[0-9]{4}[-][0-9]{2}[-][0-9]{2}$/i
            return regex.test($.trim(value));
        },
        message: '曰期格式错误,如2012-09-11.'
    },
    endThanTotay: { //时间必须大于当前时间
        validator: function (value) {
            var enddate = new Date((endTime).replace(/-/g, "/"));
            var nowdate = new Date();
            return enddate < nowdate
        },
        message: '时间不能小于当前时间.'
    },
    endThanStart: {  //结束时间小于开始时间
        validator: function (value, param) {
            var startdate = $(param).datebox("getValue");
            startdate = new Date((startdate).replace(/-/g, "/"));
            var enddate = new Date((endTime).replace(/-/g, "/"));
            return enddate > startdate
        },
        message: '结束时间小于开始时间.'
    },
    isBlank: {
        validator: function (value, param) { return $.trim(value) != '' },
        message: '不能为空，全空格也不行'
    },
    isZero: {
        validator: function (value, param) {
            return parseFloat(value) != 0
        },
        message:'不能为零'
    }
});


$.extend($.fn.datagrid.defaults.editors, {
    timespinner: {
        init: function (container, options) {
            var input = $('<input type="text">').appendTo(container);
            return input.timespinner(options);
        },
        destroy: function (target) {
            $(target).timespinner('destroy');
        },
        getValue: function (target) {
            return $(target).timespinner('getValue');
        },
        setValue: function (target, value) {
            $(target).timespinner('setValue', value);
        },
        resize: function (target, width) {
            $(target).timespinner('resize', width);
        }
    }
});
