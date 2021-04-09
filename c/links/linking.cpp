#include <stdio.h>
#include "linking.h"
#include <thread>
#include <chrono>

int offset = 5;

int add(int a, int b)
{
    std::this_thread::sleep_for(std::chrono::seconds(2));
    return a + b + offset;
}

//int main(void) {
//    printf("result %d\n", add(2, 3));
//    return 0;
//}
