var jss;

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
                //$('#forecast_histories table tbody').append(`<tr><td>${element.CreatedBy}</td><td>${element.EmployeeName}</td><td>${forecastType}</td><td>${element.Remarks}</td><td>${element.SectionName}</td><td>${element.DepartmentName}</td><td>${element.InChargeName}</td><td>${element.RoleName}</td><td>${element.ExplanationName}</td><td>${element.CompanyName}</td><td>${element.GradePoints}</td><td>${element.UnitPrice}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`);
                //$('#forecast_history_table tbody').append(`<tr><td>${element.CreatedBy}</td><td>${employeename}</td><td>${forecastType}</td><td>${element.Remarks}</td><td>${element.SectionName}</td><td>${element.DepartmentName}</td><td>${element.InChargeName}</td><td>${element.RoleName}</td><td>${explanationName}</td><td>${element.CompanyName}</td><td>${element.GradePoints}</td><td>${element.UnitPrice}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`);
                //html = $.parseHTML( str ),
                //$(html).find($('#forecast_history_table tbody').append(`<tr><td>${element.CreatedBy}</td><td>${employeename}</td><td>${forecastType}</td><td>${element.Remarks}</td><td>${element.SectionName}</td><td>${element.DepartmentName}</td><td>${element.InChargeName}</td><td>${element.RoleName}</td><td>${explanationName}</td><td>${element.CompanyName}</td><td>${element.GradePoints}</td><td>${element.UnitPrice}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`)).text();                
                if($("#forecast_history").val() ==''){
                    $('#forecast_history').val(`<tr><td>${element.CreatedBy}</td><td>${element.EmployeeName}</td><td>${forecastType}</td><td>${element.Remarks}</td><td>${element.SectionName}</td><td>${element.DepartmentName}</td><td>${element.InChargeName}</td><td>${element.RoleName}</td><td><lable>${element.ExplanationName}</label></td><td>${element.CompanyName}</td><td>${element.GradePoints}</td><td>${element.UnitPrice}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`);
                }else{
                    var tempForecastHistory = $('#forecast_history').val();
                    $('#forecast_history').val(tempForecastHistory + `<tr><td>${element.CreatedBy}</td><td>${element.EmployeeName}</td><td>${forecastType}</td><td>${element.Remarks}</td><td>${element.SectionName}</td><td>${element.DepartmentName}</td><td>${element.InChargeName}</td><td>${element.RoleName}</td><td><lable>${element.ExplanationName}</label></td><td>${element.CompanyName}</td><td>${element.GradePoints}</td><td>${element.UnitPrice}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`);
                }                 
                //$("#forecast_history_table tbody").html($('#forecast_history_table tbody').append(`<tr><td>${element.CreatedBy}</td><td>${element.EmployeeName}</td><td>${forecastType}</td><td>${element.Remarks}</td><td>${element.SectionName}</td><td>${element.DepartmentName}</td><td>${element.InChargeName}</td><td>${element.RoleName}</td><td>${element.ExplanationName}</td><td>${element.CompanyName}</td><td>${element.GradePoints}</td><td>${element.UnitPrice}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`));   
            });
            var allForecastHistory = $("#forecast_history").val();
            $("#forecast_history_table tbody").html(allForecastHistory)
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