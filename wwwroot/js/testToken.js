
$(document).ready(function () {

    $("#withoutToken").on("click", function () {
        Post("/Home/WithoutToken")
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                alert("success: " + response);
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    })

    $("#withToken").on("click", function () {
        Post("/Home/WithToken")
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