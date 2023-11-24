$.connection.hub.disconnected(function () {
    setTimeout(function () {
        $.connection.hub.start();
    }, 5000); // Restart connection after 5 seconds.
});

$.connection.hub.start();

$(function () {
    var chat = $.connection.chatHub;
    //$.connection.hub.start();
    //debugger;
    //console.log(chat);
    chat.client.addNewMessageToPage = function (name, message) {
        if (message != $('#user_name').text()) {
            alert(name + message);
        }

        $.ajax({
            url: `/api/utilities/GetUserLogs/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                var userName = $('#user_name').text();
                $('#user_log').empty();
                $.each(data, (index, value) => {
                    if (userName != value.UserName) {
                        $('#user_log').append(`<li style="float:left;background-color:lightgrey; border-radius:50px; overflow:hidden;margin-right:5px;">${value.UserName}</li>`);
                    }

                });

            }
        });
    };
});
$(document).ready(function () {
   // $("table1").DataTable();

    setInterval(() => {
        $.ajax({
            url: `/api/utilities/GetUserLogs/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                var userName = $('#user_name').text();
                $('#user_log').empty();
                $.each(data, (index, value) => {
                    if (userName !== value.UserName) {
                        $('#user_log').append(`<li style="float:left;background-color:lightgrey; border-radius:50px; overflow:hidden;margin-right:5px;">${value.UserName}</li>`);
                    }
                });

            }
        });
    }, 5000);

    $.ajax({
        url: `/api/utilities/GetUserLogs/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            var userName = $('#user_name').text();
            $('#user_log').empty();
            $.each(data, (index, value) => {
                if (userName !== value.UserName) {
                    $('#user_log').append(`<li style="float:left;background-color:lightgrey; border-radius:50px; overflow:hidden;margin-right:5px;">${value.UserName}</li>`);
                }
            });

        }
    });

});
function bs_input_file() {
    $(".input-file").before(
        function () {
            if (!$(this).prev().hasClass('input-ghost')) {
                var element = $("<input type='file' id='dataFile' name='upload' class='input-ghost' style='visibility:hidden; height:0'>");
                element.attr("name", $(this).attr("name"));
                element.change(function () {
                    element.next(element).find('input').val((element.val()).split('\\').pop());
                });
                $(this).find("button.btn-choose").click(function () {
                    element.click();
                });
                $(this).find("button.btn-reset").click(function () {
                    element.val(null);
                    $(this).parents(".input-file").find('input').val('');
                });
                $(this).find('input').css("cursor", "pointer");
                $(this).find('input').mousedown(function () {
                    $(this).parents('.input-file').prev().click();
                    return false;
                });
                return element;
            }
        }
    );
}

function clear() {
    var input = $("#dataFile").val('');
};
$(function () {
    clear();
    bs_input_file();
}); 