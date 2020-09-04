
function CustomerModel() {

    // Make the self as 'this' reference
    // Declare observable which will be bind with UI

    var self = this;
    self.BASE_URL = ko.observable(""); // if website is a subdomain
    self.ErrorMessage = ko.observable(null);
    self.CustomerList = ko.observableArray([]);
    self.SearchCustomers = ko.observable();
    self.EditCustomer = ko.observable();
    self.ShowAddForm = ko.observable(false);

    self.Name = ko.observable();
    self.Surname = ko.observable();
    self.Cellphone = ko.observable();

    self.LoadCustomers = function () {

        url = self.BASE_URL() + 'Customer/GetCustomers';
        var data = {};
        $.ajax({
            url: url,
            cache: false,
            data: data,
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                console.log($.parseJSON(data));
                self.CustomerList($.parseJSON(data));
            },
            error: function (request, status, error) {
                toastr.error(request.responseText);
            },

            beforeSend: function () {
                $('#123').hide();

            },
            complete: function () {
                $('#123').show();

            },
        });

    }


    self.ToggleAddFormVisibility = function () {
        self.Cancel_Edit_Customer();
        self.ShowAddForm(!self.ShowAddForm());

    };


    self.Edit_Customer = function (data) {
        self.EditCustomer(data);
    };

    self.Cancel_Edit_Customer = function () {
        self.EditCustomer(null);
    };






    self.Save_Customer = function () {
        var data = { Name: self.Name(), Surname: self.Surname(), Cellphone: self.Cellphone() };

        $.ajax({
            url: self.BASE_URL() + 'Customer/AddCustomer',
            cache: false,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(data),
            success: function (data) {
                toastr.success("Saved successfully");
                self.CustomerList.push($.parseJSON(data));
                self.ToggleAddFormVisibility();
            },
            error: function (request, status, error) {
                toastr.error(self.ExceptionClean(request.responseText));
            }
        });

    };


    self.Update_Customer = function () {


        $.ajax({
            url: self.BASE_URL() + 'Customer/UpdateCustomer',
            cache: false,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(self.EditCustomer()),
            success: function (data) {
                toastr.success("Saved successfully");
                self.CustomerList.remove(self.EditCustomer());
                self.CustomerList.push($.parseJSON(data));
                self.Cancel_Edit_Customer();
            },
            error: function (request, status, error) {
                console.log(request.responseText);
                toastr.error(self.ExceptionClean(request.responseText));

            }
        });

    };


    self.Delete_Customer = function (data) {

        var confirma = confirm("You are about do delete this client");

        if (confirma) {
            $.ajax({
                url: self.BASE_URL() + 'Customer/DeleteCustomer',
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(data),
                success: function (result) {
                    toastr.success("Saved successfully");
                    self.CustomerList.remove(data);

                },
                error: function (request, status, error) {


                    toastr.error(self.ExceptionClean(request.responseText));
                }
            });
        }

    };


    self.ExceptionClean = function (string) {

        return string.replace(/\\/g, "");

    }


}
