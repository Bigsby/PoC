const i18n = require("i18n");

i18n.configure({
    locales: ["en", "pt"],
    defaultLocale: "en",
    directory: "./locales",
    autoReload: true, 
    objectNotation: true,
    logDebugFn: function (msg) {
        console.log('debug', msg);
    },
    logWarnFn: function (msg) {
        console.log('warn', msg);
    },

    logErrorFn: function (msg) {
        console.log('error', msg);
    }
});

module.exports = i18n;