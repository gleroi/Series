var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
}
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
        var Service = (function () {
            function Service(url) {
                if(url.lastIndexOf("/") <= url.length) {
                    this.url = url + "/";
                } else {
                    this.url = url;
                }
            }
            return Service;
        })();        
        var SearchService = (function (_super) {
            __extends(SearchService, _super);
            function SearchService() {
                        _super.call(this, "/api/series/search/");
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
        })(Service);
        Series.SearchService = SearchService;        
        var LibraryService = (function (_super) {
            __extends(LibraryService, _super);
            function LibraryService() {
                        _super.call(this, "/api/series/library/");
            }
            LibraryService.prototype.transformResponse = function (data) {
                return ko.utils.arrayMap(data.Series, function (item) {
                    return new Serie(item.Id, item.Title, item.Description);
                });
            };
            LibraryService.prototype.get = function () {
                return $.getJSON(this.url).then(function (data) {
                    console.log(data);
                    return this.transformResponse(data);
                }).fail(function (data) {
                    console.error(data);
                });
            };
            LibraryService.prototype.add = function (serie) {
                return $.post(this.url, {
                    data: {
                        SeriesToAdd: [
                            serie
                        ]
                    }
                }).then(function (data) {
                    console.log(data);
                    return this.transformResponse(data);
                }).fail(function (data) {
                    console.error(data);
                });
            };
            return LibraryService;
        })(Service);
        Series.LibraryService = LibraryService;        
    })(exports.Series || (exports.Series = {}));

})

