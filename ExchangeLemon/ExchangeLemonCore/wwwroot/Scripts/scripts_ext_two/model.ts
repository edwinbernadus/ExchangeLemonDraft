class ItemGraph {
  Content: string;
  Value: any;
}
class ItemDate {
  Sequence: number;
  Value: number;
  Date: Date;
}

class Student {
  rate: number;
  isReady: boolean;
}
class MvMarketHistoryTransaction {
  TransactionRate: number;
  Amount: number;

  CreatedBy: string;
  Id: number;
}

class MvDetailSpotMarketItemContent {
  LastSequenceHistory: number;
  Items: MvDetailSpotMarketItem[];
}

class MvDetailSpotMarketItem {
  CurrentPair: string;
  OrderId: number;
  Rate: number;
  Amount: number;
  LeftAmount: number;
  TransactionAmount: number;
  IsBuy: boolean;
  IsShow: boolean;
  UserName: string;
  ShortUserName: string;
}

class MvOrderHistory {
  Id: number;
  OrderId: number;
  RunningAmount: number;
  RunningLeftAmount: number;
  RequestRate: number;
  CurrencyPair: string;
  IsBuy: boolean;
  TransactionId: number;
}

class AccountTransaction {
  Id: number;
  Amount: number;
  CurrencyPair: string;
  CurrencyCode: string;
  Rate: number;
  DebitCreditType: string;
  RunningBalance: number;
  CreatedDate: Date;
  IsExternal: boolean;
}

class testSchema {
  // el: any,
  wow: number;
}

// class vueMain {
//     http: any

//     // constructor(input: any) {

//     // }
// }
