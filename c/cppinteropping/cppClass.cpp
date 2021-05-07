#include <iostream>
#include "cppClass.hpp"

using namespace std;

CppClass::CppClass(int i)
{
    this->_my_i = i;
    cout << ">>>>>> constructor" << endl;
}

CppClass::~CppClass()
{
    cout << ">>>>>> destructor" << endl;
}

void CppClass::int_set(int i)
{  
    _my_i = i;
}

int CppClass::int_get()
{
    return _my_i;
}

void CppClass::set_process(process_func func)
{
    _process = func;
}

int CppClass::process()
{
    return this->_process(_my_i);
}