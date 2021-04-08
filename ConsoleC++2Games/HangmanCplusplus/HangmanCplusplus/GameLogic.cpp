#include "GameLogic.h"

GameLogic::GameLogic()
{
}

GameLogic::~GameLogic()
{
}

void GameLogic::reset()
{
	lives = 0;
	getText = texts[getRandomNumber(10) - 1];
	showText = getText;

	for (size_t i = 0; i < getText.size(); i++)
	{
		if (getText[i] != ' ')
		{
			showText[i] = '_';
		}
	}
}

void GameLogic::gameLoop()
{
	reset();
	while (true)
	{
		// Clear console
		cout << "\x1B[2J\x1B[H";

		// Print the hangman
		printHangman();

		// The text to guess
		cout << endl << showText << endl;

		// Do a Guess
		GuessALetter();

		// Check if we lose game
		LoseGame();

		// Check if we win game
		WinGame();
	}
}

void GameLogic::GuessALetter()
{
	bool correctGuessed = false;
	char mygus;
	cin >> mygus;

	for (size_t i = 0; i < getText.size(); i++)
	{
		if (getText[i] == mygus)
		{
			showText[i] = mygus;
			correctGuessed = true;
		}
	}

	lives += (correctGuessed ? 0 : 1);
}

void GameLogic::printHangman()
{
	string a1 = (lives < 1 ? " " : "|");
	string a2 = (lives < 2 ? " " : "O");
	string a3 = (lives < 3 ? " " : "/");
	string a4 = (lives < 4 ? " " : "|");
	string a5 = (lives < 5 ? " " : "\\");
	string a6 = (lives < 6 ? " " : "/");
	string a7 = (lives < 7 ? " " : "\\");

	string hangmanText[7] =
	{
		"    +---+",
		"    " + a1 + "   |",
		"    " + a2 + "   |",
		"   " + a3 + a4 + a5 + "  |",
		"   " + a6 + " " + a7 + "  |",
		"        |",
		"=======''']"
	};

	for (size_t i = 0; i < 7; i++)
	{
		cout << hangmanText[i] << endl;
	}
}

void GameLogic::WinGame()
{
	bool didWeWin = true;
	for (size_t i = 0; i < getText.size(); i++)
	{
		if (showText[i] == '_')
		{
			didWeWin = false;
		}
	}

	if (didWeWin == true)
	{
		cout << "\x1B[2J\x1B[H";
		printHangman();
		cout << endl << showText << endl;

		cout << endl << endl << "He will live your a winner!" << endl;
		cout << "Chick Enther to reset game" << endl;
		// this here get the input of any key you click as a int.
		int tmp = _getch();
		reset();
	}
}

void GameLogic::LoseGame()
{
	if (lives >= 7)
	{
		cout << "\x1B[2J\x1B[H";
		printHangman();
		cout << endl << showText << endl;

		cout << endl << endl << "He is death..." << endl;
		cout << "Chick Enther to reset game" << endl;
		// this here get the input of any key you click as a int.
		int tmp =_getch();
		reset();
	}
}

int GameLogic::getRandomNumber(int max)
{
	srand((unsigned)time(0));

	int random_integer;
	int lowest = 1;
	int highest = max;
	int range = (highest - lowest) + 1;
	random_integer = lowest + int(range * (rand() / (RAND_MAX + 1.0)));
	random_integer = lowest + int(range * (rand() / (RAND_MAX + 1.0)));

	return random_integer;
}
