// declare let Vue: any
// declare let testHello: any
// declare let toastr: any
var username = "noname";
testHello();
var isLogin = input_is_login == 1;
// let test2: testSchema = new Vue({
//     el: "#app2",
//     data: {
//         wow: -1
//     }
// })
// function woot() {
//     let a = test2.wow
//     console.log('abc', a)
// }
var app_vm = new Vue({
    el: "#app",
    data: {
        is_dev_mode: is_dev_mode,
        version: 3,
        lastRate: -1,
        availableBalanceFirst: -1,
        availableBalanceSecond: -1,
        openOrder: [],
        orderAll: [],
        orderSells: [],
        orderBuys: [],
        // holdTransactions: [],
        historyTransactions: [],
        marketHistoryTransactions: [],
        topUpVisible: false,
        dev_log_console: "Console log: " + "\n" + "Start",
        historyTransactionClickNewItemVisibility: false,
        historyTransactionClickToShowVisibility: true,
        marketPlaceClickNewItemVisibility: false,
        marketPlaceClickToShowVisibility: true,
        //lastOrderListSequence: -1,
        editMode: false,
        signalLastStatus: "none",
        signalCondition: "off",
        currency_pair: "",
        inputGenericRate: null,
        inputGenericAmount: null,
        currentMode: "buy",
        // loadingUserProfile: "",
        user: {
            username: "none"
        },
        // availableBalance : 999,
        //inputSell: -1,
        //inputRateSell: -1,
        //inputBuy: -1,
        //inputRateBuy: -1,
        class_menu_one: true,
        class_menu_two: false,
        class_menu_three: false,
        class_menu_four: false,
        class_div_one_hidden: false,
        class_div_two_hidden: true,
        class_div_three_hidden: true,
        class_div_four_hidden: true,
        current_pair_indicator: "no_pair",
        percentage_indicator: 0
    },
    computed: {
        checkLoginStatus: function () {
            var output = isLogin;
            return output;
        },
        lastRateDisplay: function () {
            console.log("[displayLastRate] - inquiry first", this.firstPair);
            console.log("[displayLastRate] - inquiry second", this.secondPair);
            var lastRate = this.lastRate;
            if (this.secondPair == "usd") {
                console.log("[displayLastRate] - usd mode");
                var output = parseFloat(lastRate);
                var output2 = this.rateAmount(output);
                console.log("[last_rate_change] - 1 - " + output2);
                document.title = output2;
                return output2;
            }
            else {
                console.log("[displayLastRate] - btc mode");
                var output = lastRate;
                console.log("[last_rate_change] - 2 - " + output);
                document.title = output;
                return output;
            }
        },
        getListOrderSells: function () {
            var input = this.orderAll;
            var inputFiltered = input.filter(function (x) { return x.IsBuy === false && x.LeftAmount > 0; });
            var output1 = inputFiltered.sort(function (x1, x2) { return x1.Rate - x2.Rate; });
            var output2 = output1.slice(0, 10);
            var output3 = output2.reverse();
            var output = output3;
            while (output.length < 10) {
                var w = new MvDetailSpotMarketItem();
                w.Rate = 99999;
                w.OrderId = -123;
                w.ShortUserName = "ShortName";
                output.unshift(w);
            }
            return output;
        },
        getListOrderBuys: function () {
            var input = this.orderAll;
            // tslint:disable-next-line:semicolon
            var inputFiltered = input.filter(function (x) { return x.IsBuy && x.LeftAmount > 0; });
            var output1 = inputFiltered.sort(function (x1, x2) { return x1.Rate - x2.Rate; }).reverse();
            var output2 = output1.slice(0, 10);
            var output = output2;
            while (output.length < 10) {
                var w = new MvDetailSpotMarketItem();
                w.Rate = 0.01;
                w.OrderId = -123;
                w.ShortUserName = "ShortName";
                output.push(w);
            }
            return output;
        },
        placeHolderAmount: function () {
            if (this.currentMode == "buy") {
                var output = "amount " + this.firstPair;
                return output;
            }
            else if (this.currentMode == "sell") {
                var output = "amount " + this.firstPair;
                return output;
            }
            return "amount N/A";
        },
        maxAmountDisplay: function () {
            if (this.currentMode == "buy") {
                var m = this.mathRound(this.maxAmount);
                var output = m + " " + this.firstPair;
                return output;
            }
            else if (this.currentMode == "sell") {
                var m = this.mathRound(this.maxAmount);
                var output = m + " " + this.firstPair;
                return output;
            }
            else {
                return "123";
            }
        },
        maxAmount: function () {
            if (this.currentMode == "buy") {
                var output = this.availableBalanceSecond / this.inputGenericRate;
                return output;
            }
            else if (this.currentMode == "sell") {
                var output = this.availableBalanceFirst;
                return output;
            }
            else {
                return -1;
            }
        },
        totalAmount: function () {
            if (this.currentMode == "buy") {
                var output = this.inputGenericAmount * this.inputGenericRate;
                return output;
            }
            else if (this.currentMode == "sell") {
                var output = this.inputGenericAmount * this.inputGenericRate;
                return output;
            }
            else {
                return -1;
            }
        },
        firstPair: function () {
            var items = this.currency_pair.split("_");
            var output = items[0];
            return output;
        },
        availableBalanceDisplay: function () {
            if (this.currentMode == "buy") {
                var m = this.mathRound(this.availableBalanceSecond);
                var output = m + " " + this.secondPair;
                return output;
            }
            else if (this.currentMode == "sell") {
                var m = this.mathRound(this.availableBalanceFirst);
                var output = m + " " + this.firstPair;
                return output;
            }
            else {
                return "Abc";
            }
        },
        secondPair: function () {
            var items = this.currency_pair.split("_");
            if (items.length > 1) {
                var output = items[1];
                return output;
            }
            else {
                return "";
            }
        },
        showButton: function () {
            var output = "";
            if (this.currentMode == "buy") {
                output = "Buy " + this.firstPair;
            }
            else if (this.currentMode == "sell") {
                output = "Sell " + this.firstPair;
            }
            return output;
        },
        currencyPairDisplay: function () {
            var item = this.currency_pair.split("_");
            // let output = item[1] + " / " +  item[0]
            //let output = item[0].toUpperCase() + " / " +  item[1].toUpperCase()
            var output = item[0] + " / " + item[1];
            var output2 = output.toUpperCase();
            // return this.currency_pair
            return output2;
        }
    },
    methods: {
        buyConverter: function (input) {
            if (input == true) {
                return "buy";
            }
            else {
                return "sell";
            }
        },
        mathRound: function (input) {
            var output = this.rateAmount(input);
            return output;
            //let m = (Math.round(input * 1000) / 1000).toFixed(3)
            //return m
        },
        orderClick: function (event) {
            if (isLogin == false) {
                toastr.error("No Login");
                return;
            }
            else if (this.inputGenericAmount <= 0 ||
                this.inputGenericAmount == null) {
                console.log("input zero");
                toastr.error("Input Zero");
                return;
            }
            // let mode = "abc";
            var output = this.currentMode +
                "-" +
                this.inputGenericRate +
                "-" +
                this.inputGenericAmount;
            console.log("order click-" + output);
            orderGeneric(this.currentMode, this.inputGenericRate, this.inputGenericAmount);
            // console.log("order click")
        },
        changeMode: function (mode) {
            this.currentMode = mode;
            this.inputGenericAmount = null;
            // this.lastOrderListSequence = -1
            // setUserProfile(this)
        },
        cancelOrder: function (orderId) {
            cancelOrder(orderId);
        },
        serverCheck: function (event) {
            var link2 = url + "/api/values";
            window.location.href = link2;
        },
        serverReset: function (event) {
            var link2 = url + "/api/reset";
            Vue.http.get(link2).then(function (response) {
                alert("done");
            });
        },
        serverClear: function (event) {
            var link2 = url + "/api/clear";
            Vue.http.get(link2).then(function (response) {
                alert("done");
            });
        },
        serverReload: function (event) {
            console.log("server reload");
            // setUserProfile(this)
            reloadAll();
        },
        menu_one_click: function (event) {
            // console.log("menu_one_click")
            this.resetNav();
            this.class_menu_one = true;
            this.class_div_one_hidden = false;
        },
        menu_two_click: function (event) {
            // console.log("menu_two_click")
            this.resetNav();
            this.class_menu_two = true;
            this.class_div_two_hidden = false;
        },
        menu_three_click: function (event) {
            // console.log("menu_three_click")
            this.resetNav();
            this.class_menu_three = true;
            this.class_div_three_hidden = false;
        },
        menu_four_click: function (event) {
            // console.log("menu_four_click")
            this.resetNav();
            this.class_menu_four = true;
            this.class_div_four_hidden = false;
        },
        lastRateClick: function (event) {
            this.inputGenericRate = this.lastRate;
            // console.log("last rate click")
        },
        editModeClick: function (event) {
            this.editMode = !this.editMode;
        },
        resetNav: function (event) {
            this.class_div_one_hidden = true;
            this.class_div_two_hidden = true;
            this.class_div_three_hidden = true;
            this.class_div_four_hidden = true;
            this.class_menu_one = false;
            this.class_menu_two = false;
            this.class_menu_three = false;
            this.class_menu_four = false;
            // console.log("reset nav")
        },
        marketPlaceClickToShow: function (event) {
            this.marketPlaceClickToShowVisibility = false;
            var vm = this;
            var pair = this.currency_pair;
            // console.log("debugClick", "woot");
            console.log("reload-all", "market  transaction");
            loadMarketHistoryTransaction(vm, pair);
        },
        marketPlaceClickNewItem: function (event) {
            this.marketPlaceClickNewItemVisibility = false;
            var vm = this;
            var pair = this.currency_pair;
            console.log("reload-all", "market  transaction - new notif from push");
            loadMarketHistoryTransaction(vm, pair);
        },
        historyTransactionClickToShow: function (event) {
            this.historyTransactionClickToShowVisibility = false;
            var vm = this;
            var pair = this.currency_pair;
            // console.log("debugClick", "woot");
            console.log("reload-all", "market  transaction");
            loadHistoryTransaction(vm, pair);
        },
        historyTransactionClickNewItem: function (event) {
            this.historyTransactionClickNewItemVisibility = false;
            var vm = this;
            var pair = this.currency_pair;
            console.log("reload-all", "market  transaction - new notif from push");
            loadHistoryTransaction(vm, pair);
        },
        debug: function (event) {
            console.log("debug clicked!");
            toastr.info("Sending Transaction");
            toastr.success("Transaction Submitted", "one");
        },
        changeCurrencyPair: function () {
            console.log("changeCurrencyPair", "this indicator", this.current_pair_indicator);
            var vm = app_vm;
            var pair = this.current_pair_indicator;
            var new_pair = pair;
            var old_pair = vm.currency_pair;
            vm.currency_pair = pair;
            console.log("changeCurrencyPair", "current pair", vm.currency_pair);
            console.log("changeCurrencyPair", "hub", hub);
            hub.invoke("unregisterCurrencyPair", old_pair);
            hub.invoke("registerCurrencyPair", new_pair);
            resetHistory();
            resetInput();
            setButtonCondition();
            reloadAll();
        },
        update_rate: function (input) {
            // console.log("show_rate - " + input)
            var vm = app_vm;
            vm.inputGenericRate = input;
            console.log("[change-title] before");
            var pair = this.currency_pair;
            var title_message = pair + " - " + input;
            console.log("[change-title] " + title_message);
            document.title = title_message;
            console.log("[change-title] after");
        },
        update_amount: function (input) {
            // console.log("show_rate - " + input)
            var vm = app_vm;
            vm.inputGenericAmount = input;
        },
        update_percentage_indicator: function (input) {
            // console.log("percentage_indicator - " + input)
            var vm = app_vm;
            vm.percentage_indicator = input;
            var one = 0;
            if (input == 1) {
                one = 0.25;
            }
            else if (input == 2) {
                one = 0.5;
            }
            else if (input == 3) {
                one = 0.75;
            }
            else if (input == 4) {
                one = 1;
            }
            if (this.currentMode == "buy") {
                vm.inputGenericAmount =
                    (vm.availableBalanceSecond * one) / vm.inputGenericRate;
            }
            else if (this.currentMode == "sell") {
                vm.inputGenericAmount = vm.availableBalanceFirst * one;
            }
            else {
                vm.inputGenericAmount = -1;
            }
        },
        syncOrderList: function () {
            console.log("syncOrderList");
            var last_rate = this.lastRate;
            var items = this.orderAll;
            console.log("items", items);
            console.log("last_rate", last_rate);
            var totalItems = items.length;
            console.log("total_items", totalItems);
            {
                var collection = items.filter(function (x) { return x.Rate >= last_rate; });
                var total_collection = collection.length;
                console.log("UpperItems", collection);
                console.log("totalUpperItems", total_collection);
            }
            {
                var collection = items.filter(function (x) { return x.Rate < last_rate; });
                var total_collection = collection.length;
                console.log("LowerItems", collection);
                console.log("totalLowerItems", total_collection);
            }
        },
        rateFormat: function (input, currency) {
            var number1 = input;
            var number2 = numeral(number1);
            var string1 = number2.format("0,000.00");
            var output = string1;
            return output;
        },
        rateAmount: function (input) {
            var number1 = input;
            var number2 = numeral(number1);
            var string1 = number2.format("0,000.00");
            var output = string1;
            return output;
        },
        dateFormat: function (input) {
            // let m0 = moment(input);
            // let m2 = m0.format('DD/MM/YY HH:mm:ss');
            // return m2
            var output = dateFormatHelper(input);
            return output;
        },
        addLog: function (input) {
            this.dev_log_console = this.dev_log_console + "\n" + input;
        }
    }
});
function dateFormatHelper(input) {
    var m0 = moment(input);
    var m2 = m0.format("DD/MM/YY HH:mm:ss");
    return m2;
}
function reloadAfterCancelOrder() {
    console.log("reload-aftercancelorder", "open order");
    var vm = app_vm;
    var pair = vm.currency_pair;
    vm.orderAll = [];
    vm.openOrder = [];
    loadOrderAll(vm, pair);
    loadOpenOrder(vm, pair);
}
function reloadAll() {
    // return;
    console.log("reload-all", 1);
    var vm = app_vm;
    //vm.lastOrderListSequence = -1
    var pair = vm.currency_pair;
    console.log("reload-all", "last rate");
    setLastRate(vm, pair);
    if (isLogin) {
        console.log("reload-all", "available balance");
        setAvailableBalance(vm, pair);
    }
    else {
        console.log("[skip-login] - bypass available balance");
    }
    console.log("reload-all", "order all");
    loadOrderAll(vm, pair);
    if (isLogin) {
        console.log("reload-all", "open order");
        loadOpenOrder(vm, pair);
    }
    else {
        console.log("[skip-login] - bypass open");
    }
}
function resetInput() {
    var vm = app_vm;
    vm.inputGenericRate = null;
    vm.inputGenericAmount = null;
}
function resetHistory() {
    var vm = app_vm;
    vm.lastRate = -1;
    vm.availableBalanceFirst = -1;
    vm.availableBalanceSecond = -1;
    vm.openOrder = [];
    vm.orderAll = [];
    vm.historyTransactions = [];
    vm.marketHistoryTransactions = [];
}
function setButtonCondition() {
    var vm = app_vm;
    vm.historyTransactionClickNewItemVisibility = false;
    vm.historyTransactionClickToShowVisibility = false;
    vm.marketPlaceClickNewItemVisibility = false;
    vm.marketPlaceClickToShowVisibility = false;
    if (isLogin) {
        vm.historyTransactionClickToShowVisibility = true;
        vm.marketPlaceClickToShowVisibility = true;
    }
}
function populateCurrentPair() {
    var a1 = document.getElementById("currency_pair");
    var pair = a1.getAttribute("value");
    console.log("populateCurrentPair");
    console.log(pair);
    var vm = app_vm;
    vm.currency_pair = pair;
    vm.current_pair_indicator = pair;
}
document.addEventListener("DOMContentLoaded", function () {
    populateCurrentPair();
    setButtonCondition();
    reloadAll();
    app_vm.menu_two_click();
    // woot()
});
// var document: Document;
document.onload = function (x) {
    console.log("[dev_log] onload", x);
    app_vm.addLog("onload");
};
document.onreadystatechange = function (x) {
    console.log("[dev_log] onreadystatechange", x);
    var w = document.readyState;
    app_vm.addLog("onreadystatechange" + "-" + w);
};
document.addEventListener("DOMContentLoaded", function () {
    app_vm.addLog("DOMContentLoaded");
});
//# sourceMappingURL=script_order.js.map