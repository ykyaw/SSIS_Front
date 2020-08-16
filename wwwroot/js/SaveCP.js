$(document).ready(function () {



    $("#savecp").on("click", function () {

        var e = document.getElementById("select").value;
        console.log(e);
        let [location, time, Id] = e.split("/");
        console.log("location:", location, "time:", time);

        $("#location").text(location);
        $("#time").text(time);


        let collectionpoint = { Id: +Id }
        Put(`/Department/updateCollectionPoint`, collectionpoint)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                if (response) {
                    alert("Successfully Change.")
                }
                else {
                    alert("Something wrong.")
                }
                alert("success: " + response);
            })
            .catch(function (err) {
                console.log("error: " + err);
                alert("Error.")
            })
    })

    
})