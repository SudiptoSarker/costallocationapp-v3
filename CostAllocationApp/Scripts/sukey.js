function LoaderShow() {
    $('#sukey_table_header').hide();
    $("#loading").css("display", "block");
}
function LoaderHide() {
    $('#sukey_table_header').show();
    $("#loading").css("display", "none");
}

$(document).ready(function () {
    $('#sukey_table_header').hide();

    $.ajax({
        url: `/api/utilities/GetForecatYear`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            $('#assignment_year').empty();
            $('#assignment_year').append(`<option value=''>年度データーの選択</option>`);
            $.each(data, function (index, element) {
                $('#assignment_year').append(`<option value='${element.Year}'>${element.Year}</option>`);
            });
        }
    });

    $('#sukey_display').on('click', function () {
        var year = $('#assignment_year').val();
        LoaderShow();
        setTimeout(function () {
            $.ajax({
                url: `/api/utilities/GetSukeyData?year=${year}`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                success: function (data) {
                    LoaderHide();
                    $('#sukey_table tbody').empty();
                    $.each(data, function (index,value) {
                        $('#sukey_table tbody').append(`<tr><td>${value.DepartmentName}</td><td>${value.OctCost.toFixed(2)}</td><td>${value.NovCost.toFixed(2)}</td><td>${value.DecCost.toFixed(2)}</td><td>${value.JanCost.toFixed(2)}</td><td>${value.FebCost.toFixed(2)}</td><td>${value.MarCost.toFixed(2)}</td><td>${value.AprCost.toFixed(2)}</td><td>${value.MayCost.toFixed(2)}</td><td>${value.JunCost.toFixed(2)}</td><td>${value.JulCost.toFixed(2)}</td><td>${value.AugCost.toFixed(2)}</td><td>${value.SepCost.toFixed(2)}</td></tr>`);
                    });
                }
            });
        }, 3000);
    });
});