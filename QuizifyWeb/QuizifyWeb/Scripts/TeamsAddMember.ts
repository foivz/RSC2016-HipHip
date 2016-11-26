declare var userId;
declare var teamId;


interface JQueryStatic {
    put(url: string, data: any, success: (message: any) => void, error: (message: any) => void): void;
}

window.onload = () => {

    let membersToAdd: Array<string> = new Array<string>();

    membersToAdd.push(userId);

    $("#btn-add-member")
        .click(e => {

            let $input = $("#MemberEmail");


            $.put("/api/Teams",
                { Email: $input.val(), TeamId: teamId },
                response => {


                    $input.val(() => "");

                    $(".table")
                        .append(`
                    <tr>
<td>${response.Name}</td>
<td>${response.Email}</td>
<tr>
                    `);


                    membersToAdd.push(response.Id);

                    console.log(membersToAdd);

                },
                error => {
                    alert(error);
                    console.log(error);
                });
        });
}