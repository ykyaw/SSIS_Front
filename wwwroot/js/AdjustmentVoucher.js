$(document).ready(function () {
    $("#generate").click(function () {
        let product = $("#product").val();
        let productid = product.split("/")[0];
        let description = product.split("/")[1];
        let quantity = $("#qtyAdjusted").val();
        let reason = $("#reason").val();
        Get(`/StoreClerk/gettenderprice/` + productid)
            .then(function (response) {
                let price = response;
                let totalprice = new Number(price) * new Number(quantity)



                $('#avdetails').append(
                    '<tr><td class="productId">' + productid +
                    '</td><td class="description">' + description +
                    '</td><td class="quantity">' + quantity +
                    '</td><td class="reason">' + reason +
                    '</td><td class="price">' + price +
                    '</td><td class="totalprice">' + totalprice +
                    '</td><td> <input type="button" value="Remove"/>' +
                    '</td></tr>');

            })
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
            let productid = $(this).text()
            let quantity = +$(this).nextAll('.quantity').text()
            let price = +$(this).nextAll('.price').text()
            let totalprice = +$(this).nextAll('.totalprice').text()
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
        Put(`/StoreClerk/SaveAdjustmentVoucherDetails`, VoucherDetails)
            .then(function (response) {
                console.log(response);
                alert("Adjustment Voucher Details Saved!");

            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    })


    $('#submit').click(function () {
        let AdjustmentVoucherId = $("#voucherId").attr("value");
        let VoucherDetails = [];
        $(".productId").each(function () {
            let productid = $(this).text()
            let quantity = +$(this).nextAll('.quantity').text()
            let price = +$(this).nextAll('.price').text()
            let totalprice = +$(this).nextAll('.totalprice').text()
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
        Put(`/StoreClerk/SubmitAdjustmentVoucherDetails`, VoucherDetails)
            .then(function (response) {
                console.log(response);
                alert("Adjustment Voucher Submited, pending approval or reject");
                window.location.href = "/StoreClerk/AdjustmentVoucherDetails/" + AdjustmentVoucherId;

            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    })

})