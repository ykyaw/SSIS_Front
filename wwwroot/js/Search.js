$(document).ready(function () {
    $('#legends').hide();
    $('#legendsbtn').on('click', function () {
        $('#legends').fadeToggle();
    })
    let select = "";
    $('#search, #select').on('input', function () {
        $('.search').each(function (index) {
            let search = $(this).html().toLowerCase();
            select = $(this).find('td.select').text();
            if (search.indexOf($('#search').val().toLowerCase()) != -1 && (select.indexOf($('#select').val()) != -1 || select == "")) {
                $(this).fadeIn();
            } else {
                $(this).fadeOut();
            }
        })
    })
    $('#list').on('click', 'button.delete', function () {
        let btn = $(this);
        if (confirm("Are you sure you want to delete this Requisition?")) {
            let Id = +$(this).parent().siblings('.listid').text();
            Delete(`/Department/Requisition/` + Id)
                .then(function (response) {
                    console.log(response);
                    btn.parent().closest('tr').fadeOut(200, function () {
                        $(this).remove();
                    });
                })
                .catch(function (err) {
                    //alert("error: " + JSON.parse(err));
                })
        }
        return false;
    })
})