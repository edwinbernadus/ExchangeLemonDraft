var vm_dev = new Vue({
    el: '#app',
    data: {
        items: [],
        editMode: "woot"
    },
    computed: {
        secondPair: function () {
            return "one";
        },
        display_data: function () {
            var vm = this;
            var items = vm.items;
            var output = items.filter(function (x) { return x.isReady; });
            return output;
        },
    },
    methods: {
        UpdateAmount: function () {
            console.log('1');
            var item = vm_dev.items[0];
            item.rate = 99999;
            console.log('2');
        },
        addSample: function () {
            var items = vm_dev.items;
            {
                var item = new Student();
                item.rate = 1000;
                item.isReady = true;
                items.push(item);
            }
            {
                var item = new Student();
                item.rate = 1500;
                item.isReady = false;
                items.push(item);
            }
            {
                var item = new Student();
                item.rate = 3500;
                item.isReady = true;
                items.push(item);
            }
        }
    }
});
vm_dev.addSample();
var number1 = 0.0000123;
var number2 = 7123.45;
var number3 = 0.789;
var string1 = numeral(number1).format('0.00000000');
var string2 = numeral(number2).format('0.00');
var string3 = numeral(number3).format('0.00000000');
console.log('string1', string1);
console.log('string2', string2);
console.log('string3', string3);
var document;
document.onload = function (x) {
    console.log('onload', x);
};
document.onreadystatechange = function (x) {
    console.log('onreadystatechange', x);
};
//# sourceMappingURL=vue_playground.js.map