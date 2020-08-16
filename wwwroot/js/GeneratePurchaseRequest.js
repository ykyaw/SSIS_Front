$(document).ready(function () {
    $('#add').click(function () {
        let row = $('#template').clone();
        $('#productlist').append(row);
        row.removeAttr('id').fadeIn();
    });

    $('#productlist').on('click', 'input[type="button"]', function () {
        $(this).closest('tr').fadeOut(200, function () {
            $(this).remove();
        });
    });

    $('#generate').click(function () {
        let prlist = [];
        $('.product').each(function () {
            let newItem = $(this).val();
            let isExist = jQuery.inArray(newItem, prlist);
            if (isExist >= 0) {
                alert("Can't select the same product more than once.");
                return;
            } else if (newItem != "0") {
                prlist.push(newItem);
            };
        });
        console.log(prlist);
        Post(`/StoreClerk/CreatePurchaseRequest`, prlist)
            .then(function (response) {
                console.log(response);
                //alert("success: " + response);
                window.location.href = "/StoreClerk/PurchaseRequest/" + response[0].purchaseRequestId;
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    });
});