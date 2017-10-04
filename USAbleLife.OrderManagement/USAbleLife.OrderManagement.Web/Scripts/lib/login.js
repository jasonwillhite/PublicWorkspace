function login() {
    var username = $('#usernameTextBox').val();
    var password = $('#passwordTextBox').val();

    $.ajax({
        type: "POST",
        url: "/Login/UserLogin",
        data: JSON.stringify({ "username": username, "password": password, "rememberMe": "true" }),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            if (result.Success === true) {
                window.location.href = "/Home/Index";
            }
            else {
                $("#invalidLogin").removeClass("content-none");
            }
        },
        error: function (err) {
            $("#invalidLogin").removeClass("content-none");
        }
    });
}


$(document).ready(function () {
    $("#passwordTextBox").keypress(function (event) {
        if (event.which === 13) {
            $('#LoginButton').click();
        }
    });
});

