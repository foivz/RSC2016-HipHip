window.onload = function () {
    var membersToAdd = new Array();
    membersToAdd.push(userId);
    $("#btn-add-member")
        .click(function (e) {
        var $input = $("#MemberEmail");
        $.post("/api/Users", { Email: $input.val() })
            .done(function (response) {
            $(".table").show();
            if (response.Id === userId) {
                alert("You're already added!");
            }
            if (membersToAdd.indexOf(response.Id) > -1) {
                alert("User already added!");
            }
            else {
                $input.val(function () { return ""; });
                $(".table")
                    .append("\n                    <tr>\n<td>" + response.Name + "</td>\n<td>" + response.Email + "</td>\n<tr>\n                    ");
                membersToAdd.push(response.Id);
                console.log(membersToAdd);
            }
        });
    });
    $("#btn-create")
        .click(function (e) {
        var $input = $("#TeamName");
        $.post("/api/Teams", { Name: $input.val(), UserIds: membersToAdd })
            .done(function (response) {
            window.location.replace("/Teams");
        }).fail(function () { return alert("Failed :(!"); });
    });
};
//# sourceMappingURL=TeamsCreate.js.map