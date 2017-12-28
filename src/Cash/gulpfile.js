/// <binding BeforeBuild='scripts' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    requirejsOptimize = require('gulp-requirejs-optimize'),
    obfuscate = require('gulp-obfuscate'),
    babel = require('gulp-babel');

var paths = {
    webroot: "./wwwroot/",
    scriptsroot: "./Scripts/App/",
    dist: "./Scripts/dist/"
};


paths.js = paths.scriptsroot + "**/*.js";
paths.minJs = paths.scriptsroot + "**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

// gulp.task("clean:js", function (cb) {
//     rimraf(paths.concatJsDest, cb);
// });

// gulp.task("clean:css", function (cb) {
//     rimraf(paths.concatCssDest, cb);
// });

// gulp.task("clean", ["clean:js", "clean:css"]);

// gulp.task("min:js", function () {
//     return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
//         .pipe(concat(paths.concatJsDest))
//         .pipe(uglify())
//         .pipe(gulp.dest("."));
// });

// gulp.task("min:css", function () {
//     return gulp.src([paths.css, "!" + paths.minCss])
//         .pipe(concat(paths.concatCssDest))
//         .pipe(cssmin())
//         .pipe(gulp.dest("."));
// });

// gulp.task('scripts', function () {
//     return gulp.src(jsModules.modules.map(function(x) { return paths.scriptsroot + "Modules/" + x; }))
//         .pipe(requirejsOptimize(function(file) {
//             return {
//                 baseUrl: paths.scriptsroot,
//                 include: 'Modules/Cash/' + file.relative,
//                 optimize: 'none',
//                 mainConfigFile: 'Scripts/App/config.js'

//             };
//         }))
//         //.pipe(babel({
//         //  minified: false
//         //}))
//         //.pipe(obfuscate())
//         .pipe(gulp.dest(paths.dist));
// });

 gulp.task("min:js", function () {
     return gulp.src(paths.js, {base: paths.scriptsroot})
         .pipe(babel({minified:true}))    
         .pipe(gulp.dest(paths.dist));
 });


 gulp.task("watch", function() {
     return gulp.watch(paths.js, ['min:js']);
 })
// gulp.task("min", ["min:js", "min:css"]);