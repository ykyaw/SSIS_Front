$(document).ready(function () {
    fromdate.min = new Date().toISOString().split("T")[0]
    todate.min = new Date($("#fromdate").val())

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
        if (isNaN(employee.Id)) {
            alert("Please select an employee");
        } else if (isNaN(employee.DelegateFromDate) || isNaN(employee.DelegateToDate)) {
            alert("Pleaes input both dates")
        } else {
            Put(`/Department/AssignDelegate`, employee)
                .then(function (response) {
                    console.log(response);
                    //alert("success: " + response);
                    $('.hide').fadeOut();
                    $('<tr><td>' + name +
                        '</td><td>' + fromdatestring +
                        '</td><td>' + todatestring +
                        '</td><td>' + '<button class="btn btn-primary">Edit</button><button class="btn btn-danger">Delete</button>' +
                        '</td></tr>').hide().appendTo('#delegates').fadeIn();
                })
                .catch(function (err) {
                    //alert("error: " + JSON.parse(err));
                })
        }

    })

    $('#productlist').on('click', 'input[type="button"]', function () {
        $(this).closest('tr').fadeOut(200, function () {
            $(this).remove();
        });
    });

    $('#productlist').on('click', 'input[type="button"]', function () {
        $(this).closest('tr').fadeOut(200, function () {
            $(this).remove();
        });
    });
})