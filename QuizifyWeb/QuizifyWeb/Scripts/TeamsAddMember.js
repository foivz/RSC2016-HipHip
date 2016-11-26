window.onload = function () {
    var membersToAdd = new Array();
    membersToAdd.push(userId);
    $("#btn-add-member")
        .click(function (e) {
        var $input = $("#MemberEmail");
        $.put("/api/Teams", { Email: $input.val(), TeamId: teamId }, function (response) {
            $input.val(function () { return ""; });
            $(".table")
                .append("\n                    <tr>\n<td>" + response.Name + "</td>\n<td>" + response.Email + "</td>\n<tr>\n                    ");
            membersToAdd.push(response.Id);
            console.log(membersToAdd);
        }, function (error) {
            alert(error);
            console.log(error);
        });
    });
};
//# sourceMappingURL=TeamsAddMember.js.map