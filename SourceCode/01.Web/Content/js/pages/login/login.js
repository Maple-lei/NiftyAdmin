$(function () {
    LoginController.init();
})

var LoginController = {
    init: function () {
        with (LoginController) {
            initData();
            changeBackgroudImage();
            bindLoginEvent();
            bindCheckEvent();
        }
    },

    initData: function () {
        var userName = common.getCookie("username");
        var password = common.getCookie("password");
        var isRemenber = common.getCookie("isRemenber");

        $("#UserName").val(userName);
        $("#UserName").val(password);

        if (isRemenber) {
            $("#isremenber").addClass("check");
        }
    },

    bindLoginEvent: function () {
        $("#submit").click(function () {
            var userName = $("#")
            $.ajax({
                url: "/Account/ValidationLogin",
                data: $("#login-form").serializeArray(),
                dataType: "json",
                success: function (d) {
                    if (d.result == 'success') {
                        window.location.href = '/Home/Index?roleID =' + d.roleID;
                    } else {
                        $("#msg").text(d.result);
                        $("#msgDiv").css("display", "block");
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    window.top.location = '/Error';
                },
                beforeSend: function () {
                    $('form').find("input").attr("disabled", true);
                    $('#submit').text("正在登录...");
                },
                complete: function () {
                    $('login-form').find("input").attr("disabled", false);
                }
            })
        })
    },

    changeBackgroudImage: function () {
        var $imgHolder = $('#demo-bg-list');
        var $bgBtn = $imgHolder.find('.demo-chg-bg');
        var $target = $('#bg-overlay');

        var bg_img_name = common.getCookie("bg-img");

        //用户自定义登录背景图
        if (bg_img_name) {
            var thumbs_img_url = "/Content/images/login/thumbs/" + bg_img_name;
            $("img[src='" + thumbs_img_url + "']").addClass("active").addClass("disable");
        }
        else {
            $imgHolder.find('.demo-chg-bg').eq(0).addClass("active").addClass("disable");
        }
        $bgBtn.on('click', function (e) {
            e.preventDefault();
            e.stopPropagation();

            var $el = $(this);
            if ($el.hasClass('active') || $imgHolder.hasClass('disabled')) return;

            $imgHolder.addClass('disabled');
            var url = $el.attr('src').replace('/thumbs', '');

            $('<img/>').attr('src', url).load(function () {
                $target.css('background-image', 'url("' + url + '")').addClass('bg-img');
                $imgHolder.removeClass('disabled');
                $bgBtn.removeClass('active');
                $el.addClass('active');

                $(this).remove();
            })

            var img_name = url.slice(("/Content/images/login/").length);

            common.setCookie("bg-img", img_name);
        });
    },

    bindCheckEvent: function () {
        $("#cb-remenber").click(function () {
            $(this).toggleClass("check");
        })
    }
}