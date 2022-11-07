function debugOne() {
    console.log("one");
}
function libConvertTime(input) {
    var m0 = moment(input);
    var m1 = m0.fromNow();
    var m2 = m0.format("MMMM Do YYYY, h:mm:ss a");
    var output = m2 + "  --  " + m1;
    return output;
}
function libGetDiff(inputTime) {
    var m0 = moment(inputTime);
    var m1 = moment();
    var output = m1.diff(m0);
    var output2 = output / 1000;
    return output2;
}
function getMandatoryArray() {
    var output = new Array();
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
//# sourceMappingURL=dashboard_lib.js.map