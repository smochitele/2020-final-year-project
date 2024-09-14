/*
Name:    HomeAlarm.ino
Author:  Mochitele Seleme
*/

#pragma region Libraries
#include <Keypad.h>
#include <Key.h>
#include <LiquidCrystal.h>
#include <Ultrasonic.h>
#pragma endregion

#pragma region Pin Setups
//Keypad setup
const byte ROWS = 4; //four rows
const byte COLS = 4; //four columns
char keys[ROWS][COLS] =
{
	{ '1','2','3','A' },
	{ '4','5','6','B' },
	{ '7','8','9','C' },
	{ '*','0','#','D' }
};
byte rowPins[ROWS] = { 9, 8, 7, 6 }; //connect to the row pinouts of the kpd
byte colPins[COLS] = { 5, 4, 3, 2 }; //connect to the column pinouts of the kpd
Keypad keypad = Keypad(makeKeymap(keys), rowPins, colPins, ROWS, COLS);
// initialize the library with the numbers of the interface pins
LiquidCrystal lcd(13, 12, 11, 10, 1, 0);
#pragma endregion

#pragma region Variables
//Variables to be used
int Counter = 0;
int NumDetections = 1;
String Password = "1234";
const int PIN_LENGTH = 4;
const int MAX_DISTANCE = 51;
char InputPassword[PIN_LENGTH];
bool LockeState = true;
bool ActiveAlarm = false;
int AccessGranted = 13;
int TrigPin = A0;
int EchoPin = A1;
int LedPin = A2;
int Distance;
Ultrasonic ultrasonic(TrigPin, EchoPin);
#pragma endregion

#pragma region Program Boot
void setup()
{
	lcd.begin(16, 2);
	pinMode(LedPin, OUTPUT);
	SetLocked(LockeState);
}
#pragma endregion

void loop()
{
	Distance = ultrasonic.Ranging(CM);
	char PressedKey = keypad.getKey();
	//Checking if a correct key was pressed
	if (LockeState)
	{
		if (PressedKey)
			CheckPin(PressedKey);
		if (IsInstruded(Distance))
		{
			if (NumDetections > 0)
			{
				lcd.clear();
				lcd.print(" INTRUDER ALERT ");
				lcd.setCursor(0, 1);
				lcd.print("ENTER PIN:");
				lcd.setCursor(10, 1);
				lcd.blink();
				ActiveAlarm = true;
				NumDetections--;
			}
		}
		if (ActiveAlarm)
		{
			digitalWrite(LedPin, HIGH);
			delay(80);
			digitalWrite(LedPin, LOW);
			delay(40);
		}
	}
	else
	{
		if (PressedKey == '#')
		{
			lcd.clear();
			lcd.print("Security  Active");
			delay(2500);
			SetLocked(LockeState);
		}
	}
}
//Checking if the the password is correct
bool IsCorrectPassword(char Input[], String CorrectPassord)
{
	for (int i = 0; i < PIN_LENGTH; i++)
	{
		if (Input[i] != CorrectPassord[i])
			return false;
	}
	return true;
}
//Password input
void EnterPassword(char PressedKey, char Input[], int& Index)
{
	Input[Index] = PressedKey;
	Index++;
}
//Locking the alarm system
void SetLocked(bool& IsLocked)
{
	lcd.clear();
	lcd.print("**Home Secured**");
	lcd.setCursor(0, 1);
	lcd.print("ENTER PIN:");
	lcd.setCursor(10, 1);
	lcd.blink();
	IsLocked = true;
}
//Unlocking the alarm system
void SetUnlocked(bool& IsLocked)
{
	lcd.clear();
	lcd.println("Community Safety");
	lcd.setCursor(0, 1);
	lcd.print("Hold # To Secure");
	IsLocked = false;
	ActiveAlarm = false;
	digitalWrite(LedPin, LOW);
}
//Checking if theres movements
bool IsInstruded(int Distance)
{
	if (Distance < MAX_DISTANCE)
		return true;
	else
		return false;
}
//If the user enters an incorrect pin
void AccessDenied()
{
	lcd.setCursor(0, 0);
	lcd.clear();
	lcd.print(" Incorrect  PIN ");
	lcd.setCursor(0, 1);
	lcd.print("*ACCESS  DENIED*");
	delay(4000);
}
//Checking if the pin input is valid
void CheckPin(char PressedKey)
{
	EnterPassword(PressedKey, InputPassword, Counter);
	lcd.print('*');
	if (Counter >= PIN_LENGTH)
	{
		if (IsCorrectPassword(InputPassword, Password))
		{
			lcd.clear();
			lcd.print(" ACCESS GRANTED ");
			delay(4000);
			NumDetections = 1;
			SetUnlocked(LockeState);
		}
		else
		{
			AccessDenied();
			SetLocked(LockeState);
		}
		Counter = 0;
	}
}