$(document).ready(function () {
    let role = $("#role").text();
    let isMoreThan250 = false;
    $(".totalPrice").each(function () {
        if (+$(this).text().substring(1) > 250) {
            isMoreThan250=true
        }
    })

    $("#approve").on("click", function () {
        let Id = $("#id").text();
        console.log(Id);
        let voucher = {
            Id,
            Status: isMoreThan250 ? role == "Manager" ? "Approved" : "Pending Manager Approval" :"Approved"
        }
        Put(`/Voucher/${Id}`, voucher)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                alert("success: " + response);
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
        Put(`/Voucher/${Id}`, voucher)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                alert("success: " + response);
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    })
})