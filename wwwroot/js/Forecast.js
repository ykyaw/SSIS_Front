$(document).ready(function () {

    $("#forecast").on("click", function () {
        let itemCode = $("select").val();
        Get(`http://localhost:5000/ProductId/${itemCode}`)
            .then(function (response) {
                console.log(response);
                let { forecast, img } = response;
                $("img").attr("src", `data:image/png;base64,${img}`)
                forecast = JSON.parse(forecast);
                console.log(forecast);
                let $forecast = "";
                let sum = 0;
                let month = new Date().getMonth() + 1;
                forecast.forEach(function (item, index) {
                    if (index % 3 == 2) {
                        $forecast += `<tr><td>${month + Math.floor(index / 3) + 1}</td><td>${sum / 3}</td></tr>`;
                        sum = 0;
                    }
                });
                $("table").append($forecast);
            })
            .catch(function (err) {
                console.log("err:", err);
            })
    })
})