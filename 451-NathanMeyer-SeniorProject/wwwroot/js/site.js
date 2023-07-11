$(function () {
    console.log("Page is ready");


    //This event waits for a button in the game field to be clicked and calls the Game Controller with the button's Id
    $(document).on("mousedown", ".game-button", function (event) {
        switch (event.which) {
            case 1:
                event.preventDefault();
                var buttonNumber = $(this).val();
                doButtonUpdate(buttonNumber, "/Game/HandleButtonClick");
                break;
            default:
                console.log("default action");
                break;

        }
    })
})

// doButtonUpdate: this method is called to change the state of a button to either the on or off position.
function doButtonUpdate(buttonNumber, urlString) {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: location.origin + urlString,
        data: {
            "buttonId": buttonNumber,

        },
        success: function (data) {
            console.log(data);
            $("#" + buttonNumber).html(data);
            updateGrid();
        }
    })
}

// updateGrid: this method updates the game buttons that have changed state so that the user can see the changes on the game board.
function updateGrid() {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: location.origin + '/game/getSize',
        success: function (data) {
            console.log("Size is " + data)

            for (let i = 0; i < data; i++) {
                $.ajax({
                    datatype: "json",
                    method: 'GET',
                    url: location.origin + '/game/updatebutton',
                    data: {
                        "buttonId": i,
                    },
                    success: function (data) {
                        $("#" + i).html(data);
                    }
                })
            }
        }
    })
    updateMessage();
}

// updateMessage: this method updates the message below the gameboard depending on the board's current state
function updateMessage() {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: location.origin + '/game/getMessage',
        success: function (data) {
            console.log("Message: " + data);
            $(".gameMessage").html(data);
            console.log("message updated");
        }
    })
}