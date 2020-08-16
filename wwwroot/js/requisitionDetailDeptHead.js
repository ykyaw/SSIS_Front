$(document).ready(function () {

    $("#approve").on("click", function () {
        let Id = +$("#reqdetailId").val();
        let reason = $("#reason").val();
        let requisition = { Id: Id, Remarks: reason, Status: "Pending Approval" }
        Put(`/Department/viewRequisitionDeptHead/${Id}`, requisition)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                if (response) {
                    alert("Successfully approved.")
                }
                else {
                    alert("Something wrong.")
                }
                alert("success: " + response);
            })
            .catch(function (err) {
                console.log("error: " + err);
                alert("Error.")
            })
    })

    $("#reject").on("click", function () {
        let Id = +$("#reqdetailId").val();
        let reason = $("#reason").val();
        let requisition = { Id: Id, Remarks: reason, Status: "Pending Approval" }
        Put(`/Department/viewRequisitionDeptHead/${Id}`, requisition)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                alert("success: " + response);
            })
            .catch(function (err) {
                console.log("error: " + err);
            })
    })
})