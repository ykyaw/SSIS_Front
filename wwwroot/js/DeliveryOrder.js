$(document).ready(function () {
    $('[type="date"]').prop('max', function () {
        return new Date().toJSON().split('T')[0];
    });
    let PurchaseOrderId = +$('#poid').text();
    $('#save').on("click", function () {
        let details = [];
        $('.detailId').each(function () {
            let SupplierDeliveryNo = $(this).nextAll().eq(5).find('input');
            if (SupplierDeliveryNo.is('input') && isNaN(SupplierDeliveryNo.val())) {
                alert("Supplier Delivery Number cannot contain letters.");
                return;
            }
            if (SupplierDeliveryNo.val() == "") {
                alert("Supplier Delivery Number cannot be empty.");
                return;
            }
            let dateString = $(this).nextAll().eq(4).find('input').val();
            let date = new Date(dateString);
            let detail = {
                Id: +$(this).val(),
                PurchaseOrderId,
                QtyReceived: +$(this).nextAll().eq(3).find('input').val(),
                ReceivedDate: date.getTime(),
                SupplierDeliveryNo: +SupplierDeliveryNo.val(),
                Remark: $(this).nextAll().eq(6).find('input').val(),
                Status: "Received"
            }
            if (!(isNaN(detail.ReceivedDate) || isNaN(detail.SupplierDeliveryNo))) {
                details.push(detail);
            }
        });
        Put(`/StoreClerk/UpdateDelivery`, details)
            .then(function (response) {
                console.log(response);
                //alert("success: " + response);
                $('.detailId').each(function () {
                    let dateString = $(this).nextAll().eq(4).find('input').val();
                    let date = new Date(dateString);
                    let SupplierDeliveryNo = +$(this).nextAll().eq(5).find('input').val();
                    if (!(isNaN(date.getTime()) || isNaN(SupplierDeliveryNo))) {
                        $(this).siblings('td').find('input').fadeOut(function () {
                            let replacement = $('<span class=' + this.className + '>' + this.value + '</span>')
                            $(this).replaceWith(replacement).fadeIn();
                        });
                        if ($('input').length - $('.detailId').length == 4) {
                            $('#save').fadeOut();
                        }
                    }
                })
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    });
});