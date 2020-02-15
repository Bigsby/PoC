const Sequelize = require("sequelize");

const sequelize = new Sequelize({
    dialect: "mysql",
    host: "localhost",
    username: "bigsby",
    password: "m!8r#9u1S",
    database: "test_db"
});

function createSchema(name) {
    sequelize.createSchema(name)
        .then(() => {
            console.log("creating schema");
            sequelize.sync({ force: true })
                .then(() => {
                    console.log("synchronized!");
                })
                .catch(err => {
                    console.log("failed synchronizing: " + err);
                });
        })
        .catch(err => {
            console.log("schema NOT created: " + err);
        });
}

function createUserModel(force) {
    return new Promise(resolve => {
        sequelize.define("user", {
            id: {
                type: Sequelize.INTEGER,
                autoIncrement: true,
                primaryKey: true
            },
            name: {
                type: Sequelize.STRING,
                allowNull: false,
                unique: true
            },
            birthday: {
                type: Sequelize.DATE,
                allowNull: false
            },
            other: {
                type: Sequelize.TEXT,
                allowNull: true
            }
        });
        sequelize.model("user").sync({force: force}).then(() => resolve());
    });
}

function createUser(name, birthday, other) {
    sequelize.model("user").create({name: name, birthday: birthday, other: other}).then(user => {
        console.log(`created user ${user.name}, born ${user.birthday} (${user.other}) with id ${user.id}`);
    });
}

sequelize.authenticate()
    .then(() => {
        console.log("in!");
        //createSchema("test_db2");
        createUserModel().then(() => {
            createUser("John Cleese", "1930-10-27");
            createUser("Eric Idle", new Date(1943, 2, 29));
            createUser("Terry Jones", new Date(1942, 1, 1));
        });


    })
    .catch(err => {
        console.log("not in: " + err);
    });