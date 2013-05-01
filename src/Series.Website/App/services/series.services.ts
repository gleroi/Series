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
        public title: KnockoutObservableString = ko.observable();
        public description: KnockoutObservableString = ko.observable();
        public episodes: KnockoutObservableArray = ko.observableArray();

        constructor (id: number, name: string, description?: string) {
            this.id(id);
            this.title(name);
            if (description != null)
                this.description(description);
        }
    }

    class Service {
        public url: string;

        constructor (url: string) {
            if (url.lastIndexOf("/") < (url.length - 1)) {
                this.url = url + "/";
            }
            else {
                this.url = url;
            }
        }
    }

    export class SearchService extends Service {

        constructor () {
            super("/api/search/");
        }

        public search(term: string): JQueryPromise {
            var request = this.url;
            return $.getJSON(request, { term: term }).then(
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
            super("/api/library/");
        }

        private transformResponse(data): Serie[] {
            return ko.utils.arrayMap(data.Series, function (item) {
                return new Serie(item.Id, item.Title, item.Description);
            });
        }

        public get(): JQueryPromise {
            return $.getJSON(this.url)
                .then((data) => {
                    console.log(data);
                    return this.transformResponse(data);
                })
                .fail((data) => {
                    console.error(data);
                });
        }

        public add(serie: Serie): JQueryPromise {
            console.log(serie);
            return $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: this.url,
                data: JSON.stringify({
                    LibraryId: 12,
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
}