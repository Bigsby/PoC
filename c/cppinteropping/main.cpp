#include "cppClass.hpp"
#include <iostream>

using namespace std;

int main(int argc, char *argv[])
{
    CppClass *c = new CppClass(1);
    cout << ">>>>>> 1st Result in C++: " << c->int_get() << endl;
    c->int_set(3);
    cout << ">>>>>> 2nd Result in C++: " << c->int_get() << endl;
    c->set_process([](int x) {
        cout << ">>>>>> Processing in C++" << endl;
        return x + 3;
    });
    int processResult = c->process();
    cout << ">>>>>> Process Result in C++: " << processResult << endl;
    cout << ">>>>>> Descrition in C++: " << c->description("C++") << endl;
    delete c;
}