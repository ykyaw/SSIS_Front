$(document).ready(function () {

    $('.number').change(function () {
        let cells = $(this).closest('tr').children('td');
        let number1 = +cells.eq(3).find('input').val();
        let number2 = +cells.eq(4).find('select').val();
        let total = number1 * number2;
        let totalprice = '$' + total.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
        cells.eq(6).text(totalprice);
    });

    $('.venderquote').keyup(function () {
        let empty = false;
        $(this).each(function () {
            if ($(this).val().length == 0) {
                empty = true;
            }
        });
        if (empty) {
            $('#submit').attr('disabled', 'disabled');
        } else {
            $('#submit').attr('disabled', false);
        }
    })

    $("#quote").on("click", function () {
        //TODO
    })

    $("#update").on("click", function () {
        let Status = "Created";
        let details = [];
        $(".detailId").each(function () {
            let cells = $(this).closest('tr').children('td');
            let number1 = +cells.eq(3).find('input').val();
            let number2 = +cells.eq(4).find('select').val();
            let TotalPrice = number1 * number2;
            let detail = {
                Id: +$(this).val(),
                ReorderQty: +$('.reorderqty').val(),
                SupplierId: $('.supplierid').find('option:selected').data('sid'),
                VenderQuote: $('.venderquote').val(),
                TotalPrice,
                Status
            }
            details.push(detail);
        })
        Put("/StoreClerk/PurchaseRequest", details)
            .then(function (response) {
                console.log(response);
                alert("updated: " + response);
            })
            .catch(function (err) {
                console.log(err);
            })
    })

    $("#submit").on("click", function () {
        let PurchaseRequestId = $("#requestId").text();
        let Status = "Pending Approval";
        let details = [];
        $(".detailId").each(function () {
            let cells = $(this).closest('tr').children('td');
            let number1 = +cells.eq(3).find('input').val();
            let number2 = +cells.eq(4).find('select').val();
            let TotalPrice = number1 * number2;
            let detail = {
                Id: +$(this).val(),
                ReorderQty: +$('.reorderqty').val(),
                SupplierId: $('.supplierid').find('option:selected').data('sid'),
                VenderQuote: $('.venderquote').val(),
                TotalPrice,
                Status
            }
            details.push(detail);
        })
        Put("/StoreClerk/PurchaseRequest", details)
            .then(function (response) {
                console.log(response);
                window.location.href = "/StoreClerk/PurchaseRequest/" + PurchaseRequestId;
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