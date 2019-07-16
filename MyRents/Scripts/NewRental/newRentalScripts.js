$(document).ready(function () {

    // Tutorial
    // https://digitalfortress.tech/tutorial/smart-search-using-twitter-typeahead-bloodhound/

    // viewModel
    var vm = {
        movieIds: []
    }; 

    var customers = new Bloodhound({
        // datumTokenizer: Function, Required. 
        // DatumTokenizer is a function which transforms the data(string/ array / object) being searched through into an array of strings.
        datumTokenizer: Bloodhound.tokenizers.whitespace('name'),
        // queryTokenizer: Function, Required.
        // queryTokenizer is a function which converts the query string into an array of strings.
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/customers?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#customer').typeahead({
        minLength: 1,
        highlight: true,
        hint: false,
        autoselect: true
    }, {
            name: 'customers',
            display: 'name',
            source: customers
        }).on("typeahead:select", function (e, customer) {
            // Storing the customer Id in the ViewModel
            vm.customerId = customer.id;
        });

    var movies = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/movies?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#movie').typeahead({
        minLength: 1,
        highlight: true,
        hint: false,
        autoselect: true
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
        }, "Please, select a valid customer");

    $.validator.addMethod("validMovieList",
        function() {
            // make sure that vm.movieIds has at least one Id
            return vm.movieIds.length > 0;
        }, "Please, select at least one movie");

    // Adding client-side validation with JQUery
    var validator = $("#newRental").validate({
        submitHandler: function () {
            // using AJAX
            $.ajax({
                url: "/api/newRentals",
                method: "post",
                data: vm
            })
                .done(function () {
                    toastr.success("Rentals successfully recorded.");

                    // clear the input field
                    $('#customer').typeahead("val", "");
                    $('#movie').typeahead("val", "");
                    $('#movies').empty();

                    // clearing the viewModel
                    vm = { movieIds: [] };

                    // reset the validator
                    validator.resetForm();
                })
                .fail(function () {
                    toastr.fail("Something unexpected happened!");
                });

            // To prevent the form from being submitted  normally by the HTML
            return false;
        }
    });

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

