$(document).ready(function () {
    $("#assign").on("click", function () {

        let e = $('input[name=cp]:checked', '#select').val();
        let id = e;
        console.log(e);
        console.log(id);
        let empId = { Id: +id }
        Put(`/Department/assignDeptRep`, empId)
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