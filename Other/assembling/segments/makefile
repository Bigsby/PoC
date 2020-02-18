segs: segs.o
	ld -m elf_x86_64 -s -o $@ $? 

segs.o: segs.asm
	nasm -f elf64 -o $@ $?

clean: 
	rm segs segs.o

