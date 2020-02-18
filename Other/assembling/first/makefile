first: first.o
	ld -m elf_x86_64 -s -o $@ $? 

first.o: first.asm
	nasm -f elf64 -o $@ $?

clean: 
	rm first first.o

