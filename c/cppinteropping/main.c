#include "wrapper.h"
#include <stdio.h>

int c_process(int x)
{
    printf(">>>>>> Processing in C\n");
    return x + 20;
}

int main(int argc, char *argv[])
{
    struct CppClass *c = cppClass_new(10);
    printf(">>>>>> 1st Result in C: %d\n", cppClass_int_get(c));
    cppClass_int_set(c, 5);
    printf(">>>>>> 2nd Result in C: %d\n", cppClass_int_get(c));
    cppClass_set_process(c, c_process);
    printf(">>>>>> Process Result in C: %d\n", cppClasss_process(c));
    const char *descrition = cppClass_description(c, "C");
    printf(">>>>>> Description in C: %s\n", descrition);
    cppClass_delete(c);
}