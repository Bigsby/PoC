"use strict";

window.onload = function () {
    const ns = 'http://www.w3.org/2000/svg';
    const wordsUrl = "https://raw.githubusercontent.com/atebits/Words/master/Words/en.txt";
    let theText = document.getElementById("theText");

    function showWord(word) {
        return new Promise(resolve => {
            theText.innerHTML = "";
            let currentCharIndex = 0;
            (function showNextLetter() {
                setTimeout(() => {
                    if (currentCharIndex < word.length - 1) {
                        let newTSpan = document.createElementNS(ns, "tspan");
                        newTSpan.innerHTML = word[currentCharIndex++];
                        theText.appendChild(newTSpan);
                        showNextLetter();
                    } else {
                        setTimeout(resolve, 1000);
                    }
                }, 500);
            })();
        });
    }

    function selectWord(words) {
        let newIndex = Math.round(Math.random() * words.length);
        return words[newIndex];
    }

    function showWords(words) {
        let newWord = selectWord(words);
        showWord(newWord).then(() => showWords(words));
    }

    function loadWords() {
        return new Promise(resolve => {
            var xhttp = new XMLHttpRequest();
            xhttp.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    resolve(xhttp.responseText.split(/\r?\n/));
                }
            };
            xhttp.open("GET", wordsUrl, true);
            xhttp.send();
        });
    }

    loadWords().then(showWords);
}