$(document).ready(function () {
    // all JQuery goes inside this part

    //Adding DataTable functions to the table
    // providing a configuration object
    var table = $("#movies").DataTable({
        ajax: {
            url: "/api/movies",
            method: "GET",
            dataSrc: ""
        },
        columns: [
            {
                data: "name"              
            },
            {
                data: "movieGenre.name"

            }
           
        ]
    });

   
});