/// <reference path="../../typings/durandal/durandal.d.ts" />
/// <reference path="../../typings/knockout/knockout.mapping.d.ts" />
/// <reference path="../../services/series.services.ts" />

import Services = module("services/series.services");

export module Torrents {
    export var displayName: string = "Torrents";
    export var series: KnockoutObservableArray = ko.observableArray();
    export var torrentsService: Services.Series.TorrentService = new Services.Series.TorrentService();

    class Serie {
        public Id: KnockoutObservableNumber = ko.observable();
        public Title: KnockoutObservableString = ko.observable();
        public Torrents: KnockoutObservableArray = ko.observableArray();
        public Ignore: KnockoutObservableBool = ko.observable();
        public Visible: KnockoutObservableBool = ko.observable(false);

        public toggleVisible() {
            var self = this;
            var last = self.Visible()
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

    class Torrent {
        public Id: KnockoutObservableNumber = ko.observable();
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
        torrentsService.get()
            .then(function (data) {
                var results = ko.utils.arrayMap(data.Series, function (item) {
                    return new Serie(item);
                });
                series(results);
            })
            .fail(function (data) {
                console.error(data);
            });
    }

    function init() {
        getTorrents();
    }

    init();
}
