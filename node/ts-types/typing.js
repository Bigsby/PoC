var MyClass = /** @class */ (function () {
    function MyClass(value) {
        this.value = value;
    }
    MyClass.prototype.getValue = function () {
        return this.value ? this.value : "not assigned";
    };
    return MyClass;
}());
function showName(ctr) {
    console.log("name:" + ctr["name"]);
}
function showFunc(ctr) {
    if (typeof ctr === "function")
        console.log(ctr["name"]);
    else
        console.log("not a func");
}
showName(MyClass);
var a = new MyClass();
var b = new MyClass("this a value");
console.log(a.getValue());
console.log(b.getValue());
console.log(typeof MyClass);
showFunc(MyClass);
