var jss;

$(document).ready(function () {
    var totalwidth = 190 * $('.modal-body').length;
    $('.container').css('width', totalwidth);

    $.ajax({
        // url: `/api/utilities/GetYearFromHistory`,
        url: `/api/utilities/GetApprovalAssignmentYearList`,
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
                url: `/api/utilities/GetApprovalTimeStamps`,
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
    $('#hidTimeStampid').val(timeStampId);
    $.ajax({
        //url: `/api/utilities/GetHistoriesByTimeStampId`,
        url: `/api/utilities/GetApprovalHistoriesByTimeStampId`,
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
                $('#forecast_history_table tbody').append(`<tr><td style='white-space:nowrap;'>${element.CreatedBy}</td><td style='white-space:nowrap;'>${element.EmployeeName}</td><td style='white-space:nowrap;'>${element.OperationType}</td><td style='white-space:nowrap;'>${element.Remarks}</td><td style='white-space:nowrap;'>${element.SectionName}</td><td style='white-space:nowrap;'>${element.DepartmentName}</td><td style='white-space:nowrap;'>${element.InChargeName}</td><td>${element.RoleName}</td><td style='white-space:nowrap;'>${element.ExplanationName}</td><td style='white-space:nowrap;'>${element.CompanyName}</td><td style='white-space:nowrap;'>${element.GradePoints}</td><td style='white-space:nowrap;'>${element.UnitPrice}</td><td style='white-space:nowrap;'>${element.OctPoints}</td><td style='white-space:nowrap;'>${element.NovPoints}</td><td style='white-space:nowrap;'>${element.DecPoints}</td><td style='white-space:nowrap;'>${element.JanPoints}</td><td style='white-space:nowrap;'>${element.FebPoints}</td><td style='white-space:nowrap;'>${element.MarPoints}</td><td style='white-space:nowrap;'>${element.AprPoints}</td><td style='white-space:nowrap;'>${element.MayPoints}</td><td style='white-space:nowrap;'>${element.JunPoints}</td><td style='white-space:nowrap;'>${element.JulPoints}</td><td style='white-space:nowrap;'>${element.AugPoints}</td><td style='white-space:nowrap;'>${element.SepPoints}</td></tr>`);             
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

$('#btn_export_approve_history_data').on('click', function () {
    var timeStampId = $('#hidTimeStampid').val();
    // DownloadHistoryByTimeStamps(timeStampId)
    // $('#jspreadsheet').jexcel('download');
    // return false;
    var _retriveddata = [];
    $.ajax({        
        //url: `/api/utilities/DownloadHistoryData`,
        url: `/api/utilities/GetApprovalHistoriesByTimeStampId`,
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
            { title: "Operation Type", type: "text", name: "OperationType", width: 100},
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

    //jss.download(true);
    jss.download(document.getElementById('jspreadsheet'), {
        filename: 'file.xsl',
        author: 's',
    });
    // $('#jspreadsheet').jexcel('download');
});