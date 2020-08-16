$(document).ready(function () {
    date.max = new Date().toISOString().split("T")[0];

    $('#submit').click(function () {
        let dateString = $("input[type='date']").val();
        let date = new Date(dateString).getTime();
        let requisition = {
            DepartmentId: $('#departmentid').val(),
            CollectionDate: +date
        }
        Post(`/StoreClerk/DisbursementDetail`, requisition)
            .then(function (response) {
                console.log(response);
                //alert("success: " + response);
                $('#replaceHtml').hide().html(response).fadeIn();
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    });
});