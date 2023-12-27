function checkState() {
    var states = $('#states');
    var state = states.val();
    var commentInputDiv = $("#comment");

    switch (state) {
        case "cancelled":
            commentInputDiv.show();
            break;

        default:
            commentInputDiv.hide();
            break;
    }
}
