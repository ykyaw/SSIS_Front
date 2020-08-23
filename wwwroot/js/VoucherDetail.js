$(document).ready(function () {
    let role = $("#role").text();
    let isMoreThan250 = false;
    $(".totalPrice").each(function () {
        if (+$(this).text().substring(1) > 250) {
            isMoreThan250 = true
        }
    })

    $("#approve").on("click", function () {
        let Id = $("#id").text();
        let Status = isMoreThan250 ? role == "Manager" ? "Approved" : "Pending Manager Approval" : "Approved";
        console.log(Id);
        let voucher = {
            Id,
            Status
        }
        Put(`/Store/Voucher/${Id}`, voucher)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                $('#operation').fadeOut();
                $('#status').hide().text(Status).fadeIn();
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    })


    $("#reject").on("click", function () {
        let Id = $("#id").text();
        let Reason = $("#reason").val();
        let voucher = {
            Id,
            Status: "Rejected",
            Reason
        }
        Put(`/Store/Voucher/${Id}`, voucher)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                $('#operation').fadeOut();
                $('#status').hide().text("Rejected").fadeIn();
                $('<tr><td>' + 'Reason: ' +
                    '</td><td>' + '&nbsp;&nbsp;&nbsp;' +
                    '</td><td>' + Reason +
                    '</td></tr>').hide().appendTo('#form').fadeIn();
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    })
})