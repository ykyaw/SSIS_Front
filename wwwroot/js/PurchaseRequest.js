$(document).ready(function () {

    let body = $('#form').children('tbody').first();
    body.on('change', '.number', function () {
        let cells = $(this).closest('tr').children('td');
        let number1 = +cells.eq(3).find('input').val();
        let number2 = +cells.eq(4).find('select').val();
        let total = number1 * number2;
        let totalprice = '$' + total.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
        cells.eq(6).text(totalprice);
    });

    $("#submit").on("click", function () {
        let PurchaseRequestId = $("#requestId").text();
        let Status = "Pending Approval";
        let details = [];
        $(".detailId").each(function () {
            let detail = {
                Id: $(this).val(),
                PurchaseRequestId,
                Status
            }
            details.push(detail);
        })
        Put("/StoreClerk/PurchaseRequest", details)
            .then(function (response) {
                console.log(response);
            })
            .catch(function (err) {
                console.log(err);
            })
    })

    $("#reject").on("click", function () {
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
    $("#approve").on("click", function () {
        let PurchaseRequestId = $("#requestId").text();
        let Status = "Approved";
        let details = [];
        $(".detailId").each(function () {
            let detail = {
                Id: $(this).val(),
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

});