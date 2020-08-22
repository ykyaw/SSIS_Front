$(document).ready(function () {
    $("#approve").click(function () {
        let Id = +$("#formid").text();
        let Remarks = $("#remarks").val();
        let requisition = {
            Id,
            Remarks,
            Status: "Approved"
        }
        Put(`/Department/UpdateRequisition/`, requisition)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                $('#updaterequisition').fadeOut();
                $('#status').hide().text("Approved").fadeIn();
                $('<tr><td>' + 'Reason' +
                    '</td><td>' + Remarks +
                    '</td></tr>').hide().appendTo('#form').fadeIn();
            })
            .catch(function (err) {
                console.log("error: " + err);
            })
    })
    $("#reject").click(function () {
        let Id = +$("#formid").text();
        let Remarks = $("#remarks").val();
        let requisition = {
            Id,
            Remarks,
            Status: "Rejected"
        }
        Put(`/Department/UpdateRequisition/`, requisition)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                $('#updaterequisition').fadeOut();
                $('#status').hide().text("Rejected").fadeIn();
                $('<tr><td>' + 'Reason' +
                    '</td><td>' + Remarks +
                    '</td></tr>').hide().appendTo('#form').fadeIn();
            })
            .catch(function (err) {
                console.log("error: " + err);
            })
    })
})