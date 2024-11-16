// Hide infobox after load 5s
function fadeOutInfobox() {
    let opacity = 1;
    const intervalID = setInterval(() => {
        const infobox = document.getElementById('infobox');
        if (opacity > 0) {
            opacity -= 0.1;
            infobox.style.opacity = opacity;
        } else {
            clearInterval(intervalID);
            infobox.style.display = 'none';
        }
    }, 500);
}

window.onload = fadeOutInfobox;

// Show toastr notifications
$(document).ready(function() {
    toastr.success('Data ladattu ja päivitetty onnistuneesti!');
   
});

// Show toastr error notification
function showErrorToast() {
    toastr.error('Virhe ladattaessa tietoja!');
}




// Fetch and display user IP address
$.getJSON("https://api.ipify.org?format=json", function(data) {
    $("#yourip").html(data.ip);
});

// Update clock and date
function updateClock() {
    const now = new Date();
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');
    const seconds = String(now.getSeconds()).padStart(2, '0');
    const time = `${hours}:${minutes}:${seconds}`;
    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');
    const day = String(now.getDate()).padStart(2, '0');
    const date = `${day}.${month}.${year}`;
    document.getElementById('clock').textContent = time;
    document.getElementById('date').textContent = date;
}

updateClock();
setInterval(updateClock, 1000);

// Create FontAwesome icons
function createIcons() {
    const iconContainer = document.getElementById('icon-container');
    const icons = ['arrow-left', 'arrow-right', 'arrow-down', 'arrow-up'];
    icons.forEach(icon => {
        const iconElement = document.createElement('i');
        iconElement.classList.add('fas', `fa-${icon}`);
        iconContainer.appendChild(iconElement);
    });
}

createIcons();

// Change text container styles
function changeFont(font) {
    document.getElementById('textContainer').style.fontFamily = font;
}

function changeFontSize(fontSize) {
    document.getElementById('textContainer').style.fontSize = fontSize + 'px';
}

function changeFontColor(color) {
    document.getElementById('textContainer').style.color = color;
}

function changeTableBGColor(backgroundColor) {
    document.getElementById('textContainer').style.backgroundColor = backgroundColor;
}