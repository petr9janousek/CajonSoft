//@TODO Nulovani pri spusteni -- nulovani g v ose kolmo dolu

#include <Wire.h>
/*
   Full scale of potentiometer
   Opt val between 1024-4096?
*/
#define LOW_FS 2000
#define MID_FS 2000
#define TOP_FS 2000
/*
   Range limit
*/
#define LOW_OFS 1000

#define DELAY       25
#define TIMEOUT     100
/*
  I2C sensor calib
*/
#define ACCELI2CADR     0x68
#define SCALEREGADR     0x1C

#define SCALE2G         B00
#define SCALE4G         B01
#define SCALE8G         B10
#define SCALE16G        B11

#define ZAXEL_H_REG     0x3F
#define XAXEL_H_REG     0x3B
#define XOFFSET_H_REG   0x06
#define XOFFSET_L_REG   0x07

/*
   Nastaveni pinu ledek
   D7 - RED
   D2 - GREEN
   D3 - BLUE
*/
const int LED_LEVEL1 = 12;
const int LED_LEVEL2 = 7;
const int LED_LEVEL3 = 2;

/*
   Nastaveni pinu pot
   A0 - channel 0
   A1 - channel 1
   A2 - channel 2
*/
#define POT_LEVEL1 0
#define POT_LEVEL2 1
#define POT_LEVEL3 2

volatile int32_t delayState;
volatile int level;
const int noColor = '0';
char lowColor = '1';
char midColor = '2';
char topColor = '3';
char colors[3];
int32_t levelLow;
int32_t levelMid;
int32_t levelTop;

int32_t calib;

int timer2_counter;

void setup() {
  ledInit();
  adcInit();
  wireInit();

  delay(200);
  calib = GetValue();

  TIMSK2 = (TIMSK2 & B11111110) | 0x01; // povolení časovače

  Serial.begin(115200);
  Serial.setTimeout(TIMEOUT);
}

void loop() {
  levelLow = calib + (((1023 - adcRead(POT_LEVEL1)) * LOW_FS) / 1024) + LOW_OFS;
  levelMid = calib + (((1023 - adcRead(POT_LEVEL2)) * MID_FS) / 1024) + levelLow;
  levelTop = calib + (((1023 - adcRead(POT_LEVEL3)) * TOP_FS) / 1024) + levelMid;

  int32_t acc;
  int maxlevel = 0;

  acc = GetValue();
  if (acc > levelTop) {
    level = 3;
    delayState = DELAY;
  }

  if (acc > levelMid && level < 3) {
    level = 2;
    delayState = DELAY;
  }
  else if (acc > levelLow && level < 2) {
    level = 1;
    delayState = DELAY;
  }

  ledChange();
}

void serialEvent() {
  while (Serial.available() > 0)
  {
    String message;
    char incoming = Serial.read();

    if (incoming == '*')
    {
      message = Serial.readStringUntil('*');

      if (message == "IDENT")
      {
        Serial.print("*CAJON*");
      }
      if (message == "WRITE")
      {
        Serial.print(lowColor);
        Serial.print(midColor);
        Serial.print(topColor);
        Serial.print("*DONE*");
      }
      if (message == "READ")
      {
        bool inrange = true;
        Serial.readBytes(colors, 3);
        for (auto& c : colors)
        {
          if (!('0' <= c && c <= '9'))
          {
            inrange = false;
            break;
          }
        }
        if (inrange)
        {
          lowColor = colors[0];
          midColor = colors[1];
          topColor = colors[2];
          Serial.print("*DONE*");
        }
      }
    }
  }
}

int32_t GetValue() {

  int32_t CalX;

  Wire.beginTransmission(ACCELI2CADR);    // Volani I2C zarizeni na andrese ACCELI2CADR
  Wire.write(XAXEL_H_REG);                // Nastaveni adresy registru ze ktereho chci cist (X acc register)
  Wire.endTransmission(false);

  Wire.requestFrom(ACCELI2CADR, 2, true); // Zadost o data ze 2 registru od zarizeni ACCELI2CADR
  CalX = Wire.read() << 8 | Wire.read();  // Ukladani dat z registru do hodnoty

  if (CalX > 0xFFF) {               // Dvojkovy doplnek
    CalX = -(0xFFF - CalX + 1);
  }

  if (CalX < 0) {             // Absolutni hodnota
    CalX = -CalX;
  }
  //Serial.println(CalX);
  return CalX;

}

ISR(TIMER2_OVF_vect) {
  delayState--;
  if (delayState > 0)
  {
    return;
  }
  delayState = DELAY;

  if (level > 0)
  {
    level = 0;
  }
  return;
}

void ledChange() {
  //Serial.println(level);
  switch (level)
  {
    case 0:
      blinkColor(noColor);
      break;
    case 1:
      blinkColor(lowColor);
      break;
    case 2:
      blinkColor(midColor);
      break;
    case 3:
      blinkColor(topColor);
      break;
    default:
      blinkColor(noColor);
      break;
  }
}

void blinkColor(char color) {
  switch (color) {
    case '0': //none
      digitalWrite(LED_LEVEL3, LOW);
      digitalWrite(LED_LEVEL2, LOW);
      digitalWrite(LED_LEVEL1, LOW);
      break;
    case '1': //red
      digitalWrite(LED_LEVEL1, HIGH);
      digitalWrite(LED_LEVEL2, LOW);
      digitalWrite(LED_LEVEL3, LOW);
      break;
    case '2': //green
      digitalWrite(LED_LEVEL2, HIGH);
      digitalWrite(LED_LEVEL3, LOW);
      digitalWrite(LED_LEVEL1, LOW);
      break;
    case '3': //blue
      digitalWrite(LED_LEVEL3, HIGH);
      digitalWrite(LED_LEVEL1, LOW);
      digitalWrite(LED_LEVEL2, LOW);
      break;
    case '4': //cyan
      digitalWrite(LED_LEVEL1, LOW);
      digitalWrite(LED_LEVEL2, HIGH);
      digitalWrite(LED_LEVEL3, HIGH);
      break;
    case '5': //magenta
      digitalWrite(LED_LEVEL2, LOW);
      digitalWrite(LED_LEVEL1, HIGH);
      digitalWrite(LED_LEVEL3, HIGH);
      break;
    case '6': //yellow
      digitalWrite(LED_LEVEL3, LOW);
      digitalWrite(LED_LEVEL2, HIGH);
      digitalWrite(LED_LEVEL1, HIGH);
      break;
    case '7': //white
      digitalWrite(LED_LEVEL1, HIGH);
      digitalWrite(LED_LEVEL2, HIGH);
      digitalWrite(LED_LEVEL3, HIGH);
      break;
    default:
      digitalWrite(LED_LEVEL3, LOW);
      digitalWrite(LED_LEVEL2, LOW);
      digitalWrite(LED_LEVEL1, LOW);
      break;
  }
}

/*
    Initialize sensor on I2C
*/
inline void wireInit(void) {
  Wire.begin();

  Wire.beginTransmission(ACCELI2CADR);     // Volani I2C zarizeni na andrese ACCELI2CADR
  Wire.write(0x6B);                  // Talk to the register 6B
  Wire.write(0x00);                  // Make reset - place a 0 into the 6B register
  Wire.endTransmission(true);

  Wire.beginTransmission(ACCELI2CADR);     // Volani I2C zarizeni na andrese ACCELI2CADR
  Wire.write(SCALEREGADR);                 // Nastaveni adresy registru do ktere chci zapisovat (Acc. scale register)
  Wire.write(SCALE2G);                     // Nastaveni citlivosti
  Wire.endTransmission(true);

  //Serial.println("Wire init DONE");
  delay(500);                                                                                                                   //////////////////////???????////////////////////////
}

/*
    Initialize ADC
*/

inline void adcInit(void) {
  ADMUX |= (1 << REFS0);
  ADCSRA |= (1 << ADEN) | (1 << ADPS2) | (1 << ADPS1) | (1 << ADPS0);
  //Serial.println("ADC init DONE");
}
/*
    Initialize LEDs
*/
inline void ledInit(void) {
  pinMode(LED_LEVEL1, OUTPUT);
  pinMode(LED_LEVEL2, OUTPUT);
  pinMode(LED_LEVEL3, OUTPUT);
}

/*
    Reads an integer from ADC and returns
    60-260us conversion time
*/
int32_t adcRead(uint8_t channel) {  //cislo kanalo AD-XY
  ADMUX &= ~(0x0F);
  ADMUX |= (channel & 0x0F);
  ADCSRA |= (1 << ADSC);
  while ((ADCSRA & (1 << ADIF)) == 0)
    ;
  return ADC;
  //return analogRead(channel);
}
