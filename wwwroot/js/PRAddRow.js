$(document).ready(function () {
    $('#add').click(function () {
        var row = $('.prrow').clone();
        $('#productlist').append(row);
        row.removeClass('prrow').addClass('pr').show();
    });
});