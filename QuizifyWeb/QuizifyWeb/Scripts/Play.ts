function refreshUserState(user: string) {
    $.get(`/api/Quizzes/${user}`)
        .done(e => {

            $(".tr-user")
                .where(x => x.attr("data-user-id") === user)
                .children(".td-user-present")
                .html(() => e.isPresent ? "Yes" : "No");
        });



}

function play() {
    $(".questions").slideToggle();


}

function checkIsStarted(quizId: number) {
    $.get(`/api/QuizzesStat/${quizId}`)
        .done(e => {

                if (e.DateTime !== "1" && $("#start-time").html() !== e.DateTime) {
                    $("#start-time").html(() => e.DateTime);
                    play();

                }

            }
        );
}

function moveNextQuestion(qId: string) {

    $(".quest").hide();
    $(".quest").where(x => x.attr("data-question-id") === qId).show();


    $.get(`/api/Roller/${qId}`);
}


window.onload = () => {


    let users: Array<string> = [];
    let questions: Array<string> = [];

    $(".tr-user")
        .each((k, v) => {
            let id = $(v).attr("data-user-id");

            users.push(id);
        });

    $(".quest")
        .each((k, v) => {
            let id = $(v).attr("data-question-id");

            questions.push(id);
        });



    setInterval(() => {

            users.forEach(u => refreshUserState(u));
            checkIsStarted(Number($("#quiz-id").html()));
        },
        1000);

    $(".next")
        .click(e => {
            //moveNextQuestion(questions.shift());

            if (questions.length === 0) {
                alert("All questions have been passed!");
                $(".next").hide();
                $(".quest").hide();
            } else {
                moveNextQuestion(questions.shift());

            }
        });


};