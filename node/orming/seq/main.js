const db = require("./models/index").sequelize;

async function main() {
    const users = await db.model("users").findAll();

    console.log("All users:", JSON.stringify(users, null, 4));
    for (const prop in users[0].dataValues) {
        console.log(`${prop}: ${typeof users[0].dataValues[prop]}`);
    }
    
    users.forEach(user => {
        console.log(JSON.stringify(user));
        console.log(JSON.stringify(user.birthday));
        console.log(typeof user.birthday);
        console.log(user.birthday.year);
    });    
}

main();
