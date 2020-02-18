section .text
    global _start

_start:
    mov edx,len             ; message length
    mov ecx,msg             ; message to write
    mov ebx,1               ; stdout file descriptor
    mov eax,4               ; sys_write
    int 0x80                ; interrupt

    mov edx,9               ; messsage length
    mov ecx,s2              ;message to write
    mov ebx,1               ; stdout file descriptor
    mov eax,4               ; sys_write
    int 0x80                ; interrupt

    mov eax,1               ; sys_exit
    int 0x80                ; interrupt

section .data
msg db 'Displaying 9 stars', 0xa    ; a message
len equ $ - msg                    ; message length
s2 times 9 db '*'

