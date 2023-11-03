var jss;

$(document).ready(function () {
    $('#change_history_tbl').hide();   
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
        $('#change_history_tbl').show();   
        //get the multi search values
        var year = $('#history_year').val();
        console.log(year);
        if (year == '' || year == null || year == undefined) {
            alert('年度を選択してください');
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
                    //$("#timestamp_list").css("display", "block");
                    let i = 1;
                    $('#timestamp_list tbody').empty();
                    $.each(data, function (index, element) {
                        $('#timestamp_list tbody').append(`<tr><td style="text-align: center;">${element.CreatedBy}</td><td style="text-align: left;"><a href='javascript:void(0);'  onclick="GetHistories(${element.Id});" style="margin: 28px;">${element.TimeStamp}</a></td></tr>`);
                        i++;
                    });

                }
            });
        }, 2000);
        
        
    });

});
var sectionsForJexcel = [];

function DownloadHistoryByTimeStamps(changeTimestampsId){
    sectionsForJexcel.push({ id: 1, name: "test section"});
    jss = $('#jspreadsheet').jspreadsheet({
        //data: _retriveddata,
        // filters: true,
        // tableOverflow: true,
        // freezeColumns: 3,
        // defaultColWidth: 50,
        // tableWidth: w-280+ "px",
        // tableHeight: (h-150) + "px",
        
        columns: [            
            // { title: "要員(Employee)", type: "text", name: "EmployeeName", width: 150 },
            // { title: "Remarks", type: "text", name: "Remarks", width: 60 },
            { title: "区分(Section)", type: "dropdown", source: sectionsForJexcel, name: "SectionId", width: 100 },    
            { title: "IsApproved", type: 'hidden', name: "IsApproved" },        
        ],        
    });
}

$('#btn_export_change_history_data').on('click', function () {
    var timeStampId = $('#hidTimeStampid').val();
    // DownloadHistoryByTimeStamps(timeStampId)
    // $('#jspreadsheet').jexcel('download');
    // return false;
    var _retriveddata = [];
    $.ajax({        
        //url: `/api/utilities/DownloadHistoryData`,
        url: `/api/utilities/GetAssignmentHistoriesByTimeStampId`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        data: { timeStampId: timeStampId },
        success: function (data) {           
            _retriveddata = data;
        }
    });


    if (jss != undefined) {
        jss.destroy();
        $('#jspreadsheet').empty();
    }
    var w = window.innerWidth;
    var h = window.innerHeight;
    
    jss = $('#jspreadsheet').jspreadsheet({
        data: _retriveddata,
        csvHeaders: true,
        //filters: true,
        // tableOverflow: true,
        // // freezeColumns: 3,
        // // defaultColWidth: 50,
        // tableWidth: w-280+ "px",
        // tableHeight: (h-150) + "px",
        
        columns: [      
            { title: "利用者", type: "text", name: "CreatedBy", width: 120},
            { title: "要員(Employee)", type: "text", name: "EmployeeName", width: 150},
            { title: "操作内容 (Type)", type: "text", name: "OperationType", width: 100},
            { title: "Remaks", type: "text", name: "Remarks", width: 100},
            { title: "区分(Section)	", type: "text", name: "SectionName", width: 120},            
            { title: "部署(Dept)", type: "text", name: "DepartmentName", width: 120},
            { title: "担当作業(In chg)", type: "text", name: "InChargeName", width: 120},
            { title: "役割(Role)", type: "text", name: "RoleName", width: 120},
            { title: "説明(expl)", type: "text", name: "ExplanationName", width: 120},
            { title: "会社(Com)", type: "text", name: "CompanyName", width: 120},
            { title: "グレード(Grade)", type: "text", name: "GradePoints", width: 50},
            { title: "単価(Unit Price)", type: "text", name: "UnitPrice", width: 100},
            { title: "10月", type: "text", name: "OctPoints", width: 60},
            { title: "11月", type: "text", name: "NovPoints", width: 60},
            { title: "12月", type: "text", name: "DecPoints", width: 60},
            { title: "1月", type: "text", name: "JanPoints", width: 60},
            { title: "2月", type: "text", name: "FebPoints", width: 60},
            { title: "3月", type: "text", name: "MarPoints", width: 60},
            { title: "4月", type: "text", name: "AprPoints", width: 60},
            { title: "5月", type: "text", name: "MayPoints", width: 60},
            { title: "6月", type: "text", name: "JunPoints", width: 60},
            { title: "7月", type: "text", name: "JulPoints", width: 60},
            { title: "8月", type: "text", name: "AugPoints", width: 60},
            { title: "9月", type: "text", name: "SepPoints", width: 60},
        ],        
    });

    jss.download(true);
});


function GetHistories(timeStampId) {    
    $('#modal_change_history').modal('show');
    $('#hidTimeStampid').val(timeStampId);

    $.ajax({
        //url: `/api/utilities/GetHistoriesByTimeStampId`,
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
                $('#forecast_history_table thead').append(`<th style='white-space:nowrap;'>利用者</th><th style='white-space:nowrap;'>従業員名(Emp)</th><th style='white-space:nowrap;'>操作内容(Type)</th><th style='white-space:nowrap;'>注記(Remaks)</th<th style='white-space:nowrap;'>区分(Section)</th><th style='white-space:nowrap;'>部署(Dept)</th><th style='white-space:nowrap;'>担当作業(In chg)</th><th style='white-space:nowrap;'>役割(Role)</th><th style='white-space:nowrap;'>説明(expl)</th><th style='white-space:nowrap;'>会社(Com)</th><th style='white-space:nowrap;'>グレード(Grade)</th><th style='white-space:nowrap;'>単価(Unit Price)</th><th style='white-space:nowrap;'>10月</th><th style='white-space:nowrap;'>11月</th><th style='white-space:nowrap;'>12月</th><th style='white-space:nowrap;'>1月</th><th style='white-space:nowrap;'>2月</th><th style='white-space:nowrap;'>3月</th><th style='white-space:nowrap;'>4月</th><th style='white-space:nowrap;'>5月</th><th style='white-space:nowrap;'>6月</th><th style='white-space:nowrap;'>7月</th><th style='white-space:nowrap;'>8月</th><th style='white-space:nowrap;'>9月</th>`);
                $("#forecast_history").val('');
    
                $.each(data, function (index, element) {
                    //console.log(element);
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