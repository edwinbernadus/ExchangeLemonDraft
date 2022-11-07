function debugOne() {
  console.log("one");
}

function libConvertTime(input) {
  let m0 = moment(input);
  let m1 = m0.fromNow();
  let m2 = m0.format("MMMM Do YYYY, h:mm:ss a");
  let output = m2 + "  --  " + m1;
  return output;
}

function libGetDiff(inputTime) {
  let m0 = moment(inputTime);
  let m1 = moment();
  let output = m1.diff(m0);
  let output2 = output / 1000;
  return output2;
}

function getMandatoryArray(): Array<string> {
  let output = new Array<string>();

  output.push("SyncFinish");
  output.push("gekko-inquiry-ticker");
  output.push("blockchain-uploaded");
  output.push("CancelAllBotSync");
  output.push("gekko-buy-order");
  output.push("gekko-cancel-order");
  output.push("gekko-inquiry-market-history");
  output.push("gekko-inquiry-open-orders");
  output.push("gekko-sell-order");
  output.push("matchOrder");
  // output.push("two");

  return output;
}
