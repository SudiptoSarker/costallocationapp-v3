$(document).ready(function () {
    $.connection.hub.disconnected(function () {
        setTimeout(function () {
            $.connection.hub.start();
        }, 5000); // Restart connection after 5 seconds.
    });
    $.connection.hub.start();
    var chat = $.connection.chatHub;
    
    $(function () {

        var chat = $.connection.chatHub;

        chat.client.addNewMessageToPage = function (name, message) {
            alert(name + message);
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
        };

    });

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
            $.each(data,(index, value) => {
                if (userName !== value.UserName) {
                    $('#user_log').append(`<li style="float:left;background-color:lightgrey; border-radius:50px; overflow:hidden;margin-right:5px;">${value.UserName}</li>`);
                }
            });

        }
    });
     
});

