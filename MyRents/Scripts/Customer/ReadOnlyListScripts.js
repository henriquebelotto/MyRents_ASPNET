$(document).ready(function () {
    // all JQuery goes inside this part

    //Adding DataTable functions to the table
    // providing a configuration object
    var table = $("#customers").DataTable({
        ajax: {
            url: "/api/customers",
            method: "GET",
            dataSrc: ""
        },
        columns: [
            {
                data: "name"  
            },
            {
                data: "membershipType.name"

            }
        ]
    });

   
});