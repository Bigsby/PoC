const { app, BrowserWindow } = require('electron');
const path = require("path");
//const commandLineArgs = require('command-line-args');
const yargs = require("yargs");
const DARWIN_PLATFORM = "darwin";

let mainWindow;

// const optionDefinitions = [
//     { name: "other", alias: "o", type: Boolean }
// ];

// const options = commandLineArgs(optionDefinitions, {
//     stopAtFirstUnknown: false,
//     partial: true
// });

const args = yargs
    .env("CMD")
    .option("other", {
        alias: "o",
        describe: "The value"
    }).parse(process.argv);

console.log(`argv: ${JSON.stringify(process.argv)}`);
console.log(`options: ${JSON.stringify(args)}`);

function createWindow(){
    mainWindow = new BrowserWindow({ 
    });

    mainWindow.loadFile(path.join(__dirname, args.other ? "other.html" : "main.html"));
}

app.on('ready', createWindow);

app.on('window-all-closed', () => {
    if (process.platform !== DARWIN_PLATFORM) {
        app.quit();
    }
});

app.on('activate', () => {
    if (mainWindow === null) {
        createWindow();
    }
});