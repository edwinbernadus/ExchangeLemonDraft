// declare var isLogin: boolean
// declare var app_vm: any
var currency_pair_html = document.getElementById("currency_pair");
var currency_pair = currency_pair_html.getAttribute("value");
// let vm = app_vm;
// let currency_pair_vuejs = "woot-1"
// currency_pair_vuejs = vm.currency_pair
// let currency_pair = "woot-2"
// console.log('vuejs', app_vm)
// console.log('vuejs  currency pair', currency_pair_vuejs)
console.log("current currency pair", currency_pair);
var hub = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Trace)
    .withUrl("/signal/transaction")
    .withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
    .build();
hub.on("ListenOrderChangeExt", function (pair, data) {
    console.log("Signal-ListenOrderChangeExt", pair, data);
    var inputs = JSON.parse(data);
    var vm = app_vm;
    var vm_pair = vm.currency_pair;
    if (pair === vm_pair) {
        loadOrderAllPush(vm, inputs);
    }
});
hub.on("ListenBalanceExt", function (pair, data) {
    console.log("Signal-ListenBalance", pair, data);
    var vm = app_vm;
    var vm_pair = vm.currency_pair;
    console.log("[listenBalance] current currency pair:", pair);
    console.log("[listenBalance] current currency pair - data:", data);
    if (pair === vm_pair) {
        var input = JSON.parse(data);
        setAvailableBalanceExt(vm, input);
    }
    else {
        console.log("[listenBalance] [warning] pair not same");
    }
});
hub.on("ListenGraph", function (data) {
    console.log("[GraphPush] Signal-ListenGraph", data);
    lib1.sayHi2(data);
    //submitGraphData(data);
});
hub.on("ListenPair", function (currency_pair, rate) {
    console.log("Signal-ListenPair", currency_pair, rate);
    var vm = app_vm;
    var pair = vm.currency_pair;
    console.log("[listenPair] current currency pair:", pair);
    if (pair === currency_pair) {
        vm.lastRate = rate;
    }
    else {
        console.log("[listenPair] [warning] pair not same");
    }
});
hub.on("ListenPendingOrder", function (currency_pair, orderId, isCancel) {
    console.log("Signal-ListenPendingOrder", currency_pair, orderId, isCancel);
    var vm = app_vm;
    var pair = vm.currency_pair;
    console.log("[listenPendingOrder] current currency pair:", currency_pair);
    if (pair === currency_pair) {
        loadOpenOrder(vm, pair);
    }
    else {
        console.log("[listenPendingOrder] [warning] pair not same");
    }
});
hub.on("ListenMarketHistory", function (currency_pair) {
    // await Clients.All.SendAsync ("ListenMarketHistory", currencyPair);
    console.log("Signal-ListenMarketHistory", currency_pair);
    var vm = app_vm;
    var pair = vm.currency_pair;
    console.log("[listenMarketHistory] current currency pair:", currency_pair);
    if (pair === currency_pair) {
        vm.marketPlaceClickNewItemVisibility = true;
        vm.marketPlaceClickToShowVisibility = false;
    }
    else {
        console.log("[listenMarketHistory] [warning] pair not same");
    }
});
hub.on("ListenHistoryTransaction", function (currency_pair) {
    console.log("Signal-ListenHistoryTransaction", currency_pair);
    var vm = app_vm;
    var pair = vm.currency_pair;
    console.log("[listenHistoryTransaction] current currency pair:", currency_pair);
    if (pair === currency_pair) {
        vm.historyTransactionClickNewItemVisibility = true;
        vm.historyTransactionClickToShowVisibility = false;
    }
    else {
        console.log("[listenHistoryTransaction] [warning] pair not same");
    }
});
hub.on("ListenMatchOrder", function (content) {
    console.log("Signal-ListenMatchOrder", content);
    console.log("script_order", "listenMatchOrder", content);
    toastr.info(content);
});
hub.on("ListenReceiveDeposit", function (content) {
    console.log("Signal-ListenReceiveDeposit", content);
    var content2 = content + "\r\n\r\n" + "Please reload page";
    console.log("script_order", "listenReceiveDeposit", content2);
    toastr.info(content2);
});
var isFirstInit = true;
hub.closedCallbacks.push(function (error) {
    console.log("[reconnecting] connection closed", error);
    console.log("[HUB] disconnect");
    app_vm.signalCondition = "off";
    app_vm.signalLastStatus = "disconnect";
    tryConnecting();
});
tryConnecting();
function tryConnecting() {
    var isConnected2 = true;
    hub
        .start()
        .catch(function (err) {
        isConnected2 = false;
        console.error("[reconnecting] error - " + err.toString());
        console.log("[HUB] reconnecting");
        app_vm.signalCondition = "off";
        app_vm.signalLastStatus = "reconnecting";
        setTimeout(tryConnecting, 3000);
    })
        .then(function (x) {
        if (isConnected2) {
            console.log("[reconnecting] SignalR Core reconnected");
            console.log("[reconnecting] [HUB] started");
            // console.log("SignalR Core connected");
            if (isFirstInit) {
                isFirstInit = false;
                if (isLogin) {
                    hub.invoke("register");
                }
                else {
                    console.log("[skip-login] - bypass register user");
                }
                console.log("[register pair]", "before");
                console.log("[register pair]", currency_pair);
                hub.invoke("registerCurrencyPair", currency_pair);
                console.log("[register pair]", "after");
                console.log("[HUB] started");
                app_vm.signalCondition = "on";
                app_vm.signalLastStatus = "started";
            }
            else {
                console.log("[HUB] reconnected");
                app_vm.signalCondition = "on";
                app_vm.signalLastStatus = "reconnected";
            }
        }
    });
}
//# sourceMappingURL=script_signal.js.map