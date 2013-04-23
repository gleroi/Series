define(["require", "exports"], function(require, exports) {
    (function (Series) {
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
        Series.Episode = Episode;        
        var Serie = (function () {
            function Serie(id, name, description) {
                this.id = ko.observable();
                this.name = ko.observable();
                this.description = ko.observable();
                this.episodes = ko.observableArray();
                this.id(id);
                this.name(name);
                if(description != null) {
                    this.description(description);
                }
            }
            return Serie;
        })();
        Series.Serie = Serie;        
        var SearchService = (function () {
            function SearchService(url) {
                if(url.lastIndexOf("/") <= url.length) {
                    this.url = url + "/";
                } else {
                    this.url = url;
                }
            }
            SearchService.prototype.search = function (term) {
                var request = this.url + term;
                return $.getJSON(request).then(function (data) {
                    return ko.utils.arrayMap(data.Series, function (item) {
                        return new Serie(item.Id, item.Title, item.Description);
                    });
                }).fail(function (data) {
                    console.error(request, data);
                });
            };
            return SearchService;
        })();
        Series.SearchService = SearchService;        
    })(exports.Series || (exports.Series = {}));
    var Series = exports.Series;
})
