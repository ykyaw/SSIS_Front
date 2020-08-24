$(document).ready(function () {
    let select = "";
    $('#search, #select').on('input', function () {
        $('.search').each(function (index) {
            let search = $(this).html().toLowerCase();
            select = $(this).find('td.select').text();
            if (search.indexOf($('#search').val().toLowerCase()) != -1 && select.indexOf($('#select').val()) != -1 || select == "")) {
                $(this).fadeIn();
            } else {
                $(this).fadeOut();
            }
        })
    })
})