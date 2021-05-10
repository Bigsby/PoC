#ifndef __MYWRAPPER_H
#define __MYWRAPPER_H

#ifdef __cplusplus
extern "C"
{
#endif
    typedef int (*w_process_func)(int);

    typedef struct CppClass CppClass;

    CppClass *cppClass_new(int i);

    void cppClass_int_set(CppClass *v, int i);

    int cppClass_int_get(CppClass *v);

    void cppClass_set_process(CppClass *v, w_process_func func);

    int cppClasss_process(CppClass *v);

    const char* cppClass_description(CppClass *v, const char* caller);

    void cppClass_delete(CppClass *v);

#ifdef __cplusplus
}
#endif
#endif