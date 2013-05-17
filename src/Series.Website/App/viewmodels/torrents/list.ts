/// <reference path="../../typings/durandal/durandal.d.ts" />
/// <reference path="../../typings/knockout/knockout.mapping.d.ts" />
/// <reference path="../../services/series.services.ts" />

import Services = module("services/series.services");

export module Torrents {
    export var displayName: string = "Torrents";
    export var series: KnockoutObservableArray = ko.observableArray();
    export var torrentsService: Services.Series.TorrentService = new Services.Series.TorrentService();
    export var libraryService: Services.Series.LibraryService = new Services.Series.LibraryService();

    class Serie {
        public Id: KnockoutObservableString = ko.observable();
        public Title: KnockoutObservableString = ko.observable();
        public Torrents: KnockoutObservableArray = ko.observableArray();
        public Ignore: KnockoutObservableBool = ko.observable();
        public Visible: KnockoutObservableBool = ko.observable(false);

        public toggleVisible() {
            var self = this;
            var last = self.Visible()
            if (!last) {
                getTorrents(self);
            }
            self.Visible(!last);
        }

        constructor(data) {
            ko.mapping.fromJS(data, {
                Torrents: {
                    create: function (context) { return new Torrent(context.data);  }
                }
            }, this);
            this.Ignore(false);
        }
    }

    class Torrent implements Services.Series.TorrentLink {
        public Id: KnockoutObservableString = ko.observable();
        public Filename: KnockoutObservableString = ko.observable();
        public Status: KnockoutObservableNumber = ko.observable();
        public Ignore: KnockoutObservableBool;

        constructor(data) {
            ko.mapping.fromJS(data, null, this);
            this.Ignore = ko.computed({
                read: () => {
                    return this.Status() == 0;
                },
                write: () => {
                    this.Status(this.Status() == 0 ? 1 : 0);
                    updateTorrent(this);
                }
            });
        }
    }

    function updateTorrent(torrent) {
        var data = ko.mapping.toJS(torrent);
        console.log("updateTorrent (vm):", data);
        torrentsService.update(data);
    }

    function getSeries() {
        libraryService.get()
            .then(function(items: Services.Series.SerieLink[]) {
                var results = ko.utils.arrayMap(items, (item: Services.Series.SerieLink) => {
                    var r = new Serie(null);
                    r.Id(item.id());
                    r.Title(item.title());
                    return r;
                });
                console.log(results);
                series(results);
            })
            .fail(function (data) {
                console.error("getSerie", data);
            });
    }

    function getTorrents(serie: Serie) {
        torrentsService.get(serie.Id())
            .then(function (data) {
                var results = ko.utils.arrayMap(data.Series, function (item) {
                    return new Serie(item);
                });
                if (results != null && results.length > 0) {
                    serie.Torrents(results[0].Torrents());
                }
            })
            .fail(function (data) {
                console.error(data);
            });
    }

    function init() {
        getSeries();
    }

    init();
}
