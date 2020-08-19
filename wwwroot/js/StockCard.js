$(document).ready(function () {
    let Balance = +$("#balance").val();
    $("#addEntry").click(function () {
        let ProductId = $("#productId").val();
        let date = new Date();
        let dateString = date.getFullYear() + "/" + ('0' + (date.getMonth() + 1)).slice(-2) + "/" + date.getDate();
        let Description = $("#description").val();
        let RefCode = $("#refcode").val();
        let Qty = +$("#qty").val();
        Balance += Qty;
        let updatedByEmp = $("#updatedByEmp").val();
        if (!$.isNumeric($("#qty").val())) {
            alert("Quantity must be a number");
            return;
        } else if (Balance < 0) {
            alert("There is not enough balance.");
            return;
        } else {
            $('<tr><td>' + dateString +
                '</td><td>' + Description +
                '</td><td>' + RefCode +
                '</td><td>' + Qty +
                '</td><td>' + Balance +
                '</td><td>' + updatedByEmp +
                '</td></tr>').hide().appendTo('#transactionList').fadeIn();

            let transaction = {
                ProductId,
                Date: date.getTime(),
                Description,
                RefCode,
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
        }
    });
});