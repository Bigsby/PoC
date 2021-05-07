default_target: all

cppClass.o: cppClass.cpp
	$(info in cppClass.o)
	g++ -c -o $@ $<

wrapper.a: wrapper.cpp
	$(info in wrapper.a)
	g++ -c -o $@ $?


mainC: main.c wrapper.a cppClass.o
	$(info in MainC)
	gcc $? -o $@ -lstdc++

c: mainC
	$(info in all)
	./$<
.PHONY: c

mainCpp: main.cpp cppClass.o
	g++ $? -o $@

cpp: mainCpp
	./$<
.PHONY: cpp

all: c cpp
.PHONY: all

clean:
	-rm cppClass.o wrapper.a mainC mainCpp
