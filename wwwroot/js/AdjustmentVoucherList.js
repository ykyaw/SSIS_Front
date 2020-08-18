$(document).ready(function () {
    $("#search").on("input", debounce(function () {
        $(".adjustmentvoucher").each(function () {
            if (!$(this).html().includes($("#search").val())) {
                $(this).fadeOut();
            } else {
                $(this).fadeIn();
            }
        })
    }, 1000));
    $(".adjustmentvoucher").each(function () {
        let status = $(this).nextAll('.status').val();
        if (status != "Created") {
            $(this).nextAll('#amend').disabled = true;
           
        }
    })
})