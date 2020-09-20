$(document).ready(function () {
    AddEventClickSignIn();
});

AddEventClickSignIn = function () {
    $("#SignIn").on("click", function () {
        var json = {
            UserName: $("#UserName").val(),
            Password: $("#Password").val()
        }

        if (ValidateEmptyOrNullFields(json.UserName, json.Password)) {
            $("#errorMessage").removeClass("d-none").text("Debes ingresar un usuario y una contraseña");

            setTimeout(function () {
                $("#errorMessage").addClass("d-none");
            }, 3000);
        } else {
            request.post("/Users/Index", json, OnSuccessSignIn, OnErrorSignIn);
        }
    });
}

ValidateEmptyOrNullFields = function (UserName, Password) {
    if (UserName.length <= 0 || Password.length <= 0) {
        return true;
    }
    return false;
}

OnSuccessSignIn = function (data) {
    if (data[0] === "/")
    {
        window.location = data;
    }
    else
    {
        $("#errorMessage").removeClass("d-none").text(data);

        setTimeout(function () {
            $("#errorMessage").addClass("d-none");
        }, 3000);
    }
}

OnErrorSignIn = function (XMLHttpRequest, textStatus, errorMessage) {
    alert("Error: " + textStatus + ". " + errorMessage);
}