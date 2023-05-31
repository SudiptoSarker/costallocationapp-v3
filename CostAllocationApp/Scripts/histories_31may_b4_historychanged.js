var jss;

$(document).ready(function () {

    $.ajax({
        url: `/api/utilities/GetYearFromHistory`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $('#history_year').empty();
            $('#history_year').append('<option value="">年度データーの選択</option>');
            $.each(data, function (index, value) {
                $('#history_year').append(`<option value="${value}">${value}</option>`);
            });
        }
    });

    $('#history_data_btn').on('click', function () {        
        //get the multi search values
        var year = $('#history_year').val();
        console.log(year);
        if (year == '' || year == null || year == undefined) {
            alert('Select Year');
            return false;
        }
        LoaderShow();
        setTimeout(function () {
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
                        $('#timestamp_list tbody').append(`<tr><td>${element.CreatedBy}</td><td style="text-align: left;"><a href='javascript:void(0);'  onclick="GetHistories(${element.Id});" style="margin: 28px;">${element.TimeStamp}</a></td></tr>`);
                        i++;
                    });

                }
            });
        }, 2000);
        
        
    });

});




function GetHistories(timeStampId) {

    $('#modal_change_history').modal('show');

    $.ajax({
        url: `/api/utilities/GetHistoriesByTimeStampId`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: { timeStampId: timeStampId },
        success: function (data) {
            //$('#display_matched_rows table').css('display', 'inline-table');
            $('#forecast_histories table tbody').empty();
            $.each(data, function (index, element) {
                //console.log(element);
                $('#forecast_histories table tbody').append(`<tr><td>${element.CreatedBy}</td><td>${element.EmployeeName}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`);
            });
        }
    });


}
function LoaderShow() {    
    $("#timestamp_list").css("display", "none");
    $("#loading").css("display", "block");
}
function LoaderHide() {    
    $("#loading").css("display", "none");
    $("#timestamp_list").css("display", "block");
}

$(document).ajaxComplete(function(){
    LoaderHide();
});