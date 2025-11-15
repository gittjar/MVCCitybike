// Hide infobox after load 5s
function fadeOutInfobox() {
    const infobox = document.getElementById('infobox');
    if (!infobox) return; // Exit if element doesn't exist
    
    let opacity = 1;
    const intervalID = setInterval(() => {
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




// Fetch and display user IP address
const yourIpElement = document.getElementById('yourip');
if (yourIpElement) {
    $.getJSON("https://api.ipify.org?format=json", function(data) {
        $("#yourip").html(data.ip);
    });
}

// Update clock and date
function updateClock() {
    const clockElement = document.getElementById('clock');
    const dateElement = document.getElementById('date');
    
    if (!clockElement || !dateElement) return; // Exit if elements don't exist
    
    const now = new Date();
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');
    const seconds = String(now.getSeconds()).padStart(2, '0');
    const time = `${hours}:${minutes}:${seconds}`;
    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');
    const day = String(now.getDate()).padStart(2, '0');
    const date = `${day}.${month}.${year}`;
    clockElement.textContent = time;
    dateElement.textContent = date;
}

if (document.getElementById('clock') && document.getElementById('date')) {
    updateClock();
    setInterval(updateClock, 1000);
}

// Create FontAwesome icons
function createIcons() {
    const iconContainer = document.getElementById('icon-container');
    if (!iconContainer) return; // Exit if element doesn't exist
    
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
    const textContainer = document.getElementById('textContainer');
    if (textContainer) textContainer.style.fontFamily = font;
}

function changeFontSize(fontSize) {
    const textContainer = document.getElementById('textContainer');
    if (textContainer) textContainer.style.fontSize = fontSize + 'px';
}

function changeFontColor(color) {
    const textContainer = document.getElementById('textContainer');
    if (textContainer) textContainer.style.color = color;
}

function changeTableBGColor(backgroundColor) {
    const textContainer = document.getElementById('textContainer');
    if (textContainer) textContainer.style.backgroundColor = backgroundColor;
}