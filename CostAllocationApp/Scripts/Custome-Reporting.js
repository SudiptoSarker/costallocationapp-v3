/*------------------------------------Reporting-----------------------------------------*/

$(document).ready(function () {
    // ------------------Pie-Chart---------------------------//
    google.charts.load('current', { 'packages': ['corechart', 'bar'] });
    google.charts.setOnLoadCallback(TotalCostChart);
    google.charts.setOnLoadCallback(TotalHeadCountChart);
    google.charts.setOnLoadCallback(MonthlyCostChart);
    google.charts.setOnLoadCallback(MonthlyHeadCountChart);

    function TotalCostChart() {
        // Data for the bar chart
        var data = google.visualization.arrayToDataTable([
            ['Departments', 'Cost'],
            ['NewBlend', 650],
            ['導入', 550],
            ['移行', 500],
            ['自治体', 250],
            ['運用保守', 1100],
            ['その他', 2000],
        ]);

        var options = {
            chart: {
                title: 'Total Cost',
                subtitle: 'Total cost: 2030'
            },
            bars: 'vertical',
            vAxis: {format: 'decimal'},
            height: 400,
            colors: '#1e81b0',
        };

        var chart = new google.charts.Bar(document.getElementById('total_cost_chart'));
        chart.draw(data, google.charts.Bar.convertOptions(options));
    }

    function TotalHeadCountChart() {
        // Data for the bar chart
        var data = google.visualization.arrayToDataTable([
            ['Departments', 'Head Count'],
            ['NewBlend', 12],
            ['導入', 3],
            ['移行', 7],
            ['自治体', 21],
            ['運用保守', 1],
            ['その他', 33],
        ]);

        var options = {
            chart: {
                title: 'Total Head Count',
                subtitle: 'Total head count: 2030',
            },
            bars: 'vertical',
            // vAxis: { format: 'decimal'},
            height: 400,
            colors: '#42f5b3'
        };

        var chart = new google.charts.Bar(document.getElementById('total_head_count_chart'));
        chart.draw(data, google.charts.Bar.convertOptions(options));
    }

    function MonthlyCostChart() {
        var data = google.visualization.arrayToDataTable([
            ['Months', 'Forecasted', 'Budget', 'Actual'],
            ['10月', 0, 1000000, 1253000],
            ['11月', 0, 1100000, 1253000],
            ['12月', 0, 1200000, 1253000],
            ['1月', 0, 1300000, 1253000],
            ['2月', 0, 1400000, 1253000],
            ['3月', 0, 1500000, 1253000],
            ['4月', 0, 1600000, 1253000],
            ['5月', 0, 1700000, 1253000],
            ['6月', 0, 1800000, 1253000],
            ['7月', 1253000, 1900000, 0],
            ['8月', 1253000, 2000000, 0],
            ['9月', 1253000, 2100000, 0],
        ]);

        var options = {
            chart: {
                title: 'Monthly cost chart'
            },
            bars: 'vertical',
            vAxis: {format: 'decimal'},
            height: 400,
            colors: ['#f5da42', '#42f54b', '#f56642']
        };

        var chart = new google.charts.Bar(document.getElementById('monthly_cost_chart'));
        chart.draw(data, google.charts.Bar.convertOptions(options));
    }   

    function MonthlyHeadCountChart() {
        var data = google.visualization.arrayToDataTable([
            ['Months', 'Forecasted', 'Budget'],
            ['10月', 65, 80],
            ['11月', 72, 80],
            ['12月', 70, 80],
            ['1月', 60, 80],
            ['2月', 59, 80],
            ['3月', 73, 80],
            ['4月', 68, 80],
            ['5月', 65, 80],
            ['6月', 77, 80],
            ['7月', 66, 80],
            ['8月', 55, 80],
            ['9月', 59, 80],
        ]);

        var options = {
            chart: {
                title: 'Monthly Head Count'
            },
            bars: 'vertical',
            vAxis: {format: 'decimal'},
            height: 400,
            colors: ['#17570a', '#42f5b3']
        };

        var chart = new google.charts.Bar(document.getElementById('monthly_head_count_chart'));
        chart.draw(data, new google.charts.Bar.convertOptions(options));
    }
});