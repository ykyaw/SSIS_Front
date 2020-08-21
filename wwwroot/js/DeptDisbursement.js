﻿$(document).ready(function () {
    $('#acknowledge').click(function () {
        let RequisitionId = +$('#requisitionid').val();
        let empname = $('#empname').val();
        let date = new Date();
        let dateString = date.getFullYear() + "/" + ('0' + (date.getMonth() + 1)).slice(-2) + "/" + date.getDate();
        let details = []
        $(".detailid").each(function () {
            let detail = {
                Id: +$(this).val(),
                RequisitionId,
                QtyReceived: +$(this).nextAll().eq(4).find('input').val(),
                RepRemark: $(this).nextAll().eq(5).find('input').val()
            }
            details.push(detail);
        })
        Put(`/Department/AckDisbursement`, details)
            .then(function (response) {
                console.log(response);
                //alert("success: " + response);
                $('.replace').fadeOut(function () {
                    let replacement = $('<span class=' + this.className + '>' + this.value + '</span>')
                    $(this).replaceWith(replacement).fadeIn();
                });
                $('#acknowledge').fadeOut();
                $('#receivedby').hide().text(empname).fadeIn();
                $('#receiveddate').hide().text(dateString).fadeIn();
            })
            .catch(function (err) {
                //alert("error: " + JSON.parse(err));
            })
    });
});