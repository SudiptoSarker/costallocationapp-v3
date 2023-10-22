$(document).ready(function () {
    var totalCostChartData = [];
    var monthlyCostChartData = [];
    var totalHeadCountChartData = [];
    var monthlyBudgetHeadCountData = [];
    var monthlyForecastHeadCountData = [];
    var monthlyHeadCountData = [];
    var monthlyBudgetData = [];
    var monthlyForecastData = [];
    var monthlyActualData = [];
    var year = 9999;
    var totalCost = 0;
    var totalHeadCount = 0;

    // API call for chart data
    $.ajax({
        url: `/api/dash/GetTotalCost/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
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

    // API call for head count
    $.ajax({
        url: `/api/dash/GetHeadCount/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            totalHeadCountChartData = data["ChartData"];
            monthlyForecastHeadCountData = data["monthlyHeadcount"];
            totalHeadCount = data["totalHeadcount"];
        }
    });

    // API call for head count
    $.ajax({
        url: `/api/dash/GetBudgetHeadCount/`,
        contentType: 'application/json',
        type: 'GET',
        async: false,
        dataType: 'json',
        success: function (data) {
            monthlyBudgetHeadCountData = data["monthlyBudgetHeadcount"];
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
        var data = google.visualization.arrayToDataTable(totalHeadCountChartData);

        var options = {
            chart: {
                title: 'Total Head Count (' + year + ')',
                subtitle: 'Total head count: ' + totalHeadCount,
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
        //prepare the chart data with month sequance 10(Oct)-1(Jan)
        let i = 10;
        do {
            monthlyCostChartData.push([i + "月", monthlyForecastData[i], monthlyBudgetData[i], monthlyActualData[i]]);
            i = (i == 12) ? 1 : i+1;
        } while (i !== 10);

        console.log(monthlyCostChartData);
        var data = google.visualization.arrayToDataTable(monthlyCostChartData);

        var options = {
            chart: {
                title: 'Monthly cost chart (' + year + ')',
            },
            bars: 'vertical',
            vAxis: { format: 'short' },
            height: 400,
            colors: ['#1e81b0', '#42f54b', '#f56642']
        };

        var chart = new google.charts.Bar(document.getElementById('monthly_cost_chart'));
        chart.draw(data, google.charts.Bar.convertOptions(options));
    }

    //Function for render monthly headcount bar chart
    function MonthlyHeadCountChart() {
        monthlyHeadCountData.push(['Months', 'Forecasted', 'Budget']);

        let i = 10;
        do {
            monthlyHeadCountData.push([i + "月", monthlyForecastHeadCountData[i], monthlyBudgetHeadCountData[i]]);
            i = (i == 12) ? 1 : i + 1;
        } while (i !== 10);

        var data = google.visualization.arrayToDataTable(monthlyHeadCountData);

        var options = {
            chart: {
                title: 'Monthly Head Count (' + year + ')',
            },
            bars: 'vertical',
            vAxis: { format: 'decimal' },
            height: 400,
            colors: ['#17570a', '#42f5b3']
        };

        var chart = new google.charts.Bar(document.getElementById('monthly_head_count_chart'));
        chart.draw(data, new google.charts.Bar.convertOptions(options));
    }

});