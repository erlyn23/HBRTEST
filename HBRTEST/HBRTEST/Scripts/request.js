var request = {
    post: function (url, params, successMethod, errorMethod)
    {
        $.ajax({
            url: url,
            type: 'post',
            body: JSON.stringify(params),
            contentType: "application/json",
            success: function (data) {
                if (successMethod != null) {
                    successMethod(data);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorMessage) {
                if (errorMethod != null) {
                    errorMethod(XMLHttpRequest, textStatus, errorMessage);
                }
            }
        });
    }
}