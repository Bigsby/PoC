.assembly extern mscorlib {}
.assembly _ {}
.module FirstModule

.method static void Start() cil managed
{
	.entrypoint
	ldstr "Hello, World!"
	call void [mscorlib]System.Console::Write(string)
	ret
}

.module AnotherModule
.method static void Start() cil managed
{
	ldstr "Hello, World!"
	call void [mscorlib]System.Console::Write(string)
	ret
}