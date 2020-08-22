$(document).ready(function () {
    
    let element = document.getElementById("reason");
    element.setAttribute('required', '')
    $("#generate").click(function () {
        $(".disappear").hide();
        let e = document.getElementById("product");
        let result = e.options[e.selectedIndex].value;
        if (result != "0") {
            let product = $("#product").val();
            let productid = product.split("/")[0].trim();
            console.log(productid)
            let description = product.split("/")[1];
            let uom = product.split("/")[2];
            let quantity = +$("#qtyAdjusted").val();
            let reason = $("#reason").val();
            console.log(reason)
            if (quantity != 0 && reason != "") {
                Get(`/StoreClerk/gettenderprice/` + productid)
                    .then(function (response) {
                        let price = response;
                        let totalprice = new Number(price) * new Number(quantity);



                        $('#avdetails').append(
                            '<tr><td class="productId">' + productid +
                            '</td><td class="description">' + description +
                            '</td><td class="quantity">' + quantity +
                            '</td><td class="uom">' + uom +
                            '</td><td class="reason">' + reason +
                            '</td><td class="price">' + '$' + price.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") +
                            '</td><td class="totalprice">' + '$' + totalprice.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") +
                            '</td><td> <input type="button" value="Remove"/>' +
                            '</td></tr>');

                    })
            }
            else { alert("Both quantity and reason should not be empty") }
        }
        else { alert("please select a product") }

    })
    $('#avdetails').on('click', 'input[type="button"]', function () {
        $(this).closest('tr').fadeOut(200, function () {
            $(this).remove();
        })
    });

    $('#save').click(function () {
        let AdjustmentVoucherId = $("#voucherId").attr("value");
        let VoucherDetails = [];
        $(".productId").each(function () {
            let productid = $(this).text().trim()
            let quantity = +$(this).nextAll('.quantity').text()
            let price = $(this).nextAll('.price').text()
            price = Number(price.replace(/[^0-9.-]+/g, ""));
            let totalprice = $(this).nextAll('.totalprice').text()
            totalprice = Number(totalprice.replace(/[^0-9.-]+/g, ""));
            let reason = $(this).nextAll('.reason').text()
            //console.log(id +"qty:"+qty)
            AdjustmentVoucherDetail = {
                AdjustmentVoucherId: AdjustmentVoucherId,
                ProductId: productid,
                QtyAdjusted: quantity,
                Unitprice: price,
                TotalPrice: totalprice,
                Reason: reason
            }

            VoucherDetails.push(AdjustmentVoucherDetail);
        })
        console.log(VoucherDetails);
        if (VoucherDetails.length != 0) {
            Put(`/StoreClerk/SaveAdjustmentVoucherDetails`, VoucherDetails)
                .then(function (response) {
                    console.log(response);
                    alert("Adjustment Voucher Details Saved");

                })
                .catch(function (err) {
                    //alert("error: " + JSON.parse(err));
                })
        }
        else {
            Put(`/StoreClerk/SaveEmptyAdjustmentVoucherDetails/`, AdjustmentVoucherId)
                .then(function (response) {
                    console.log(response);
                    alert("Adjustment Voucher Details Saved")
                })
                .catch(function (err) {
                    //alert("error: " + JSON.parse(err));
                })
        }

    })


    $('#submit').click(function () {
        let AdjustmentVoucherId = $("#voucherId").attr("value");
        let VoucherDetails = [];
        $(".productId").each(function () {
            let productid = $(this).text().trim()
            let quantity = +$(this).nextAll('.quantity').text()
            let price = $(this).nextAll('.price').text()
            price = Number(price.replace(/[^0-9.-]+/g, ""));
            let totalprice = $(this).nextAll('.totalprice').text()
            totalprice = Number(totalprice.replace(/[^0-9.-]+/g, ""));
            let reason = $(this).nextAll('.reason').text()
            AdjustmentVoucherDetail = {
                AdjustmentVoucherId: AdjustmentVoucherId,
                ProductId: productid,
                QtyAdjusted: quantity,
                Unitprice: price,
                TotalPrice: totalprice,
                Reason: reason
            }

            VoucherDetails.push(AdjustmentVoucherDetail);

            console.log(VoucherDetails)
            if (VoucherDetails.length != 0) {
                console.log(VoucherDetails);
                Put(`/StoreClerk/SubmitAdjustmentVoucherDetails`, VoucherDetails)
                    .then(function (response) {
                        console.log(response);
                        alert("Adjustment Voucher Submited, pending approval");
                        window.location.href = "/StoreClerk/AdjustmentVoucherDetails/" + AdjustmentVoucherId;

                    })
                    .catch(function (err) {
                        //alert("error: " + JSON.parse(err));
                    })
            }
            else {
                alert("unable to submit a empty voucher")
            }
        })

    })
})