var request = {
    post: function (url, params, successMethod, errorMethod){
        $.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify(params),
            contentType: "application/json; charset=UTF-8",
            dataType: "json",
            success: function (data){
                if (successMethod != null)
                {
                    successMethod(data);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorMessage){
                if (errorMethod != null)
                {
                    errorMethod(XMLHttpRequest, textStatus, errorMessage);
                }
            }
        });
    }
}