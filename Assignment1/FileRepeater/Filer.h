#pragma once
#include <string>

using namespace std;

class Filer
{
public:
	Filer();
	void CopyAndPaste(int num);
	void Looper(int num);
	void SetDestination(string x);
	void SetBeginning(string x);
private:
	string destination;
	string beginning;
};

