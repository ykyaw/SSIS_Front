$(document).ready(function () {
    $('#search').keyup(function () {
        let search = new RegExp($(this).val(), 'i');
        $('.search tr').hide();
        $('.search tr').filter(function () {
            return search.test($(this).text());
        }).show();
    })
    $('#select').change(function () {
        let search = new RegExp($(this).val(), 'i');
        $('.search tr').hide();
        $('.search tr').filter(function () {
            return search.test($(this).text());
        }).show();
    })
})