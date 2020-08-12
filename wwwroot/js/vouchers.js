$(document).ready(function () {
    $("#search").on("input", debounce(function () {
        $(".voucher").each(function () {
            if (!$(this).html().includes($("#search").val())) {
                $(this).fadeOut();
            } else {
                $(this).fadeIn();
            }
        })
    }, 1000));
})