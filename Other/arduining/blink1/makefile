compile: blink1.ino
	arduino-cli compile -b arduino:avr:nano:cpu=atmega328old

upload: compile
	arduino-cli upload -b arduino:avr:nano:cpu=atmega328old -p /dev/cu.usbserial-1410
