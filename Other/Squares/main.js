"use strict";
const readline = require("readline");
const emptyValue = " ";

String.prototype.paddingLeft = function (character, length) {
    return String(new Array(length + 1).join(character) + this).slice(-length);
};


function createEmptyMatrix(side, fill) {
    var result = new Array(side);

    for (var rowIndex = 0; rowIndex < side; rowIndex++) {
        var newRow = new Array(side);
        if (fill)
            newRow.fill(fill);
        result[rowIndex] = newRow;
    }

    return result;
}

function calculateFillMatrix(side) {
    if (side % 2 === 0)
        throw new Error("Side needs to be odd number.");

    var calcSide = side * 2 - 1;
    var calc = createEmptyMatrix(calcSide, emptyValue);
    calc.middlePosition = Math.floor(calcSide / 2);

    var current = { row: 0, column: calc.middlePosition, startRow: 0, startColumn: calc.middlePosition };

    for (var count = 1; count <= side * side; count++) {
        calc[current.row][current.column] = count;

        if (count % side != 0) {
            current.row++;
            current.column--;
        }
        else {
            current.row = ++current.startRow;
            current.column = ++current.startColumn;
        }
    }

    return calc;
}

function calculateFinal(matrix, side) {
    var outside = (side - 1) / 2;

    function processOutside(rowStart, rowEnd, columnStart, columnEnd, rowIncrement, columnIncrement) {
        for (var rowIndex = rowStart; rowIndex < rowEnd; rowIndex++) {
            for (var columnIndex = columnStart; columnIndex < columnEnd; columnIndex++) {
                var value = matrix[rowIndex][columnIndex];

                if (value == emptyValue)
                    continue;

                matrix[rowIndex + side * rowIncrement][columnIndex + side * columnIncrement] = value;
                matrix[rowIndex][columnIndex] = emptyValue;
            }
        }
    }

    processOutside(0, outside, 0, matrix.length, 1, 0);
    processOutside(matrix.length - outside, matrix.length, 0, matrix.length, -1, 0);
    processOutside(0, matrix.length, 0, outside, 0, 1);
    processOutside(0, matrix.length, matrix.length - outside, matrix.length, 0, -1);

    var result = createEmptyMatrix(side);
    for (var rowIndex = outside; rowIndex < outside + side; rowIndex++)
        for (var columnIndex = outside; columnIndex < outside + side; columnIndex++)
            result[rowIndex - outside][columnIndex - outside] = matrix[rowIndex][columnIndex];

    result.sum = matrix[matrix.middlePosition].reduce(function (total, value) {
        return value == emptyValue ?
            total :
            total + value;
    }, 0);

    return result;
}

function displayMatrix(matrix) {
    var length = Math.max(Math.pow(matrix.length, 2).toString().length + 1, 2);

    for (var rowIndex = 0; rowIndex < matrix.length; rowIndex++) {
        var rowText = "";
        for (var columnIndex = 0; columnIndex < matrix.length; columnIndex++) {
            rowText += matrix[rowIndex][columnIndex].toString().paddingLeft(" ", length);
        }
        process.stdout.write(rowText + "\n");
    }
}

function askSide() {
    var rl = readline.createInterface({
        input: process.stdin,
        output: process.stdout
    });

    rl.question("Input side (odd):", function (input) {
        rl.close();
        var side = parseInt(input);

        if (side % 2 == 0) {
            console.log("Side not odd!");
            process.exit();
        }
        
        var calcMatrix = calculateFillMatrix(side);
        //displayMatrix(calcMatrix);
        var finalMatrix = calculateFinal(calcMatrix, side);
        displayMatrix(finalMatrix);
        console.log("Sum: " + finalMatrix.sum);
    });
}

askSide();