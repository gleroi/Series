/// <reference path="../../typings/durandal/durandal.d.ts" />
/// <reference path="../../typings/knockout/knockout.mapping.d.ts" />
/// <reference path="../../services/series.services.ts" />

import Services = module("services/series.services");

export module Search {

    // Main library view model

    export var displayName: string = "Search new series";
    export var followedSeries: KnockoutObservableArray = ko.observableArray();
    export var foundedSeries: KnockoutObservableArray = ko.observableArray();
    export var searchTerm: KnockoutObservableString = ko.observable();

    var searchService: Services.Series.SearchService = new Services.Series.SearchService();
    var libraryService: Services.Series.LibraryService = new Services.Series.LibraryService();

    export function search() {
        var result = searchService.search(searchTerm())
            .then(function (data) {
                foundedSeries(data);
            });
    }

    export function addSerie(serie: Services.Series.Serie) {
        var data = ko.mapping.toJS(serie);
        libraryService.add(data)
            .then((series) => { followedSeries(series); })
            .fail((data) => { console.error(data); });
    }

    function makeSerie(title: string, epCount: number): Services.Series.Serie {
        var serie = new Services.Series.Serie(0, title);
        var result = [];
        for (var i = 1; i <= epCount; i++) {
            var ep = new Services.Series.Episode(title + " " + i, 1, i);
            result.push(ep);
        }
        serie.episodes(result);
        return serie;
    }

    function init() {
        libraryService.get().then(function (series) {
            console.log(series);
            followedSeries(series);
        });
    }

    init();
}