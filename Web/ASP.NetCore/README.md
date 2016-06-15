# Manual Web Core RC2

This is to test and describe the steps to create a ASP.Net Core web application

1. Installations
    1. [DotNet Core](https://www.microsoft.com/net/core)
	2. [NodeJS with npm](https://nodejs.org)
	3. [Visual Studio Code](https://code.visualstudio.com)
	
restart

> npm install npm -g
> npm install yo -g
> npm install generator-aspnet -g
> yo aspnet
-> Select Empty Web Application
-> Enter «appName>
(as instructed by yo)
> cd core
> dotnet restore
> dotnet build
> dotnet run
test browser http://localhost:5000/
Open VS Code
> code .

