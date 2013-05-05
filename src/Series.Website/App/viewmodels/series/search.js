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
        function init() {
            libraryService.get().then(function (series) {
                console.log(series);
                Search.followedSeries(series);
            });
        }
        init();
    })(exports.Search || (exports.Search = {}));

})

