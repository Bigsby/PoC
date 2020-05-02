CREATE TABLE grandparents (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    [name] TEXT NOT NULL
);

CREATE TABLE parents (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    [name] TEXT NOT NULL,
    grandparent_id INTEGER NOT NULL,
    FOREIGN KEY (grandparent_id) REFERENCES grandparents (id) ON UPDATE RESTRICT ON DELETE RESTRICT
);
CREATE INDEX 'parents_grandparent_id' ON 'parents'('grandparent_id');

CREATE TABLE types (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    [name] TEXT NOT NULL
);

CREATE TABLE children (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    [name] TEXT NOT NULL,
    parent_id INTEGER NOT NULL,
    type_id INTEGER NOT NULL,
    FOREIGN KEY (parent_id) REFERENCES parents (id) ON UPDATE RESTRICT ON DELETE RESTRICT,
    FOREIGN KEY (type_id) REFERENCES types (id) ON UPDATE RESTRICT ON DELETE RESTRICT
);
CREATE INDEX 'children_parent_id' ON 'children'('parent_id');
CREATE INDEX 'children_type_id' ON 'children'('type_id');
