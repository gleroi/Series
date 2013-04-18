define(["require", "exports"], function(require, exports) {
    (function (Library) {
        Library.displayName = "Library";
        Library.followedSeries = ko.observableArray();
        var Serie = (function () {
            function Serie(id, name) {
                this.id = ko.observable();
                this.name = ko.observable();
                this.id(id);
                this.name(name);
            }
            return Serie;
        })();        
        function init() {
            Library.followedSeries([
                new Serie(0, "0000"), 
                new Serie(1, "0001"), 
                new Serie(2, "0002"), 
                new Serie(3, "0003")
            ]);
        }
        init();
    })(exports.Library || (exports.Library = {}));

})

