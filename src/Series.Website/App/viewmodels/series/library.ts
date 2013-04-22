/// <reference path="../../typings/durandal/durandal.d.ts" />
/// <reference path="../../services/series.search.ts" />

import Series = module("services/series.search");

export module Library {

    class Episode {
        public title: KnockoutObservableString = ko.observable();
        public opus: KnockoutObservableNumber = ko.observable();
        public season: KnockoutObservableNumber = ko.observable();

        constructor (title: string, season: number, opus: number) {
            this.title(title);
            this.season(season);
            this.opus(opus);
        }
    }

    class Serie {
        public id: KnockoutObservableNumber = ko.observable();
        public name: KnockoutObservableString = ko.observable();
        public episodes: KnockoutObservableArray = ko.observableArray();

        constructor (id: number, name: string) {
            this.id(id);
            this.name(name);
        }
    }


    // Main library view model

    export var displayName: string = "Library";
    export var followedSeries: KnockoutObservableArray = ko.observableArray();
    export var foundedSeries: KnockoutObservableArray = ko.observableArray();
    export var searchTerm: KnockoutObservableString = ko.observable();

    var searchService: Series.Series.SearchService = new Series.Series.SearchService("/api/series/search/");

    export function search() {
        var result = searchService.search(searchTerm())
            .then(function (data) {
                foundedSeries(data);
            });
        foundedSeries();
    }

    function makeSerie(title: string, epCount: number): Serie {
        var serie = new Serie(0, title);
        var result = [];
        for (var i = 1; i <= epCount; i++) {
            var ep = new Episode(title + " " + i, 1, i);
            result.push(ep);
        }
        serie.episodes(result);
        return serie;
    }

    function init() {
        followedSeries([
            makeSerie("Californication", 15),
            makeSerie("How i met your mother", 16),
            makeSerie("Big Bang Theory", 6),
            makeSerie("Game of Throne", 17),
        ]);
    }

    init();

}