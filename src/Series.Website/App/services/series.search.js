define(["require", "exports"], function(require, exports) {
    (function (Series) {
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
                return $.getJSON(request).done(function (data) {
                    return data;
                }).fail(function (data) {
                    console.error(request, data);
                });
            };
            return SearchService;
        })();
        Series.SearchService = SearchService;        
    })(exports.Series || (exports.Series = {}));

})

