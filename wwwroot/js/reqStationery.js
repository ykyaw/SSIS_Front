﻿
var tdydate = new Date();
var dd = String(tdydate.getDate()).padStart(2, '0');
var mm = String(tdydate.getMonth() + 1).padStart(2, '0');
var yyyy = tdydate.getFullYear();

tdydate = dd + '/' + mm + '/' + yyyy;
document.getElementById('tdydate').innerHTML = tdydate


function myFunction() {
    //Get combobox data 
    var e = document.getElementById("ddlViewBy");
    var strUser = e.options[e.selectedIndex].value;

    //Get qty data
    var e1 = document.getElementById("qty");
    var strUser1 = e1.value;


    //get table body:
    //var tableRef = document.getElementById('myTable').getElementsByTagName('tbody')[0];

    var table = document.getElementById("myTable").getElementsByTagName('tbody')[0];

    var row = table.insertRow(0);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    cell1.innerHTML = strUser;
    cell2.innerHTML = strUser1;

    //javascript add delete button to each added row
    cell3.innerHTML = "<button class='btn btn-danger' onclick = deleterow(this)>Delete</button>";

}
function deleterow(el) {
    $(el).closest('tr').remove();
}

function draft() {
}

//Start Jquery 
$(document).ready(function () {

    $("#savedraft").click(function () {
        let requisitionDetail = getRequisitionDetail(); //create requisitionDetail
        console.log("requisitionDetail", requisitionDetail); //ReqDetail
        requisitionDetail.Status = "Creating"; // Creating
        Put("/Department/RequisitionDetail", requisitionDetail) // /Department/RequisitionDetail
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                alert("success: " + response);
            })
            .catch(function (err) {
                alert("error: " + JSON.parse(err));
            })
    });


});
////////////// JQUERY ENDS //////////////

//Get Table Req Form
function getRequisitionDetail() {
    //let actualNumbers = $(".actualNumber");
    //let requisitionDetails = [];
    //for (let numberInput of actualNumbers) {
    //    let requisition = {
    //        QtyDisbursed: +$(numberInput).val(),
    //        DisburseRemark: $(numberInput).parent().next().children(".remarks").val(),
    //        Id: +$(numberInput).parent().parent().attr("id"),
    //    };
    //    requisitionDetails.push(requisition);
    //}
    //let retrievalId = $("#retrievalId").attr("value");
    //let NeedAdjustment = $("#needVoucher").is(":checked");
    //let Remark = $("#comments").val();
    let getReqForm = {
     //Adding dummy 
        ProductId: 'C001',
        QtyNeeded: '3'

    }
    return getReqForm;
}
/////////////////////////////////////////////