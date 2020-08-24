$(document).ready(function () {
    fromdate.min = new Date().toISOString().split("T")[0];
    todate.min = $("#fromdate").val();

    $("#assign").on("click", function () {
        let Id = +$('input[type=radio]:checked', '#employee').val();
        let name = $('input[type=radio]:checked', '#employee').next().text();
        let fromdate = $("#fromdate").val();
        let delegateFromDate = new Date(fromdate);
        let fromdatestring = delegateFromDate.getFullYear() + "/" + ('0' + (delegateFromDate.getMonth() + 1)).slice(-2) + "/" + ('0' + delegateFromDate.getDate()).slice(-2);
        let todate = $("#todate").val();
        let delegateToDate = new Date(todate);
        let todatestring = delegateToDate.getFullYear() + "/" + ('0' + (delegateToDate.getMonth() + 1)).slice(-2) + "/" + ('0' + delegateToDate.getDate()).slice(-2);
        let employee = {
            Id,
            DelegateFromDate: delegateFromDate.getTime(),
            DelegateToDate: delegateToDate.getTime() + (1000 * 24 * 60 * 60) - 1000
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
                    $('.hide').hide();
                    $('<tr>' + '<input class="empid" type="hidden" value="' + Id + '" />' +
                        '<td>' + name +
                        '</td><td>' + fromdatestring +
                        '</td><td>' + todatestring +
                        '</td><td>' + '<button class="btn btn-warning" style="margin-right: 10px;">Edit</button><button class="btn btn-danger">Delete</button>' +
                        '</td></tr>').hide().appendTo('#delegates').fadeIn();
                })
                .catch(function (err) {
                    //alert("error: " + JSON.parse(err));
                })
        }

    })

    $('#delegates').on('click', 'button.delete', function () {
        if (confirm("Are you sure you want to delete this delegate assignment?")) {
            let btn = $(this);
            let Id = +$(this).parent().siblings('.empid').val();
            let employee = {
                Id
            }
            console.log(employee);
            Put(`/Department/AssignDelegate`, employee)
                .then(function (response) {
                    console.log(response);
                    btn.closest('tr').fadeOut(200, function () {
                        $(this).remove();
                    });
                    if ($('.delete').length == 1) {
                        $('<tr>' +
                            '<td colspan="4">There is no assigned delegate for this department.' +
                            '</td></tr>').hide().appendTo('#delegates').fadeIn();
                    }
                })
                .catch(function (err) {
                    //alert("error: " + JSON.parse(err));
                })
        }
        return false;
    })

    //$('#delegates').on('click', 'button.edit', function () {
    //    let employee = {
    //        Id: +$(this).parent().siblings('.empid').val()
    //    }
    //    Put(`/Department/AssignDelegate`, employee)
    //        .then(function (response) {
    //            console.log(response);
    //            $(this).closest('tr').fadeOut(200, function () {
    //                $(this).remove();
    //            });
    //        })
    //        .catch(function (err) {
    //            //alert("error: " + JSON.parse(err));
    //        })
    //})
})