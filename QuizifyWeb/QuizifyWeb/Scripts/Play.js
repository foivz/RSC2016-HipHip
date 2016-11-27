function refreshUserState(user) {
    $.get("/api/Quizzes/" + user)
        .done(function (e) {
        $(".tr-user")
            .where(function (x) { return x.attr("data-user-id") === user; })
            .children(".td-user-present")
            .html(function () { return e.isPresent ? "Yes" : "No"; });
    });
}
function play() {
    $(".questions").slideToggle();
}
function checkIsStarted(quizId) {
    $.get("/api/QuizzesStat/" + quizId)
        .done(function (e) {
        if (e.DateTime !== "1" && $("#start-time").html() !== e.DateTime) {
            $("#start-time").html(function () { return e.DateTime; });
            play();
        }
    });
}
function moveNextQuestion(qId) {
    $(".quest").hide();
    $(".quest").where(function (x) { return x.attr("data-question-id") === qId; }).show();
    $.get("/api/Roller/" + qId);
}
window.onload = function () {
    var users = [];
    var questions = [];
    $(".tr-user")
        .each(function (k, v) {
        var id = $(v).attr("data-user-id");
        users.push(id);
    });
    $(".quest")
        .each(function (k, v) {
        var id = $(v).attr("data-question-id");
        questions.push(id);
    });
    setInterval(function () {
        users.forEach(function (u) { return refreshUserState(u); });
        checkIsStarted(Number($("#quiz-id").html()));
    }, 1000);
    $(".next")
        .click(function (e) {
        //moveNextQuestion(questions.shift());
        if (questions.length === 0) {
            alert("All questions have been passed!");
            $(".next").hide();
            $(".quest").hide();
        }
        else {
            moveNextQuestion(questions.shift());
        }
    });
};
//# sourceMappingURL=Play.js.map