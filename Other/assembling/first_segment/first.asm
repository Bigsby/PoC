section .text
    global _start

_start:
    mov edx,len     ; message length
    mov ecx,msg     ; message start point in memory
    mov ebx,1       ; stdout file descriptor
    mov eax,4       ; sys_write system call
    int 0x80        ; call interrupt

    mov eax,1       ; sys_exit system call
    int 0x80        ; call interrupt

segment .data
msg db "Hello, Bigsby!", 0xa    ; the message to write
len equ $ - msg     ; message length

