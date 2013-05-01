define(["require", "exports", "services/series.services"], function(require, exports, __Services__) {
    var Services = __Services__;

    (function (Search) {
        Search.displayName = "Search new series";
        Search.followedSeries = ko.observableArray();
        Search.foundedSeries = ko.observableArray();
        Search.searchTerm = ko.observable();
        var searchService = new Services.Series.SearchService();
        var libraryService = new Services.Series.LibraryService();
        function search() {
            var result = searchService.search(Search.searchTerm()).then(function (data) {
                Search.foundedSeries(data);
            });
        }
        Search.search = search;
        function addSerie(serie) {
            var data = ko.mapping.toJS(serie);
            libraryService.add(data).then(function (series) {
                Search.followedSeries(series);
            }).fail(function (data) {
                console.error(data);
            });
        }
        Search.addSerie = addSerie;
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
            libraryService.get().then(function (series) {
                console.log(series);
                Search.followedSeries(series);
            });
        }
        init();
    })(exports.Search || (exports.Search = {}));

})

