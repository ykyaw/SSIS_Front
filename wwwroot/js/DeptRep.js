$(document).ready(function () {
    $("#assign").on("click", function () {
        let Id = +$('input[type=radio]:checked', '#employee').val();
        let name = $('input[type=radio]:checked', '#employee').next().text();
        Put(`/Department/AssignDeptRep`, Id)
            .then(function (response) {
                console.log(response);
                //alert("success: " + response);
                $('#rep').hide().text(name).fadeIn();
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    })
})