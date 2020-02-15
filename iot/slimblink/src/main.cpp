#include <Arduino.h>
#define led 13

void setup() {
  pinMode(3, OUTPUT);
}

void loop() {
  digitalWrite(led, HIGH);
  delay(500);
  digitalWrite(led, LOW);
  delay(300);
}