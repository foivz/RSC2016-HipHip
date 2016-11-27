window.onload = function () {
    $(".btn-add-team")
        .click(function (e) {
        var $btn = $(e.target);
        var teamId = $btn.attr("data-team-id");
        var quizId = $btn.attr("data-quiz-id");
        $.post("/Quizzes/AddTeamToQuiz", { teamId: teamId, quizId: quizId })
            .done(function (d) {
            var row = $(".teams").where(function (x) { return x.attr("data-team-id") === teamId; });
            $(".teams").where(function (x) { return x.attr("data-team-id") === teamId; }).remove();
            $(".table-added-teams").append(row).focus();
        })
            .fail(function (e) {
            alert("Failed to add team :( !");
        });
    });
    $(".btn-remove-from-quiz")
        .click(function (e) {
        var $btn = $(e.target);
        var teamId = $btn.attr("data-team-id");
        var quizId = $btn.attr("data-quiz-id");
        $.post("/Quizzes/RemoveFromQuiz", { teamId: teamId, quizId: quizId })
            .done(function (d) {
            var row = $(".quiz-team").where(function (x) { return x.attr("data-team-id") === teamId; });
            $(".quiz-team").where(function (x) { return x.attr("data-team-id") === teamId; }).remove();
            $(".table-all-teams").append(row).focus();
            window.location.reload();
        })
            .fail(function (e) {
            alert("Failed to remove team :( !");
        });
    });
};
//# sourceMappingURL=AddTeam.js.map