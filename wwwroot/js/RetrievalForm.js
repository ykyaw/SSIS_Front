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
        retrieval.Status = "Created";
        Put("/StoreClerk/Retrieval", retrieval)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                if (response.code && response.code != 200) {
                    alert(response.msg);
                } else {
                    alert("Successfully updated");
                }
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    });

    $("#finalise").on("click", function () {
        let retrieval = getUpdateRetriveal();
        console.log("retrieval", retrieval);
        retrieval.Status = "Retrieved";
        Put("/StoreClerk/Retrieval", retrieval)
            .then(function (response) {
                if (response.code && response.code != 200) {
                    alert(response.msg);
                } else {
                    let date = new Date();
                    let dateString = date.getFullYear() + "/" + ('0' + (date.getMonth() + 1)).slice(-2) + "/" + date.getDate();
                    $('#status').hide().text("Retrieved").fadeIn();
                    $('#date').hide().text(dateString).fadeIn();
                    $('#buttons').fadeOut();
                    $('#comments').prop("disabled", true);
                    $('.hideinputs').children('td').find('input').fadeOut(function () {
                        let replacement = $('<span class=' + this.className + '>' + this.value + '</span>')
                        $(this).replaceWith(replacement).fadeIn();
                    })
                }
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    })

    $(".actualNumber").on("change", function () {
        console.log($(this).parent().prev().text());
        if (+$(this).val() > +$(this).parent().prev().text()) {
            $(this).val($(this).parent().prev().text());
            return;
        }
        let retrievedSum = 0;
        $(this).parents("table").find(".actualNumber").each(function () {
            retrievedSum += +$(this).val();
        })
        $(this).parents("table").find(".retrieved").text(retrievedSum);
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