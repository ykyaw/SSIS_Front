$(document).ready(function () {
    let currentBalance = $("#balance").val(); 
    $("#addEntry").click(function () {
        let date = new Date();
        let dateString = date.getFullYear() + " " + (date.getMonth() + 1) + " " + date.getDate();
        let description = $("#description").val();
        let qty = $("#qty").val();
        let balance = new Number(currentBalance) + new Number(qty);
        let updatedByEmp = $("#updatedByEmp").val();
        $('#transactionList').append(
            '<tr><td>' + dateString +
            '</td><td>' + description +
            '</td><td>' + qty +
            '</td><td>' + balance +
            '</td><td>' + updatedByEmp +
            '</td></tr>');

        currentBalance = balance;
    });
})