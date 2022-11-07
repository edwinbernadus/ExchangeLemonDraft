// google.charts.load("current", { packages: ["corechart"] });
// google.charts.setOnLoadCallback(initGraph);
//let sourceItems: ItemDate[] = [];
//let items = [];
function submitGraphData(inputItem) {
    drawChart(inputItem);
    populateGraphTwo(inputItem);
}
function populateGraphTwo(inputItem) {
    var item2 = inputItem;
    var item3 = item2.map(function (x) { return x.Value; });
    var item4 = item3.map(function (x) { return parseFloat(x); });
    logTestTwo(item4);
    logTest();
    // let inputs = JSON.parse(inputItem);
}
function initGraph() {
    //axios.get("/api/ReportGraph/btc_usd").then(response => {
    //  //console.log('response', response)
    //  let data: ItemDate[] = response.data;
    //  console.log("[data-graph] init", data);
    //  // drawChart(data);
    //  populateGraphTwo(data);
    //});
}
function drawChart(sourceItems) {
    console.log("[data-graph] Signal-ListenGraph-submitGraphData", sourceItems);
    var items = [];
    var x1 = ["Data", "Btc"];
    items.push(x1);
    sourceItems.forEach(function (element) {
        var t1 = element["Value"];
        var t2 = parseFloat(t1);
        items.push(["", t2]);
    });
    var total = items.length;
    console.log("total items", total);
    var data = google.visualization.arrayToDataTable(items);
    var options = {
        title: "Btc Title",
        curveType: "function"
        // legend: { position: 'bottom' }
    };
    var chart = new google.visualization.LineChart(document.getElementById("curve_chart"));
    chart.draw(data, options);
    // ------------------------------
}
//# sourceMappingURL=detail_graph.js.map