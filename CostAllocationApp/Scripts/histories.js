﻿var jss;

$(document).ready(function () {
    var totalwidth = 190 * $('.modal-body').length;
    $('.container').css('width', totalwidth);

    $.ajax({
        // url: `/api/utilities/GetYearFromHistory`,
        url: `/api/utilities/GetAssignmentYearList`,
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
        //url: `/api/utilities/GetHistoriesByTimeStampId`,
        url: `/api/utilities/GetAssignmentHistoriesByTimeStampId`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: { timeStampId: timeStampId },
        success: function (data) {
            //$('#display_matched_rows table').css('display', 'inline-table');
            // $('#forecast_histories table tbody').empty();
            $('#forecast_history_table tbody').empty();
            $("#forecast_history").val('');

            $.each(data, function (index, element) {
                //console.log(element);
                var forecastType = "";
                if(element.IsUpdate){
                    forecastType = "Updated";
                }
                else{
                    forecastType = "Inserted";
                }                              
                $('#forecast_history_table tbody').append(`<tr><td style='white-space:nowrap;'>${element.CreatedBy}</td><td style='white-space:nowrap;'>${element.EmployeeName}</td><td style='white-space:nowrap;'>${forecastType}</td><td style='white-space:nowrap;'>${element.Remarks}</td><td style='white-space:nowrap;'>${element.SectionName}</td><td style='white-space:nowrap;'>${element.DepartmentName}</td><td style='white-space:nowrap;'>${element.InChargeName}</td><td>${element.RoleName}</td><td style='white-space:nowrap;'>${element.ExplanationName}</td><td style='white-space:nowrap;'>${element.CompanyName}</td><td style='white-space:nowrap;'>${element.GradePoints}</td><td style='white-space:nowrap;'>${element.UnitPrice}</td><td style='white-space:nowrap;'>${element.OctPoints}</td><td style='white-space:nowrap;'>${element.NovPoints}</td><td style='white-space:nowrap;'>${element.DecPoints}</td><td style='white-space:nowrap;'>${element.JanPoints}</td><td style='white-space:nowrap;'>${element.FebPoints}</td><td style='white-space:nowrap;'>${element.MarPoints}</td><td style='white-space:nowrap;'>${element.AprPoints}</td><td style='white-space:nowrap;'>${element.MayPoints}</td><td style='white-space:nowrap;'>${element.JunPoints}</td><td style='white-space:nowrap;'>${element.JulPoints}</td><td style='white-space:nowrap;'>${element.AugPoints}</td><td style='white-space:nowrap;'>${element.SepPoints}</td></tr>`);
                
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