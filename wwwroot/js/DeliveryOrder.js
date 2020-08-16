$(document).ready(function () {
    $("#save").on("click", function () {
        let doDetails = [];
        let qtyReceived = $(".qtyReceived");
        for (let numberInput of qtyReceived) {
            let receivedDate = dateTransform($(numberInput).)
            let deliveryorder = {
                Id: +$(numberInput).parent().parent().attr("id"),
                QtyReceived: +$(numberInput).val(),
                ReceivedDate
            }
        }
        let Id;
        let QtyReceived;
        let ReceivedDate;
        let SupplierDeliveryNo;
        let Remark;
        let Status;
    });
})