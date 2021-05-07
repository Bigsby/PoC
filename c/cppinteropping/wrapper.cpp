#include<iostream>
#include "cppClass.hpp"
#include "wrapper.h"

extern "C"
{
    CppClass *cppClass_new(int i)
    {
        return new CppClass(i);
    }

    void cppClass_int_set(CppClass *v, int i)
    {
        v->int_set(i);
    }

    int cppClass_int_get(CppClass *v)
    {
        return v->int_get();
    }

    void cppClass_set_process(CppClass *v, w_process_func func)
    {
        v->set_process(func);
    }

    int cppClasss_process(CppClass *v)
    {
        return v->process();
    }

    void cppClass_delete(CppClass *v)
    {
        delete v;
    }
}