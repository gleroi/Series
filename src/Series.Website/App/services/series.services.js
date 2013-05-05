var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
}
define(["require", "exports"], function(require, exports) {
    (function (Series) {
        var SerieLink = (function () {
            function SerieLink() {
                this.id = ko.observable();
                this.title = ko.observable();
                this.url = ko.observable();
            }
            return SerieLink;
        })();
        Series.SerieLink = SerieLink;        
        var Service = (function () {
            function Service(url) {
                if(url.lastIndexOf("/") < (url.length - 1)) {
                    this.url = url + "/";
                } else {
                    this.url = url;
                }
            }
            Service.prototype.transformResponse = function (data) {
                return ko.utils.arrayMap(data.Series, function (item) {
                    var s = new SerieLink();
                    s.id(item.Id);
                    s.title(item.Title);
                    s.url(item.Url);
                    return s;
                });
            };
            return Service;
        })();        
        var SearchService = (function (_super) {
            __extends(SearchService, _super);
            function SearchService() {
                        _super.call(this, "/api/search/");
            }
            SearchService.prototype.search = function (term) {
                var _this = this;
                var request = this.url;
                return $.getJSON(request, {
                    term: term
                }).then(function (data) {
                    return _this.transformResponse(data);
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
                        _super.call(this, "/api/library/");
            }
            LibraryService.prototype.get = function () {
                var _this = this;
                return $.getJSON(this.url).then(function (data) {
                    console.log(data);
                    return _this.transformResponse(data);
                }).fail(function (data) {
                    console.error(data);
                });
            };
            LibraryService.prototype.add = function (serie) {
                var _this = this;
                console.log(serie);
                return $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    url: this.url,
                    data: JSON.stringify({
                        Series: [
                            serie
                        ]
                    })
                }).then(function (data) {
                    console.log(data);
                    return _this.transformResponse(data);
                }).fail(function (data) {
                    console.error(data);
                });
            };
            return LibraryService;
        })(Service);
        Series.LibraryService = LibraryService;        
    })(exports.Series || (exports.Series = {}));

})

