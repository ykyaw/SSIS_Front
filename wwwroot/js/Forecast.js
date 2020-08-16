$(document).ready(function () {

    $("#forecast").on("click", function () {
        let itemCode = $("select").val();
        Get(`http://192.168.1.228:5000/itemcode/${itemCode}`)
            .then(function (response) {
                console.log(response);
            })
            .catch(function (err) {
                console.log("err:", err);
            })
    })
})