$(document).ready(function () {
    $('[type="date"]').prop('max', function () {
        return new Date().toJSON().split('T')[0];
    });

    $('#save').on("click", function () {
        let details = [];
        let QtyReceived;
        let ReceivedDate;
        let SupplierDeliveryNo;
        let Remark;
        $('.detailId').each(function () {
            let detail = {
                Id: $(this).val(),
                QtyReceived,
                ReceivedDate,
                SupplierDeliveryNo,
                Remark,
                Status: "Received"
            }
            details.push(detail);
        });
    });
});