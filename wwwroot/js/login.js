
$(document).ready(function () {

   
});

function validate() {
    $("#errmsg").html("");
    let username = $("input[name='username']").val();
    let password = $("input[name='plainPassword']").val();
    $("#password").val(CryptoJS.SHA256(password).toString())


    if (username.length == 0 || password.length == 0) {
        $("#errmsg").html("Both Username and Password are required.");
        console.log("Both Username and Password are required.");
        return false;
    }
    return true;

    //let $btn = $(this);
    //$btn.attr("class", "btn btn-primary disabled");
    //$btn.text("verifying...");
    //Post("/Login/Verify", { username, password: hashPwd })
    //    .then(function (response) {
    //        let result = JSON.parse(response);
    //        console.log(result);
    //        $btn.attr("class", "btn btn-success");
    //        $btn.text("Login");
    //        if (result.ErrMsg == null) {
    //            window.location.replace(result.Value);
    //        } else {
    //            $("#errmsg").html(result.ErrMsg);
    //        }
    //    })
    //    .catch(function (err) {
    //        console.log("err", err);
    //        $btn.attr("class", "btn btn-success");
    //        $btn.text("Login");
    //    })
};