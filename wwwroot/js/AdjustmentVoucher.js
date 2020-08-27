$(document).ready(function () {
    $('#legends').hide();
    $('#legendsbtn').on('click', function () {
        $('#legends').fadeToggle();
    })
    let clerkid = $('#select').attr('data-cid');
    $('#search').on('input', function () {
        $('.search').each(function (index) {
            let search = $(this).html().toLowerCase();
            let select = $(this).find('td.select').attr('value');
            if (clerkid == "") {
                if (search.indexOf($('#search').val().toLowerCase()) != -1 && select == $('#select').attr('data-cid')) {
                    $(this).fadeIn();
                } else {
                    $(this).fadeOut();
                }
            } else {
                if (search.indexOf($('#search').val().toLowerCase()) != -1) {
                    $(this).fadeIn();
                } else {
                    $(this).fadeOut();
                }
            }

        })
    })
    $('#select').on('click', function () {
        $('.search').each(function (index) {
            select = $(this).find('td.select').attr('value');
            if (clerkid == select || clerkid == "") {
                $(this).fadeIn();
            } else {
                $(this).fadeOut();
            }
        })
        if (clerkid == "") {
            clerkid = $('#select').attr('data-cid');
            $('#select').val("View My Adjustment Vouchers");
        } else {
            clerkid = "";
            $('#select').val("View All Adjustment Vouchers");
        }
    })
    $('#list').on('click', 'button.delete', function () {
        let btn = $(this);
        if (confirm("Are you sure you want to delete this Adjustment Voucher?")) {
            let Id = $(this).parent().siblings('.listid').text();
            Delete(`/StoreClerk/DeleteAdjustmentVoucher/` + Id)
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