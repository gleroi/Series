define(["require", "exports", "services/series.search"], function(require, exports, __Series__) {
    var Series = __Series__;

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
        var searchService = new Series.Series.SearchService("/api/series/search/");
        function search() {
            var result = searchService.search(Library.searchTerm()).then(function (data) {
                Library.foundedSeries(data);
            });
            Library.foundedSeries();
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

