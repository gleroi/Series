﻿requirejs.config({
    paths: {
        'text': 'durandal/amd/text'
    }
});

define(['durandal/app',
    'durandal/viewLocator',
    'durandal/system',
    'durandal/plugins/router'],
    function (app, viewLocator, system, router) {
        //>>excludeStart("build", true);
        system.debug(true);
        //>>excludeEnd("build");

        app.title = 'Series';
        app.start().then(function () {
            //Replace 'viewmodels' in the moduleId with 'views' to locate the view.
            //Look for partial views in a 'views' folder in the root.
            viewLocator.useConvention();

            //configure routing
            router.useConvention();
            router.mapNav('series/search', null, 'Search');
            router.mapNav('torrents/list', null, 'Torrents');
            router.mapNav('torrents/latest', null, 'Latest');
            app.adaptToDevice();

            //Show the app by setting the root view model for our application with a transition.
            app.setRoot('viewmodels/shell', 'entrance');
        });
    });