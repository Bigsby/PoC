"use strict";
(function () {
    requirejs.config({
        paths: {
            "hljs": "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.10.0/highlight.min",
            "hljs-fsharp": "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.10.0/languages/fsharp.min",
            "hljs-go": "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.4.0/languages/go.min",
            "hljs-powershell": "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.10.0/languages/powershell.min",
            "hljs-vbnet": "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.10.0/languages/vbnet.min"
        },
        "shim":{
            "hljs-fsharp":[
                "hljs"
            ],
            "hljs-go":[
                "hljs"
            ],
            "hljs-powershell":[
                "hljs"
            ],
            "hljs-vbnet":[
                "hljs"
            ]
        }
    });

    var files = {
        languages: [
            {
                language: "bash",
                hideLineNumbers: true,
                code: `> for a in: node filename.js`
            },
            {
                language: "text",
                hideLineNumbers: true,
                code: `This is the sentence.
This is the sentence.
This is another sentence.`
            },
            {
                language: "javascript",
                code: `console.log("Hello, World!");`
            },
            {
                language: "cpp",
                code: `#include <iostream>

int main()
{
	std::cout << "Hello, World!";
	return 0;
}`
            },
            {
                language: "python",
                code: `print("Hello, World!")`
            },
            {
                language: "fsharp",
                code: `printfn "Hello, World!"`
            },
            {
                language: "go",
                code: `package main

import "fmt"

func main() {
	fmt.Println("Hello, World!")
}`
            },
            {
                language: "ruby",
                code: `puts 'Hello, Comments!'

# This is a comment

=begin
This is a 
multiline comment
=end`
            },
            {
                language: "csharp",
                code: `class Program
{
	public static void Main()
	{
		System.Console.Write("Hello, World!");
	}
}`
            },
            {
                language: "java",
                code: `public class hello {

    public static void main(String[] args) {
        System.out.println("Hello, World!");
    }

}`
            },
            {
                language: "perl",
                code: `print "Hello, World!";`
            },
            {
                language: "powershell",
                code: `Write-Host "Hello, World!"`
            },
            {
                language: "php",
                code: `<?php echo "Hello, World!"; ?>`
            },
            {
                language: "vbnet",
                code: `Public Module Program
	Sub Main()
		System.Console.Write("Hello, Comments!")

		' This is a comment
		REM This is a comment
		
		' Multi line comments
		' are not supported
	End Sub
End Module`
            }
        ]
    };

    var container = document.getElementById("container");
    requirejs([
        "hljs-go",
        "hljs-fsharp",
        "hljs-powershell",
        "hljs-vbnet"
    ],
        function () {
            files.languages.forEach(function (lang) {

                var pre = document.createElement("pre");
                if (!lang.hideLineNumbers)
                    pre.className = "line-numbers";
                var code = document.createElement("code");
                code.className = lang.language;
                code.textContent = lang.code;
                pre.appendChild(code);
                hljs.highlightBlock(code);
                container.appendChild(pre);

            });
        });
})();