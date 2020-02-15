#include <Arduino.h>
#define data 12
#define clk 11
#define latch 10

void setup() {
  pinMode(data, OUTPUT);
  pinMode(clk, OUTPUT);
  pinMode(latch, OUTPUT);

  digitalWrite(latch, LOW);
}

void setData(int value) {
  shiftOut(data, clk, LSBFIRST, 0x55);
  digitalWrite(latch, HIGH);
  digitalWrite(latch, LOW);
  delay(200);
}

void loop() {
  setData(0x55);
  setData(0xaa);
  setData(0x55);
  setData(0xaa);
  setData(0x55);
}