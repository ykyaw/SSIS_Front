$(document).ready(function () {
    $("#allvouchers").hide();
    $("#search").on("input", debounce(function () {
        $(".adjustmentvoucher").each(function () {
            if (!$(this).html().includes($("#search").val())) {
                $(this).fadeOut();
            } else {
                $(this).fadeIn();
            }
        })
    }, 1000));
    $("#viewclerkhistory").click(function () {
        $("#viewclerkhistory").hide();
        $("#allvouchers").show();
        let clerkid = $("input[type='hidden']").val();
        console.log(clerkid)
        $(".adjustmentvoucher").each(function () {
            let clerkid2 = $(this).nextAll('.clerkid').attri("value");
            console.log(clerkid2)
            if (clerid2 != clerid) {
                $(this).fadeOut();
            } else {
                $(this).fadeIn();
            }
        })
    });

})
