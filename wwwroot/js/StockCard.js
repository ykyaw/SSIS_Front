﻿$(document).ready(function () {
    let Balance = +$("#balance").val();
    $("#addEntry").click(function () {
        let ProductId = $("#productId").val();
        let date = new Date();
        let dateString = date.getFullYear() + " " + ('0' + (date.getMonth() + 1)).slice(-2) + " " + date.getDate();
        let Description = $("#description").val();
        let Qty = +$("#qty").val();
        Balance += Qty;
        let updatedByEmp = $("#updatedByEmp").val();
        $('<tr><td>' + dateString +
            '</td><td>' + Description +
            '</td><td>' + Qty +
            '</td><td>' + Balance +
            '</td><td>' + updatedByEmp +
            '</td></tr>').hide().appendTo('#transactionList').fadeIn();

        let transaction = {
            ProductId,
            Date: date.getTime(),
            Description,
            Qty,
            Balance
        }
        Post(`/StoreClerk/UpdateStockCard`, transaction)
            .then(function (response) {
                console.log(response);
                //alert("success: " + response);
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    });
});