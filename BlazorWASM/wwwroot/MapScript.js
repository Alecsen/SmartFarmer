var map;
var addMarkerOnClick = false;

function initializeMap() {
    map = L.map('mapid').setView([51.505, -0.09], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);

    // Event listener for map click, only adds marker if addMarkerOnClick is true
    map.on('click', function(e) {
        if (addMarkerOnClick) {
            addMarker(e.latlng);
        }
    });
}

function drawPolygonFromCoordinateString(coordinateString) {
    var coordinatePairs = coordinateString.match(/\(([^)]+)\)/g).map(function(coord) {
        return coord.replace(/[()]/g, '');
    });

    var coordinates = coordinatePairs.map(function(pair) {
        var parts = pair.split(', ');
        return [parseFloat(parts[1]), parseFloat(parts[0])]; // Leaflet expects [lat, lng]
    });

    L.polygon(coordinates, {color: 'blue'}).addTo(map);
}


// Function to add a marker at the given location
function addMarker(latlng) {
    L.marker(latlng).addTo(map)
        .bindPopup("<b>New Marker</b><br>Latitude: " + latlng.lat + "<br>Longitude: " + latlng.lng)
        .openPopup();
}

// Function to toggle marker placement on and off
function toggleMarkerPlacement() {
    addMarkerOnClick = !addMarkerOnClick;
}
