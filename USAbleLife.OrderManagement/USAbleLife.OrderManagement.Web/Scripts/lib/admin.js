// The popup dialog used within the Admin partial views
var dialog;

// When the admin page loads, immediately select the Discounts page
$(function () {
    loadView("Discounts", initDiscountsView);
    $('#HomeLink').removeClass('active');
    $('#OrderLink').removeClass('active');
    $('#AdminLink').addClass('active');

});

// Loads the appropriate admin view
function loadView(action, init) {
    $.ajax({
        type: "POST",
        url: "/Admin/" + action,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $(".ui-dialog").remove();
            $('#adminDiv').html(result);
            init();
        }
    });
}

// Launch dialog for adding new item (e.g. Discount, Menu Item, Tax etc)
function showNewDialog() {
    dialog.dialog("open");
}

// Initialize the dialog, and define the 'close' method
function initDialog(closeFunction) {
    dialog = $("#dialog-form").dialog({
        autoOpen: false,
        height: 300,
        width: 450,
        modal: true,
        buttons: {},
        close: closeFunction
    });
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

/* Discounts */
function initDiscountsView() {
    initDialog(function () {
        $('#discount-name')[0].value = '';
        $('#discount-amount')[0].value = '';
        $('#discount-type').val(0);
        $('#discount-id')[0].value = '-1';
    });

    $('#AdminMenuItemsLink').removeClass('selected');
    $('#AdminEmployeesLink').removeClass('selected');
    $('#AdminTaxesLink').removeClass('selected');
    $('#AdminDiscountsLink').addClass('selected');
}

function editDiscount(amount, name, type, id) {
    $('#discount-name')[0].value = name;
    $('#discount-amount')[0].value = amount;
    $('#discount-type').val(type);
    $('#discount-id')[0].value = id;
    dialog.dialog("open");
}

function saveDiscount() {
    var name = $('#discount-name')[0].value;
    var amount = $('#discount-amount')[0].value;
    var type = $('#discount-type').val();
    var id = $('#discount-id')[0].value;

    $('#errorLabel').text("");

    if (name.trim() === "") {
        $('#errorLabel').text('Name must be filled out');
        return;
    }

    if (amount === "") {
        $('#errorLabel').text('Amount must be filled out');
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Admin/SaveDiscount",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "name": name, "amount": amount, "type": type, "id": id }),
        success: function (result) {
            if (result.Success === true) {
                $('#errorLabel').text("");
                dialog.dialog("close");
                loadView('Discounts', initDiscountsView);
                return false;
            }
            else {
                $('#errorLabel').text(result.Error);
                return false;
            }
        }
    });
}

function deleteDiscount(id) {
    if (confirm('Are you sure you want to delete this discount?') === true) {
        $.ajax({
            type: "POST",
            url: "/Admin/DeleteDiscount",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "id": id }),
            success: function (result) {
                dialog.dialog("close");
                loadView('Discounts', initDiscountsView);
            }
        });
    }
}



/* Taxes */
function initTaxesView() {
    initDialog(function () {
        $('#name')[0].value = "";
        $('#amount')[0].value = "";
        $('#id')[0].value = "-1";
    });

    $('#AdminMenuItemsLink').removeClass('selected');
    $('#AdminEmployeesLink').removeClass('selected');
    $('#AdminTaxesLink').addClass('selected');
    $('#AdminDiscountsLink').removeClass('selected');
}

function editTax(amount, name, id) {
    $('#name')[0].value = name;
    $('#amount')[0].value = amount;
    $('#id')[0].value = id;
    dialog.dialog("open");
}

function saveTax() {
    $('#errorLabel').text('');

    var name = $('#name')[0].value;
    if (name.trim() === '') {
        $('#errorLabel').text('Name must be filled out');
        return;
    }

    var amount = $('#amount')[0].value;
    if (amount === '') {
        $('#errorLabel').text('Amount must be a valid decimal');
        return;
    }

    var id = $('#id')[0].value;
    $.ajax({
        type: "POST",
        url: "/Admin/SaveTax",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "name": name, "amount": amount, "id": id }),
        success: function (result) {
            if (result.Success === true) {
                $('#errorLabel').text("");
                dialog.dialog("close");
                loadView('Taxes', initTaxesView);
                return false;
            }
            else {
                $('#errorLabel').text(result.Error);
                return false;
            }
        }
    });
}

function deleteTax(id) {
    if (confirm('Are you sure you want to delete this tax?') === true) {
        $.ajax({
            type: "POST",
            url: "/Admin/DeleteTax",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "id": id }),
            success: function (result) {
                dialog.dialog("close");
                $('#adminDiv').html(result);
                initTaxesView();
            }
        });
    }
}





/* Employees */
function changePasswordCheckChanged(checkbox) {
    // User wishes to change password
    if (checkbox.checked === true) {
        $('#password').removeAttr('disabled');
    }
        // User does not want to change password
    else {
        $('#password').attr('disabled', '');
    }
}

function initEmployeesView() {
    initDialog(function () {
            $('#username')[0].value = '';
            $('#firstname')[0].value = '';
            $('#lastname').value = '';
            $('#password').value = '';
            $('#id')[0].value = '-1';
        });

    $('#AdminMenuItemsLink').removeClass('selected');
    $('#AdminEmployeesLink').addClass('selected');
    $('#AdminTaxesLink').removeClass('selected');
    $('#AdminDiscountsLink').removeClass('selected');
}

function editEmployee(username, firstname, lastname, id) {
    $('#username')[0].value = username;
    $('#firstname')[0].value = firstname;
    $('#lastname')[0].value = lastname;
    $('#id')[0].value = id;
    dialog.dialog("open");
}

function saveEmployee() {
    $('#errorLabel').text('');
    var username = $('#username')[0].value;
    var firstname = $('#firstname')[0].value;
    var lastname = $('#lastname')[0].value;
    var password = $('#password')[0].value;
    var changePassword = $('#changePassword')[0].checked;
    var id = $('#id')[0].value;

    if (username.trim() === '') {
        $('#errorLabel').text('Username must be filled out');
        return;
    }

    if (firstname.trim() === '') {
        $('#errorLabel').text('First name must be filled out');
        return;
    }

    if (lastname.trim() === '') {
        $('#errorLabel').text('Last name must be filled out');
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Admin/SaveEmployee",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "username": username, "firstname": firstname, "lastname": lastname, "password": password, "changePassword": changePassword, "id": id }),
        success: function (result) {
            if (result.Success === true) {
                $('#errorLabel').text("");
                dialog.dialog("close");
                loadView('Employees', initEmployeesView);
                return false;
            }
            else {
                $('#errorLabel').text(result.Error);
                return false;
            }
        }
    });
}

function deleteEmployee(id) {
    if (confirm('Are you sure you want to delete this employee?') === true) {
        $.ajax({
            type: "POST",
            url: "/Admin/DeleteEmployee",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "id": id }),
            success: function (result) {
                dialog.dialog("close");
                $('#adminDiv').html(result);
                initEmployeesView();
            }
        });
    }
}



/* Menu Items */
function initMenuItemsView() {
   initDialog(function () {
            $('#name')[0].value = '';
            $('#amount')[0].value = '';
            $('#id')[0].value = '-1';
        }   );

    $('#AdminMenuItemsLink').addClass('selected');
    $('#AdminEmployeesLink').removeClass('selected');
    $('#AdminTaxesLink').removeClass('selected');
    $('#AdminDiscountsLink').removeClass('selected');
}

function editMenuItem(name, amount, id) {
    $('#name')[0].value = name;
    $('#amount')[0].value = amount;
    $('#id')[0].value = id;
    dialog.dialog("open");
}

function saveMenuItem() {
    var name = $('#name')[0].value;
    if (name.trim() === '') {
        $('#errorLabel').text('Name must be filled out');
        return;
    }

    var amount = $('#amount')[0].value;
    if (amount === '') {
        $('#errorLabel').text('Amount must be a valid decimal');
        return;
    }

    var id = $('#id')[0].value;

    $.ajax({
        type: "POST",
        url: "/Admin/SaveMenuItem",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "name": name, "amount": amount, "id": id }),
        success: function (result) {
            if (result.Success === true) {
                $('#errorLabel').text("");
                dialog.dialog("close");
                loadView('MenuItems', initMenuItemsView);
                return false;
            }
            else {
                $('#errorLabel').text(result.Error);
                return false;
            }
        }});
}

function deleteMenuItem(id) {
    if (confirm('Are you sure you want to delete this menu item?') === true) {
        $.ajax({
            type: "POST",
            url: "/Admin/DeleteMenuItem",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "id": id }),
            success: function (result) {
                dialog.dialog("close");
                $('#adminDiv').html(result);
                initMenuItemsView();
            }
        });
    }
}