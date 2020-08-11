
function dateTransform() {
    let dateString = $("input[type='date']").val();
    let date = new Date(dateString);
    $("#date").val(date.getTime());
    return true;
}