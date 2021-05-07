#ifndef __MYCLASS_H
#define __MYCLASS_H

typedef int (*process_func)(int);

class CppClass
{
private:
    int _my_i;
    int (*_process)(int);

public:
    CppClass(int i);
    ~CppClass();
    void int_set(int i);
    int int_get();
    void set_process(process_func);
    int process();
};

#endif