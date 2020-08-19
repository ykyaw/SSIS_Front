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
            $('<tr>' + '<input class="productid" type="hidden" value="' + $('#product').val() + '" />' +
                '<td>' + $('#product').find(':selected').data('desc') +
                '</td><td>' + +$('#qty').val() +
                '</td></tr>').hide().appendTo('#requestlist').fadeIn();
        }
    });

    $('#save').click(function () {
        let details = getUpdatedDetails();
        Post(`/Department/SaveReqStationery`, details)
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
        Post(`/Department/SubmitReqStationery`, details)
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
            QtyNeeded: +$(this).next().next().text()
        }
        details.push(detail);
    });
    return details;
}