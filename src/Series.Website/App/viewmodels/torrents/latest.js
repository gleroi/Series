define(["require", "exports", "services/series.services"], function(require, exports, __Services__) {
    var Services = __Services__;

    (function (Latest) {
        Latest.displayName = "Latest";
        Latest.torrents = ko.observableArray();
        Latest.torrentsService = new Services.Series.TorrentService();
        Latest.latestService = new Services.Series.LatestService();
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
            Latest.torrentsService.update(data);
        }
        function getTorrents() {
            Latest.latestService.get().then(function (data) {
                var results = ko.utils.arrayMap(data, function (item) {
                    return new Torrent(item);
                });
                Latest.torrents(results);
            });
        }
        function init() {
            getTorrents();
        }
        init();
    })(exports.Latest || (exports.Latest = {}));
    var Latest = exports.Latest;
})
