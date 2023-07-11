$(function () {

    $(".nav-sidebar>li>a").click(function () {
        if($(this).attr("target")=="_blank")
            return ;
        var brothers = $(this).parents("ul")//所在ul
            .siblings("ul")//所有兄弟的ul
            .find("li>ul")
            .collapse("hide");
    });

    $(".sidebar .nav-sub").on("show.bs.collapse", function (event) {
        var el = $(event.target).parent().prev();
        el.find(".menu-xiangyou").hide();
        el.find(".menu-xiangxia").show();


    });

    $(".sidebar .nav-sub").on("hide.bs.collapse", function (event) {
        var el = $(event.target).parent().prev();
        el.find(".menu-xiangyou").show();
        el.find(".menu-xiangxia").hide();
    });

});