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

//https://blogs.msdn.microsoft.com/junfeng/2004/07/15/multimodule-assemblies/
//https://blogs.msdn.microsoft.com/junfeng/2005/02/12/netmodule-vs-assembly/