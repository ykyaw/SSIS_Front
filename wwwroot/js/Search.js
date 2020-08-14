$(document).ready(function () {
    (function ($) {
        $('#search').keyup(function () {
            var search = new RegExp($(this).val(), 'i');
            $('.search tr').hide();
            $('.search tr').filter(function () {
                return search.test($(this).text());
            }).show();
        })
        $('#select').change(function () {
            var search = new RegExp($(this).val(), 'i');
            $('.search tr').hide();
            $('.search tr').filter(function () {
                return search.test($(this).text());
            }).show(); 
        })
    }(jQuery));
})