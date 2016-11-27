
interface Expression<T> {
    (t: T): boolean;
}

interface JQuery {
    where(expression: Expression<any>): JQuery;
}

window.onload = () => {
    $(".btn-add-team")
        .click(e => {
            let $btn = $(e.target);

            let teamId = $btn.attr("data-team-id");
            let quizId = $btn.attr("data-quiz-id");

            $.post("/Quizzes/AddTeamToQuiz", { teamId: teamId, quizId: quizId })
                .done(d => {

                    const row = $(".teams").where(x => x.attr("data-team-id") === teamId);

                    $(".table-added-teams").append(row);

                })
                .fail(e => {
                    alert("Failed to add team :( !");
                });
        });
};
