$(document).ready(function () {

    $("#submit").on("click", function () {
        let itemCode = $("#itemCode").val();
        let year = $("#year").val();
        let yearregx = /\d{4}/;
        if (year == "" || !yearregx.test(year)) {
            alert("please input a year in a yyyy format");
            return;
        }
        Get(`http://localhost:5000/${itemCode}/${year}`)
            .then(function (response) {
                console.log(response);
            })
            .catch(function (err) {
                console.error(err);
            })
    })
})