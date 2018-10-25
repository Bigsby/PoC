// console.log("this is just a test");

// for (let i = 0; i < 10; i++){
//     console.log(i);
// }

let loaded = [];

for (let i = 0; i < 26; i++) {
    loaded[String.fromCharCode(97 + i)] = true;
    console.log(loaded[String.fromCharCode(97 + i)]);
}

console.log(Array.isArray(loaded));

// let passed_count = 0;
// let file_count = 0;
// for (let prop in loaded) {
//     file_count += 1;
//     if (loaded.hasOwnProperty(prop) && loaded[prop]) {
//         passed_count += 1;
//     }
// }
// const passed_count = loaded.filter((v) => v).length;

//let all_done = passed_count === file_count;
//const all_done = loaded.reduce( (pv, v) => { return pv && v });

// const passed_count = loaded.filter((v) => v).length;
// const all_done = loaded.reduce( (pv, v) => { return pv && v });

// console.log(`Passed count: ${passed_count}`);
// console.log(`All done: ${all_done}`);
console.log(`length: ${loaded.length}`);
console.log(JSON.stringify(loaded));