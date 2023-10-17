var expanded = false;
$(function () {
    var chat = $.connection.chatHub;
    $.connection.hub.start();
    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page.
        $('#save_notifications').append(`<li>${name} ${message}</li>`);
    };

});









$(document).ready(function () {    
    GetAllForecastYears();   
    var count = 1;

    $('#employee_list').select2();

    $('#update_forecast_history').on('click', function () {
        var storeMessage = [];
        var _duplicateFlag = false;
        var _employeeIds = [];
        var _uniqueEmployeeIds=[];
        var employeeCount = 0;
        var rowCount = 0;
        $.ajax({
            url: `/api/utilities/GetUserLogs/`,
            contentType: 'application/json',
            type: 'GET',
            async: false,
            dataType: 'json',
            success: function (data) {
                employeeCount = data.length;
            }
        });
        debugger;
        if (jssInsertedData.length > 0) {
            for (var i = 0; i < jssInsertedData.length; i++) {
                if (jssInsertedData[i].sectionId == '' || jssInsertedData[i].departmentId == '' || jssInsertedData[i].companyId == '' || (jssInsertedData[i].unitPrice == 0 || isNaN(jssInsertedData[i].unitPrice))) {
                    storeMessage.push('invalid input for ' + jssInsertedData[i].employeeName);
                }
            }
        }

        if (jssUpdatedData.length > 0) {
            
            var tempUpdateData = [];
            const _distinctAssignmentIds = [...new Set(jssUpdatedData.map((item) => item.assignmentId))];
            var _allData = jss.getData();
            for (var i = 0; i < _distinctAssignmentIds.length; i++) {
                 for (var j = 0; j < _allData.length; j++) {
                     if (parseInt(_distinctAssignmentIds[i]) == parseInt(_allData[j][0])) {
                         tempUpdateData.push({
                             assignmentId: _allData[j][0],
                             employeeName: _allData[j][1],
                             remarks: _allData[j][2],
                             sectionId: _allData[j][3],
                             departmentId: _allData[j][4],
                             inchargeId: _allData[j][5],
                             roleId: _allData[j][6],
                             explanationId: _allData[j][7],
                             companyId: _allData[j][8],
                             gradeId: _allData[j][9],
                             unitPrice: _allData[j][10],
                             octPoint: _allData[j][11],
                             novPoint: _allData[j][12],
                             decPoint: _allData[j][13],
                             janPoint: _allData[j][14],
                             febPoint: _allData[j][15],
                             marPoint: _allData[j][16],
                             aprPoint: _allData[j][17],
                             mayPoint: _allData[j][18],
                             junPoint: _allData[j][19],
                             julPoint: _allData[j][20],
                             augPoint: _allData[j][21],
                             sepPoint: _allData[j][22],
                             bCYRApproved: _allData[j][41],
                             bCYRCell: _allData[j][39],
                             bCYRCellApproved: _allData[j][42],
                             bCYRCellPending: _allData[j][44],
                             bcyr: _allData[j][38],
                             employeeId: _allData[j][37],
                             isActive: _allData[j][40],
                             isApproved: _allData[j][43],
                             isDeletePending: _allData[j][46],
                             isRowPending: _allData[j][45],
                             rowType: _allData[j][47],
                             year: $('#assignment_year_list').val(),
                         });
                     }
                 }
            }

            jssUpdatedData = tempUpdateData;

            for (var k = 0; k < jssUpdatedData.length; k++) {
                if (jssUpdatedData[k].sectionId == '' || jssUpdatedData[k].departmentId == '' || jssUpdatedData[k].companyId == '' || (jssUpdatedData[k].unitPrice == 0 || isNaN(jssUpdatedData[k].unitPrice))) {
                    storeMessage.push('invalid input for ' + jssUpdatedData[k].employeeName);
                }
            }
        }

        if (storeMessage.length > 0) {
            let displayMessage = '';
            $.each(storeMessage, (index, value) => {
                displayMessage += value + '\n';
            });
            alert(displayMessage);
            return false;
        }

        
        var allTableData = jss.getData();

        for (var i = 0; i < jssInsertedData.length; i++) {
            _employeeIds.push(jssInsertedData[i].employeeId);
        }

        for (var i = 0; i < jssUpdatedData.length; i++) {
            _employeeIds.push(jssUpdatedData[i].employeeId);
        }

        if (_employeeIds.length > 0) {
            _uniqueEmployeeIds = _employeeIds.filter((value, index, array) => {
                return array.indexOf(value) === index;
            });
        }
        
        if (_uniqueEmployeeIds.length > 0) {
            var tempArray = [];
            var tempArrayCopy=[];
            for (var i = 0; i < _uniqueEmployeeIds.length; i++) {
                for (var j = 0; j < allTableData.length; j++) {
                    if (_uniqueEmployeeIds[i].toString() == allTableData[j][37].toString()) {
                        tempArray.push(allTableData[j]);
                    } 
                }
            }
            var singleRowDuplicationCount = 0;
            for (var i = 0; i < tempArray.length; i++) {
                
                if (tempArrayCopy.length == 0) {
                    tempArrayCopy.push(tempArray[i]);
                }
                else {
                    let tempArrayCount = tempArrayCopy.length;
                    for (var k = 0; k < tempArrayCount; k++) {
                        singleRowDuplicationCount = 0;

                        //section
                        if (tempArray[i][3] == tempArrayCopy[k][3]) {
                            singleRowDuplicationCount++;
                        }
                        //department
                        if (tempArray[i][4] == tempArrayCopy[k][4]) {
                            singleRowDuplicationCount++;
                        }
                        //in-charge
                        if (tempArray[i][5] == tempArrayCopy[k][5]) {
                            singleRowDuplicationCount++;
                        }
                        //role
                        if (tempArray[i][6] == tempArrayCopy[k][6]) {
                            singleRowDuplicationCount++;
                        }
                        //explanation
                        if (tempArray[i][7] == tempArrayCopy[k][7]) {
                            singleRowDuplicationCount++;
                        }
                        //company
                        if (tempArray[i][8]== tempArrayCopy[k][8]) {
                            singleRowDuplicationCount++;
                        }
                        //grade
                        if (tempArray[i][9] == tempArrayCopy[k][9]) {
                            singleRowDuplicationCount++;
                        }
                        //unit price
                        if (tempArray[i][10] == tempArrayCopy[k][10]) {
                            singleRowDuplicationCount++;
                        }
                        //employee id
                        if (tempArray[i][37] == tempArrayCopy[k][37]) {
                            singleRowDuplicationCount++;
                        }

                        if (singleRowDuplicationCount == 9) {
                            alert(tempArray[i][1]+" が重複しています");
                            _duplicateFlag = true;
                            break;
                        }
                        else {
                            tempArrayCopy.push(tempArray[i]);
                        }
                    }
                }
                if (_duplicateFlag == true) {
                    break;
                }
            }

            if (_duplicateFlag == true) {
                return false;
            }
        }

        
        if (jssInsertedData.length > 0) {
            var insertedUniqueEmployeeData_unitPrice = [];
            var insertedUniqueEmployeeData_role = [];
            var insertedUniqueEmployeeData_both = [];

            var lastColumnsData_unitPrice = [];
            var lastColumnsData_role = [];
            var lastColumnsData_both = [];

            var _unitPriceFlag = false;
            var _roleFlag = false;
            var _bothFlag = false;

            var _allData = jss.getData();            
            for (let i = 0; i < jssInsertedData.length; i++) {
                // checking unit price....
                if (jssInsertedData[i].rowType.toLowerCase().includes('unit')) {
                    {
                        if (jssInsertedData.length > 0) {
                            for (let a = 0; a < jssInsertedData.length; a++) {
                                if (jssInsertedData[a].rowType != '' || jssInsertedData[a].rowType != undefined) {
                                    if (jssInsertedData[a].rowType.toLowerCase().includes('unit')) {
                                        lastColumnsData_unitPrice.push(jssInsertedData[a].rowType);
                                    }
                                    
                                }
                            }

                            if (lastColumnsData_unitPrice.length > 0) {
                                insertedUniqueEmployeeData_unitPrice = lastColumnsData_unitPrice.filter((value, index, array) => {
                                    return array.indexOf(value) === index;
                                });
                            }

                            if (insertedUniqueEmployeeData_unitPrice.length > 0) {
                                var newUnitPriceList = [];
                                var newUnitPriceListCopy = [];
                                
                                var rowCount = 0;
                                for (let b = 0; b < insertedUniqueEmployeeData_unitPrice.length; b++) {
                                    newUnitPriceList = [];
                                    newUnitPriceListCopy = [];
                                    var splittedString = insertedUniqueEmployeeData_unitPrice[b].split('_');
                                    newUnitPriceList.push(jss.getRowData(parseInt(splittedString[2])));


                                    for (let k = 0; k < _allData.length; k++) {
                                        if (insertedUniqueEmployeeData_unitPrice[b] == _allData[k][45]) {
                                            newUnitPriceList.push(_allData[k]);
                                        }
                                    }

                                    for (let l = 0; l < newUnitPriceList.length; l++) {
                                        if (newUnitPriceListCopy.length == 0) {
                                            newUnitPriceListCopy.push(newUnitPriceList[l]);
                                        }
                                        else {
                                            let tempArrayCount = newUnitPriceListCopy.length;
                                            for (let m = 0; m < tempArrayCount; m++) {
                                                rowCount = 0;

                                                //oct point
                                                if (parseFloat(newUnitPriceList[l][11]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][11]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }

                                                //nov point
                                                if (parseFloat(newUnitPriceList[l][12]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][12]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //dec point
                                                if (parseFloat(newUnitPriceList[l][13]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][13]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jan point
                                                if (parseFloat(newUnitPriceList[l][14]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][14]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //feb point
                                                if (parseFloat(newUnitPriceList[l][15]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][15]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //mar point
                                                if (parseFloat(newUnitPriceList[l][16]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][16]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //apr point
                                                if (parseFloat(newUnitPriceList[l][17]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][17]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //may point
                                                if (parseFloat(newUnitPriceList[l][18]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][18]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jun point
                                                if (parseFloat(newUnitPriceList[l][19]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][19]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //jul point
                                                if (parseFloat(newUnitPriceList[l][20]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][20]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //aug point
                                                if (parseFloat(newUnitPriceList[l][21]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][21]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }
                                                //sep point
                                                if (parseFloat(newUnitPriceList[l][22]) > 0) {
                                                    if (parseFloat(newUnitPriceListCopy[m][22]) > 0) {
                                                        rowCount++;
                                                        _unitPriceFlag = true;
                                                        alert('duplicate (unit price) row(s) found for ' + newUnitPriceList[l][1]);
                                                        break;
                                                    }
                                                }

                                                newUnitPriceListCopy.push(newUnitPriceList[l]);
                                            }
                                        }
                                    }
                                    if (_unitPriceFlag == true) {
                                        break;
                                    }

                                }// main loop
                            }
                            if (_unitPriceFlag == true) {
                                return false;
                            }
                        }
                    }
                    continue;
                }
                // checking role
                if (jssInsertedData[i].rowType.toLowerCase().includes('role')) {
                    {
                        if (jssInsertedData.length > 0) {
                            for (let a = 0; a < jssInsertedData.length; a++) {
                                if (jssInsertedData[a].rowType != '' || jssInsertedData[a].rowType != undefined) {
                                    if (jssInsertedData[a].rowType.toLowerCase().includes('role')){
                                        lastColumnsData_role.push(jssInsertedData[a].rowType);
                                    }
                                    
                                }
                            } 

                            if (lastColumnsData_role.length > 0) {
                                insertedUniqueEmployeeData_role = lastColumnsData_role.filter((value, index, array) => {
                                    return array.indexOf(value) === index;
                                });
                            }

                            if (insertedUniqueEmployeeData_role.length > 0) {
                                var newRoleList = [];
                                var newRoleListCopy = [];
                                var rowCount = 0;
                                for (let b = 0; b < insertedUniqueEmployeeData_role.length; b++) {
                                    newRoleList = [];
                                    newRoleListCopy = [];
                                    var splittedString = insertedUniqueEmployeeData_role[b].split('_');
                                    newRoleList.push(jss.getRowData(parseInt(splittedString[2])));


                                    for (let k = 0; k < _allData.length; k++) {
                                        if (insertedUniqueEmployeeData_role[b] == _allData[k][45]) {
                                            newRoleList.push(_allData[k]);
                                        }
                                    }

                                    for (let l = 0; l < newRoleList.length; l++) {
                                        if (newRoleListCopy.length == 0) {
                                            newRoleListCopy.push(newRoleList[l]);
                                        }
                                        else {
                                            let tempArrayCount = newRoleListCopy.length;
                                            for (let m = 0; m < tempArrayCount; m++) {
                                                let rowCountRole = 0;

                                                //role column
                                                if (newRoleList[l][3] == newRoleListCopy[m][3]) {
                                                    rowCountRole++;
                                                }
                                                //role column
                                                if (newRoleList[l][4] == newRoleListCopy[m][4]) {
                                                    rowCountRole++;
                                                }
                                                //role column
                                                if (newRoleList[l][5] == newRoleListCopy[m][5]) {
                                                    rowCountRole++;
                                                }
                                                //role column
                                                if (newRoleList[l][6] == newRoleListCopy[m][6]) {
                                                    rowCountRole++;
                                                }

                                                if (rowCountRole==4) {
                                                    _roleFlag = true;
                                                    alert('duplicate (role) row(s) found for ' + newRoleList[l][1]);
                                                    break;
                                                }

                                                newRoleListCopy.push(newRoleList[l]);
                                            }
                                        }
                                    }
                                    if (_roleFlag == true) {
                                        break;
                                    }

                                }// main loop
                            }
                            if (_roleFlag == true) {
                                return false;
                            }
                        }
                    }
                    continue;
                }
                // checking both
                if (jssInsertedData[i].rowType.toLowerCase().includes('both')) {
                    {
                        if (jssInsertedData.length > 0) {
                            for (let a = 0; a < jssInsertedData.length; a++) {
                                if (jssInsertedData[a].rowType != '' || jssInsertedData[a].rowType != undefined) {
                                    if (jssInsertedData[a].rowType.toLowerCase().includes('both')) {
                                        lastColumnsData_both.push(jssInsertedData[a].rowType);
                                    }
                                    
                                }
                            }

                            if (lastColumnsData_both.length > 0) {
                                insertedUniqueEmployeeData_both = lastColumnsData_both.filter((value, index, array) => {
                                    return array.indexOf(value) === index;
                                });
                            }

                            if (insertedUniqueEmployeeData_both.length > 0) {
                                var newBothList = [];
                                var newBothListCopy = [];

                                var rowCount = 0;
                                for (let b = 0; b < insertedUniqueEmployeeData_both.length; b++) {
                                    newBothList = [];
                                    newBothListCopy = [];
                                    var splittedString = insertedUniqueEmployeeData_both[b].split('_');
                                    newBothList.push(jss.getRowData(parseInt(splittedString[2])));


                                    for (let k = 0; k < _allData.length; k++) {
                                        if (insertedUniqueEmployeeData_both[b] == _allData[k][45]) {
                                            newBothList.push(_allData[k]);
                                        }
                                    }

                                    for (let l = 0; l < newBothList.length; l++) {
                                        if (newBothListCopy.length == 0) {
                                            newBothListCopy.push(newBothList[l]);
                                        }
                                        else {
                                            let tempArrayCount = newBothListCopy.length;
                                            for (let m = 0; m < tempArrayCount; m++) {
                                                bothRowCount = 0;
                                                //section column
                                                if (newBothList[l][3] == newBothListCopy[m][3]) {
                                                    bothRowCount++;
                                                }
                                                //department column
                                                if (newBothList[l][4] == newBothListCopy[m][4]) {
                                                    bothRowCount++;
                                                }
                                                //in-charge column
                                                if (newBothList[l][5] == newBothListCopy[m][5]) {
                                                    bothRowCount++;
                                                }
                                                //role column
                                                if (newBothList[l][6] == newBothListCopy[m][6]) {
                                                    bothRowCount++;
                                                }
                                                if (bothRowCount == 4) {
                                                    _bothFlag = true;
                                                    let _countNumber = 0;
                                                    alert('duplicate (unitprice/role) row(s) found for ' + newBothList[l][1]);
                                                    for (var o = 0; o < _allData.length; o++) {
                                                        if (_allData[o][1] == newBothList[l][1]) {
                                                            break;
                                                        }
                                                        _countNumber++;
                                                    }
                                                    jss.setStyle(`B${_countNumber + 1}`, "background-color", "red");
                                                    jss.setStyle(`B${_countNumber + 1}`, "color", "black");
                                                    break;
                                                }
                                                // check unitprice
                                                if (newBothList[l][10] == newBothListCopy[m][10]) {
                                                    rowCount++;
                                                    _bothFlag = true;
                                                    let _countNumber = 0;
                                                    alert('duplicate (unitprice/role) row(s) found for ' + newBothList[l][1]);
                                                    for (var o = 0; o < _allData.length; o++) {
                                                        if (_allData[o][1] == newBothList[l][1]) {
                                                            break;
                                                        }
                                                        _countNumber++;
                                                    }
                                                    jss.setStyle(`B${_countNumber + 1}`, "background-color", "red");
                                                    jss.setStyle(`B${_countNumber + 1}`, "color", "black");
                                                    break;
                                                }
                                                newBothListCopy.push(newBothList[l]);
                                            }
                                        }
                                    }
                                    if (_bothFlag == true) {
                                        break;
                                    }

                                }// main loop
                            }
                            if (_bothFlag == true) {
                                return false;
                            }
                        }
                    }
                    continue;
                }
            }
        }        
        var isUpdateInsertDelete = false;

        if (jssInsertedData.length > 0 || jssUpdatedData.length > 0 || deletedExistingRowIds.length > 0) {
            isUpdateInsertDelete = true;
        } else {
            isUpdateInsertDelete = false;
        }

        if (jssUpdatedData.length > 0) {

            $.ajax({
                url: `/api/utilities/GetMatchedRowNumber/`,
                contentType: 'application/json',
                type: 'POST',
                async: false,
                dataType: 'json',
                data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: '' }),
                success: function (data) {
                    rowCount = data;
                    $.ajax({
                        url: `/api/utilities/GetMatchedUserNames/`,
                        contentType: 'application/json',
                        type: 'POST',
                        async: false,
                        dataType: 'json',
                        data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: '' }),
                        success: function (userNames) {
                            $('#user_names').empty();
                            $('#user_names').append(userNames);
                        }
                    });
                    $('#row_count').empty();
                    $('#row_count').append(data);
                }
            });
        }


        if (employeeCount == 1) {
            $('#header_show').css('display', 'none');
            $('#back_button_show').css('display', 'none');
        }
        else {
            if (rowCount == 0) {
                $('#header_show').css('display', 'none');
                $('#back_button_show').css('display', 'none');
            }
            else {
                $('#header_show').css('display', 'block');
                $('#back_button_show').css('display', 'block');
            }
         
        }
        if(isUpdateInsertDelete){
            UpdateForecast();
        }else{            
            alert("変更されていないので、保存できません");
            return false;
        }        
    });




    $(document).on('change', '#section_search', function () {

        var sectionId = $(this).val();

        $.getJSON(`/api/utilities/DepartmentsBySection/${sectionId}`)
            .done(function (data) {
                $('#department_search').empty();
                $('#department_search').append(`<option value=''>部署を選択</option>`);
                $.each(data, function (key, item) {
                    $('#department_search').append(`<option value='${item.Id}'>${item.DepartmentName}</option>`);
                });
            });
    });
    
    $(document).on('click', '#assignment_year_data ', function () { 
        ClearnAllJexcelData();

        $('#changed_cell_with_assignmentid').val("");  
        
        var assignmentYear = $('#assignment_year_list').val();
        if (assignmentYear == '' || assignmentYear == null || assignmentYear == undefined) {
            alert('年度を選択してください!!!');
            return false;
        }     
        
        LoaderShowJexcel();
            
        setTimeout(function () {                       
            ShowForecastResults(assignmentYear,"edit");
        }, 3000);

        
    });
    $(document).on('click', '#cancel_forecast_history ', function () {    
        ClearnAllJexcelData();

        var assignmentYear = $('#assignment_year_list').val();          
        if(assignmentYear==''){
            assignmentYear = 2023;
        }

        deletedExistingRowIds = [];
        LoaderShowJexcel();            
        setTimeout(function () {                               
            ShowForecastResults(assignmentYear,"edit");
        }, 3000);
        
    });
    
    $(document).ajaxComplete(function(){
        LoaderHideJexcel();
    });

});


$("#hider").hide();
$(".search_p").hide();
$(".search_section").hide();
$(".search_department").hide();
$(".search_incharge").hide();
$(".search_role").hide();
$(".search_explanation").hide();
$(".search_company").hide();
$(".search_grade").hide();
$(".search_unit_price").hide();

$("#buttonClose,#buttonClose_section,#buttonClose_department,#buttonClose_incharge,#buttonClose_role,#buttonClose_explanation,#buttonClose_company,#buttonClose_grade,#buttonClose_unit_price").click(function () {

    $("#hider").fadeOut("slow");
    $('.search_p').fadeOut("slow");
    $('.search_section').fadeOut("slow");
    $('.search_department').fadeOut("slow");
    $('.search_incharge').fadeOut("slow");
    $('.search_role').fadeOut("slow");
    $('.search_explanation').fadeOut("slow");
    $('.search_company').fadeOut("slow");
    $('.search_grade').fadeOut("slow");
    $('.search_unit_price').fadeOut("slow");
});

var newRowInserted = function (instance, x, y, newRow) {
    var totalRow = jss.getData(false);
    jss.setStyle(`A${totalRow.length - 2}`, 'color', 'red');    
}


function updateArray(array, retrivedData) {
    var index = jssUpdatedData.findIndex(d => d.assignmentId == retrivedData.assignmentId);

    array[index].employeeId = retrivedData.employeeId;
    array[index].sectionId = retrivedData.sectionId;
    array[index].departmentId = retrivedData.departmentId;
    array[index].inchargeId = retrivedData.inchargeId;
    array[index].roleId = retrivedData.roleId;
    array[index].explanationId = retrivedData.explanationId;
    array[index].companyId = retrivedData.companyId;
    array[index].gradeId = retrivedData.gradeId;
    array[index].unitPrice = retrivedData.unitPrice;
    array[index].octPoint = retrivedData.octPoint;
    array[index].novPoint = retrivedData.novPoint;
    array[index].decPoint = retrivedData.decPoint;
    array[index].janPoint = retrivedData.janPoint;
    array[index].febPoint = retrivedData.febPoint;
    array[index].marPoint = retrivedData.marPoint;
    array[index].aprPoint = retrivedData.aprPoint;
    array[index].mayPoint = retrivedData.mayPoint;
    array[index].junPoint = retrivedData.junPoint;
    array[index].julPoint = retrivedData.julPoint;
    array[index].augPoint = retrivedData.augPoint;
    array[index].sepPoint = retrivedData.sepPoint;
    array[index].year = retrivedData.year;
}

function updateArrayForInsert(array, retrivedData, x,y, cell, value, beforeChangedValue) {
    var index = array.findIndex(d => d.assignmentId == retrivedData.assignmentId);
    array[index].assignmentId = retrivedData.assignmentId;
    array[index].employeeId = retrivedData.employeeId;
    array[index].employeeName = retrivedData.employeeName;
    
    array[index].sectionId = retrivedData.sectionId;
    array[index].departmentId = retrivedData.departmentId;
    array[index].inchargeId = retrivedData.inchargeId;
    array[index].roleId = retrivedData.roleId;
    
    array[index].companyId = retrivedData.companyId;
    array[index].gradeId = retrivedData.gradeId;
    array[index].unitPrice = retrivedData.unitPrice;
    array[index].rowType = retrivedData.rowType;
    if (x == 2) {
        array[index].remarks = retrivedData.remarks;
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }
    }
    if (x == 7) {
        array[index].explanationId = retrivedData.explanationId;
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }
    }
    if (x == 11) {
        var octSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                octSum += parseFloat(dataValue[11]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || octSum > 1) {
            octSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);

        }
        else {
            array[index].octPoint = retrivedData.octPoint;
        }

        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39,y);
            currentValue += ',new-x_'+x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }


    }

    if (x == 12) {
        var novSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                novSum += parseFloat(dataValue[12]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || novSum > 1) {
            novSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].novPoint = retrivedData.novPoint;
        }

        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }


    }
    if (x == 13) {
        var decSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                decSum += parseFloat(dataValue[13]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || decSum > 1) {
            decSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].decPoint = retrivedData.decPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 14) {
        var janSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                janSum += parseFloat(dataValue[14]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || janSum > 1) {
            janSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].janPoint = retrivedData.janPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 15) {
        var febSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                febSum += parseFloat(dataValue[15]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || febSum > 1) {
            febSum = 1;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].febPoint = retrivedData.febPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    
    if (x == 16) {
        var marSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                marSum += parseFloat(dataValue[16]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || marSum > 1) {
            marSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].marPoint = retrivedData.marPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 17) {
        var aprSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                aprSum += parseFloat(dataValue[17]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || aprSum > 1) {
            aprSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].aprPoint = retrivedData.aprPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 18) {
        var maySum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                maySum += parseFloat(dataValue[18]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || maySum > 1) {
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].mayPoint = retrivedData.mayPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 19) {
        var junSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                junSum += parseFloat(dataValue[19]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || junSum > 1) {
            junSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].junPoint = retrivedData.junPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 20) {
        var julSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                julSum += parseFloat(dataValue[20]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || julSum > 1) {
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].julPoint = retrivedData.julPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
    if (x == 21) {
        var augSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                augSum += parseFloat(dataValue[21]);
            }

        });
        if (isNaN(value) || parseFloat(value) < 0 || augSum > 1) {
            augSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].augPoint = retrivedData.augPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }
    }
    if (x == 22) {
        var sepSum = 0;
        $.each(jss.getData(), (index, dataValue) => {
            if (dataValue[37].toString() == retrivedData.employeeId.toString() && dataValue[40] == true) {
                sepSum += parseFloat(dataValue[22]);
            }

        });
        if (isNaN(value) || parseFloat(sepSum) < 0 || sepSum > 1) {
            sepSum = 0;
            alert('入力値が不正です');
            jss.setValueFromCoords(x, y, beforeChangedValue, false);
        }
        else {
            array[index].sepPoint = retrivedData.sepPoint;
        }
        
        if (!newRowChangeEventFlag) {
            $(cell).css('color', 'red');
            $(cell).css('background-color', 'yellow');
            var currentValue = jss.getValueFromCoords(39, y);
            currentValue += ',new-x_' + x;
            jss.setValueFromCoords(39, y, currentValue, false);
        }

    }
        
    array[index].year = retrivedData.year;
    array[index].bcyr = retrivedData.bcyr;  

    array[index].duplicateFrom = retrivedData.duplicateFrom;  
    array[index].duplicateCount = retrivedData.duplicateCount;  
    array[index].roleChanged = retrivedData.roleChanged;  
    array[index].unitPriceChanged = retrivedData.unitPriceChanged;  
    
    
    if(array[index].bCYRCell.length <= retrivedData.bCYRCell.length){
        array[index].bCYRCell= retrivedData.bCYRCell;  
    }            
}

function retrivedObject(rowData) {
    return {
        assignmentId: rowData[0],
        employeeName: rowData[1],
        remarks: rowData[2],
        employeeId: rowData[37],
        sectionId: rowData[3],
        departmentId: rowData[4],
        inchargeId: rowData[5],
        roleId: rowData[6],
        explanationId: rowData[7],
        companyId: rowData[8],
        gradeId: rowData[9],
        unitPrice: parseFloat(rowData[10]),
        octPoint: parseFloat(rowData[11]),
        novPoint: parseFloat(rowData[12]),
        decPoint: parseFloat(rowData[13]),
        janPoint: parseFloat(rowData[14]),
        febPoint: parseFloat(rowData[15]),
        marPoint: parseFloat(rowData[16]),
        aprPoint: parseFloat(rowData[17]),
        mayPoint: parseFloat(rowData[18]),
        junPoint: parseFloat(rowData[19]),
        julPoint: parseFloat(rowData[20]),
        augPoint: parseFloat(rowData[21]),
        sepPoint: parseFloat(rowData[22]),
        year: document.getElementById('assignment_year_list').value,
        bcyr: rowData[38],
        bCYRCell: rowData[39],
        isActive: rowData[40],
        bCYRApproved: rowData[41],
        bCYRCellApproved: rowData[42],
        isApproved: rowData[43],
        bCYRCellPending: rowData[44],
        isRowPending: rowData[45],
        isDeletePending: rowData[46],
        rowType: rowData[47],
        duplicateFrom: rowData[48],
        duplicateCount: rowData[49],
        roleChanged: rowData[50],
        unitPriceChanged: rowData[51],
        
    };
}

function DeleteRecords() {
    $.getJSON(`/api/utilities/DeleteAssignments/`)
        .done(function (data) {
        });
}

//employee insert
function InsertEmployee() {
    var apiurl = "/api/utilities/CreateEmployee/";
    let employeeName = $("#employee_name").val();
    if (employeeName == "" || employeeName == null || employeeName == undefined) {
        $(".employee_err").show();
        return false;
    } else {
        $(".employee_err").hide();
        var data = {
            FullName: employeeName.trim()
        };

        $.ajax({
            url: apiurl,
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (result) {
                if (result > 0) {
                    jss.setValueFromCoords(1, globalY, data.FullName, false);
                    jss.setValueFromCoords(37, globalY, result, false);
                    $("#page_load_after_modal_close").val("yes");
                    ToastMessageSuccess('データが保存されました!');
                    $('#employee_name').val('');
                    $('#jexcel_add_employee_modal').modal('hide');
                }                
            },
            error: function (result) {
                alert(result.responseJSON.Message);
            }
        });
    }
}

//Get employee list
function GetEmployeeList() {
    var assignment_year = $("#assignment_year_list").val();

    $('#employee_list').empty();
    $.getJSON(`/api/utilities/EmployeeListForEmployeeAssignment/${assignment_year}`)
        .done(function (data) {
            $.each(data, function (key, item) {
                $('#employee_list').append(`<option value='${item.Id}'>${item.FullName}</option>`);
            });
        });
}

/***************************\                           
  Add Employee through Modal                      
\***************************/
function AddEmployee() {
    var employeeId = $('#employee_list').val();
    var employeeName = $('#employee_list').find("option:selected").text();

    //show the duplicate name: rule can be for: role/unit/both
    var employeeNameForAddEmployee = '';
    var allTableData = jss.getData();    
    var activeEmployeeCountForAddEmployee =0;
    var masterEmployeeNameForAddEmployee = "";
    var inactiveEmployeeCountForAddEmployee = 0;

    for (let x of allTableData) {            
        console.log("x: "+x);                
        console.log("retrivedData.employeeId: "+employeeId);                
		if(parseInt(x[37]) == parseInt(employeeId)){
			activeEmployeeCountForAddEmployee = activeEmployeeCountForAddEmployee+1;
		}                          
	}
    var tempEmployeeAssignmentId=0;
    var year = $("#assignment_year_list").val();
	$.ajax({
		url: `/api/utilities/GetEmployeeNameForMenuChange`,
		contentType: 'application/json',
		type: 'GET',
		async: false,
		dataType: 'json',
		data: "employeeAssignmentId=" + tempEmployeeAssignmentId+"&employeeId="+employeeId+"&menuType=unit"+"&year="+year,
		success: function (data) { 
			masterEmployeeNameForAddEmployee = data.EmployeeName;
			inactiveEmployeeCountForAddEmployee = data.EmployeeCount;
		}
	});                             
    employeeNameForAddEmployee =   masterEmployeeNameForAddEmployee +" ("+(parseInt(activeEmployeeCountForAddEmployee)+parseInt(inactiveEmployeeCountForAddEmployee)+1)+")";

    jss.setValueFromCoords(1, globalY, employeeName, false);
    jss.setValueFromCoords(37, globalY, employeeId, false);
    $('#jexcel_add_employee_modal').modal('hide');
}

function UpdateForecast() {
    $("#update_forecast").modal("hide");
    $("#jspreadsheet").hide();
    LoaderShow();

    var userName = '';

    $.ajax({
        url: `/Registration/GetSession/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            userName = data;
        }
    });


    var updateMessage = "";
    var insertMessage = "";
    var deleteMessage = "";

    var promptValue = prompt("履歴ファイル保存名", '');
    $("#timeStamp_ForUpdateData").val('');
    if (promptValue == null || promptValue == undefined || promptValue == "") {
        return false;
    } else {
        var dateObj = new Date();
        var month = dateObj.getUTCMonth() + 1; //months from 1-12
        var day = dateObj.getDate();
        var year = dateObj.getUTCFullYear();
        var miliSeconds = dateObj.getMilliseconds();
        var timestamp = `${year}${month}${day}${miliSeconds}_`;
        
        if (jssUpdatedData.length > 0) {           
                updateMessage = "Successfully data updated";
                $.ajax({
                    url: `/api/utilities/UpdateForecastData`,
                    contentType: 'application/json',
                    type: 'POST',
                    async: false,
                    dataType: 'json',
                    data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: timestamp + promptValue, CellInfo: cellwiseColorCode }),
                    success: function (data) {
                        var year = $("#assignment_year_list").val();
                        ShowForecastResults(year,"edit");

                        $("#timeStamp_ForUpdateData").val(data);
                        var chat = $.connection.chatHub;
                        $.connection.hub.start();
                        // Start the connection.
                        $.connection.hub.start().done(function () {
                            chat.server.send('data has been updated by ', userName);
                        });
                        $("#jspreadsheet").show();
                        //$("#head_total").show();
                        LoaderHide();
                    }
                });
                jssUpdatedData = [];                        
        }
        else {
            $("#jspreadsheet").show();
            LoaderHide();
            updateMessage = ""
        }

        if (jssInsertedData.length > 0) {
            var elementIndex = jssInsertedData.findIndex(object => {
                return object.employeeName.toLowerCase() == 'total';
            });
            if (elementIndex >= 0) {
                jssInsertedData.splice(elementIndex, 1);
            }

            
            var update_timeStampId = $("#timeStamp_ForUpdateData").val();
            
                insertMessage = "Successfully data inserted.";
                $.ajax({
                    url: `/api/utilities/ExcelAssignment/`,
                    contentType: 'application/json',
                    type: 'POST',
                    async: false,
                    dataType: 'json',
                    data: JSON.stringify({ ForecastUpdateHistoryDtos: jssInsertedData, HistoryName: timestamp + promptValue, CellInfo: cellwiseColorCode, TimeStampId: update_timeStampId }),
                    success: function (data) {                                              
                        var year = $("#assignment_year_list").val();
                        ShowForecastResults(year,"edit");

                        $("#timeStamp_ForUpdateData").val('');
                        var chat = $.connection.chatHub;
                        $.connection.hub.start();
                        // Start the connection.
                        $.connection.hub.start().done(function () {
                            chat.server.send('data has been inserted by ', userName);
                        });
                        $("#jspreadsheet").show();                        
                        LoaderHide();
                    }
                });
                jssInsertedData = [];
                newRowCount = 1;

        }

        if (deletedExistingRowIds.length > 0) {
            var year = $("#assignment_year_list").val();
            $.ajax({
                url: `/api/utilities/ExcelDeleteAssignment/`,
                contentType: 'application/json',
                type: 'DELETE',
                async: false,
                dataType: 'json',                
                data: JSON.stringify({ ForecastUpdateHistoryDtos: "", HistoryName: timestamp + promptValue,TimeStampId: update_timeStampId,DeletedRowIds: deletedExistingRowIds,Year:year}),
                success: function (data) {
                    deleteMessage = "Successfully data deleted!";
                    var year = $("#assignment_year_list").val();
                    ShowForecastResults(year,"edit");
                }
            });

            $("#timeStamp_ForUpdateData").val('');
            var chat = $.connection.chatHub;
            $.connection.hub.start();
            // Start the connection.
            $.connection.hub.start().done(function () {
                chat.server.send('data has been deleted by ', userName);
            });
            $("#jspreadsheet").show();
            //$("#head_total").show();
            LoaderHide();
            deletedExistingRowIds = [];
        }
    }

    if (updateMessage == "" && insertMessage == "" && deleteMessage == "") {       
        alert("変更されていないので、保存できません");       
    }
    else{   
        ToastMessageSuccess_Center('保存されました.');
    }    
}

/*
    author: sudipto.
    get all the forecasted year. and build the year dropdown.
*/
function CompareUpdatedData() {
    if (jssUpdatedData.length > 0) {
        $.ajax({
            url: `/api/utilities/GetMatchedRows`,
            contentType: 'application/json',
            type: 'POST',
            async: false,
            dataType: 'json',
            data: JSON.stringify({ ForecastUpdateHistoryDtos: jssUpdatedData, HistoryName: '' }),
            success: function (data) {
                $('#display_matched_rows table tbody').empty();
                $.each(data, function (index, element) {
                    $('#display_matched_rows table tbody').append(`<tr><td>${element.CreatedBy}</td><td>${element.EmployeeName}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`);
                });
            }
        });
    }
}


/*
    author: sudipto.
    import data from excel file.
*/
function ImportCSVFile() {
    var userName = '';


    $.ajax({
        url: `/Registration/GetSession/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            userName = data;
        }
    });

    $.ajax({
        url: `/Forecasts/Index/`,
        contentType: 'application/json',
        type: 'POST',
        async: false,
        dataType: 'json',
        data: JSON.stringify(jssInsertedData),
        success: function (data) {
            var chat = $.connection.chatHub;
            $.connection.hub.start();
            $.connection.hub.start().done(function () {
                chat.server.send('data has been inserted by ', userName);
            });
        }
    });
}

/*
    author: sudipto.
    get all the forecasted year. and build the year dropdown.
*/
function GetAllForecastYears() {
    $.ajax({
        url: `/api/utilities/GetForecatYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $('#assignment_year_list').append(`<option value=''>年度データーの選択</option>`);
            $.each(data, function (index, element) {
                $('#assignment_year_list').append(`<option value='${element.Year}'>${element.Year}</option>`);
            });
        }
    });
}

function CheckForecastYear(){
    var year = $('#select_year_to_import').find(":selected").val();
    if(year!="" && typeof year != "undefined"){
        $('#select_import_year').val(parseInt(year)+1);
    }
}

/*
    author: sudipto.
    validate replicate data. 
*/
function CheckDuplicateYear(){
    var year = $('#select_year_to_import').find(":selected").val();
    if(year!=""){
        $('#duplciateYear').val(parseInt(year)+1);
        $('#replicate_from').val(year);
    }else{
        $('#duplciateYear').val('');
        $('#replicate_from').val('');
    }
}

/*
    author: sudipto.
    replicate the budget data from previous year budget.
*/
function DuplicateForecast(){    
    var insertYear  = $('#duplciateYear').find(":selected").val();
    var copyYear = $('#replicate_from').find(":selected").val();

    if(copyYear!="" && insertYear!=""){
        $("#replicate_from_previous_year").modal("hide");
        $("#loading").css("display", "block");
        $.ajax({
            url: `/api/utilities/DuplicateForecastYear`,
            contentType: 'application/json',
            type: 'GET',
            async: true,
            dataType: 'json',
            data: "copyYear=" + copyYear+"&insertYear="+insertYear,
            success: function (data) {               
                LoaderHide();
                window.location.reload();
                alert("data has been replicated.")
            }
        });
    }else{
        alert("please select From and To year!");
        return false;
    }
}

/*
    author: sudipto.
    budget validation and submit the import file to server.
*/
function validate(){
    var selectedYear = $('#select_import_year').find(":selected").val();
    var import_file = $('#import_file_excel').val();
   
    if(selectedYear =="" || typeof selectedYear === "undefined"){
        alert("please 年度を選択してください!");
        return false;
    }else if(import_file =="" || typeof import_file === "undefined"){
        alert("please select import file!");
        return false;
    }else { return true; }
}

$('#frm_import_year_data').submit(validate);

/*
    author: sudipto.
    date:17July23.
    approve,not approved and deleted rows color code functions.
*/
function SetColorCommonRow(rowNumber,backgroundColor,textColor,requestType){         
    if(requestType != "deleted"){
        $(jss.getCell("A" + (rowNumber))).removeClass('readonly');
    }    
    jss.setStyle("A"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("A"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("A" + (rowNumber))).addClass('readonly');
    }    

    if(requestType != "deleted"){
        $(jss.getCell("B" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("B"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("B"+rowNumber,"color", textColor);    
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("B" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("C" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("C"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("C"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("C" + (rowNumber))).addClass('readonly');
    }


    if(requestType != "deleted"){
        $(jss.getCell("D" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("D"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("D"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("D" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("E" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("E"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("E"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("E" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("F" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("F"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("F"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("F" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("G" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("G"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("G"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("G" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("H" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("H"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("H"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("H" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("I" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("I"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("I"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("I" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("J" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("J"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("J"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("J" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("K" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("K"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("K"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("K" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("L" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("L"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("L"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("L" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("M" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("M"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("M"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("M" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("N" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("N"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("N"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("N" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("O" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("O"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("O"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("O" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("P" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("P"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("P"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("P" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("Q" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("Q"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("Q"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("Q" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("R" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("R"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("R"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("R" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("S" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("S"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("S"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("S" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("T" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("T"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("T"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("T" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("U" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("U"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("U"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("U" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("V" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("V"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("V"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("V" + (rowNumber))).addClass('readonly');
    }

    if(requestType != "deleted"){
        $(jss.getCell("W" + (rowNumber))).removeClass('readonly');
    }  
    jss.setStyle("W"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("W"+rowNumber,"color", textColor);
    if(requestType == "readonly" || requestType == "deleted"){
        $(jss.getCell("W" + (rowNumber))).addClass('readonly');
    }

    $(jss.getCell("X" + (rowNumber))).removeClass('readonly');
    jss.setStyle("X"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("X"+rowNumber,"color", textColor);
    $(jss.getCell("X" + (rowNumber))).addClass('readonly');

    $(jss.getCell("Y" + (rowNumber))).removeClass('readonly');
    jss.setStyle("Y"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("Y"+rowNumber,"color", textColor);
    $(jss.getCell("Y" + (rowNumber))).addClass('readonly');

    $(jss.getCell("Z" + (rowNumber))).removeClass('readonly');
    jss.setStyle("Z"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("Z"+rowNumber,"color", textColor);
    $(jss.getCell("Z" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AA" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AA"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AA"+rowNumber,"color", textColor);
    $(jss.getCell("AA" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AB" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AB"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AB"+rowNumber,"color", textColor);
    $(jss.getCell("AB" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AC" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AC"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AC"+rowNumber,"color", textColor);
    $(jss.getCell("AC" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AD" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AD"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AD"+rowNumber,"color", textColor);
    $(jss.getCell("AD" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AE" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AE"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AE"+rowNumber,"color", textColor);
    $(jss.getCell("AE" + (rowNumber))).addClass('readonly');
    
    $(jss.getCell("AF" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AF"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AF"+rowNumber,"color", textColor);
    $(jss.getCell("AF" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AG" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AG"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AG"+rowNumber,"color", textColor);
    $(jss.getCell("AG" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AH" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AH"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AH"+rowNumber,"color", textColor);
    $(jss.getCell("AH" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AI" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AI"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AI"+rowNumber,"color", textColor);
    $(jss.getCell("AI" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AJ" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AJ"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AJ"+rowNumber,"color", textColor);
    $(jss.getCell("AJ" + (rowNumber))).addClass('readonly');

    $(jss.getCell("AK" + (rowNumber))).removeClass('readonly');
    jss.setStyle("AK"+rowNumber,"background-color", backgroundColor);
    jss.setStyle("AK"+rowNumber,"color", textColor);
    $(jss.getCell("AK" + (rowNumber))).addClass('readonly');
}

/*
    author: sudipto
    after cell change, store that cell information into hidden field.
*/
function CheckIfAlreadyExists(selectedCells,assignmentId){
    var previousChangedCells = $("#changed_cell_with_assignmentid").val();
    var isCellExists = false;    

	if (previousChangedCells != '' && previousChangedCells != null && previousChangedCells != undefined){
        var arrPreviousCells = previousChangedCells.split(",");                                
        $.each(arrPreviousCells, function (nextedIndex, nestedValue){
            var arrNestedCells = nestedValue.split("_");
            if(arrNestedCells[0] == assignmentId && arrNestedCells[1] == selectedCells){
                isCellExists = true;
            }      
        })        
    } 
    return isCellExists;  
}

function StoreChangeCellData(selectedCells,assignmentId){
	var previousChangedCells = $("#changed_cell_with_assignmentid").val();
	var changedCellStored = "";
    var isCellExists = false;    

	if (previousChangedCells != '' && previousChangedCells != null && previousChangedCells != undefined){

        var arrPreviousCells = previousChangedCells.split(",");                                
        $.each(arrPreviousCells, function (nextedIndex, nestedValue){
            var arrNestedCells = nestedValue.split("_");  
            if(arrNestedCells[0] == assignmentId && arrNestedCells[1] == selectedCells){
                isCellExists = true;
            }   

            if(changedCellStored==""){
                changedCellStored = arrNestedCells[0]+"_"+arrNestedCells[1];
            }else{
                var arrChangedCellStored = changedCellStored.split(",");
                var isExists =false;

                $.each(arrChangedCellStored, function (nextedIndex, nestedValue2){
                    var arrNestedValue2 = nestedValue2.split("_");
                    if(arrNestedValue2[0] == arrNestedCells[0] && arrNestedValue2[1] == arrNestedCells[1]){
                        isExists = true;
                    }      
                })   
                if(!isExists){
                    changedCellStored = changedCellStored +","+arrNestedCells[0]+"_"+arrNestedCells[1];
                }                
            } 
        })

        if(!isCellExists){
            if(changedCellStored == ""){
                changedCellStored = assignmentId+"_"+selectedCells;     
            }else{
                changedCellStored = changedCellStored +","+assignmentId+"_"+selectedCells;     
            }
        }
    }
    else{
        changedCellStored = assignmentId+"_"+selectedCells;     
    }	
    $("#changed_cell_with_assignmentid").val(changedCellStored);
}
function SetColorForCells(strBackgroundColor,strTextColor,CellPosition){
	jss.setStyle(CellPosition,"background-color", strBackgroundColor);
	jss.setStyle(CellPosition,"color", strTextColor);
}
function SetColorForCostsCells(strBackgroundColor,strTextColor,CellPosition){
	$(jss.getCell(CellPosition)).removeClass('readonly');
    jss.setStyle(CellPosition,"background-color", strBackgroundColor);
	jss.setStyle(CellPosition,"color", strTextColor);
    $(jss.getCell(CellPosition)).addClass('readonly');
}