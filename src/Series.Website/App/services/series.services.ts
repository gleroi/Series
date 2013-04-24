/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />

export module Series {

    export class Episode {
        public title: KnockoutObservableString = ko.observable();
        public opus: KnockoutObservableNumber = ko.observable();
        public season: KnockoutObservableNumber = ko.observable();

        constructor (title: string, season: number, opus: number) {
            this.title(title);
            this.season(season);
            this.opus(opus);
        }
    }

    export class Serie {
        public id: KnockoutObservableNumber = ko.observable();
        public name: KnockoutObservableString = ko.observable();
        public description: KnockoutObservableString = ko.observable();
        public episodes: KnockoutObservableArray = ko.observableArray();

        constructor (id: number, name: string, description?: string) {
            this.id(id);
            this.name(name);
            if (description != null)
                this.description(description);
        }
    }

    class Service {
        public url: string;

        constructor (url: string) {
            if (url.lastIndexOf("/") <= url.length) {
                this.url = url + "/";
            }
            else {
                this.url = url;
            }
        }
    }

    export class SearchService extends Service {

        constructor () {
            super("/api/series/search/");
        }

        public search(term: string): JQueryPromise {
            var request = this.url + term;
            return $.getJSON(request).then(
                function (data) {
                    return ko.utils.arrayMap(data.Series, function (item) {
                        return new Serie(item.Id, item.Title, item.Description);
                    });
                })
                .fail(function (data) {
                    console.error(request, data);
                });
        }
    }

    export class LibraryService extends Service {
        constructor () {
            super("/api/series/library/");
        }

        private transformResponse(data): Serie[] {
            return ko.utils.arrayMap(data.Series, function (item) {
                return new Serie(item.Id, item.Title, item.Description);
            });
        }

        public get(): JQueryPromise {
            return $.getJSON(this.url).then(
                function (data) {
                    console.log(data);
                    return this.transformResponse(data);
                })
            .fail(function (data) {
                console.error(data);
            });
        }

        public add(serie: Serie): JQueryPromise {
            return $.post(this.url, {
                data: { SeriesToAdd: [serie] }
            })
            .then(function (data) {
                console.log(data);
                return this.transformResponse(data);
            })
            .fail(function (data) {
                console.error(data);
            });
        }
    }
}