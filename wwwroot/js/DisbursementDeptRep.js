$(document).ready(function () {
    //date.max = new Date().toISOString().split("T")[0];

    $('#submit').click(function () {
        let dateString = $("input[type='date']").val();
        let date = new Date(dateString).getTime();
        console.log(date);
        //let requisition = {
            
        //    CollectionDate: +date
        //}
        Get(`/Department/viewDisbursementDetailDeptRep`, date)
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