function testHello() {
    console.log("start loading page");
}
var url = getCurrentBaseUrl();
console.log("baseUrl");
console.log(url);
function getCurrentBaseUrl() {
    var full = location.protocol +
        "//" +
        location.hostname +
        (location.port ? ":" + location.port : "");
    return full;
}
function cancelOrder(id) {
    toastr.warning("Sending Transaction");
    // toastr.success('Transaction Submitted')
    var link2 = url + "/api/cancel/" + id;
    var vm = app_vm;
    var formData = new FormData();
    //Vue.http.headers.common['Content-Type'] = 'application/json'
    Vue.http.headers.common["Content-Type"] =
        "application/x-www-form-urlencoded;charset=UTF-8";
    Vue.http.headers.common["Access-Control-Allow-Origin"] = "*";
    var v = this;
    Vue.http
        .post(link2, formData, {
        emulatedJson: true
    })
        .then(function (response) {
        var temp = response.data;
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
    var link2 = url + "/api/OrderItemMain";
    var vm = app_vm;
    console.log("order generic", "url transaction", link2);
    Vue.http.headers.common["Content-Type"] =
        "application/x-www-form-urlencoded;charset=UTF-8";
    Vue.http.headers.common["Access-Control-Allow-Origin"] = "*";
    // let username = "";
    var current_pair = vm.currency_pair;
    var formData = {
        rate: inputRate,
        amount: inputAmount,
        mode: mode,
        current_pair: current_pair
    };
    console.log("current_pair: " + current_pair);
    var inputTwo = JSON.stringify(formData);
    var v = this;
    Vue.http
        .post(link2, inputTwo, {
        emulatedJson: true
    })
        .then(function (response) {
        console.log("debug3", "response", response);
        // v.reloadAll()
        // v.reloadAllSignal()
        // toastr.info('Sending Transaction')
        toastr.success("Transaction Submitted");
        // vm.inputGenericRate = -1
        // vm.inputAmount = -2
    })
        .catch(function (response) {
        console.log("z1 1");
        // console.log('error')
        // console.log(response)
        // console.log(response.body)
        // console.log(response.body.ExceptionMessage)
        console.log("z1 error 123");
        console.log("z1 error", response);
        // let message = response.data.error
        var message = response.data;
        var errorMsg = response.statusText + " - " + message;
        toastr.error(errorMsg);
        vm.topUpVisible = true;
    });
}
function loadOrderAll(vm, pair) {
    var link2 = url + "/api/OrderItemMain/" + pair;
    console.log("start order all");
    Vue.http.get(link2).then(function (response) {
        var inputs = response.data;
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
function loadOrderAllPush(vm, inputs) {
    console.log("loadOrderAll", inputs);
    populateOrderList(inputs, vm);
    vm.syncOrderList();
}
function populateOrderList(inputs, vm) {
    var itemNew = inputs.Items;
    var last = inputs.LastSequenceHistory;
    console.log("last1", inputs.LastSequenceHistory);
    console.log("last2", last);
    var items = vm.orderAll;
    console.log("order item main total", items.length);
    items.splice(0, items.length);
    for (var i = 0; i < itemNew.length; i++) {
        var item = itemNew[i];
        vm.orderAll.push(item);
        // console.log(item)
        // console.log(vm.orderSells.length)
    }
    vm.lastOrderListSequence = last;
    console.log("end order all - " + itemNew.length);
}
function loadOpenOrder(vm, pair) {
    var link2 = url + "/api/openOrder/" + pair;
    // A = [];
    // while(A.length > 0) {
    //     A.pop();
    // }
    Vue.http.get(link2).then(function (response) {
        var temp = response.data;
        // console.log(temp)
        var itemNew = temp;
        var items = vm.openOrder;
        items.splice(0, items.length);
        for (var i = 0; i < itemNew.length; i++) {
            var item = itemNew[i];
            vm.openOrder.push(item);
            // console.log(item)
            // console.log(vm.orderSells.length)
        }
    });
}
function loadMarketHistoryTransaction(vm, pair) {
    var link2 = url + "/api/marketHistoryTransaction/" + pair;
    Vue.http.get(link2).then(function (response) {
        var itemNew = response.data;
        var items2 = vm.marketHistoryTransactions;
        items2.splice(0, items2.length);
        for (var i = 0; i < itemNew.length; i++) {
            var item = itemNew[i];
            vm.marketHistoryTransactions.push(item);
        }
    });
}
function loadHistoryTransaction(vm, pair) {
    var link2 = url + "/api/historyTransaction/" + pair;
    Vue.http.get(link2).then(function (response) {
        var temp = response.data;
        var itemNew = temp;
        var items = vm.historyTransactions;
        items.splice(0, items.length);
        for (var i = 0; i < itemNew.length; i++) {
            var item = itemNew[i];
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
function setLastRate(vm, pair, skipUpdateRate) {
    if (skipUpdateRate === void 0) { skipUpdateRate = false; }
    var link2 = url + "/api/lastRate/" + pair;
    Vue.http.get(link2).then(function (response) {
        var lastRate = response.data;
        vm.lastRate = lastRate;
        if (skipUpdateRate == false) {
            vm.inputGenericRate = lastRate;
        }
    });
}
function setAvailableBalance(vm, pair) {
    console.log("start available balance");
    var link2 = url + "/api/availableBalance/" + pair;
    Vue.http.get(link2).then(function (response) {
        console.log("set available balance result", response.data);
        var t = response.data;
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
//# sourceMappingURL=script_api.js.map