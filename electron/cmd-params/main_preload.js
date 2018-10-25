const { ipcRenderer } = require("electron");

console.log("in preload");
ipcRenderer.on("display", (event, value) => {
    console.log("in on display");
    window.display && window.display(value);
});

console.log(`argv.length: ${process.argv.length}`);
console.log(`argv: ${JSON.stringify(process.argv[0])}`);
window.display && window.display(process.argv[0]);