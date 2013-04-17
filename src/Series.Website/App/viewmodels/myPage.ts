/// <reference path="../typings/durandal/durandal.d.ts" />

import app = module("durandal/app");

export module myPage {
    export var displayName : string = 'My Page';
    export function showMessage() {
        app.showMessage('Hello there!');
    }
}