class MyClass {
    constructor()
    constructor(value: string)
    constructor(private value?: string) {

    }

    getValue(): string {
        return this.value ? this.value : "not assigned";
    }
}

function showName<T>(ctr: { new(): T }): void {
    console.log("name:" + ctr["name"]);
}

function showFunc(ctr: Function) {
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