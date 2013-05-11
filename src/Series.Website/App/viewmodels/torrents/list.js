define(["require", "exports", "services/series.services"], function(require, exports, __Services__) {
    var Services = __Services__;

    (function (Torrents) {
        Torrents.displayName = "Torrents";
        Torrents.series = ko.observableArray();
        Torrents.torrentsService = new Services.Series.TorrentService();
        Torrents.libraryService = new Services.Series.LibraryService();
        var Serie = (function () {
            function Serie(data) {
                this.Id = ko.observable();
                this.Title = ko.observable();
                this.Torrents = ko.observableArray();
                this.Ignore = ko.observable();
                this.Visible = ko.observable(false);
                ko.mapping.fromJS(data, {
                    Torrents: {
                        create: function (context) {
                            return new Torrent(context.data);
                        }
                    }
                }, this);
                this.Ignore(false);
            }
            Serie.prototype.toggleVisible = function () {
                var self = this;
                var last = self.Visible();
                if(!last) {
                    getTorrents(self);
                }
                self.Visible(!last);
            };
            return Serie;
        })();        
        var Torrent = (function () {
            function Torrent(data) {
                var _this = this;
                this.Id = ko.observable();
                this.Filename = ko.observable();
                this.Status = ko.observable();
                ko.mapping.fromJS(data, null, this);
                this.Ignore = ko.computed({
                    read: function () {
                        return _this.Status() == 0;
                    },
                    write: function () {
                        _this.Status(_this.Status() == 0 ? 1 : 0);
                        updateTorrent(_this);
                    }
                });
            }
            return Torrent;
        })();        
        function updateTorrent(torrent) {
            var data = ko.mapping.toJS(torrent);
            console.log("updateTorrent (vm):", data);
            Torrents.torrentsService.update(data);
        }
        function getSeries() {
            Torrents.libraryService.get().then(function (items) {
                var results = ko.utils.arrayMap(items, function (item) {
                    var r = new Serie(null);
                    r.Id(item.id());
                    r.Title(item.title());
                    return r;
                });
                console.log(results);
                Torrents.series(results);
            }).fail(function (data) {
                console.error("getSerie", data);
            });
        }
        function getTorrents(serie) {
            Torrents.torrentsService.get(serie.Id()).then(function (data) {
                var results = ko.utils.arrayMap(data.Series, function (item) {
                    return new Serie(item);
                });
                if(results != null && results.length > 0) {
                    serie.Torrents(results[0].Torrents());
                }
            }).fail(function (data) {
                console.error(data);
            });
        }
        function init() {
            getSeries();
        }
        init();
    })(exports.Torrents || (exports.Torrents = {}));

})

