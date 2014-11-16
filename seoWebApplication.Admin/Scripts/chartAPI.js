
//this function is used by UI to trigger chart display
$(document).ready(function () {
    //process data on button click
    $(".chartButton").click(function (sender) {

        //read button attributes
        var entity = $(this).attr("data-entity");
        var targetDiv = $(this).attr("data-targetDiv");
        var params = $(this).attr("data-params") + "&start=" + $('#start').val() + "&end=" + $('#end').val();

        //call encapsulated function
        getEntityData(entity, params, function (data, status, response) {
            if (status === "success") {
                renderChart(data, targetDiv);
            }
            else if (status === "error") {
                //process the error here if needed

                alert('An error occurred: ' + response);
            }
            else if (status === "complete") {
                //attach your load completed events here if needed

                //alert('Data loaded!');
            }
        });
    });
});

//Gets data for the entity
//This function is encapsulated 
function getEntityData(entityName, params, callbackFn) {
    if (typeof callbackFn != 'function') {
        alert('Incorrect callback function attached!'); return;
    }

    //make sure correct path is set
    var root = location.protocol + "//" + location.host;

    $.getJSON(root + '/API/' + entityName + "?" + params, null)
    .success(function (data, status, response) {
        //create object from json
        var resultObject = JSON.parse(data);
        callbackFn(resultObject, status, response);
    })
    .error(function (e, status, response) {
        callbackFn(e, status, response);
    })
    .complete(function (e, status, response) {
        callbackFn(e, "complete", response);
    });
}

//Renders chart
function renderChart(data, renderTo) {
    var chart1 = new Highcharts.Chart({
        chart: {
            renderTo: renderTo,
            defaultSeriesType: data.ChartConfig.defaultSeriesType,
            width: data.ChartConfig.width,
            height: data.ChartConfig.height,
        },
        title: {
            text: data.ChartConfig.chartTitle,
        },
        tooltip: {
         pointFormat: data.ChartConfig.tooltipPointFormat,
        },
        plotOptions: {
                pie: {
                    allowPointSelect: data.ChartConfig.pie_allowPointSelect,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: data.ChartConfig.pie_dataLabelsEnabled,
                    },
                    showInLegend: data.ChartConfig.pie_showInLegend,
                },
            },
        xAxis: {
            title: {
                text: 'date'
            },
            type: 'datetime',
        },
        yAxis: {
            title: {
                text: 'value'
            }
        },
    });

    $.each(data.Series, function (index, item) {
        //add series
        chart1.addSeries({
            name: item.name,
            dashStyle: item.DashStyle,
            color: item.Color,
            data: []
        }, false);

        //add data points
        $.each(item.Points, function (index, pointData) {
            chart1.series[chart1.series.length - 1].addPoint([
                Date.parse(new Date(parseInt(pointData.Date.substr(6)))),//format date
                pointData.Value,
            ], false);
        });
    });

    chart1.redraw();
}

