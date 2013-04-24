define(["require", "exports", "services/series.services"], function(require, exports, __Services__) {
    var Services = __Services__;

    (function (Library) {
        Library.displayName = "Library";
        Library.followedSeries = ko.observableArray();
        Library.foundedSeries = ko.observableArray();
        Library.searchTerm = ko.observable();
        var searchService = new Services.Series.SearchService();
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
    })(exports.Library || (exports.Library = {}));

})

