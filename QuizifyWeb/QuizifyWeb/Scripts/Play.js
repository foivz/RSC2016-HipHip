function refreshUserState(user) {
    $.get("/api/Quizzes/" + user)
        .done(function (e) {
        console.log(e);
        $(".tr-user")
            .where(function (x) { return x.attr("data-user-id") === user; })
            .children(".td-user-present")
            .html(function () { return e.isPresent ? "Yes" : "No"; });
    });
}
function checkIsStarted(quizId) {
    $.get("/api/QuizzesStat/" + quizId)
        .done(function (e) {
        if (e.DateTime !== "1" && $("#start-time").html() !== e.DateTime) {
            $("#start-time").html(function () { return e.DateTime; });
        }
    });
}
window.onload = function () {
    var users = [];
    $(".tr-user")
        .each(function (k, v) {
        var id = $(v).attr("data-user-id");
        users.push(id);
    });
    console.log(users);
    setInterval(function () {
        users.forEach(function (u) { return refreshUserState(u); });
        checkIsStarted(Number($("#quiz-id").html()));
    }, 1000);
};
//# sourceMappingURL=Play.js.map