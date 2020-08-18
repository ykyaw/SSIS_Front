$(document).ready(function () {
   
    $("#assign").on("click", function () {

        let id = $("input[type='radio']:checked").val();
        
        let name = $("input[type='radio']:checked").next().text();
        let fromdate = $("#fromdate").val();
        let date1 = new Date(fromdate).getTime();
        let todate = $("#todate").val();
        let date2 = new Date(todate).getTime();
        let employee = { Id: +id, DelegateFromDate: date1, DelegateToDate: date2 }
        Put(`/Department/assignDelegate`,employee)
            .then(function (response) {
                //let result = JSON.parse(response);
                console.log(response);
                if (response) {
                    $("#empName").text(name);
                    $("#startDate").text(date1);
                    $("#endDate").text(date2);
                   
                    //alert("Successfully Change.")
                }
                else {
                    //alert("Something wrong.")
                }
                //alert("success: " + response);
            })
            .catch(function (err) {
                console.log("error: " + err);
                //alert("Error.")
            })
    })


})