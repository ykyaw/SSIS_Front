$(document).ready(function () {

    var body = $('#myTable').children('tbody').first();
    body.on('change', '.number', function () {
        var cells = $(this).closest('tr').children('td');
        var number1 = cells.eq(3).find('input').val();
        var number2 = cells.eq(4).find('select').val();
        var total = new Number(number1) * new Number(number2);
        var totalprice = '$' + total.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
        cells.eq(6).text(totalprice);
    });

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