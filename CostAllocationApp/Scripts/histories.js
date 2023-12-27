var jss;
var sectionsForJexcel = [];

$(document).ready(function () {    
    $('#change_history_tbl').hide();   
    var totalwidth = 190 * $('.modal-body').length;
    $('.container').css('width', totalwidth);

    //history year list into dropdown
    $.ajax({
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
    
    //after click on the search button, timestamp list will show
    $('#history_data_btn').on('click', function () {                      
        var year = $('#history_year').val();
        console.log(year);
        if (year == '' || year == null || year == undefined) {
            alert('年度を選択してください');
            return false;
        }
        LoaderShow();        
        $('#change_history_tbl').show(); 
        $.ajax({
            url: `/api/utilities/GetTimeStamps`,
            contentType: 'application/json',
            type: 'GET',
            async: true,
            dataType: 'json',
            data: { year: year },
            success: function (data) {                
                let i = 1;
                $('#timestamp_list tbody').empty();
                $.each(data, function (index, element) {
                    $('#timestamp_list tbody').append(`<tr><td style="text-align: center;">${element.CreatedBy}</td><td style="text-align: left;"><a href='javascript:void(0);'  onclick="GetHistories(${element.Id});" style="margin: 28px;">${element.TimeStamp}</a></td></tr>`);
                    i++;
                });
                LoaderHide();
            }
        });                
    });
});

//show forecast history deatils on modal 
function GetHistories(timeStampId) {    
    $('#modal_change_history').modal('show');
    $('#hidTimeStampid').val(timeStampId);

    $.ajax({
        url: `/api/utilities/GetAssignmentHistoriesByTimeStampId`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: { timeStampId: timeStampId },
        success: function (data) {
            if (data == '' || data == null || data == undefined) {
                $('#forecast_history_table thead').empty();
                $('#forecast_history_table tbody').empty();
                $('#forecast_history_table tbody').append(`<tr><td style='white-space:nowrap;' colspan='24'>データ変更なし!</td></tr>`);
            }else{

                $('#forecast_history_table thead').empty();
                $('#forecast_history_table tbody').empty();
                $('#forecast_history_table thead').append(`<th style='white-space:nowrap;'>利用者</th><th style='white-space:nowrap;'>従業員名</th><th style='white-space:nowrap;'>操作内容</th><th style='white-space:nowrap;'>注記</th><th style='white-space:nowrap;'>区分</th><th style='white-space:nowrap;'>部署</th><th style='white-space:nowrap;'>担当作業</th><th style='white-space:nowrap;'>役割</th><th style='white-space:nowrap;'>説明</th><th style='white-space:nowrap;'>会社</th><th style='white-space:nowrap;'>グレード</th><th style='white-space:nowrap;'>単価</th><th style='white-space:nowrap;'>10月</th><th style='white-space:nowrap;'>11月</th><th style='white-space:nowrap;'>12月</th><th style='white-space:nowrap;'>1月</th><th style='white-space:nowrap;'>2月</th><th style='white-space:nowrap;'>3月</th><th style='white-space:nowrap;'>4月</th><th style='white-space:nowrap;'>5月</th><th style='white-space:nowrap;'>6月</th><th style='white-space:nowrap;'>7月</th><th style='white-space:nowrap;'>8月</th><th style='white-space:nowrap;'>9月</th>`);
                $("#forecast_history").val('');
    
                $.each(data, function (index, element) {
                    var forecastType = "";
                    if(element.IsUpdate){
                        forecastType = "更新(Updated)";
                    }
                    else{
                        forecastType = "追加(Inserted)";
                    }   
                    $('#forecast_history_table tbody').append(`<tr><td style='white-space:nowrap;'>${element.CreatedBy}</td><td style='white-space:nowrap;'>${element.EmployeeName}</td><td style='white-space:nowrap;'>${element.OperationType}</td><td style='white-space:nowrap;'>${element.Remarks}</td><td style='white-space:nowrap;'>${element.SectionName}</td><td style='white-space:nowrap;'>${element.DepartmentName}</td><td style='white-space:nowrap;'>${element.InChargeName}</td><td style='white-space:nowrap;'>${element.RoleName}</td><td style='white-space:nowrap;'>${element.ExplanationName}</td><td style='white-space:nowrap;'>${element.CompanyName}</td><td style='white-space:nowrap;'>${element.GradePoints}</td><td style='white-space:nowrap;text-align: right;'>${element.UnitPrice}</td><td style='white-space:nowrap;'>${element.OctPoints}</td><td style='white-space:nowrap;'>${element.NovPoints}</td><td style='white-space:nowrap;'>${element.DecPoints}</td><td style='white-space:nowrap;'>${element.JanPoints}</td><td style='white-space:nowrap;'>${element.FebPoints}</td><td style='white-space:nowrap;'>${element.MarPoints}</td><td style='white-space:nowrap;'>${element.AprPoints}</td><td style='white-space:nowrap;'>${element.MayPoints}</td><td style='white-space:nowrap;'>${element.JunPoints}</td><td style='white-space:nowrap;'>${element.JulPoints}</td><td style='white-space:nowrap;'>${element.AugPoints}</td><td style='white-space:nowrap;'>${element.SepPoints}</td></tr>`);
                    
                });     
            }                               
        }
    });


}

//loader show
function LoaderShow() {    
    $("#timestamp_list").css("display", "none");
    $("#loading").css("display", "block");
}

//loader hide
function LoaderHide() {    
    $("#loading").css("display", "none");
    $("#timestamp_list").css("display", "block");
}