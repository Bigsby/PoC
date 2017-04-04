import * as http from "http";
import * as fs from "fs";
import * as path from "path";

import config from "./config";

const server = http.createServer((request, response) => {
    function notFound() {
        response.writeHead(404);
        response.end("Resource not found.\n");
        response.end();
    }

    function getMimeType(ext: string) {
        var mime = config.defaultMimeType;
        for (let mimeType of config.mimeTypes)
            if (mimeType.extensions.indexOf(ext) != -1)
                mime = mimeType.type;

        return mime;
    }

    let virtualPath = request.url;

    if (virtualPath == "/" && config.defaultDocument)
        virtualPath = "/" + config.defaultDocument;

    let filePath = config.root + virtualPath;
    let contentType = getMimeType(path.extname(filePath));

    if (fs.existsSync(filePath))
        fs.readFile(filePath, function (error, content) {
            if (error) {
                if (error.code == 'ENOENT') {
                    notFound();
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
    else
        notFound();
});

server.listen(config.port, config.hostname, () => {
    console.log(`Server running at http://${config.hostname}:${config.port}/`);
});