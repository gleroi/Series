define(["require", "exports", "services/series.search"], function(require, exports, __Services__) {
    var Services = __Services__;

    (function (Library) {
        Library.displayName = "Library";
        Library.followedSeries = ko.observableArray();
        Library.foundedSeries = ko.observableArray();
        Library.searchTerm = ko.observable();
        var searchService = new Services.Series.SearchService("/api/series/search/");
        function search() {
            var result = searchService.search(Library.searchTerm()).then(function (data) {
                Library.foundedSeries(data);
            });
            Library.foundedSeries();
        }
        Library.search = search;
        function addSerie(serie) {
            Library.followedSeries.push(serie);
        }
        Library.addSerie = addSerie;
        function makeSerie(title, epCount) {
            var serie = new Services.Series.Serie(0, title);
            var result = [];
            for(var i = 1; i <= epCount; i++) {
                var ep = new Services.Series.Episode(title + " " + i, 1, i);
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
    var Library = exports.Library;
})
