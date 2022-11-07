declare var Vue: any

declare var numeral: any

let vm_dev = new Vue({
    el: '#app',
    data: {
        items: [],
        editMode: "woot"
    },
    computed: {
        secondPair: function () {
            return "one"
        },

        display_data: function () {
            let vm = this
            let items: Student[] = vm.items
            let output = items.filter(x => x.isReady)
            return output
        },
    },
    methods: {
        UpdateAmount: function () {

            console.log('1')
            let item = vm_dev.items[0]
            item.rate = 99999
            console.log('2')
        },
        addSample: function () {
            let items: Student[] = vm_dev.items

            {
                var item = new Student()
                item.rate = 1000
                item.isReady = true

                items.push(item)
            }

            {
                var item = new Student()
                item.rate = 1500
                item.isReady = false


                items.push(item)
            }


            {
                var item = new Student()
                item.rate = 3500
                item.isReady = true


                items.push(item)
            }

        }
    }
})



vm_dev.addSample()


let number1: number = 0.0000123
let number2: number = 7123.45
let number3: number = 0.789

let string1 = numeral(number1).format('0.00000000');
let string2 = numeral(number2).format('0.00');
let string3 = numeral(number3).format('0.00000000');

console.log('string1', string1)
console.log('string2', string2)
console.log('string3', string3)


var document: Document;
document.onload = (x) => {
    console.log('onload', x)
};



document.onreadystatechange = (x) => {
    console.log('onreadystatechange', x)
};


