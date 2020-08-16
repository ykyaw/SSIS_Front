$(document).ready(function () {
    $("#save").on("click", function () {
        let details = [];

        $(".detailId").each(function () {

            let detail = {
                Id: $(this).val(),
                
            }










        $(".totalPrice").each(function () {
            if (+$(this).text().substring(1) > 250) {
                isMoreThan250 = true
            }
        })

        let Remarks = $("#Reason").val();
        let PurchaseRequestId = $("#requestId").text();
        let Status = "Rejected";
        let details = [];
        $(".detailId").each(function () {
            let detail = {
                Id: $(this).val(),
                Remarks,
                PurchaseRequestId,
                Status
            }
            details.push(detail);
        })
        Put("/Store/PurchaseRequestDetail", details)
            .then(function (response) {
                console.log(response);
            })
            .catch(function (err) {
                console.log(err);
            })
    })
})