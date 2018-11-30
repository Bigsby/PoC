var net = require('net');
const { StringDecoder } = require('string_decoder');

const decoder = new StringDecoder("ascii");
const LENGTH_STRING_LENGTH = 6;
const READ_LENGTH = 0;
const READ_CONTENT = 1;
let currentMessage;
let state; // 0, reading length, 1 reading content;

let currentDesiredLength;
let currentContent;

var server = net.createServer(function(socket) {
    socket.bufferSize = 10;
    socket.on("data", function(data){
        if (!currentMessage) {
            currentMessage =  { };
            state = READ_LENGTH;
            currentDesiredLength = LENGTH_STRING_LENGTH;
        }
        
        const buffer = Buffer.from(data);
        
        let data = "";
        if (buffer.length > currentDesiredLength)
        {
            currentContent = 
        }
        console.log("Server received length:" + buffer.length);
        for (let index = 0; index < buffer.length; index++) {
            const element = buffer[index];
            console.log(`${index} - ${element} - ${decoder.write(Buffer.from([element]))}`);
        }
    });

    socket.on("close", () => {
        console.log("Shutingdown!");
        server.close();
    });
});

server.listen(1337);
server.maxConnections = 1;
server.on('connection', function(sock) {
    console.log('CONNECTED: ' + sock.remoteAddress +':'+ sock.remotePort);
});
