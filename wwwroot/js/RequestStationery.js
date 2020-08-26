$(document).ready(function () {
    if ($('#requestlist tr').length >= 1) {
        $('#submit').prop('disabled', false);
    }
    $('#product').change(function () {
        $('#uom').text($(this).find(':selected').data('uom'))
    });
    $('#add').click(function () {
        if ($('#product').val() == 0) {
            alert("Please select a product");
            return;
        } else if ($('#qty').val() > 99 || $('#qty').val() < 1) {
            alert("Quantity must be between 1 and 99");
            return;
        } else if (isNaN($('#qty').val())) {
            alert("Quantity must be a number");
            return;
        } else {
            $('#submit').prop('disabled', false);
            $('<tr>' + '<input class="productid" type="hidden" value="' + $('#product').val() + '" />' +
                '<td>' + $('#product').find(':selected').data('desc') +
                '</td><td>' + '<input type="number" class="form-control" min="1" max="99" value="' + $('#qty').val() + '" />' +
                '</td><td>' + $('#product').find(':selected').data('uom') +
                '</td><td>' + '<input type="button" class="btn btn-danger" value="Remove" />' +
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