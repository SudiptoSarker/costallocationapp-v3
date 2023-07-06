jss = $('#jspreadsheet').jspreadsheet({
        data: _retriveddata,
        filters: true,
        tableOverflow: true,
        freezeColumns: 3,
        defaultColWidth: 50,
        tableWidth: w-280+ "px",
        tableHeight: (h-150) + "px",
        
        columns: [            
            { title: "要員(Employee)", type: "text", name: "EmployeeName", width: 150 },
            { title: "Remarks", type: "text", name: "Remarks", width: 60 },
            { title: "区分(Section)", type: "dropdown", source: sectionsForJexcel, name: "SectionId", width: 100 },
            { title: "部署(Dept)", type: "dropdown", source: departmentsForJexcel, name: "DepartmentId", width: 100 },
            { title: "担当作業(In chg)", type: "dropdown", source: inchargesForJexcel, name: "InchargeId", width: 100 },
            { title: "役割 ( Role)", type: "dropdown", source: rolesForJexcel, name: "RoleId", width: 60 },
            { title: "説明(expl)", type: "dropdown", source: explanationsForJexcel, name: "ExplanationId", width: 150 },
            { title: "会社(Com)", type: "dropdown", source: companiesForJexcel, name: "CompanyId", width: 100 },
            { 
                title: "グレード(Grade)", 
                type: "dropdown", 
                source: gradesForJexcel, 
                name: "GradeId", 
                width: 60 
            },
            { title: "単価(Unit Price)", type: "number", name: "UnitPrice", mask: "#,##0", width: 85 },
            {
                title: "10月",
                type: "decimal",
                name: "OctPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "11月",
                type: "decimal",
                name: "NovPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "12月",
                type: "decimal",
                name: "DecPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "1月",
                type: "decimal",
                name: "JanPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "2月",
                type: "decimal",
                name: "FebPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "3月",
                type: "decimal",
                name: "MarPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "4月",
                type: "decimal",
                name: "AprPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "5月",
                type: "decimal",
                name: "MayPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "6月",
                type: "decimal",
                name: "JunPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "7月",
                type: "decimal",
                name: "JulPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "8月",
                type: "decimal",
                name: "AugPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "9月",
                type: "decimal",
                name: "SepPoints",
                mask: '#.##,0',
                decimal: '.'
            },
            {
                title: "10月",
                type: "number",
                //readOnly: true,
                mask: "#,##0",
                name: "OctTotal",
                width: 60
            },
            {
                title: "11月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "NovTotal"
            },
            {
                title: "12月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "DecTotal"
            },
            {
                title: "1月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "JanTotal"
            },
            {
                title: "2月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "FebTotal"
            },
            {
                title: "3月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "MarTotal"
            },
            {
                title: "4月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "AprTotal"
            },
            {
                title: "5月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "MayTotal"
            },
            {
                title: "6月",
                type: "decimal",
                // readOnly: true,
                mask: "#,##0",
                name: "JunTotal"
            },
            {
                title: "7月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "JulTotal"
            },
            {
                title: "8月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "AugTotal"
            },
            {
                title: "9月",
                type: "decimal",
                //readOnly: true,
                mask: "#,##0",
                name: "SepTotal"
            },
            { title: "Employee Id", type: 'hidden', name: "EmployeeId" },
            { title: "BCYR", type: 'hidden', name: "BCYR" },
            { title: "BCYRCell", type: 'hidden', name: "BCYRCell" },
            { title: "IsActive", type: 'hidden', name: "IsActive" },
            { title: "BCYRApproved", type: 'hidden', name: "BCYRApproved" },
            { title: "BCYRCellApproved", type: 'hidden', name: "BCYRCellApproved" },
            { title: "IsApproved", type: 'hidden', name: "IsApproved" },
        ],
        minDimensions: [6, 10],
        columnSorting: true,        
    });