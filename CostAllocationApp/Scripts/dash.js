$(document).ready(function () {
    var totalCostChartData = [];
    var monthlyCostChartData = [];
    var totalHeadCountChartData = [];
    var monthlyBudgetHeadCountData = [];
    var monthlyForecastHeadCountData = [];
    var monthlyBudgetData = [];
    var monthlyForecastData = [];
    var monthlyActualData = [];
    var year = 9999;
    var totalCost = 0;

    // API call for chart data
    $.ajax({
        url: `/api/dash/GetTotalCost/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            console.log(data);
            totalCostChartData = data["chartData"];
            year = data["Year"];
            totalCost = data["totalCost"];
            monthlyForecastData = data["forecastCost"];
            monthlyActualData = data["actualCost"];
        }
    });


    // API call for budget data
    $.ajax({
        url: `/api/dash/GetBudgetCost/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            monthlyBudgetData = data;
        }
    });

    google.charts.load('current', { 'packages': ['corechart', 'bar'] });
    google.charts.setOnLoadCallback(TotalCostChart);
    google.charts.setOnLoadCallback(TotalHeadCountChart);
    google.charts.setOnLoadCallback(MonthlyCostChart);
    google.charts.setOnLoadCallback(MonthlyHeadCountChart);

    //Function for render total cost bar chart
    function TotalCostChart() {
        // Data for the bar chart
        var data = google.visualization.arrayToDataTable(totalCostChartData);

        var options = {
            chart: {
                title: 'Total Cost (' + year + ')',
                subtitle: 'Total cost: ' + totalCost,
            },
            bars: 'vertical',
            vAxis: {
                format: 'short'
            },
            height: 400,
            colors: '#1e81b0',
            isStacked: true,

        };

        var chart = new google.charts.Bar(document.getElementById('total_cost_chart'));
        chart.draw(data, google.charts.Bar.convertOptions(options));
    }

    //Function for render total head count bar chart
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

    //Function for render monthly cost bar chart
    function MonthlyCostChart() {
        monthlyCostChartData.push(['Months', 'Forecasted', 'Budget', 'Actual']);
        for (let i = 1; i <= 12; i++)
        {
            monthlyCostChartData.push([i + "月", monthlyForecastData[i], monthlyBudgetData[i], monthlyActualData[i]]);
        }
        console.log(monthlyCostChartData);
        var data = google.visualization.arrayToDataTable(monthlyCostChartData);

        var options = {
            chart: {
                title: 'Monthly cost chart (' + year + ')',
            },
            bars: 'vertical',
            vAxis: { format: 'short' },
            height: 400,
            colors: ['#f5da42', '#42f54b', '#f56642']
        };

        var chart = new google.charts.Bar(document.getElementById('monthly_cost_chart'));
        chart.draw(data, google.charts.Bar.convertOptions(options));
    }

    //Function for render monthly headcount bar chart
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
            vAxis: { format: 'decimal' },
            height: 400,
            colors: ['#17570a', '#42f5b3']
        };

        var chart = new google.charts.Bar(document.getElementById('monthly_head_count_chart'));
        chart.draw(data, new google.charts.Bar.convertOptions(options));
    }



    //$.ajax({
    //    url: `/api/utilities/GetTotal?companiIds=1`,
    //    contentType: 'application/json',
    //    type: 'GET',
    //    async: false,
    //    dataType: 'json',
    //    success: function (data) {
    //        totalList = data;
    //        differenceTable = structuredClone(totalList);
    //        //differenceTable = totalList.map(function (arr) {
    //        //    return arr.slice();
    //        //});
    //        console.log(totalList);
    //    }
    //});

    //$.ajax({
    //    url: `/api/Companies/`,
    //    contentType: 'application/json',
    //    type: 'GET',
    //    async: false,
    //    dataType: 'json',
    //    success: function (data) {
    //        console.log(data);
    //    }
    //});

    //$.ajax({
    //    url: `/api/Departments`,
    //    contentType: 'application/json',
    //    type: 'GET',
    //    async: false,
    //    dataType: 'json',
    //    success: function (data) {

    //        console.log(data);
    //    }
    //});
});