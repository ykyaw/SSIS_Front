$(document).ready(function () {
    $("#retrieve").on("click", function () {
        $("#th").nextAll().remove();
        console.log($("#select").val())
        let id = $("#select").val();
        Get(`/TenderQuotations/${id}`)
            .then(function (quotations) {
                //let result = JSON.parse(response);
                console.log(quotations);
                let $tr = `<tr id=${quotations[0].productId}><td>${quotations[0].productId}</td><td>${quotations[0].product.description}</td>`;
                $tr += `<td>${quotations.filter(function (item) { return item.rank == 1 })[0].supplier.name}</td>`;
                $tr += `<td>${quotations.filter(function (item) { return item.rank == 2 })[0].supplier.name}</td>`;
                $tr += `<td>${quotations.filter(function (item) { return item.rank == 3 })[0].supplier.name}</td>`;
                $("table").append($tr);
                $("table").fadeIn();


                let $suppliers=quotations.map(function (item) {
                    return `<option value='${item.supplier.id}'>${item.supplier.name}</option>`;
                })
                $(".rankSelect").children().remove();
                $(".rankSelect").each(function () {
                    $(this).append($suppliers);
                })
                $(".rank").fadeIn();
            })
            .catch(function (err) {
                console.log(err);
            })
    })

    $("#save").on("click", function () {
        let first = $("#1st").val();
        let second = $("#2nd").val();
        let third = $("#3rd").val();
        console.log(first, second, third);
        if (first == third || first == second || second == third) {
            alert("suplier can not be same");
            return;
        }
        let quotations = [];
        let ProductId = $("#select").val();
        let rank1 = {
            SupplierId: first,
            ProductId,
            Rank: 1
        };
        let rank2 = {
            SupplierId: second,
            ProductId,
            Rank: 2
        };
        let rank3 = {
            SupplierId: third,
            ProductId,
            Rank: 3
        };
        quotations.push(rank1)
        quotations.push(rank2);
        quotations.push(rank3);
        Put(`/TenderQuotations/${ProductId}`, quotations)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
            })
            .catch(function (err) {
                console.log(err);
            })
    })
})