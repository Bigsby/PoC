const i18n = require("./services/i18n");

function test(locale) {
    i18n.setLocale(locale);
    console.log(`Testing ${locale}:`)
    console.log(i18n.__("lang"));
    const name = "Bigsby";
    console.log(i18n.__("hello", name));
    console.log(i18n.__("hello_n", { name: name }));
    console.log(i18n.__mf("hello_mf", { name: name }));
    console.log(i18n.__n('%s dog', 1));
    console.log(i18n.__n('%s dog', 2));
    console.log(i18n.__mf("people", { count: 1 }));
    console.log(i18n.__mf("people", { count: 2 }));
    console.log(i18n.__mf("gender_select", { name: "John", gender: "male"}));
    console.log(i18n.__mf("gender_select", { name: "Ann", gender: "female"}));
    console.log(i18n.__mf("gender_select", { name: "Blah"}));
    console.log(i18n.__("nested.value"));
    console.log();
}

i18n.getLocales().forEach(test);

process.exit(0);