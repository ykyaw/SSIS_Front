﻿$(document).ready(function () {

    $("#forecast").on("click", function () {
        let itemCode = $("select").val();
        Get(`http://localhost:5000/ProductId/${itemCode}`)
            .then(function (response) {
                console.log(response);
                let { forecast, img } = response;
                $("#forecastimg").attr("src", `data:image/png;base64,${img}`)
                forecast = JSON.parse(forecast);
                console.log(forecast);
                let $forecast = "";
                let sum = 0;
                let months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];
                forecast.forEach(function (item, index) {
                    $forecast += `<tr><td>${months[index]}</td><td>${forecast[index]}</td></tr>`;

                });
                $("tbody").html("");
                $("tbody").append($forecast);

            })
            .catch(function (err) {
                console.log("err:", err);
            })
    })
})
