$(document).ready(function () {


    var vm = {
        movieIds: []
    }; // viewModel

    var customers = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/customers?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#customer').typeahead({
        minLength: 1,
        highlight: true,
    }, {
            name: 'customers',
            display: 'name',
            source: customers
        }).on("typeahead:select", function (e, customer) {
            // Storing the customer Id in the ViewModel
            vm.customerId = customer.id;
        });

    var movies = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/movies?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#movie').typeahead({
        minLength: 1,
        highlight: true
    },
        {
            name: 'movies',
            display: 'name',
            source: movies
        }).on("typeahead:select",
            function (e, movie) {
                // storing the movieId in the viewModel
                vm.movieId = movie.id;

                // modifying the DOM directly
                $('#movies').append("<li class='list-group-item'>" + movie.name + "</li>");

                // clear the input field
                $('#movie').typeahead("val", "");

                // store the value inside the movieIds array
                vm.movieIds.push(movie.id);

            });

    // Handling form submit

    // Enforcing that a valid customer will be selected
    $.validator.addMethod("validCustomer",
        function () {
            // make sure that the viewModel (vm) has a property called customerId and it's not zero
            return vm.customerId && vm.customerId !== 0;
        }, "Please, select a valid customer from the list");

    // Adding client-side validation with JQUery
    $("#newRental").validate({
        submitHandler: function () {
            // If this function is valid, it will call the submit method for the form
            e.preventDefault();

            // using AJAX
            $.ajax({
                url: "/api/newRentals",
                method: "post",
                data: vm
            })
                .done(function () {
                    toastr.success("Rentals successfully recorded.");
                })
                .fail(function () {
                    toastr.fail("Something unexpected happened!");
                });
        }
    });

    $("#newRental").validate();
    // Submit method move inside the validation method
    //$('#newRental').submit(function(e) {
    //    // Preventing to have a traditional HTML form
    //    e.preventDefault();

    //    console.log(vm);

    //    // using AJAX
    //    $.ajax({
    //            url: "/api/newRentals",
    //            method: "post",
    //            data: vm
    //        })
    //        .done(function () {
    //            toastr.success("Rentals successfully recorded.");
    //        })
    //        .fail(function () {
    //            toastr.fail("Something unexpected happened!");
    //        });
    //});
});

