$(document).ready(function () {
    $('#acknowledge').click(function () {
        let RequisitionId = +$('#requisitionid').val();
        let clerkname = $('#clerkname').val();
        let date = new Date();
        let dateString = date.getFullYear() + "/" + ('0' + (date.getMonth() + 1)).slice(-2) + "/" + date.getDate();
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
                $('#clerk').hide().text(clerkname).fadeIn();
                $('#date').hide().text(dateString).fadeIn();
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    });
});