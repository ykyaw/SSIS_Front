$(document).ready(function () {
    $("#assign").on("click", function () {
        let Id = +$('input[type=radio]:checked', '#employee').val();
        let name = $('input[type=radio]:checked', '#employee').next().text();
        let fromdate = $("#fromdate").val();
        let delegateFromDate = new Date(fromdate);
        let fromdatestring = delegateFromDate.getFullYear() + "/" + ('0' + (delegateFromDate.getMonth() + 1)).slice(-2) + "/" + delegateFromDate.getDate();
        let todate = $("#todate").val();
        let delegateToDate = new Date(todate);
        let todatestring = delegateToDate.getFullYear() + "/" + ('0' + (delegateToDate.getMonth() + 1)).slice(-2) + "/" + delegateToDate.getDate();
        let employee = {
            Id,
            DelegateFromDate: delegateFromDate.getTime(),
            DelegateToDate: delegateToDate.getTime()
        }
        Put(`/Department/assignDelegate`, employee)
            .then(function (response) {
                console.log(response);
                //alert("success: " + response);
                $('<tr><td>' + name +
                    '</td><td>' + fromdatestring +
                    '</td><td>' + todatestring +
                    '</td><td>' + '<button class="btn btn-primary">Edit</button><button class="btn btn-danger">Delete</button>' +
                    '</td></tr>').hide().appendTo('#delegates').fadeIn();
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    })
})