$(document).ready(function () {
    $("#change").on("click", function () {
        let Id = +$('input[type=radio]:checked', '#collectionpoint').val();
        let location = $('input[type=radio]:checked', '#collectionpoint').data('loc');
        let time = $('input[type=radio]:checked', '#collectionpoint').data('time');
        $("#location").hide().text(location).fadeIn();
        $("#time").hide().text(time).fadeIn();
        let collectionpoint = {
            Id
        }
        Put(`/Department/SetCollectionPoint`, collectionpoint)
            .then(function (response) {
                console.log(response);
                //alert("success: " + response);
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    })
})