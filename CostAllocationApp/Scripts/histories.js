$(document).ready(function () {


    $('#history_data_btn').on('click', function () {
        //get the multi search values
        var year = $('#history_year').val();
        console.log(year);
        if (year == '' || year == null || year == undefined) {
            alert('Select Year');
            return false;
        }
        $.ajax({
            url: `/api/utilities/GetTimeStamps`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            data: { year: year },
            success: function (data) {
                let i = 1;
                $('#timestamp_list tbody').empty();
                $.each(data, function (index, element) {
                    $('#timestamp_list tbody').append(`<tr><td>${i}</td><td data-id='${element.Id}' style='cursor:pointer;'>${element.TimeStamp}</td></tr>`);
                    i++;
                });
            }
        });

    });


    $('#timestamp_list tbody tr td').click(function () {
        alert('clicked');
    });

});