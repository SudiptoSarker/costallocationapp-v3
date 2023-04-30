var jss;
var _retriveddata;


$(document).ready(function () {
    $.ajax({
        url: `/api/utilities/GetForecatYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $('#assignment_year').empty();
            $('#assignment_year').append(`<option value=''>select year</option>`);
            $.each(data, function (index, element) {
                $('#assignment_year').append(`<option value='${element.Year}'>${element.Year}</option>`);
            });
        }
    });

    $('#assignment_year').on('change', function () {

        var year = $(this).val();

        if (year == null || year == '' || year == undefined) {
            alert('Select Year!!!');
            return false;
        }

        $.ajax({
            url: `/api/utilities/GetAssignmentsByYear?year=${year}`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                //console.log(data);
                _retriveddata = data;

                if (jss != undefined) {
                    jss.destroy();
                    $('#jspreadsheet').empty();
                }

                var sectionsForJexcel = [];
                var departmentsForJexcel = [];
                var inchargesForJexcel = [];
                var rolesForJexcel = [];
                var explanationsForJexcel = [];
                var companiesForJexcel = [];

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
                

                jss = $('#jspreadsheet').jspreadsheet({
                    data: _retriveddata,
                    filters: true,
                    columns: [
                        { title: "Assignment Id", type: 'hidden', name: "AssignmentId" },
                        { title: "要員(Employee)", type: "text", name: "EmployeeName", width: 150 },
                        { title: "Remarks", type: "text", name: "Remarks", width: 60 },
                        { title: "区分(Section)", type: "dropdown", source: sectionsForJexcel, name: "SectionId", width: 100 },
                        { title: "部署(Dept)", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 100 },
                        { title: "担当作業(In chg)", type: "dropdown", source: inchargesForJexcel, name: "InchargeId", width: 100 },
                        { title: "役割 ( Role)", type: "dropdown", source: rolesForJexcel, name: "RoleId", width: 60 },
                        { title: "説明(expl)", type: "dropdown", source: explanationsForJexcel, name: "ExplanationId", width: 150 },
                        { title: "会社(Com)", type: "dropdown", source: companiesForJexcel, name: "CompanyId", width: 100 },
                        //{ type: 'number', title:'Price', mask:'$ #.##0,00', decimal:',' }
                        {
                            title: "10月",
                            type: "decimal",
                            name: "OctCost",
                            mask: "#,##0",
                            //decimal:'.',
                            width:100,
                        },
                        {
                            title: "11月",
                            type: "decimal",
                            name: "NovCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                        },
                        {
                            title: "12月",
                            type: "decimal",
                            name: "DecCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                        },
                        {
                            title: "1月",
                            type: "decimal",
                            name: "JanCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                        },
                        {
                            title: "2月",
                            type: "decimal",
                            name: "FebCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                        },
                        {
                            title: "3月",
                            type: "decimal",
                            name: "MarCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                        },
                        {
                            title: "4月",
                            type: "decimal",
                            name: "AprCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                        },
                        {
                            title: "5月",
                            type: "decimal",
                            name: "MayCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                        },
                        {
                            title: "6月",
                            type: "decimal",
                            name: "JunCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                        },
                        {
                            title: "7月",
                            type: "decimal",
                            name: "JulCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                        },
                        {
                            title: "8月",
                            type: "decimal",
                            name: "AugCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                        },
                        {
                            title: "9月",
                            type: "decimal",
                            name: "SepCost",
                            mask: "#,##0",
                            //decimal: '.',
                            width: 100,
                        },
                    ],
                    columnSorting: true,
                    contextMenu: function (obj, x, y, e) {

                    }
                });
                jss.deleteColumn(21, 4);
            }
        });
    });

    $('#create_actual_cost').on('click', function () {
        var dataToSend = [];
        var year = $('#assignment_year').val();

        if (jss != undefined) {
            var data = jss.getData(false);
            $.each(data, function (index,value) {
                var obj = {
                    assignmentId: value[0],
                    octCost: parseFloat(value[9]),
                    novCost: parseFloat(value[10]),
                    decCost: parseFloat(value[11]),
                    janCost: parseFloat(value[12]),
                    febCost: parseFloat(value[13]),
                    marCost: parseFloat(value[14]),
                    aprCost: parseFloat(value[15]),
                    mayCost: parseFloat(value[16]),
                    junCost: parseFloat(value[17]),
                    julCost: parseFloat(value[18]),
                    augCost: parseFloat(value[19]),
                    sepCost: parseFloat(value[20])
                };

                dataToSend.push(obj);
            });

            
            $.ajax({
                url: `/api/utilities/CreateActualCost`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: JSON.stringify({ ActualCosts: dataToSend, Year: year }),
                success: function (data) {
                    alert("Operation Success.");
                }
            });
        }
        else {
            alert('No Data Found!');
        }
    });
});