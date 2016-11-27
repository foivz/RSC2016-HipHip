function refreshUserState(user: string) {
    $.get(`/api/Quizzes/${user}`)
        .done(e => {
            console.log(e);

            $(".tr-user")
                .where(x => x.attr("data-user-id") === user)
                .children(".td-user-present")
                .html(() => e.isPresent ? "Yes" : "No");
        });
}

function checkIsStarted(quizId: number) {
    $.get(`/api/QuizzesStat/${quizId}`)
        .done(e => {

                if (e.DateTime !== "1" && $("#start-time").html() !== e.DateTime) {
                    $("#start-time").html(() => e.DateTime);
                }

            }
        );
}


window.onload = () => {


    let users: Array<string> = [];

    $(".tr-user")
        .each((k, v) => {
            let id = $(v).attr("data-user-id");

            users.push(id);
        });

    console.log(users);


    setInterval(() => {

            users.forEach(u => refreshUserState(u));
            checkIsStarted(Number($("#quiz-id").html()));
        },
        1000);


};