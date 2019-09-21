#include <SoftwareSerial.h>
#include <Servo.h>
//

int mainLight = 12, aircon = 11, television = 10, dimLight = 9;
Servo doorLockServo;
int pos = 0;
String inData = "";
void setup()
{
  pinMode(mainLight, OUTPUT);
  pinMode(aircon, OUTPUT);
  pinMode(television, OUTPUT);
  pinMode(dimLight, OUTPUT);
  doorLockServo.attach(6);
  Serial.begin(9600);
}

void loop()
{
  while (Serial.available())
  { 
    delay(10); 
    char c = Serial.read(); 
    inData += c; 
  }

  if(inData.length() == 8 && 
     inData.indexOf("#") == 0 && 
     inData.indexOf("$") == 1 && 
     inData.indexOf("%") == 7)
  {
    //Door Lock
    if(inData.charAt(2) == '1')
    {
      doorLockServo.write(180);
    }
    else if(inData.charAt(2) == '0')
    {
      doorLockServo.write(0);
    }

    //Main Light
    if(inData.charAt(3) == '1')
    {
       digitalWrite(mainLight, HIGH);
    }
    else if(inData.charAt(3) == '0')
    {
      digitalWrite(mainLight, LOW);
    }

    //Aircon
    if(inData.charAt(4) == '1')
    {
       digitalWrite(aircon, HIGH);
    }
    else if(inData.charAt(4) == '0')
    {
      digitalWrite(aircon, LOW);
    }

    //Television
    if(inData.charAt(5) == '1')
    {
       digitalWrite(television, HIGH);
    }
    else if(inData.charAt(5) == '0')
    {
      digitalWrite(television, LOW);
    }

    //Television
    if(inData.charAt(6) == '1')
    {
       digitalWrite(dimLight, HIGH);
    }
    else if(inData.charAt(6) == '0')
    {
      digitalWrite(dimLight, LOW);
    }
  }

  inData = "";  
}
