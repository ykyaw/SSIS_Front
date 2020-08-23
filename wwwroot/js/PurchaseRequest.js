$(document).ready(function () {
    validate();
    $('input').on('keyup', validate);
    $('.number').change(function () {
        let cells = $(this).closest('tr').children('td');
        let number1 = +cells.eq(3).find('input').val();
        let number2 = +cells.eq(4).find('select').val();
        let total = number1 * number2;
        let totalprice = '$' + total.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
        cells.eq(6).text(totalprice);
    });

    $("#quote").on("click", function () {
        let PurchaseRequestId = +$("#requestId").text();
        let Status = "Created";
        let details = [];
        $(".detailId").each(function () {
            let cells = $(this).closest('tr').children('td');
            let number1 = +cells.eq(3).find('input').val();
            let number2 = +cells.eq(4).find('select').val();
            let TotalPrice = number1 * number2;
            let detail = {
                Id: +$(this).val(),
                PurchaseRequestId,
                ProductId: $(this).next().next().closest('td').data('productid'),
                ReorderQty: +$(this).nextAll().eq(3).find('input').val(),
                SupplierId: $(this).nextAll().eq(4).find('option:selected').data('sid'),
                VenderQuote: $(this).nextAll().eq(5).find('input').val(),
                TotalPrice,
                Status
            }
            details.push(detail);
        })
        Post("/StoreClerk/GenerateQuote", details)
            .then(function (response) {
                console.log(response);
                alert("generated: " + response);
            })
            .catch(function (err) {
                console.log(err);
            })
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
                ReorderQty: +$(this).nextAll().eq(3).find('input').val(),
                SupplierId: $(this).nextAll().eq(4).find('option:selected').data('sid'),
                VenderQuote: $(this).nextAll().eq(5).find('input').val(),
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
                ReorderQty: +$(this).nextAll().eq(3).find('input').val(),
                SupplierId: $(this).nextAll().eq(4).find('option:selected').data('sid'),
                VenderQuote: $(this).nextAll().eq(5).find('input').val(),
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
        let PurchaseRequestId = +$("#requestId").text();
        let Status = "Rejected";
        let details = [];
        $(".detailId").each(function () {
            let detail = {
                Id: +$(this).val(),
                Remarks,
                PurchaseRequestId,
                Status
            }
            details.push(detail);
        })
        Put("/Store/PurchaseRequestDetail", details)
            .then(function (response) {
                console.log(response);
                $('#status').hide().text(Status).fadeIn();
                $('#supervisorButton').fadeOut();
            })
            .catch(function (err) {
                console.log(err);
            })
    })
    $("#approve").on("click", function () {
        let PurchaseRequestId = +$("#requestId").text();
        let Status = "Approved";
        let details = [];
        $(".detailId").each(function () {
            let detail = {
                Id: +$(this).val(),
                PurchaseRequestId,
                Status
            }
            details.push(detail);
        })
        Put("/Store/PurchaseRequestDetail", details)
            .then(function (response) {
                console.log(response);
                $('#status').hide().text(Status).fadeIn();
                $('#supervisorButton').fadeOut();
                $('<tr><td>' + 'Reason: ' +
                    '</td><td>' + '&nbsp;&nbsp;&nbsp;' +
                    '</td><td>' + Reason +
                    '</td></tr>').hide().appendTo($('#status').parent().parent()).fadeIn();
            })
            .catch(function (err) {
                console.log(err);
            })
    })

});

function validate() {
    let inputs = 0;
    let myinputs = $("input");
    myinputs.each(function (e) {
        if ($(this).val()) {
            inputs += 1;
        }
    });

    if (inputs == myinputs.length) {
        $("#submit").prop("disabled", false);
    } else {
        $("#submit").prop("disabled", true);
    }
}