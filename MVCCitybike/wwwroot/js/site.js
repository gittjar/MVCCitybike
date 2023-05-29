// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*
// hide infobox after load 5s
setTimeout(() => {
    const boxi = document.getElementById('infobox');
    boxi.style.display = 'none';
}, 5000); */

var opacity = 0;
var intervalID = 0;
window.onload = fadeout;
function fadeout() {
    setInterval(hide, 500);
}
function hide() {
    var body = document.getElementById("infobox");
    opacity =
        Number(window.getComputedStyle(body).getPropertyValue("opacity"))

    if (opacity > 0) {
        opacity = opacity - 0.1;
        body.style.opacity = opacity
    }
    else {
        clearInterval(intervalID);
    }
}