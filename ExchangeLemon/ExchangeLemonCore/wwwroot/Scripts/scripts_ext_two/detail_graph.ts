declare var signalR: any;
//declare let items: [any, any][]
//declare let sourceItems: [number, number][]
declare var axios: any;
declare let logTest: any;

declare let logTestTwo: any;
declare var google : any;

// google.charts.load("current", { packages: ["corechart"] });
// google.charts.setOnLoadCallback(initGraph);

//let sourceItems: ItemDate[] = [];
//let items = [];

function submitGraphData(inputItem: ItemDate[]) {
  drawChart(inputItem);
  populateGraphTwo(inputItem);
}

function populateGraphTwo(inputItem: ItemDate[]) {
  let item2: any[] = inputItem;
  let item3 = item2.map(x => x.Value);
  let item4 = item3.map(x => parseFloat(x));
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

function drawChart(sourceItems: ItemDate[]) {
  console.log("[data-graph] Signal-ListenGraph-submitGraphData", sourceItems);

  let items: [string, any][] = [];
  let x1: [string, string] = ["Data", "Btc"];
  items.push(x1);
  sourceItems.forEach(element => {
    let t1: any = element["Value"];
    let t2 = parseFloat(t1);
    items.push(["", t2]);
  });

  let total = items.length;
  console.log("total items", total);

  let data = google.visualization.arrayToDataTable(items);

  let options = {
    title: "Btc Title",
    curveType: "function"
    // legend: { position: 'bottom' }
  };

  let chart = new google.visualization.LineChart(
    document.getElementById("curve_chart")
  );
  chart.draw(data, options);

  // ------------------------------
}
