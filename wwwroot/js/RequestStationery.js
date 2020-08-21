$(document).ready(function () {
    $('#add').click(function () {
        if (!$.isNumeric($('#qty').val())) {
            alert("Quantity must be a number");
            return;
        } else if ($('#qty').val() == 0) {
            alert("Quantity must be more than 0");
            return;
        } else if ($('#product').val() == 0) {
            alert("Please select a product");
            return;
        } else {
            $('#submit').prop('disabled', false);
            $('<tr>' + '<input class="productid" type="hidden" value="' + $('#product').val() + '" />' +
                '<td>' + $('#product').find(':selected').data('desc') +
                '</td><td>' + '<input type="number" min="1" value="' + $('#qty').val() + '" />' +
                '</td><td>' + '<input type="button" value="Remove" />' +
                '</td></tr>').hide().appendTo('#requestlist').fadeIn();
        }
    });

    $('#requestlist').on('click', 'input[type="button"]', function () {
        $(this).closest('tr').fadeOut(200, function () {
            $(this).remove();
        });
        if ($('#requestlist tr').length == 1) {
            $('#submit').prop('disabled', true);
        }
    });

    $('#save').click(function () {
        let details = getUpdatedDetails();
        Post(`/Department/SaveRequest`, details)
            .then(function (response) {
                console.log(response);
                alert("success: " + response);
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    });

    $('#submit').click(function () {
        let details = getUpdatedDetails();
        Post(`/Department/SubmitRequest`, details)
            .then(function (response) {
                console.log(response);
                alert("success: " + response);
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    });
});

function getUpdatedDetails() {
    let RequisitionId = +$('#formid').text();
    let details = [];
    $(".productid").each(function () {
        let detail = {
            RequisitionId,
            ProductId: $(this).val(),
            QtyNeeded: +$(this).nextAll().find('input[type="number"]').val()
        }
        details.push(detail);
    });
    return details;
}