var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

var webroot = "./wwwroot/";

var paths = {
    js: "Content/js/**/*.js",
    minJs: "Content/js/**/*.min.js",
    css: "Content/css/**/*.css",
    minCss: "Content/css/**/*.min.css",
    concatJsDest: webroot + "js/site.min.js",
    concatCssDest: webroot + "css/site.min.css",
    libsConcatJsDest: webroot + "js/libs.min.js",
    libsConcatCssDest: webroot + "css/libs.min.css",
    fontsDest: webroot + "fonts",
    imagesDest: webroot + "images",
    jsDest: webroot + "js"
};

var libsJs = {
    jquery: "node_modules/jquery/dist/jquery.js",
    bootstrap: "node_modules/bootstrap/dist/js/bootstrap.js"
};

var libsCss = {
    bootstrap: "node_modules/bootstrap/dist/css/bootstrap.css"
};

var libsFonts = {
    bootstrap: [
        "node_modules/bootstrap/dist/fonts/**/*.otf", "node_modules/bootstrap/dist/fonts/**/*.eot",
        "node_modules/bootstrap/dist/fonts/**/*.ttf", "node_modules/bootstrap/dist/fonts/**/*.woff",
        "node_modules/bootstrap/dist/fonts/**/*.woff2", "node_modules/bootstrap/dist/fonts/**/*.svg"
    ]
};


gulp.task("clean:wwwroot", function (cb) {
    rimraf(webroot + "*", cb);
});

gulp.task("min:js", function () {
    return gulp.src([paths.js, paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min:libsJs", function () {
    var dataArray = new Array;
    for (var o in libsJs) {
        dataArray.push(libsJs[o]);
    }
    //Object.values(libsJs) will replace above in future js releases
    return gulp.src(dataArray, { base: "." })
        .pipe(concat(paths.libsConcatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:libsCss", function () {
    var dataArray = new Array;
    for (var o in libsCss) {
        dataArray.push(libsCss[o]);
    }
    return gulp.src(dataArray)
        .pipe(concat(paths.libsConcatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});


gulp.task("copy:files", function () {
    gulp.src("./Content/favicon/**/*.*")
        .pipe(gulp.dest(webroot));
    gulp.src("./Content/images/**/*.*")
        .pipe(gulp.dest(paths.imagesDest));

    for (var o in libsFonts) {
        gulp.src(libsFonts[o])
            .pipe(gulp.dest(paths.fontsDest));
    }
});

gulp.task("process", ["clean:wwwroot"], function () {
    gulp.start(["min:libsJs", "min:js", "min:libsCss", "min:css", "copy:files"]);
});