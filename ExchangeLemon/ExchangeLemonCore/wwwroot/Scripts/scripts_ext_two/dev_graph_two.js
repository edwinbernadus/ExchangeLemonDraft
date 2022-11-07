function one() {
    console.log("one");
    two();
    return "one";
}

function logTest() {
    console.log('log test new graph')
}


function logTestTwo(array) {
    array.forEach(element => {
        let items = chart.data.datasets[0].data;
        items.shift();
        items.push(element);
    });
    // chart.update();
}

function two() {
    let total = chart.data.datasets[0].data.length;
    console.log("two", total);

    let items = chart.data.datasets[0].data;
    items.shift();
    items.push(50);

    let labels = chart.data.labels;
    // items.shift();
    // items.push('aa');

    chart.update();
    return "two";
}

// var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
var config = {
    type: 'line',
    data: {
        // labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
        labels: ['-1', '-1', '-1', '-1', '-1', '-1', '-1'],
        datasets: [{
                label: '',
                backgroundColor: window.chartColors.red,
                borderColor: window.chartColors.red,
                data: [
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor()
                ],
                fill: false,
            },
            //               {
            //label: 'My Second dataset',
            //fill: false,
            //backgroundColor: window.chartColors.blue,
            //borderColor: window.chartColors.blue,
            //data: [
            //	randomScalingFactor(),
            //	randomScalingFactor(),
            //	randomScalingFactor(),
            //	randomScalingFactor(),
            //	randomScalingFactor(),
            //	randomScalingFactor(),
            //	randomScalingFactor()
            //],
            //               }
        ]
    },
    options: {
        legend: {
            display: false
        },
        responsive: true,
        title: {
            display: false,
            text: 'Chart.js Line Chart z123'
        },
        tooltips: {
            mode: 'index',
            intersect: false,
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        scales: {
            xAxes: [{
                gridLines: {
                    // color: "#CCC" 
                },
                display: true,
                scaleLabel: {
                    display: false,
                    labelString: 'Month'
                },
                ticks: {
                    fontColor: "#CCC", // this here
                },
            }],
            yAxes: [{
                gridLines: {
                    color: "#CCC"
                },
                display: true,
                scaleLabel: {
                    display: false,
                    labelString: 'Value'
                },
                ticks: {
                    fontColor: "#CCC", // this here
                },
            }]
        }
    }
};

let chart = null;
window.onload = function () {
    // var ctx = document.getElementById('canvas').getContext('2d');
    // chart = new Chart(ctx, config);
    // window.myLine = chart;
};