var gulp = require('gulp');
var clean = require('gulp-clean');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var es = require("event-stream");
var htmlmin = require('gulp-htmlmin');
var cleanCss = require('gulp-clean-css');
var runSequence = require('run-sequence');
var ngAnnotate = require('gulp-ng-annotate');

gulp.task('clean', function() {
    return gulp.src('public/dist/')
        .pipe(clean());
});

gulp.task('uglify',['clean'], function() {
    return es.merge([ 
            gulp.src(['bower_components/jquery/dist/jquery.min.js',
                      'bower_components/datatables.net/js/jquery.dataTables.min.js',
                      'bower_components/bootstrap/dist/js/bootstrap.min.js',
                      'bower_components/angular/angular.min.js',
                      'bower_components/angular-animate/angular-animate.min.js',
                      'bower_components/angular-touch/angular-touch.min.js',
                      'bower_components/angular-route/angular-route.min.js',
                      'bower_components/angular-datatables/dist/angular-datatables.min.js',
                      'bower_components/angular-datatables/dist/plugins/bootstrap/angular-datatables.bootstrap.min.js',
                      'bower_components/ngMask/dist/ngMask.min.js',
                      'bower_components/toastr/toastr.min.js',
                      'bower_components/moment/min/moment.min.js',
                      'bower_components/angular-i18n/angular-locale_pt-br.js',
                      'bower_components/angular-input-masks/angular-input-masks-standalone.min.js',
                      'bower_components/bower_components/br-masks/releases/br-masks.min.js',
                      'bower_components/angular-br-filters/release/angular-br-filters.min.js',
                      'bower_components/angular-bootstrap/ui-bootstrap.min.js',
                      'bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js',
                      'bower_components/angular-ui-mask/dist/mask.min.js'
            ]),
            gulp.src(['app/**/*.js', 'public/dev/assets/**/*.js'])
                .pipe(concat('scripts.js'))
                .pipe(ngAnnotate())
                .pipe(uglify())
        ])
        .pipe(concat('all-1.0.0.min.js'))
        .pipe(gulp.dest('public/dist/assets/js'));
});

gulp.task('htmlmin', function() {
    return gulp.src(['public/dev/**/*html', 'public/*html'])
        .pipe(htmlmin({collapseWhitespace: true}))
        .pipe(gulp.dest('public/dist/'));
});

gulp.task('cssmin', function() {
    return es.merge([
            gulp.src([
                'bower_components/bootswatch/united/bootstrap.min.css',
                'bower_components/animate.css/animate.min.css',
                'bower_components/font-awesome/css/font-awesome.min.css',
                'bower_components/toastr/toastr.min.css',
                'bower_components/angular-datatables/dist/css/angular-datatables.min.css',
                'bower_components/angular-datatables/dist/plugins/bootstrap/datatables.bootstrap.min.css'
            ]),
            gulp.src('public/dev/assets/**/*.css').pipe(concat('styles.css')).pipe(cleanCss())
        ])
        .pipe(concat('styles-1.0.0.min.css'))
        .pipe(gulp.dest('public/dist/assets/css'));
});

gulp.task('fonts', function () {
    return gulp.src('bower_components/font-awesome/fonts/**.*')
        .pipe(gulp.dest('public/dist/assets/fonts'));
});

gulp.task('images', function () {
    return gulp.src('public/dev/**/images/**.*')
        .pipe(gulp.dest('public/dist/'));
});

gulp.task('default', function(cb) {
    return runSequence('clean', ['uglify', 'htmlmin', 'cssmin','fonts', 'images'], cb);
});

