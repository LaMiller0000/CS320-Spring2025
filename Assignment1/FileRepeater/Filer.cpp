#include "Filer.h"
#include <string>
#include <fstream>
#include <regex>
#include <climits>
#include <filesystem>

using namespace std;

Filer::Filer()
{

}
void Filer::SetBeginning(string x)
{
	beginning = x;
}
void Filer::SetDestination(string x)
{
	destination = x;
}
void Filer::CopyAndPaste(int num)
{
	ifstream src(beginning, ios::binary);
	ofstream dest(destination, ios::binary);

	// Check if both files opened successfully
	if (!src || !dest) {
		//cerr << "Error opening files!" << std::endl;
		return;
	}

	// Copy file content byte by byte
	dest << src.rdbuf();

	//cout << "File copied and renamed successfully!" << std::endl;
}
void Filer::Looper(int num = 0)
{
	int i = 0;
	ifstream file(destination);
	{
		string filePath = destination; // Example path
		size_t pos = destination.find_last_of("/\\");
		filePath = (pos != std::string::npos) ? destination.substr(0, pos) : destination;

		regex numberRegex("\\d+");
		smatch match;
		int largestNumber = INT_MIN;

		for (auto iter = sregex_iterator(filePath.begin(), filePath.end(), numberRegex); iter != std::sregex_iterator(); ++iter)
			largestNumber = max(largestNumber, std::stoi(iter->str()));
		num, i + largestNumber;
	}
	for (i < num; i++;)
	{
		CopyAndPaste(i);
	}
}