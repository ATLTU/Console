#pragma once

#include <string>
#include <iostream>
#include <ctime> 
#include <conio.h>

using namespace std;

class GameLogic
{
public:
	GameLogic();
	~GameLogic();
	void reset();
	void gameLoop();
	void GuessALetter();
	void printHangman();
	void WinGame();
	void LoseGame();
	int getRandomNumber(int max);

private:
	string getText;
	string showText;
	int lives = 0;
	string texts[10] = 
	{ 
		"fisk",
		"kasper",
		"jamen det da nice",
		"lol ok er det da godt",
		"john",
		"peter",
		"thomas",
		"hop lige der over ok",
		"han er ikke sur er han gald",
		"super det nice"
	};


};
