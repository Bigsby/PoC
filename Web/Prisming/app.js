"use strict";
(function () {
    requirejs.config({
        paths: {
            "prism": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-core.min",
            "prism-bash": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-bash.min",
            "prism-linenumber": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/plugins/line-numbers/prism-line-numbers.min",
            "prism-clike": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-clike.min",
            "prism-javascript": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-javascript.min",
            "prism-c": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-c.min",
            "prism-cpp": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-cpp.min",
            "prism-python": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-python.min",
            "prism-fsharp": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-fsharp.min",
            "prism-go": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-go.min",
            "prism-ruby": "prism-ruby",
            "prism-csharp": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-csharp.min",
            "prism-java": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-java.min",
            "prism-perl": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-perl.min",
            "prism-powershell": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-powershell.min",
            "prism-php": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-php.min",
            "prism-basic": "https://cdnjs.cloudflare.com/ajax/libs/prism/1.6.0/components/prism-basic.min",
            "prism-vbnet": "prism-vbnet"
        },
        shim: {
            "prism": {
                "exports": "Prism"
            },
            "prism-bash": [
                "prism"
            ],
            "prism-linenumber": [
                "prism",
            ],
            "prism-clike": [
                "prism"
            ],
            "prism-javascript": [
                "prism-clike"
            ],
            "prism-c": [
                "prism-clike"
            ],
            "prism-cpp": [
                "prism-c"
            ],
            "prism-python": [
                "prism"
            ],
            "prism-fsharp": [
                "prism-clike"
            ],
            "prism-go": [
                "prism-clike"
            ],
            "prism-ruby": [
                "prism-clike"
            ],
            "prism-csharp": [
                "prism-clike"
            ],
            "prism-java": [
                "prism-clike"
            ],
            "prism-perl": [
                "prism"
            ],
            "prism-powershell": [
                "prism"
            ],
            "prism-php": [
                "prism-clike"
            ],
            "prism-basic":[
                "prism"
            ],
            "prism-vbnet": [
                "prism-basic"
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
        "prism-bash",
        "prism-linenumber",
        "prism-javascript",
        "prism-cpp",
        "prism-python",
        "prism-fsharp",
        "prism-go",
        "prism-ruby",
        "prism-csharp",
        "prism-java",
        "prism-perl",
        "prism-powershell",
        "prism-php",
        "prism-vbnet"
    ],
        function () {
            files.languages.forEach(function (lang) {

                var pre = document.createElement("pre");
                if (!lang.hideLineNumbers)
                    pre.className = "line-numbers";
                var code = document.createElement("code");
                code.className = "language-" + lang.language;
                code.textContent = lang.code;
                pre.appendChild(code);
                Prism.highlightElement(code);
                container.appendChild(pre);

            });
        });
})();