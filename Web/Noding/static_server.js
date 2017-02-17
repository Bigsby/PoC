const http = require("http");
const fs = require("fs");
const path = require("path");

const hostname = '127.0.0.1';
const port = 3000;

const server = http.createServer((request, response) => {
    var filePath = '.' + request.url;
    if (filePath == './')
        filePath = './index.html';

    var extname = path.extname(filePath);
    var contentType = 'application/octet-stream';
    switch (extname) {
        case ".html":
        case ".htm":
            contentType = 'text/html';
            break;
        case '.js':
            contentType = 'text/javascript';
            break;
        case '.css':
            contentType = 'text/css';
            break;
        case '.json':
            contentType = 'application/json';
            break;
        case '.png':
            contentType = 'image/png';
            break;
        case '.jpg':
            contentType = 'image/jpg';
            break;
        case '.wav':
            contentType = 'audio/wav';
            break;
    }

    fs.readFile(filePath, function (error, content) {
        if (error) {
            if (error.code == 'ENOENT') {
                // fs.readFile('./404.html', function (error, content) {
                //     response.writeHead(200, { 'Content-Type': contentType });
                //     response.end(content, 'utf-8');
                // });
                response.writeHead(404);
                response.end('Resource not found.\n');
                response.end();
            }
            else {
                response.writeHead(500);
                response.end('Sorry, check with the site admin for error: ' + error.code + ' ..\n');
                response.end();
            }
        }
        else {
            response.writeHead(200, { 'Content-Type': contentType });
            response.end(content, 'utf-8');
        }
    });
});

server.listen(port, hostname, () => {
    console.log(`Server running at http://${hostname}:${port}/`);
});