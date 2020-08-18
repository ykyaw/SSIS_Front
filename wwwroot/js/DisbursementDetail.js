$(document).ready(function () {
    $('#acknowledge').click(function () {
        let RequisitionId = $('#requisitionid').val();
        let details = []
        $(".detailid").each(function () {
            let detail = {
                Id: +$(this).val(),
                RequisitionId,
                ClerkRemark: $(this).nextAll().eq(6).find('input').val()
            }
            details.push(detail);
        })
        Put(`/StoreClerk/AckDisbursement`, details)
            .then(function (response) {
                console.log(response);
                alert("success: " + response);
                $('#clerk').hide().text("test").fadeIn();
                $('#date').hide().text()
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    });
});