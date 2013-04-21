define(["require", "exports"], function(require, exports) {
    (function (Library) {
        var Episode = (function () {
            function Episode(title, season, opus) {
                this.title = ko.observable();
                this.opus = ko.observable();
                this.season = ko.observable();
                this.title(title);
                this.season(season);
                this.opus(opus);
            }
            return Episode;
        })();        
        var Serie = (function () {
            function Serie(id, name) {
                this.id = ko.observable();
                this.name = ko.observable();
                this.episodes = ko.observableArray();
                this.id(id);
                this.name(name);
            }
            return Serie;
        })();        
        Library.displayName = "Library";
        Library.followedSeries = ko.observableArray();
        Library.foundedSeries = ko.observableArray();
        Library.searchTerm = ko.observable();
        function search() {
            Library.foundedSeries([
                makeSerie("Once upon a time", 15), 
                makeSerie("The walking dead", 16), 
                makeSerie("Misfits", 6), 
                makeSerie("Community", 17), 
                
            ]);
        }
        Library.search = search;
        function makeSerie(title, epCount) {
            var serie = new Serie(0, title);
            var result = [];
            for(var i = 1; i <= epCount; i++) {
                var ep = new Episode(title + " " + i, 1, i);
                result.push(ep);
            }
            serie.episodes(result);
            return serie;
        }
        function init() {
            Library.followedSeries([
                makeSerie("Californication", 15), 
                makeSerie("How i met your mother", 16), 
                makeSerie("Big Bang Theory", 6), 
                makeSerie("Game of Throne", 17), 
                
            ]);
        }
        init();
    })(exports.Library || (exports.Library = {}));

})

