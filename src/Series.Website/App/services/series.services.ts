/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />

export module Series {

    export class SerieLink { 
        public id: KnockoutObservableString = ko.observable();
        public title: KnockoutObservableString = ko.observable();
        public url: KnockoutObservableString = ko.observable();
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

    export class SearchService extends Service {

        constructor () {
            super("/api/search/");
        }

        public search(term: string): JQueryPromise {
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

        public add(serie: string): JQueryPromise {
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
}