$.put = function (url,data, success, error) {
    $.ajax({
        url: url,
        type: "PUT",
        data: data,
        success(result) {
            success(result);
        },
        error(message) {
            error(message);
        }
    });
}