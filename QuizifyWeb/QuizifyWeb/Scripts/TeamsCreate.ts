
declare var userId;


window.onload = () => {
    let membersToAdd: Array<string> = new Array<string>();

    membersToAdd.push(userId);


    $("#btn-add-member")
        .click(e => {
           

            let $input = $("#MemberEmail");

            

            $.post("/api/Users", { Email: $input.val() })
                .done(response => {
                    $(".table").show();

                    if (response.Id === userId) {
                        alert("You're already added!");
                    }

                    if (membersToAdd.indexOf(response.Id) > -1) {
                        alert("User already added!");

                    } else {

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
                    }
                });
        });

    $("#btn-create")
        .click(e => {
            let $input = $("#TeamName");


            $.post("/api/Teams", { Name: $input.val(), UserIds: membersToAdd })
                .done(response => {
                    window.location.replace("/Teams");
                }).fail(() => alert("Failed :(!"));


        });


};