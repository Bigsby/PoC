class ConfigObject {
    constructor(
        public defaultMimeType: string,
        public root: string,
        public defaultDocument: string,
        public mimeTypes: MimeType[],
        public hostname: string = "127.0.0.1",
        public port: number = 3000
    ) { }
}

export class MimeType {
    constructor(
        public type: string,
        public extensions: string[]
    ) { }
}

export default new ConfigObject(
    "application/octet-stream",
    "./",
    "index.html",
    [
        {
            type: "text/html",
            extensions: [
                ".html",
                ".htm"
            ]
        },
        {
            type: "application/json",
            extensions: [
                ".json"
            ]
        },
        {
            type: "text/css",
            extensions: [
                ".css"
            ]
        },
        {
            type: "image/png",
            extensions: [
                ".png"
            ]
        },
        {
            type: "image/jpg",
            extensions: [
                ".jpg",
                ".jpeg"
            ]
        },
        {
            type: "image/gif",
            extensions: [
                ".gif"
            ]
        }
    ]
);