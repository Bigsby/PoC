const path = require("path");
const http = require("http");
const fs = require("fs");

const PORT = 8080;
const basePath = path.join(__dirname);
const defaultDocument = "index.html";
const mimeTypes = {
    html: "text/html",
    css: "text/css",
    js: "application/javascript",
    mjs:  "application/javascript",
    json: "application/json",
    ico: "image/x-icon",
    default: "application/octet-stream"
};

function getMimeType(filePath) {
    const extension = path.extname(filePath).substring(1);
    return mimeTypes[extension] || mimeTypes.default;
}

http.createServer(function(req, res) {
    let virtualPath = req.url;
    if (virtualPath === "/") {
        virtualPath = "/" + defaultDocument;
    }

    const fullPath = path.join(basePath, virtualPath);

    if (fs.existsSync(fullPath)) {
        res.writeHead(200, {"Content-Type": getMimeType(fullPath)});
        res.end(fs.readFileSync(fullPath));
    } else {
        res.writeHead(404);
        res.end();
    }
}).listen(PORT);
console.log(`Listening on http://localhost:${PORT}`);