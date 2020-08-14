$(document).ready(function () {
    $("#confirm").on("click", function () {
        let requisition = getUpdateRequisition();
        console.log("requistion", requistion);
        Put("/StoreClerk/UpdateRequisition", requisition)
            .then(function (response) {
                console.log(response);
                alert("success: " + response);
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    });
});

function getUpdateRequisition() {
    let Id = $("#requisitionId").attr("value");;
    let dateString = $("input[type='date']").val();
    let date = new Date(dateString);
    let CollectionDate = date.getTime();
    let Status = "Confirmed";
    let requisition = {
        Id,
        CollectionDate,
        Status
    }
    return requisition;
}