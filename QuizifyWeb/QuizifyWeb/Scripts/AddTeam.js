window.onload = function () {
    $(".btn-add-team")
        .click(function (e) {
        var $btn = $(e.target);
        var teamId = $btn.attr("data-team-id");
        var quizId = $btn.attr("data-quiz-id");
        $.post("/Quizzes/AddTeamToQuiz", { teamId: teamId, quizId: quizId })
            .done(function (d) {
            var row = $(".teams").where(function (x) { return x.attr("data-team-id") === teamId; });
            $(".table-added-teams").append(row);
        })
            .fail(function (e) {
            alert("Failed to add team :( !");
        });
    });
};
