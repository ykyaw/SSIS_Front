$(document).ready(function () {
    var body = $('#myTable').children('tbody').first();
    body.on('change', '.number', function () {
        var cells = $(this).closest('tr').children('td');
        var number1 = cells.eq(3).find('input').val();
        var number2 = cells.eq(4).find('select').val();
        var total = new Number(number1) * new Number(number2);
        var totalprice = '$' + total.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
        cells.eq(6).text(totalprice);
    });
});