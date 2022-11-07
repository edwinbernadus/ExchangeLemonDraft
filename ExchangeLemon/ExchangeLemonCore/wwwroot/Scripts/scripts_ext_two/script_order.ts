// declare let Vue: any
// declare let testHello: any
// declare let toastr: any

let username = "noname";
declare var numeral: any;
declare var moment: any;
testHello();

declare var input_is_login: number;
let isLogin = input_is_login == 1;
declare var is_dev_mode : boolean;


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

let app_vm = new Vue({
  el: "#app",
  data: {
    is_dev_mode : is_dev_mode,
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
    checkLoginStatus: function(): boolean {
      let output = isLogin;
      return output;
    },
    lastRateDisplay: function(): number {
      console.log("[displayLastRate] - inquiry first", this.firstPair);
      console.log("[displayLastRate] - inquiry second", this.secondPair);

      let lastRate = this.lastRate;
      if (this.secondPair == "usd") {
        console.log("[displayLastRate] - usd mode");
        let output = parseFloat(lastRate);
        let output2 = this.rateAmount(output);
        console.log("[last_rate_change] - 1 - " + output2);
        document.title = output2;
        return output2;
      } else {
        console.log("[displayLastRate] - btc mode");
        let output = lastRate;
        console.log("[last_rate_change] - 2 - " + output);
        document.title = output;
        return output;
      }
    },
    getListOrderSells: function(): MvDetailSpotMarketItem[] {
      let input: MvDetailSpotMarketItem[] = this.orderAll;
      let inputFiltered = input.filter(
        x => x.IsBuy === false && x.LeftAmount > 0
      );
      let output1 = inputFiltered.sort((x1, x2) => x1.Rate - x2.Rate);
      let output2 = output1.slice(0, 10);
      let output3 = output2.reverse();
      let output: MvDetailSpotMarketItem[] = output3;

      while (output.length < 10) {
        let w = new MvDetailSpotMarketItem();
        w.Rate = 99999;
        w.OrderId = -123;
        w.ShortUserName = "ShortName";
        output.unshift(w);
      }
      return output;
    },
    getListOrderBuys: function(): MvDetailSpotMarketItem[] {
      let input: MvDetailSpotMarketItem[] = this.orderAll;
      // tslint:disable-next-line:semicolon
      let inputFiltered = input.filter(x => x.IsBuy && x.LeftAmount > 0);
      let output1 = inputFiltered.sort((x1, x2) => x1.Rate - x2.Rate).reverse();
      let output2 = output1.slice(0, 10);

      let output: MvDetailSpotMarketItem[] = output2;

      while (output.length < 10) {
        let w = new MvDetailSpotMarketItem();
        w.Rate = 0.01;
        w.OrderId = -123;
        w.ShortUserName = "ShortName";
        output.push(w);
      }
      return output;
    },
    placeHolderAmount: function() {
      if (this.currentMode == "buy") {
        let output = "amount " + this.firstPair;
        return output;
      } else if (this.currentMode == "sell") {
        let output = "amount " + this.firstPair;
        return output;
      }
      return "amount N/A";
    },

    maxAmountDisplay: function() {
      if (this.currentMode == "buy") {
        let m = this.mathRound(this.maxAmount);
        let output = m + " " + this.firstPair;
        return output;
      } else if (this.currentMode == "sell") {
        let m = this.mathRound(this.maxAmount);
        let output = m + " " + this.firstPair;
        return output;
      } else {
        return "123";
      }
    },
    maxAmount: function() {
      if (this.currentMode == "buy") {
        let output = this.availableBalanceSecond / this.inputGenericRate;
        return output;
      } else if (this.currentMode == "sell") {
        let output = this.availableBalanceFirst;
        return output;
      } else {
        return -1;
      }
    },
    totalAmount: function() {
      if (this.currentMode == "buy") {
        let output = this.inputGenericAmount * this.inputGenericRate;
        return output;
      } else if (this.currentMode == "sell") {
        let output = this.inputGenericAmount * this.inputGenericRate;
        return output;
      } else {
        return -1;
      }
    },
    firstPair: function() {
      let items = this.currency_pair.split("_");
      let output = items[0];
      return output;
    },
    availableBalanceDisplay: function() {
      if (this.currentMode == "buy") {
        let m = this.mathRound(this.availableBalanceSecond);
        let output = m + " " + this.secondPair;
        return output;
      } else if (this.currentMode == "sell") {
        let m = this.mathRound(this.availableBalanceFirst);
        let output = m + " " + this.firstPair;
        return output;
      } else {
        return "Abc";
      }
    },
    secondPair: function() {
      let items = this.currency_pair.split("_");
      if (items.length > 1) {
        let output = items[1];
        return output;
      } else {
        return "";
      }
    },
    showButton: function() {
      let output = "";
      if (this.currentMode == "buy") {
        output = "Buy " + this.firstPair;
      } else if (this.currentMode == "sell") {
        output = "Sell " + this.firstPair;
      }
      return output;
    },
    currencyPairDisplay: function() {
      let item = this.currency_pair.split("_");
      // let output = item[1] + " / " +  item[0]
      //let output = item[0].toUpperCase() + " / " +  item[1].toUpperCase()
      let output = item[0] + " / " + item[1];
      let output2 = output.toUpperCase();
      // return this.currency_pair
      return output2;
    }
  },

  methods: {
    buyConverter(input) {
      if (input == true) {
        return "buy";
      } else {
        return "sell";
      }
    },
    mathRound: function(input) {
      let output = this.rateAmount(input);
      return output;
      //let m = (Math.round(input * 1000) / 1000).toFixed(3)
      //return m
    },
    orderClick: function(event) {
      if (isLogin == false) {
        toastr.error("No Login");
        return;
      } else if (
        this.inputGenericAmount <= 0 ||
        this.inputGenericAmount == null
      ) {
        console.log("input zero");
        toastr.error("Input Zero");
        return;
      }

      // let mode = "abc";
      let output =
        this.currentMode +
        "-" +
        this.inputGenericRate +
        "-" +
        this.inputGenericAmount;
      console.log("order click-" + output);
      orderGeneric(
        this.currentMode,
        this.inputGenericRate,
        this.inputGenericAmount
      );
      // console.log("order click")
    },

    changeMode: function(mode) {
      this.currentMode = mode;
      this.inputGenericAmount = null;
      // this.lastOrderListSequence = -1
      // setUserProfile(this)
    },

    cancelOrder: function(orderId) {
      cancelOrder(orderId);
    },
    serverCheck: function(event) {
      let link2 = url + "/api/values";
      window.location.href = link2;
    },
    serverReset: function(event) {
      let link2 = url + "/api/reset";
      Vue.http.get(link2).then(function(response) {
        alert("done");
      });
    },
    serverClear: function(event) {
      let link2 = url + "/api/clear";
      Vue.http.get(link2).then(function(response) {
        alert("done");
      });
    },
    serverReload: function(event) {
      console.log("server reload");
      // setUserProfile(this)
      reloadAll();
    },

    menu_one_click: function(event) {
      // console.log("menu_one_click")

      this.resetNav();

      this.class_menu_one = true;
      this.class_div_one_hidden = false;
    },
    menu_two_click: function(event) {
      // console.log("menu_two_click")

      this.resetNav();

      this.class_menu_two = true;
      this.class_div_two_hidden = false;
    },
    menu_three_click: function(event) {
      // console.log("menu_three_click")
      this.resetNav();

      this.class_menu_three = true;
      this.class_div_three_hidden = false;
    },
    menu_four_click: function(event) {
      // console.log("menu_four_click")

      this.resetNav();

      this.class_menu_four = true;
      this.class_div_four_hidden = false;
    },
    lastRateClick: function(event) {
      this.inputGenericRate = this.lastRate;
      // console.log("last rate click")
    },
    editModeClick: function(event) {
      this.editMode = !this.editMode;
    },
    resetNav: function(event) {
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

    marketPlaceClickToShow: function(event) {
      this.marketPlaceClickToShowVisibility = false;
      let vm = this;
      let pair = this.currency_pair;
      // console.log("debugClick", "woot");
      console.log("reload-all", "market  transaction");
      loadMarketHistoryTransaction(vm, pair);
    },

    marketPlaceClickNewItem: function(event) {
      this.marketPlaceClickNewItemVisibility = false;
      let vm = this;
      let pair = this.currency_pair;
      console.log("reload-all", "market  transaction - new notif from push");
      loadMarketHistoryTransaction(vm, pair);
    },
    historyTransactionClickToShow: function(event) {
      this.historyTransactionClickToShowVisibility = false;
      let vm = this;
      let pair = this.currency_pair;
      // console.log("debugClick", "woot");
      console.log("reload-all", "market  transaction");
      loadHistoryTransaction(vm, pair);
    },

    historyTransactionClickNewItem: function(event) {
      this.historyTransactionClickNewItemVisibility = false;
      let vm = this;
      let pair = this.currency_pair;
      console.log("reload-all", "market  transaction - new notif from push");
      loadHistoryTransaction(vm, pair);
    },

    debug: function(event) {
      console.log("debug clicked!");
      toastr.info("Sending Transaction");
      toastr.success("Transaction Submitted", "one");
    },

    changeCurrencyPair: function() {
      console.log(
        "changeCurrencyPair",
        "this indicator",
        this.current_pair_indicator
      );
      let vm = app_vm;
      let pair = this.current_pair_indicator;

      let new_pair = pair;
      let old_pair = vm.currency_pair;

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
    update_rate: function(input) {
      // console.log("show_rate - " + input)
      let vm = app_vm;
      vm.inputGenericRate = input;
      console.log("[change-title] before");

      let pair = this.currency_pair;
      let title_message = pair + " - " + input;
      console.log("[change-title] " + title_message);
      document.title = title_message;
      console.log("[change-title] after");
    },
    update_amount: function(input) {
      // console.log("show_rate - " + input)
      let vm = app_vm;
      vm.inputGenericAmount = input;
    },
    update_percentage_indicator: function(input) {
      // console.log("percentage_indicator - " + input)
      let vm = app_vm;

      vm.percentage_indicator = input;
      let one = 0;
      if (input == 1) {
        one = 0.25;
      } else if (input == 2) {
        one = 0.5;
      } else if (input == 3) {
        one = 0.75;
      } else if (input == 4) {
        one = 1;
      }

      if (this.currentMode == "buy") {
        vm.inputGenericAmount =
          (vm.availableBalanceSecond * one) / vm.inputGenericRate;
      } else if (this.currentMode == "sell") {
        vm.inputGenericAmount = vm.availableBalanceFirst * one;
      } else {
        vm.inputGenericAmount = -1;
      }
    },
    syncOrderList: function() {
      console.log("syncOrderList");

      let last_rate = this.lastRate;
      let items = this.orderAll;
      console.log("items", items);
      console.log("last_rate", last_rate);
      let totalItems = items.length;
      console.log("total_items", totalItems);

      {
        let collection = items.filter(x => x.Rate >= last_rate);
        let total_collection = collection.length;
        console.log("UpperItems", collection);
        console.log("totalUpperItems", total_collection);
      }

      {
        let collection = items.filter(x => x.Rate < last_rate);
        let total_collection = collection.length;
        console.log("LowerItems", collection);
        console.log("totalLowerItems", total_collection);
      }
    },
    rateFormat: function(input: number, currency: string) {
      let number1 = input;
      let number2 = numeral(number1);
      let string1 = number2.format("0,000.00");
      let output = string1;
      return output;
    },
    rateAmount: function(input: number) {
      let number1 = input;
      let number2 = numeral(number1);
      let string1 = number2.format("0,000.00");
      let output = string1;
      return output;
    },
    dateFormat: function(input: string): string {
      // let m0 = moment(input);
      // let m2 = m0.format('DD/MM/YY HH:mm:ss');
      // return m2
      let output = dateFormatHelper(input);
      return output;
    },
    addLog: function(input: string) {
      this.dev_log_console = this.dev_log_console + "\n" + input;
    }
  }
});

function dateFormatHelper(input: string): string {
  let m0 = moment(input);
  let m2 = m0.format("DD/MM/YY HH:mm:ss");
  return m2;
}

function reloadAfterCancelOrder() {
  console.log("reload-aftercancelorder", "open order");
  let vm = app_vm;
  let pair = vm.currency_pair;

  vm.orderAll = [];
  vm.openOrder = [];

  loadOrderAll(vm, pair);
  loadOpenOrder(vm, pair);
}
function reloadAll() {
  // return;
  console.log("reload-all", 1);
  let vm = app_vm;
  //vm.lastOrderListSequence = -1

  let pair = vm.currency_pair;

  console.log("reload-all", "last rate");
  setLastRate(vm, pair);

  if (isLogin) {
    console.log("reload-all", "available balance");
    setAvailableBalance(vm, pair);
  } else {
    console.log("[skip-login] - bypass available balance");
  }

  console.log("reload-all", "order all");
  loadOrderAll(vm, pair);

  if (isLogin) {
    console.log("reload-all", "open order");
    loadOpenOrder(vm, pair);
  } else {
    console.log("[skip-login] - bypass open");
  }
}

function resetInput() {
  let vm = app_vm;
  vm.inputGenericRate = null;
  vm.inputGenericAmount = null;
}

function resetHistory() {
  let vm = app_vm;
  vm.lastRate = -1;
  vm.availableBalanceFirst = -1;
  vm.availableBalanceSecond = -1;
  vm.openOrder = [];
  vm.orderAll = [];

  vm.historyTransactions = [];
  vm.marketHistoryTransactions = [];
}

function setButtonCondition() {
  let vm = app_vm;
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
  let pair = a1.getAttribute("value");

  console.log("populateCurrentPair");
  console.log(pair);
  let vm = app_vm;
  vm.currency_pair = pair;
  vm.current_pair_indicator = pair;
}

document.addEventListener("DOMContentLoaded", function() {
  populateCurrentPair();
  setButtonCondition();
  reloadAll();
  app_vm.menu_two_click();
  // woot()
});

// var document: Document;
document.onload = x => {
  console.log("[dev_log] onload", x);
  app_vm.addLog("onload");
};

document.onreadystatechange = x => {
  console.log("[dev_log] onreadystatechange", x);
  let w = document.readyState;
  app_vm.addLog("onreadystatechange" + "-" + w);
};

document.addEventListener("DOMContentLoaded", function() {
  app_vm.addLog("DOMContentLoaded");
});
