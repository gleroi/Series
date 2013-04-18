/// <reference path="../../typings/durandal/durandal.d.ts" />

export module Library {

    export var displayName: string = "Library";
    export var followedSeries: KnockoutObservableArray = ko.observableArray();


    class Serie {
        public id: KnockoutObservableNumber = ko.observable();
        public name: KnockoutObservableString = ko.observable();

        constructor(id: number, name: string) {
            this.id(id);
            this.name(name);
        }
    }

    function init() {
        followedSeries([
            new Serie(0, "0000"),
            new Serie(1, "0001"),
            new Serie(2, "0002"),
            new Serie(3, "0003")
        ]);
    }

    init();

}