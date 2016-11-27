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

$.fn.where = function (expression) {
    var $this = this;
    return $this.filter(function (index) {
        var $obj = $($this[index]);
        return expression($obj);
    });
};