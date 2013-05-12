/// <reference path="../../typings/durandal/durandal.d.ts" />
/// <reference path="../../typings/knockout/knockout.mapping.d.ts" />
/// <reference path="../../services/series.services.ts" />

import Services = module("services/series.services");

export module Latest {
    export var displayName: string = "Latest";
    export var torrents: KnockoutObservableArray = ko.observableArray();
    export var torrentsService: Services.Series.TorrentService = new Services.Series.TorrentService();
    export var latestService: Services.Series.LatestService = new Services.Series.LatestService();

    class Torrent {
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

    function getTorrents() {
        latestService.get()
            .then((data) => {
                var results = ko.utils.arrayMap(data, (item) => {
                    return new Torrent(item);
                });
                console.log(results);
                torrents(results);
            });
    }

    function init() {
        getTorrents();
    }

    init();
}
