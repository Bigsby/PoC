class Artist {
    name: string;
}

class MusicDefinition {
    enum: Key;
    mode: Mode;
    octave: number;
    cleff: Clef;
    time?: TimeSignature;
    tempo?: string | number;
}

class Music {
    title: string;
    alternative_titles: string[];
    composer: Artist | Artist[];
    lyrics:  Artist | Artist[];
    definition: MusicDefinition;
}

enum Key {
    A, B, C, D, E, F, G
}

class Clef {
    key: Key.G | Key.F | Key.C;
    line?: number;
}

enum ModeBase {
    Major,
    Harmonic,
    Melodic
}

class Mode {
    base: ModeBase;
    degree: number;
}

class TimeSignature {
    count: number;
    quality: 2 | 4 | 8 | 16;
}

class Bar {
    defintion?: MusicDefinition;
    notes: Note[];
}

class Note {

}