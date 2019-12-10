function login() {
    var username = $('#usernameTextBox').val();
    var password = $('#passwordTextBox').val();

    $.ajax({
        type: "POST",
        url: "/Login/UserLogin",
        data: JSON.stringify({ "username": username, "password": password}),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            console.log(result);
            if (result.Success === true) {
                window.location.href = "/Home/Index";
            }
            else {
                $("#invalidLogin").removeClass("content-none");
                $("#invalidLogin").text(result.Value);
            }
        },
        error: function (err) {
            console.log(err);
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

