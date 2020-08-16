﻿$(document).ready(function () {
    date.min = new Date().toISOString().split("T")[0];

    let Id = +$('#requisitionId').text();
    $('#confirm').click(function () {
        let dateString = $("input[type='date']").val();
        let CollectionDate = new Date(dateString);
        let requisition = {
            Id,
            CollectionDate: CollectionDate.getTime(),
            Status: "Confirmed"
        }
        Put(`/StoreClerk/UpdateRequisition`, requisition)
            .then(function (response) {
                console.log(response);
                //alert("success: " + response);
                $('#confirmDate').fadeOut();
                $('#status').fadeOut(function () {
                    $(this).text("Confirmed").fadeIn();
                });
                $("<tr><td>Disbursement Date" +
                    "</td><td>" + (CollectionDate.getFullYear() + " " + ('0' + (CollectionDate.getMonth() + 1)).slice(-2) + " " + CollectionDate.getDate()) +
                    "</td></tr>").hide().appendTo('#form').fadeIn();
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    })
});