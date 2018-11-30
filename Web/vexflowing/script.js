"use strict";
let VF = Vex.Flow;
let div = document.getElementById("target");
let renderer = new VF.Renderer(div, VF.Renderer.Backends.SVG);
renderer.resize(800, 200);
let context = renderer.getContext();

let stave = new VF.Stave(10, 10, 800);
stave.addClef("treble").addTimeSignature("4/4");
stave.setContext(context).draw();
stave.draw();

// var notes = [
//     // A quarter-note C.
//     new VF.StaveNote({clef: "treble", keys: ["c/4"], duration: "q" }),

//     // A quarter-note D.
//     new VF.StaveNote({clef: "treble", keys: ["d/4"], duration: "q" }),

//     // A quarter-note rest. Note that the key (b/4) specifies the vertical
//     // position of the rest.
//     new VF.StaveNote({clef: "treble", keys: ["b/4"], duration: "qr" }),

//     // A C-Major chord.
//     new VF.StaveNote({clef: "treble", keys: ["c/4", "e/4", "g/4"], duration: "q" })
//   ];

//   // Create a voice in 4/4 and add above notes
//   var voice = new VF.Voice({num_beats: 4,  beat_value: 4});
//   voice.addTickables(notes);

//   // Format and justify the notes to 400 pixels.
//   var formatter = new VF.Formatter().joinVoices([voice]).format([voice], 200);

//   // Render voice
//   voice.draw(context, stave);


var notes = [
  new VF.StaveNote({ clef: "treble", keys: ["f/5"], duration: "q" }),
  new VF.StaveNote({ clef: "treble", keys: ["d/4"], duration: "q" }),
  new VF.StaveNote({ clef: "treble", keys: ["b/4"], duration: "qr" }),
  new VF.StaveNote({ clef: "treble", keys: ["c/4", "e/4", "g/4"], duration: "q" })
];

var notes2 = [
  new VF.StaveNote({ clef: "treble", keys: ["c/4"], duration: "w" })
];

var notes3 = [
  new VF.TextNote({
    text: "D",
    subscript: "9",
    superscript: "6",
    duration: "w",
    slash_bass: "F"
  })
    .setStave(stave)
  //.setJustification(Vex.Flow.TextNote.Justification.CENTER)
];

// Create a voice in 4/4 and add above notes
var voices = [
  new VF.Voice({ num_beats: 4, beat_value: 4 }).addTickables(notes),
  new VF.Voice({ num_beats: 4, beat_value: 4 }).addTickables(notes2),
  new VF.Voice({ num_beats: 4, beat_value: 4 }).addTickables(notes3)
];

// Format and justify the notes to 400 pixels.
var formatter = new VF.Formatter().joinVoices(voices).format(voices, 400);

// Render voices
voices[0].draw(context, stave);
voices[1].draw(context, stave);
voices[2].draw(context, stave);