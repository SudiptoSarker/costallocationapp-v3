function LoaderShow() {
    $('#sukey_qa_table_header').hide();
    $("#loading").css("display", "block");
}
function LoaderHide() {
    $('#sukey_qa_table_header').show();
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

    $('#sukey_qa_display').on('click', function () {
        var year = $('#assignment_year').val();
        LoaderShow();
        setTimeout(function () {
            $.ajax({
                url: `/api/utilities/GetSukeyWithQA?year=${year}`,
                contentType: 'application/json',
                type: 'GET',
                async: false,
                dataType: 'json',
                success: function (data) {
                    LoaderHide();
                    $('#sukey_qa_table tbody').empty();
                    $.each(data, function (index, value) {
                        $('#sukey_qa_table tbody').append(`<tr>
                        <td>${value.DepartmentName}</td>
                        <td>${value.OctCost[0].toLocaleString('en')}<br/>${value.OctCost[1].toLocaleString('en')}<br/>${value.OctCost[2].toLocaleString('en')}</td>
                        <td>${value.NovCost[0].toLocaleString('en')}<br/>${value.NovCost[1].toLocaleString('en')}<br/>${value.NovCost[2].toLocaleString('en')}</td>
                        <td>${value.DecCost[0].toLocaleString('en')}<br/>${value.DecCost[1].toLocaleString('en')}<br/>${value.DecCost[2].toLocaleString('en')}</td>
                        <td>${value.JanCost[0].toLocaleString('en')}<br/>${value.JanCost[1].toLocaleString('en')}<br/>${value.JanCost[2].toLocaleString('en')}</td>
                        <td>${value.FebCost[0].toLocaleString('en')}<br/>${value.FebCost[1].toLocaleString('en')}<br/>${value.FebCost[2].toLocaleString('en')}</td>
                        <td>${value.MarCost[0].toLocaleString('en')}<br/>${value.MarCost[1].toLocaleString('en')}<br/>${value.MarCost[2].toLocaleString('en')}</td>
                        <td>${value.AprCost[0].toLocaleString('en')}<br/>${value.AprCost[1].toLocaleString('en')}<br/>${value.AprCost[2].toLocaleString('en')}</td>
                        <td>${value.MayCost[0].toLocaleString('en')}<br/>${value.MayCost[1].toLocaleString('en')}<br/>${value.MayCost[2].toLocaleString('en')}</td>
                        <td>${value.JunCost[0].toLocaleString('en')}<br/>${value.JunCost[1].toLocaleString('en')}<br/>${value.JunCost[2].toLocaleString('en')}</td>
                        <td>${value.JulCost[0].toLocaleString('en')}<br/>${value.JulCost[1].toLocaleString('en')}<br/>${value.JulCost[2].toLocaleString('en')}</td>
                        <td>${value.AugCost[0].toLocaleString('en')}<br/>${value.AugCost[1].toLocaleString('en')}<br/>${value.AugCost[2].toLocaleString('en')}</td>
                        </tr>`);
                    });
                }
            });
        }, 3000);
    });
});