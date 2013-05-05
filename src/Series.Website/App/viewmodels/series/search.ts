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

    export function addSerie(serie: KnockoutObservableString) {
        var data = ko.mapping.toJS(serie);
        libraryService.add(data)
            .then((series) => { followedSeries(series); })
            .fail((data) => { console.error(data); });
    }

    function init() {
        libraryService.get().then(function (series) {
            console.log(series);
            followedSeries(series);
        });
    }

    init();
}