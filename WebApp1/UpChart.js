var Logdata = [];
UpdateChart = function () {
    $.get("./api/MonitoringData.ashx?nametable=monitoringdata", function (arrayOfValues) {
        window.myLiveChart.data.datasets[0].data = arrayOfValues[0];
        window.myLiveChart.data.datasets[1].data = arrayOfValues[1];
        window.myLiveChart.data.labels = arrayOfValues[2];
        var copyWidth = (myLiveChart.scales['y-axis-0'].width + 10) * arrayOfValues[1].length;
        $('.chartAreaWrapper2').width(copyWidth);
        window.myLiveChart.update();
    }, "json");
}
UpdateLog = function () {
    $.get("./api/MonitoringData.ashx?nametable=monitoringlog", function (res) {
        var LoadingLog = document.getElementById('LogTxt');
        if (Logdata.length == 0) {
            Logdata.push(res);
            LoadingLog.innerHTML = Logdata;
        }

        if (Logdata[Logdata.length - 1] == res) {}
        else {
            if (Logdata.length == 1000) {
                Logdata.shift();
            }
            Logdata.push(res);
            LoadingLog.innerHTML = Logdata.join("");
        }
    }, "json");
}

Chart.defaults.global.defaultFontSize = 14;
Chart.defaults.global.defaultFontColor = '#c0c0c0';
initChart = function () {
    var config = {
        type: 'bar',
        data: {
            labels: [],
            datasets: [
                {
                    label: 'CPU (%)',
                    backgroundColor: 'rgb(0,255,0)',
                    borderColor: 'rgb(0,255,0)',
                    data: [],
                },
                {
                    label: 'RAM (%)',
                    backgroundColor: 'rgb(255, 99, 132)',
                    borderColor: 'rgb(255, 99, 132)',
                    data: [],
                }]
        },
        options: {
            maintainAspectRatio: false,
            responsive: true,
            title: {
                display: true,
                text: 'Загрузка CPU и RAM',
                backgroundColor: 'rgb(224, 222, 235)',
            },
            tooltips: {
                mode: 'index',
                intersect: false,
            },
            hover: {
                mode: 'nearest',
                intersect: false,
            },
            scales: {
                xAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: 'Время'
                    },
                }],
                yAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: '%'
                    },

                    ticks: {
                        min: 0,
                        max: 100,
                    }
                }]
            }
        }
    };
    var ctx = document.getElementById('cpu_chart').getContext('2d');
    window.myLiveChart = new Chart(ctx, config);
    UpdateChart();
}

window.onload = function () {
    initChart();
    UpdateChart();
    UpdateLog();
};

setInterval(UpdateChart, 3000);
setInterval(UpdateLog, 300);
