declare var axios: any;
declare var bars: any;

class LibOne {
  // supportedResolutions = ["1", "3", "5", "15", "30", "60", "120", "240", "D"];
  //supportedResolutions = ["D"];
  supportedResolutions = ["1"];
  // supportedResolutions = ["60"];

  config = {
    supported_resolutions: this.supportedResolutions
  };

  onRealtimeCallback: any;

    sayHi2(item: any) {
        console.log("hi2", "[GraphPush]", item);
        console.log("period-track", "signal", "item", item);
        console.log("period-track", "signal", "time-display", item.timeDisplay);

        this.onRealtimeCallback(item);
      
    }

  sayHi2Old(items: Array<any>) {
    console.log("hi2", "[GraphPush]", items);
    console.log("hi2", "[GraphPush]", "total", items.length);
    let time1 = items[0].time;
    console.log("hi2", "[GraphPush]", "time1", time1);
    // let items2 = JSON.parse(items);

      let item1 = items[0]
      console.log("period-track", "signal", "first", items[0]);
      let last = items[items.length - 1];
      console.log("period-track", "signal", "last", last);

      console.log("period-track", "signal", "counter", items.length);

      
      console.log("period-track", "signal", "time-display", last.timeDisplay);

    this.onRealtimeCallback(last);
    items.forEach(element => {

      // element.time = parseInt(element.time);
      // element.time = element.time * 1000;
      // element.time = element.time / 1000;
      //this.onRealtimeCallback(element);
    });

    // this.onRealtimeCallback(items);
  }
  sayHi() {
    console.log("hi");
    // bar: object {time, close, open, high, low, volume}

    {
      let adjust = 152;
      let item = {
        time: 1544140800,
        close: adjust + 4,
        open: adjust + 2,
        high: adjust + 10,
        low: adjust + 1,
        volume: 20
      };
      // this.onRealtimeCallback(item);
    }

    {
      let adjust = 162;
      let item = {
        time: 1544227200000,
        close: adjust + 4,
        open: adjust + 2,
        high: adjust + 10,
        low: adjust + 1,
        volume: 20
      };
      //   this.onRealtimeCallback(item);
    }
    {
      let adjust = 152;
      let item = {
        time: 1544313600000,
        close: adjust + 4,
        open: adjust + 2,
        high: adjust + 10,
        low: adjust + 1,
        volume: 20
      };
      this.onRealtimeCallback(item);
    }
  }
  onReady(cb: any) {
    console.log("on ready");
    setTimeout(() => cb(this.config), 0);
  }
  calculateHistoryDepth(
    resolution: string,
    resolutionBack: any,
    intervalBack: number
  ) {
    //return "D";
    return "1";
    // return "60";
    // throw new Error("Method not implemented.");
    // return resolution < 60
    //   ? { resolutionBack: "D", intervalBack: "1" }
    //   : undefined;
  }
  getMarks?(
    symbolInfo: any,
    from: number,
    to: number,
    onDataCallback: any,
    resolution: string
  ): void {
    throw new Error("Method not implemented.");
  }
  getTimescaleMarks?(
    symbolInfo: any,
    from: number,
    to: number,
    onDataCallback: any,
    resolution: string
  ): void {
    throw new Error("Method not implemented.");
  }
  getServerTime?(callback: any): void {
    // callback(0);
    // console.log("4");
    // throw new Error("Method not implemented.");
  }
  searchSymbols(
    userInput: string,
    exchange: string,
    symbolType: string,
    onResult: any
  ): void {
    throw new Error("Method not implemented.");
  }
  resolveSymbol(symbolName: string, onResolve: any, onError: any): void {
    console.log("resolve symbol", symbolName);
    console.log("======resolveSymbol running");
    // console.log('resolveSymbol:',{symbolName})
    var split_data = symbolName.split(/[:/]/);
    // console.log({split_data})
    var symbol_stub = {
      name: symbolName,
      description: "",
      type: "crypto",
      session: "24x7",
      timezone: "Etc/UTC",
      ticker: symbolName,
      exchange: split_data[0],
      minmov: 1,
      pricescale: 100000000,
      has_intraday: true,
      intraday_multipliers: ["1", "60"],
      // intraday_multipliers: ["60"],
      supported_resolution: this.supportedResolutions,
      volume_precision: 8,
      data_status: "streaming"
    };

    // if (split_data[2].match(/USD|EUR|JPY|AUD|GBP|KRW|CNY/)) {
    //   symbol_stub.pricescale = 100;
    // }
    setTimeout(function() {
      onResolve(symbol_stub);
      console.log("Resolving that symbol....", symbol_stub);
    }, 0);
  }
  getBars(
    symbolInfo: any,
    resolution: string,
    rangeStartDate: number,
    rangeEndDate: number,
    onResult: any,
    onError: any,
    isFirstCall: boolean
  ): void {
    let w = this;
    let url =
      // "https://localhost:5001/data/history?symbol=" +
      // "https://localhost:5001/data/historyExt?symbol=" +
      // "/api/historyExt?symbol=" +
      // symbolInfo.name +
      // "&resolution=D&from=" +
      // rangeStartDate +
      // "&to=" +
      // rangeEndDate;
      "/api/reportGraphExt/btc_usd" +
      "?from=" +
      rangeStartDate +
      "&to=" +
      rangeEndDate;
    console.log("[get-bars]", "symbolInfo", symbolInfo);
    console.log("[get-bars]", "symbolInfo-2", symbolInfo.name);
    console.log("[get-bars]", "url", url);
    axios
      .get(url)
      .then(function(response: any) {
        console.log("[get-bars]", "response", response);
        console.log("[get-bars]", "response-data", response.data);
          let data : Array<any> = response.data;
          if (data.length) {
              console.log("period-track", "bars", "first", data[0]);
              let counter = data.length - 1
              console.log("period-track", "bars", "counter", counter);

              console.log("period-track", "bars", "last", data[counter]);
              console.log("period-track", "bars", "time-display", data[counter].timeDisplay);
          onResult(data, { noData: false });
        } else {
          onResult(data, { noData: true });
        }
        //onResult(data);
        //onHistoryCallback(bars, { noData: false })
      })
      .catch(function(error: any) {
        // handle error
        console.log("[get-bars]", "response-catch", error);
      })
      .then(function() {
        // always executed
      });
    // throw new Error("Method not implemented.");
  }
  subscribeBars(
    symbolInfo: any,
    resolution: string,
    onRealtimeCallback: any,
    listenerGuid: string,
    onResetCacheNeededCallback: () => void
  ): void {
    // throw new Error("Method not implemented.");
    console.log("[subscribeBars]", "symbolInfo", symbolInfo);
    console.log("[subscribeBars]", "resolution", resolution);
    console.log("[subscribeBars]", "onRealtimeCallback", onRealtimeCallback);
    console.log("[subscribeBars]", "listenerGuid", listenerGuid);
    this.onRealtimeCallback = onRealtimeCallback;
  }
  unsubscribeBars(listenerGuid: string): void {
    throw new Error("Method not implemented.");
  }
  subscribeDepth?(symbolInfo: any, callback: any): string {
    throw new Error("Method not implemented.");
  }
  unsubscribeDepth?(subscriberUID: string): void {
    throw new Error("Method not implemented.");
  }
}

let lib1 = new LibOne();

function sayHello() {
  console.log("hello");
  lib1.sayHi();
}
