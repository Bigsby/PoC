current_dir = $(shell pwd)

default_target: all

cppClass.o: cppClass.cpp
	$(info in cppClass.o)
	g++ -c -fPIC -o $@ $<

libwrapper.so: wrapper.cpp cppClass.o
	$(info in wrapper.a)
	g++ -shared -o $@ $?


mainC: main.c libwrapper.so
	$(info in MainC)
	gcc $< -o $@ -L. -lwrapper

c: mainC
	$(info in all)
	LD_LIBRARY_PATH=${current_dir} ./$<
.PHONY: c

mainCpp: main.cpp cppClass.o
	g++ $? -o $@

cpp: mainCpp
	./$<
.PHONY: cpp

all: c cpp
.PHONY: all

clean:
	-rm cppClass.o libwrapper.so mainC mainCpp
