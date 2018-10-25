var net = require('net');

var server = net.createServer(function(socket) {
	socket.write('Echo server\r\n');
    socket.on("data", function(data){
        console.log("Server received: " + data);
    });
});

server.listen(1337, '127.0.0.1');

server.on('connection', function(sock) {
    
    console.log('CONNECTED: ' + sock.remoteAddress +':'+ sock.remotePort);
});