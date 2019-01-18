export function doStuff(what) {
    alert(`this is module ${counter}: ${what} `);
}

let counter = 0;

(function(){
    counter++;
})();
alert("module 1");