/// <reference path="../typings/jquery/jquery.d.ts" />

export module Series {
    export class SearchService {
        private url : string;

        constructor(url: string) {
            if (url.lastIndexOf("/") <= url.length) {
                this.url = url + "/";
            }
            else {
                this.url = url;
            }
        }

        public search(term: string) : JQueryPromise {
            var request = this.url + term;
            return $.getJSON(request)
                .done(function (data) {
                    return data;
                })
                .fail(function (data) {
                    console.error(request, data);
                });
        }
    }
}