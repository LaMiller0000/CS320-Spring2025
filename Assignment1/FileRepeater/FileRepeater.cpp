#include <iostream>
#include <string>
#include <fstream>
#include <tuple>
#include "Filer.h"

using namespace std;


tuple<string, string> checkForFile()
{
    string path = "C:\\Users\\Lucius\\Desktop\\Settings.txt";
    string myText, a, b;
    ifstream file(path);
    if (file)
    {
        int i = 0;
        while (getline(file, myText)) 
        {
            switch (i)
            {
            case 0:
                a = myText;
                break;
            case 1:
                b = myText;
                break;
            default:
                break;
            }
            cout << myText << endl;
            i++;
        }
        file.close();
    }
    else 
    {
        ofstream oFile(path);
        cout << "There is no settings file\n";
    }
    return make_tuple(a, b);
}

int main()
{
    auto result = checkForFile();
    Filer* file = new Filer();
    file->SetBeginning(get<0>(result));
    file->SetDestination(get<1>(result));
    file->Looper(5);
}
