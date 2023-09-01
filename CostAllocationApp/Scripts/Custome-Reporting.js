/*------------------------------------Reporting-----------------------------------------*/

$(document).ready(function () {
    // ------------------Pie-Chart---------------------------//
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(CostManagement);
    google.charts.setOnLoadCallback(Employees);
    google.charts.setOnLoadCallback(Grades);

    // Draw the chart and set the chart values
    function CostManagement() {
        var data = google.visualization.arrayToDataTable([
            ['Task', 'Number of Modules'],
            ['区分', 5],
            ['部署', 10],
            ['会社', 20],
            ['Fiscal year calculated', 6],
            ['グレード', 18]
        ]);

        // Optional; add a title and set the width and height of the chart
        var options = { 'title': '経費管理システム', 'width': 550, 'height': 400 };

        // Display the chart inside the <div> element with id="piechart"
        var chart = new google.visualization.PieChart(document.getElementById('cost_management'));
        chart.draw(data, options);
    }

    function Employees() {
        var data = google.visualization.arrayToDataTable([
            ['Task', 'Employees'],
            ['要員', 100],
            ['Employee assign to Section', 85],
            ['Employee assign to Department', 90],
            ['Employees with company', 95],
            ['要員のグレード', 50]
        ]);

        // Optional; add a title and set the width and height of the chart
        var options = { 'title': '要員', 'width': 550, 'height': 400 };

        // Display the chart inside the <div> element with id="piechart"
        var chart = new google.visualization.PieChart(document.getElementById('employees'));
        chart.draw(data, options);
    }
    function Grades() {
        var data = google.visualization.arrayToDataTable([
            ['Task', 'Grades'],
            ['Number Of Grades', 55],
            ['Number of MW companies', 10],
            ['他社', 45]
        ]);

        // Optional; add a title and set the width and height of the chart
        var options = { 'title': 'グレードシステム', 'width': 550, 'height': 400 };

        // Display the chart inside the <div> element with id="piechart"
        var chart = new google.visualization.PieChart(document.getElementById('grade_system'));
        chart.draw(data, options);
    }




    //------------------------------- Bar-Chart----------------------//
    var xValues = ["Year: 2023 ", "Year: 2022", "Year: 2021", "Year: 2020", "Year:2019"];
    var yValues = [65, 55, 50, 25, 11];
    var barColors = ["red", "green", "blue", "orange", "brown"];

    new Chart("myChart", {
        type: "bar",
        data: {
            labels: xValues,
            datasets: [{
                backgroundColor: barColors,
                data: yValues
            }]
        },
        options: {
            legend: { display: false },
            title: {
                display: true,
                text: "Forecast through the year"
            }
        }
    });
});