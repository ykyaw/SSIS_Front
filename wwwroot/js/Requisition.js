$(document).ready(function () {
    date.min = new Date().toISOString().split("T")[0];

    let Id = +$('#requisitionId').text();
    $('#confirm').click(function () {
        let dateString = $("input[type='date']").val();
        if (dateString == "") {
            alert("Please select a Disbursement Date to confirm!")
            return 
        }
        console.log(dateString);
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
                $("<tr><td>Disbursement Date: " +
                    "</td><td>&nbsp;&nbsp;&nbsp;</td><td>" + (CollectionDate.getFullYear() + "/" + ('0' + (CollectionDate.getMonth() + 1)).slice(-2) + "/" + ('0' + CollectionDate.getDate()).slice(-2) +
                    "</td></tr>").hide().appendTo('#form').fadeIn();
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    })
});