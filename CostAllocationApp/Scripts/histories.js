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
                    $('#timestamp_list tbody').append(`<tr><td>${i}</td><td><a href='javascript:void(0);'  onclick="GetHistories(${element.Id});">${element.TimeStamp}</a></td></tr>`);
                    i++;
                });
            }
        });

    });


    $('#timestamp_list tbody a').click(function () {
        alert('clicked');
    });

});

function GetHistories(timeStampId) {

    if (jss != undefined) {
        jss.destroy();
        $('#jspreadsheet').empty();
    }

    jssUpdatedData = [];


    var data_info = {
        employeeName: '',
        sectionId: '',
        departmentId: '',
        inchargeId: '',
        roleId: '',
        explanationId: '',
        companyId: '',
        status: '', year: '', timeStampId: timeStampId
    };



    var sectionsForJexcel = [];
    var departmentsForJexcel = [];
    var inchargesForJexcel = [];
    var rolesForJexcel = [];
    var explanationsForJexcel = [];
    var companiesForJexcel = [];
    var gradesForJexcel = [];

    
    $.ajax({
        url: `/api/Sections`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                sectionsForJexcel.push({ id: value.Id, name: value.SectionName });
            });
        }
    });
    $.ajax({
        url: `/api/Departments`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {

            $.each(data, (index, value) => {
                departmentsForJexcel.push({ id: value.Id, name: value.DepartmentName });
            });
            //console.log(data);
        }
    });
    $.ajax({
        url: `/api/InCharges`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                inchargesForJexcel.push({ id: value.Id, name: value.InChargeName });
            });
        }
    });
    $.ajax({
        url: `/api/Roles`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                rolesForJexcel.push({ id: value.Id, name: value.RoleName });
            });
        }
    });
    $.ajax({
        url: `/api/Explanations`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                explanationsForJexcel.push({ id: value.Id, name: value.ExplanationName });
            });
        }
    });
    $.ajax({
        url: `/api/Companies`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                companiesForJexcel.push({ id: value.Id, name: value.CompanyName });
            });
        }
    });
    $.ajax({
        url: `/api/Salaries`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $.each(data, (index, value) => {
                gradesForJexcel.push({ id: value.Id, name: value.SalaryGrade });
            });
        }
    });

    var _retriveddata = [];

    //get the history data
    $.ajax({
        url: `/api/utilities/SearchForecastEmployee`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        //data: globalSearchObject,
        data: "employeeName=" + data_info.employeeName + "&sectionId=" + data_info.sectionId + "&departmentId=" + data_info.departmentId + "&inchargeId=" + data_info.inchargeId + "&roleId=" + data_info.roleId + "&explanationId=" + data_info.explanationId + "&companyId=" + data_info.companyId + "&status=" + data_info.year + "&year=" + data_info.year + "&timeStampId=" + data_info.timeStampId,
        success: function (data) {
            _retriveddata = data;
        }
    });

    //show history data like excel: plugin: jspreadsheet
    jss = $('#jspreadsheet').jspreadsheet({
        data: _retriveddata,
        tableOverflow: true,
        lazyLoading: true,
        tableWidth: '1200px',
        freezeColumns: 10,
        columns: [
            { title: "Id", type: 'hidden', name: "Id" },
            { title: "要員(Employee)", type: "text", name: "EmployeeName", width: 150 },
            { title: "区分(Section)", type: "dropdown", source: sectionsForJexcel, name: "SectionId", width: 85 },
            { title: "部署(Dept)", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 85 },
            { title: "担当作業(In chg)", type: "dropdown", source: inchargesForJexcel, name: "InchargeId", width: 85 },
            { title: "役割( Role)", type: "dropdown", source: rolesForJexcel, name: "RoleId", width: 85 },
            { title: "説明(expl)", type: "dropdown", source: explanationsForJexcel, name: "ExplanationId", width: 150 },
            { title: "会社(Com)", type: "dropdown", source: companiesForJexcel, name: "CompanyId", width: 85 },
            { title: "グレード(Grade)", type: "dropdown", source: gradesForJexcel, name: "GradeId" },
            { title: "単価(Unit Price)", type: "number", name: "UnitPrice", mask: "#,##0", width: 85 },

            {
                title: "10月",
                type: "decimal",
                name: "OctPoints"
            },
            {
                title: "11月",
                type: "decimal",
                name: "NovPoints"
            },
            {
                title: "12月",
                type: "decimal",
                name: "DecPoints"
            },
            {
                title: "1月",
                type: "decimal",
                name: "JanPoints"
            },
            {
                title: "2月",
                type: "decimal",
                name: "FebPoints"
            },
            {
                title: "3月",
                type: "decimal",
                name: "MarPoints"
            },
            {
                title: "4月",
                type: "decimal",
                name: "AprPoints"
            },
            {
                title: "5月",
                type: "decimal",
                name: "MayPoints"
            },
            {
                title: "6月",
                type: "decimal",
                name: "JunPoints"
            },
            {
                title: "7月",
                type: "decimal",
                name: "JulPoints"
            },
            {
                title: "8月",
                type: "decimal",
                name: "AugPoints"
            },
            {
                title: "9月",
                type: "decimal",
                name: "SepPoints"
            },
            {
                title: "10月",
                type: "number",
                readOnly: true,
                mask: "#,##0",
                name: "OctTotal"
            },
            {
                title: "11月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "NovTotal"
            },
            {
                title: "12月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "DecTotal"
            },
            {
                title: "1月",
                type: "decimal",
                readOnly: true,
                name: "JanTotal"
            },
            {
                title: "2月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "FebTotal"
            },
            {
                title: "3月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "MarTotal"
            },
            {
                title: "4月",
                type: "decimal",
                readOnly: true,
                name: "AprTotal"
            },
            {
                title: "5月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "MayTotal"
            },
            {
                title: "6月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "JunTotal"
            },
            {
                title: "7月",
                type: "decimal",
                readOnly: true,
                name: "JulTotal"
            },
            {
                title: "8月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "AugTotal"
            },
            {
                title: "9月",
                type: "decimal",
                readOnly: true,
                mask: "#,##0",
                name: "SepTotal"
            },
            { title: "Employee Id", type: 'hidden', name: "EmployeeId" },
        ],
        //minDimensions: [6, 10],
        columnSorting: true,
        //onchange: changed,
        contextMenu: function (obj, x, y, e) {
           
        }



    });

    jss.deleteColumn(35, 16);
}