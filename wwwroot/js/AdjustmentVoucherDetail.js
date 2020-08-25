$(document).ready(function () {
    $("#generate").click(function () {
        $('.disappear').fadeOut();
        if ($('#product').val() != "0") {
            let productid = $('#product').val();
            let quantity = +$("#qtyAdjusted").val();
            let reason = $("#reason").val();
            if (quantity != 0 && reason != "") {
                Get(`/StoreClerk/GetProductPrice/` + productid)
                    .then(function (response) {
                        let totalprice = response * quantity;
                        $('#avdetails').append(
                            '<tr><td class="productId">' + $('#product').val() +
                            '</td><td class="description">' + $('#product').find(':selected').data('desc') +
                            '</td><td class="quantity">' + quantity +
                            '</td><td class="uom">' + $('#product').find(':selected').data('uom') +
                            '</td><td class="reason">' + reason +
                            '</td><td class="price">' + '$' + response.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") +
                            '</td><td class="totalprice">' + '$' + totalprice.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") +
                            '</td><td> <input type="button" value="Remove" class="btn btn-danger"/>' +
                            '</td></tr>');
                    })
            }
            else {
                alert("Quantity can't be 0 and Reason can't be empty")
            }
        }
        else {
            alert("Please select a product")
        }
    })

    $('#avdetails').on('click', 'input[type="button"]', function () {
        $(this).closest('tr').fadeOut(200, function () {
            $(this).remove();
        })
    });

    $('#save').click(function () {
        let details = getUpdatedDetails();
        if (details.length != 0) {
            Put(`/StoreClerk/SaveVoucher`, details)
                .then(function (response) {
                    console.log(response);
                    alert("Adjustment Voucher Details Saved");
                })
                .catch(function (err) {
                    //alert("error: " + JSON.parse(err));
                })
        } else {
            alert("Adjustment Voucher Details Saved")
        }
    })

    $('#submit').click(function () {
        let details = getUpdatedDetails();
        if (details.length != 0) {
            Put(`/StoreClerk/SubmitVoucher`, details)
                .then(function (response) {
                    console.log(response);
                    alert("Adjustment Voucher Submited, pending approval");
                    window.location.href = "/StoreClerk/AdjustmentVoucherDetails/" + response.ad;
                })
                .catch(function (err) {
                    //alert("error: " + JSON.parse(err));
                })
        }
        else {
            alert("Can't submit an empty voucher.")
        }
    })
})

function getUpdatedDetails() {
    let AdjustmentVoucherId = $("#voucherId").attr('value');
    let details = [];
    $(".productId").each(function () {
        let detail = {
            AdjustmentVoucherId,
            ProductId: $(this).text(),
            QtyAdjusted: +$(this).nextAll('.quantity').text(),
            Unitprice: +$(this).nextAll('.price').text().replace(/[^\d.]/g, ''),
            TotalPrice: +$(this).nextAll('.totalprice').text().replace(/[^\d.]/g, ''),
            Reason: $(this).nextAll('.reason').text()
        }
        details.push(detail);
    });
    return details;
}