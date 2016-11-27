
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


                    $(".teams").where(x => x.attr("data-team-id") === teamId).remove();

                    $(".table-added-teams").append(row).focus();


                })
                .fail(e => {
                    alert("Failed to add team :( !");
                });
        });

    $(".btn-remove-from-quiz")
        .click(e => {
            let $btn = $(e.target);

            let teamId = $btn.attr("data-team-id");
            let quizId = $btn.attr("data-quiz-id");

            $.post("/Quizzes/RemoveFromQuiz", { teamId: teamId, quizId: quizId })
                .done(d => {

                    const row = $(".quiz-team").where(x => x.attr("data-team-id") === teamId);


                    $(".quiz-team").where(x => x.attr("data-team-id") === teamId).remove();

                    $(".table-all-teams").append(row).focus();

                    window.location.reload();
                })
                .fail(e => {
                    alert("Failed to remove team :( !");
                });
        });
};
