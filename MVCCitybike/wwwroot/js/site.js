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
// User IP address in indexpage
$.getJSON("https://api.ipify.org?format=json", function (data) {

    // Setting text of element
    $("#yourip").html(data.ip);
})
// clock
function updateClock() {
    const now = new Date();

    // Format time as "hh:mm:ss"
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');
    const seconds = String(now.getSeconds()).padStart(2, '0');
    const time = `${hours}:${minutes}:${seconds}`;

    // Format date as "dd.mm.yyyy"
    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');
    const day = String(now.getDate()).padStart(2, '0');
    const date = `${day}.${month}.${year}`;

    // Update the clock and date elements
    document.getElementById('clock').textContent = time;
    document.getElementById('date').textContent = date;
}

// Call updateClock() initially to display the current time and date
updateClock();

// Update the clock every second
setInterval(updateClock, 1000);

// Fontawesome icons
function createIcons() {
    const iconContainer = document.getElementById('icon-container');

    // Create the arrow-left icon
    const arrowLeftIcon = document.createElement('i');
    arrowLeftIcon.classList.add('fas', 'fa-arrow-left');
    iconContainer.appendChild(arrowLeftIcon);

    // Create the arrow-right icon
    const arrowRightIcon = document.createElement('i');
    arrowRightIcon.classList.add('fas', 'fa-arrow-right');
    iconContainer.appendChild(arrowRightIcon);
}

// Call the createIcons function to generate the icons
createIcons();

