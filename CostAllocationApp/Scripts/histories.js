var jss;

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
                    $('#timestamp_list tbody').append(`<tr><td>${element.CreatedBy}</td><td><a href='javascript:void(0);'  onclick="GetHistories(${element.Id});">${element.TimeStamp}</a></td></tr>`);
                    i++;
                });
            }
        });

    });



});

function GetHistories(timeStampId) {

    
    $.ajax({
        url: `/api/utilities/GetHistoriesByTimeStampId`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: { timeStampId: timeStampId },
        success: function (data) {
            $('#forecast_histories table').css('display', 'inline-table');
            $('#forecast_histories table tbody').empty();
            $.each(data, function (index, element) {
                //console.log(element);
                $('#forecast_histories table tbody').append(`<tr><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`);
            });
        }
    });


}