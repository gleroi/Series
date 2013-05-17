/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />

export module Series {

    export class SerieLink { 
        public id: KnockoutObservableString = ko.observable();
        public title: KnockoutObservableString = ko.observable();
        public url: KnockoutObservableString = ko.observable();
    }

    export class Service {
        public url: string;

        constructor (url: string) {
            if (url.lastIndexOf("/") < (url.length - 1)) {
                this.url = url + "/";
            }
            else {
                this.url = url;
            }
        }

        public transformResponse(data): SerieLink[] {
            return ko.utils.arrayMap(data.Series, function (item) {
                var s = new SerieLink();
                s.id(item.Id);
                s.title(item.Title);
                s.url(item.Url);
                return s;
            });
        }
    }

    export interface SerieLinkPromise extends JQueryPromise {
        then(doneCallbacks: (series: SerieLink[]) => any): JQueryPromise;
    }

    export class SearchService extends Service {

        constructor () {
            super("/api/search/");
        }

        public search(term: string): SerieLinkPromise {
            var request = this.url;
            return $.getJSON(request, { term: term }).then(
                (data) => {
                    return this.transformResponse(data);
                })
                .fail((data) => {
                    console.error(request, data);
                });
        }
    }

    export class LibraryService extends Service {

        constructor () {
            super("/api/library/");
        }

        public get(): SerieLinkPromise {
            return $.getJSON(this.url)
                .then((data) => {
                    console.log(data);
                    return this.transformResponse(data);
                })
                .fail((data) => {
                    console.error(data);
                });
        }

        public add(serie: string): SerieLinkPromise {
            console.log(serie);
            return $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: this.url,
                data: JSON.stringify({
                    Series: [serie]
                })
            })
            .then((data) => {
                console.log(data);
                return this.transformResponse(data);
            })
            .fail((data) => {
                console.error(data);
            });
        }
    }

    export interface SerieItem {
        Id: string;
        Title: string;
        Torrents: TorrentLink[];
    }

    export interface TorrentsResponse {
        Series: SerieItem[];
    }

    export interface TorrentsPromise extends JQueryPromise {
        then(doneCallbacks: (response: TorrentsResponse) => any): JQueryPromise;
    }

    export class TorrentService extends Service {

        constructor () {
            super("/api/torrents/");
        }

        public get(serieId: string): TorrentsPromise {
            var request = this.url;
            return $.getJSON(request, { serieId: serieId })
                .then((data: TorrentsResponse) => { return data; })
                .fail((data) => { console.error(request, data); });
        }

        public update(torrent: TorrentLink) {
            var data = {
                Id: torrent.Id,
                Status: torrent.Status
            };
            console.log("update (service)", data);
            return $.ajax({
                type: 'PUT',
                dataType: 'json',
                contentType: 'application/json',
                url: this.url,
                data: JSON.stringify(data)
            })
            .fail((data) => { console.error(data); });
        }
    }

    export interface TorrentLink {
        Id: string;
        SerieLinkId: string;
        CreatedAt: Date;
        Filename: string;
        Status: number;
        Url: string;
    }

    export interface LatestTorrentsPromise extends JQueryPromise {
        then(doneCallbacks: (torrents: TorrentLink[]) => any): JQueryPromise;
    }


    export class LatestService extends Service {
        constructor () {
            super("/api/latest/");
        }

        public get (): LatestTorrentsPromise {
            return $.getJSON(this.url)
                .fail((data) => { console.error("latest.get", data); });
        }
    }
}