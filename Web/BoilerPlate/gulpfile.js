const gulp = require("gulp");
const ts = require("gulp-typescript");
const less = require("gulp-less");
const concat = require("gulp-concat");

gulp.task("styles", function () {
    return gulp.src("styles/main.less")
        .pipe(less())
        .pipe(gulp.dest("wwwroot/styles"));
});

gulp.task("scripts", function () {
    return gulp.src("scripts/**/*ts")
        .pipe(ts({
            declarationFiles: true,
            noExternalResolve: true,
            noImplicitAny: true,
            experimentalDecorators: true
        }))
        //.js.pipe(concat("app.js"))
        .js.pipe(gulp.dest("wwwroot/scripts"));

});

gulp.task("default", ["scripts", "styles"]);