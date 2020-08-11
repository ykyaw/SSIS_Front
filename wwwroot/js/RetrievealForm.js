$(document).ready(function () {
    $("#needVoucher").on("click", function () {
        if ($("#needVoucher").is(":checked")) {
            $("#comments").show();
        } else {
            $("#comments").hide();
        }
    })


    $("#update").on("click", function () {
        let retrieval = getUpdateRetriveal();
        console.log("retrieval", retrieval);
        Put("/StoreClerk/Retrieval", retrieval)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                alert("success: " + response);
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    });

    $("#finalise").on("click", function () {
        let retrieval = getUpdateRetriveal();
        console.log("retrieval", retrieval);
        Put("/StoreClerk/FinaliseRetrieval", retrieval)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                alert("success: " + response);
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    })
});


function getUpdateRetriveal() {
    let actualNumbers = $(".actualNumber");
    let requisitionDetails = [];
    for (let numberInput of actualNumbers) {
        let requisition = {
            QtyDisbursed: +$(numberInput).val(),
            DisburseRemark: $(numberInput).parent().next().children(".remarks").val(),
            Id: +$(numberInput).parent().parent().attr("id"),
        };
        requisitionDetails.push(requisition);
    }
    let retrievalId = $("#retrievalId").attr("value");
    let NeedAdjustment = $("#needVoucher").is(":checked");
    let Remark = $("#comments").val();
    let retrieval = {
        Id: +retrievalId,
        requisitionDetails,
        NeedAdjustment,
        Remark
    }
    return retrieval;
}