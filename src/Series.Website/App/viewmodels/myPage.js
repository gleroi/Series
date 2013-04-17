define(["require", "exports", "durandal/app"], function(require, exports, __app__) {
    var app = __app__;

    (function (myPage) {
        myPage.displayName = 'My Page';
        function showMessage() {
            app.showMessage('Hello there!');
        }
        myPage.showMessage = showMessage;
    })(exports.myPage || (exports.myPage = {}));

})

