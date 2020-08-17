$(document).ready(function () {
    $("#savecp").on("click", function () {
        
        let e = $('input[name=cp]:checked', '#select').val();
        let [location, time, id] = e.split("/");
        console.log(e);
        console.log(id);
        $("#location").hide().text(location).fadeIn();
        $("#time").hide().text(time).fadeIn();
        let collectionpoint = { Id: +id }
        Put(`/Department/Savecp`, collectionpoint)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                if (response) {
                    //alert("Successfully Change.")
                }
                else {
                    //alert("Something wrong.")
                }
                //alert("success: " + response);
            })
            .catch(function (err) {
                console.log("error: " + err);
                //alert("Error.")
            })
    })


})