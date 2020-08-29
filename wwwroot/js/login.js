
$(document).ready(function () {
});

function validate() {
    $("#errmsg").html("");
    let email = $("input[name='email']").val();
    let password = $("input[name='plainPassword']").val();
    $("#password").val(CryptoJS.SHA256(password).toString())


    if (email.length == 0 || password.length == 0) {
        $("#errmsg").html("Both email and Password are required.");
        console.log("Both email and Password are required.");
        return false;
    }
    return true;
};