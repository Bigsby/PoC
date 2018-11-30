var net = require('net');

var client = new net.Socket();
client.setEncoding("binary");
client.bufferSize = 10;

String.prototype.padLeft = function (length, character) {
    return String(new Array(length + 1).join(character) + this).slice(-length);
};

client.connect(1337, '127.0.0.1', function() {
	console.log('Connected');
	const message = "the message";
	const command = [91];
	const length = String(message.length + 1).padLeft(6, "0");
	client.write(length);
	client.write(Buffer.from(command));
	client.write(message);
});

client.on('data', function(data) {
	console.log('Client Received: ' + data);
	//client.destroy(); // kill client after server's response
});

client.on('close', function(hadError) {
	console.log('Connection closed. ' + hadError);
});