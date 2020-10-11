var messageManager = {
    setErrorMessage: function (element, message, deleteId) {
        $(element).removeClass("d-none").append(message);
        setTimeout(function () {
            $(deleteId).remove();
            $(element).addClass("d-none");
        }, 3000);
    }
}