// declare var isLogin: boolean
// declare var app_vm: any
declare var Vue: any;
declare var toastr: any;

function testHello() {
  console.log("start loading page");
}

let url = getCurrentBaseUrl();
console.log("baseUrl");
console.log(url);

function getCurrentBaseUrl() {
  var full =
    location.protocol +
    "//" +
    location.hostname +
    (location.port ? ":" + location.port : "");
  return full;
}
function cancelOrder(id) {
  toastr.warning("Sending Transaction");
  // toastr.success('Transaction Submitted')

  let link2 = url + "/api/cancel/" + id;

  let vm = app_vm;
  var formData = new FormData();

  //Vue.http.headers.common['Content-Type'] = 'application/json'
  Vue.http.headers.common["Content-Type"] =
    "application/x-www-form-urlencoded;charset=UTF-8";

  Vue.http.headers.common["Access-Control-Allow-Origin"] = "*";

  let v = this;

  Vue.http
    .post(link2, formData, {
      emulatedJson: true
    })
    .then(function(response) {
      let temp = response.data;
      // v.reloadAll()
      v.reloadAfterCancelOrder();
      // toastr.info('Sending Transaction')
      toastr.success("Transaction Submitted");
    });
}

function orderGeneric(mode, inputRate, inputAmount) {
  console.log("orderGeneric-" + mode + "-" + inputRate + "-" + inputAmount);

  toastr.warning("Sending Transaction");
  // toastr.success('Transaction Submitted')

  let link2 = url + "/api/OrderItemMain";
  let vm = app_vm;

  console.log("order generic", "url transaction", link2);

  Vue.http.headers.common["Content-Type"] =
    "application/x-www-form-urlencoded;charset=UTF-8";

  Vue.http.headers.common["Access-Control-Allow-Origin"] = "*";

  // let username = "";

  let current_pair = vm.currency_pair;

  var formData = {
    rate: inputRate,
    amount: inputAmount,
    mode: mode,
    current_pair: current_pair
  };

  console.log("current_pair: " + current_pair);

  let inputTwo = JSON.stringify(formData);
  let v = this;

  Vue.http
    .post(link2, inputTwo, {
      emulatedJson: true
    })
    .then(function(response) {
      console.log("debug3","response",response);
      // v.reloadAll()
      // v.reloadAllSignal()
      // toastr.info('Sending Transaction')
      toastr.success("Transaction Submitted");
      // vm.inputGenericRate = -1
      // vm.inputAmount = -2
    })
    .catch(function(response) {
      console.log("z1 1");
      // console.log('error')
      // console.log(response)
      // console.log(response.body)
      // console.log(response.body.ExceptionMessage)

      console.log("z1 error 123");
      console.log("z1 error", response);

      // let message = response.data.error
      let message = response.data;
      let errorMsg = response.statusText + " - " + message;

      toastr.error(errorMsg);
      vm.topUpVisible = true;
    });
}

function loadOrderAll(vm, pair) {
  let link2 = url + "/api/OrderItemMain/" + pair;

  console.log("start order all");

  Vue.http.get(link2).then(function(response) {
    let inputs: MvDetailSpotMarketItemContent = response.data;
    console.log("loadOrderAll", inputs);

    // let itemNew = inputs.Items
    // let last = inputs.LastSequenceHistory
    // console.log('last1', inputs.LastSequenceHistory)
    // console.log('last2', last)

    // let items = vm.orderAll

    // console.log("order item main total", items.length);
    // items.splice(0, items.length)

    // for (let i = 0; i < itemNew.length; i++) {
    //     let item = itemNew[i]
    //     vm.orderAll.push(item)
    //     // console.log(item)
    //     // console.log(vm.orderSells.length)

    // }
    // vm.lastOrderListSequence = last

    populateOrderList(inputs, vm);
    vm.syncOrderList();
  });
}

function loadOrderAllPush(vm, inputs: MvDetailSpotMarketItemContent) {
  console.log("loadOrderAll", inputs);
  populateOrderList(inputs, vm);
  vm.syncOrderList();
}

function populateOrderList(inputs: MvDetailSpotMarketItemContent, vm: any) {
  let itemNew = inputs.Items;
  let last = inputs.LastSequenceHistory;
  console.log("last1", inputs.LastSequenceHistory);
  console.log("last2", last);

  let items = vm.orderAll;

  console.log("order item main total", items.length);
  items.splice(0, items.length);

  for (let i = 0; i < itemNew.length; i++) {
    let item = itemNew[i];
    vm.orderAll.push(item);
    // console.log(item)
    // console.log(vm.orderSells.length)
  }
  vm.lastOrderListSequence = last;
  console.log("end order all - " + itemNew.length);
}

function loadOpenOrder(vm, pair) {
  let link2 = url + "/api/openOrder/" + pair;

  // A = [];
  // while(A.length > 0) {
  //     A.pop();
  // }

  Vue.http.get(link2).then(function(response) {
    let temp = response.data;
    // console.log(temp)
    let itemNew = temp;

    let items = vm.openOrder;
    items.splice(0, items.length);

    for (let i = 0; i < itemNew.length; i++) {
      let item = itemNew[i];
      vm.openOrder.push(item);
      // console.log(item)
      // console.log(vm.orderSells.length)
    }
  });
}

function loadMarketHistoryTransaction(vm, pair) {
  let link2 = url + "/api/marketHistoryTransaction/" + pair;

  Vue.http.get(link2).then(function(response) {
    let itemNew: MvMarketHistoryTransaction[] = response.data;

    let items2 = vm.marketHistoryTransactions;
    items2.splice(0, items2.length);

    for (let i = 0; i < itemNew.length; i++) {
      let item = itemNew[i];
      vm.marketHistoryTransactions.push(item);
    }
  });
}

function loadHistoryTransaction(vm, pair) {
  let link2 = url + "/api/historyTransaction/" + pair;

  Vue.http.get(link2).then(function(response) {
    let temp = response.data;
    let itemNew: AccountTransaction[] = temp;

    let items = vm.historyTransactions;
    items.splice(0, items.length);

    for (let i = 0; i < itemNew.length; i++) {
      let item = itemNew[i];
      vm.historyTransactions.push(item);
    }
  });
}

// function loadOrderListTransaction(vm, pair: string, sequence: number) {

//     let link2 = url + "/api/orderList/" + pair + "/" + sequence
//     console.log('[loadOrderListTransaction] start', link2)
//     Vue.http.get(link2).then(function (response) {
//         let items: MvOrderHistory[] = response.data;
//         console.log('[loadOrderListTransaction] end', items)
//         let total = items.length
//         if (total > 0) {
//             let sortedItems = items.sort((x, y) => y.Id - x.Id)
//             let first = sortedItems[0]
//             console.log('[loadOrderListTransaction last sequence id', first.Id)
//             vm.lastOrderListSequence = first.Id

//             let collection: MvDetailSpotMarketItem[] = vm.orderAll
//             items.forEach(element => {
//                 let newItem = new MvDetailSpotMarketItem()
//                 newItem.LeftAmount = element.RunningLeftAmount;
//                 newItem.Amount = element.RunningAmount;
//                 newItem.Rate = element.RequestRate;
//                 newItem.OrderId = element.OrderId
//                 newItem.IsBuy = element.IsBuy

//                 let newCollection = collection
//                     .filter(x => x.OrderId != newItem.OrderId
//                         && x.LeftAmount > 0)
//                 newCollection.push(newItem)

//                 let leftCollection = collection.filter(x => x.OrderId == newItem.OrderId)
//                 let leftTotal = leftCollection.length;
//                 console.log('left total', leftTotal)
//                 collection = newCollection
//             });
//             vm.orderAll = collection

//             vm.syncOrderList()
//         }

//     })
// }

function setLastRate(vm, pair, skipUpdateRate: boolean = false) {
  let link2 = url + "/api/lastRate/" + pair;
  Vue.http.get(link2).then(function(response) {
    let lastRate = response.data;
    vm.lastRate = lastRate;
    if (skipUpdateRate == false) {
      vm.inputGenericRate = lastRate;
    }
  });
}

function setAvailableBalance(vm, pair) {
  console.log("start available balance");
  let link2 = url + "/api/availableBalance/" + pair;
  Vue.http.get(link2).then(function(response) {
    console.log("set available balance result", response.data);
    let t = response.data;
    setAvailableBalanceExt(vm, t);
    // vm.availableBalance = t.First
    // vm.availableBalanceFirst = t.First
    // vm.availableBalanceSecond = t.Second
    console.log("available balance", t);
  });
}

function setAvailableBalanceExt(vm, t) {
  vm.availableBalance = t.First;
  vm.availableBalanceFirst = t.First;
  vm.availableBalanceSecond = t.Second;
}
