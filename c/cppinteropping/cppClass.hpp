#ifndef __MYCLASS_H
#define __MYCLASS_H

#include <string>

typedef int (*process_func)(int);

class CppClass
{
private:
    int _my_i;
    int (*_process)(int);

public:
    CppClass(int);
    ~CppClass();
    void int_set(int);
    int int_get();
    void set_process(process_func);
    int process();
    std::string description(std::string);
};

#endif