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
                data: "name",
                render: function (data, type, customer) {
                    return "<a href='/customer/edit/'" + customer.id + ">" + customer.name + "</a>";
                }
            },
            {
                data: "membershipType.name"

            },
            {
                data: "id",
                render: function (data) {
                    return "<button type='button' class='btn btn-link js-delete' data-customer-id='" + data + "'" +
                        "data-toggle='tooltip' data-placement='right' title='Using JQuery & AJAX'> Delete </button>"
                }

            }

        ]
    });

    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

    // Select #customers table and filter elements with the class js-delete
    // This way, it's more efficient because there will be only one event handler in the memory, which gets the #customers id
    $("#customers").on("click",
        ".js-delete",
        function () {

            //getting a reference for the button to be used when it's clicked
            var button = $(this);

            // get the reference to the button to be used by the modal
            $("#deleteConfirm").data('ref', button);

            // call the modal (display the dialog box on the screen)
            $("#deleteModal").modal();

        });

    // This way, it would work if I kept one click function inside the other
    //$("#deleteConfirm").off().bind("click", function () { }

    $("#deleteConfirm").on("click",
        function () {

            // getting the reference to the delete button
            var button = $(this).data('ref');

            $.ajax({
                // Using AJAX to send a request to the server using the api
                // the customerId is obtained through the data-attribute in the delete button
                url: "/api/customers/" + button.attr("data-customer-id"),
                method: "DELETE",
                success: function () {
                    // case it's success, remove the row from the table
                    table.row(button.parents("tr")).remove().draw();
                    button.parents("tr").remove();
                }
            });

            // hide de modal (dialog box)
            $("#deleteModal").modal("hide");
        });

});