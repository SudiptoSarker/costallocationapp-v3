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
                    $.each(data, function (index, value) {
                        $('#sukey_table tbody').append(`<tr><td>${value.DepartmentName}</td><td>${value.OctCost == 0 ? "" : value.OctCost.toLocaleString('en')}</td><td>${value.NovCost==0 ? "" : value.NovCost.toLocaleString('en')}</td><td>${value.DecCost == 0 ? "" : value.DecCost.toLocaleString('en')}</td><td>${value.JanCost == 0 ? "" : value.JanCost.toLocaleString('en')}</td><td>${value.FebCost == 0 ? "" : value.FebCost.toLocaleString('en')}</td><td>${value.MarCost == 0 ? "" : value.MarCost.toLocaleString('en')}</td><td>${value.AprCost == 0 ? "" : value.AprCost.toLocaleString('en')}</td><td>${value.MayCost == 0 ? "" : value.MayCost.toLocaleString('en')}</td><td>${value.JunCost == 0 ? "" : value.JunCost.toLocaleString('en')}</td><td>${value.JulCost == 0 ? "" : value.JulCost.toLocaleString('en')}</td><td>${value.AugCost == 0 ? "" : value.AugCost.toLocaleString('en')}</td><td>${value.SepCost==0?"":value.SepCost.toLocaleString('en')}</td></tr>`);
                    });
                }
            });
        }, 3000);
    });
});