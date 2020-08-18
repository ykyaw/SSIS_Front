$(document).ready(function () {
    $('#acknowledge').click(function () {
        let RequisitionId = $('#requisitionid').val();
        let details = []
        $(".detailid").each(function () {
            let detail = {
                Id: +$(this).val(),
                QtyReceived: +$(this).val(),
                RequisitionId,
                RepRemark: $(this).nextAll().eq(6).find('input').val()
            }
            details.push(detail);
        })
        Put(`/Department/AckDisbursement`, details)
            .then(function (response) {
                console.log(response);
                //alert("success: " + response);
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    });
});