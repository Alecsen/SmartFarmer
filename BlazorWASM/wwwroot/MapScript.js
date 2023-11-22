var map;
var addMarkerOnClick = false;
var allBounds = L.latLngBounds();
var polygonVertices = [];
var geoJsonDataFromPoly = null;

// Flag to check if drawing mode is active
var isDrawingMode = false;
function initializeMap() {
    map = L.map('mapid').setView([51.505, -0.09], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);

    var drawControl = new L.Control.Draw();
    map.addControl(drawControl);

    var drawnFeatures = new L.FeatureGroup();
    map.addLayer(drawnFeatures);
    
    // Event listener for map click, only adds marker if addMarkerOnClick is true
    map.on('click', function(e) {
        if (addMarkerOnClick) {
            addMarker(e.latlng);
        }
    });

    if (geoJsonDataFromPoly == null) {
        map.on("draw:created", function (e) {
            var layer = e.layer;
            geoJsonDataFromPoly = layer.toGeoJSON();
            console.log(geoJsonDataFromPoly)
            drawnFeatures.addLayer(layer);
        })
    }
    
}
function createPolygonFromPoints() {
    map.o
}

function drawPolygonFromCoordinateString(coordinateString) {
    var coordinatePairs = coordinateString.match(/\(([^)]+)\)/g).map(function(coord) {
        return coord.replace(/[()]/g, '');
    });

    var coordinates = coordinatePairs.map(function(pair) {
        var parts = pair.split(', ');
        return [parseFloat(parts[1]), parseFloat(parts[0])]; // Leaflet expects [lat, lng]
    });

    var polygon = L.polygon(coordinates, {color: 'blue'}).addTo(map);
    console.log(coordinateString);
    // Extend the bounds to include each polygon's bounds
    allBounds.extend(polygon.getBounds());
}


function zoomToFitAllFields() {
    if (!allBounds.isValid()) {
        console.error("Bounds are not valid.");
        return;
    }

    map.fitBounds(allBounds);
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

/*
function toggleDrawingMode() {
    console.log("Method being called in Js")
    isDrawingMode = !isDrawingMode;
    if (!isDrawingMode) {
        // Complete the polygon drawing here
        completePolygonDrawing();
    }
    
   

    map.on("draw:created", function (e) {
        var layer = e.layer;
        geoJsonDataFromPoly = layer.toGeoJSON();
        console.log(geoJsonDataFromPoly)
        drawnFeatures.addLayer(layer);
    })
    
    map.on('click', function (e) {
        if (isDrawingMode) {
            addVertexToPolygon(e.latlng);
        }
    });
}
*/
function createField(){
    if (!geoJsonDataFromPoly) {
        console.error("GeoJSON data is not defined");
        return;
    }

    var dataToBlazor = convertGeoJsonToCoordinatesString(geoJsonDataFromPoly);
    console.log(dataToBlazor);
    
        DotNet.invokeMethodAsync('BlazorWASM', 'ReceiveDataFromJs', dataToBlazor)
            .then(result => {
                console.log('Data sent to Blazor successfully');
            }).catch(error => {
            console.error('Error in sending data to Blazor', error);
    
        });
     
}
function addVertexToPolygon(latlng) {
    console.log("Adding vertex: ", latlng);
    polygonVertices.push([latlng.lat, latlng.lng]);
    updatePolygonOnMap();
}

function updatePolygonOnMap() {
    // Logic to update the polygon on the map using the polygonVertices array
}

function completePolygonDrawing() {
    // Logic to close and finalize the polygon
    // Optionally, clear the polygonVertices array and reset the isDrawingMode flag
}

function convertGeoJsonToCoordinatesString(geoJson) {
    if (!geoJson || !geoJson.geometry || geoJson.geometry.type !== 'Polygon') {
        console.error("Invalid GeoJSON or not a Polygon:", geoJson);
        return "";
    }

    var coordinates = geoJson.geometry.coordinates[0]; // Antager den ydre grænse af Polygonen
    var formattedCoordinates = [];

    for (var i = 0; i < coordinates.length; i++) {
        // Tjekker for at undgå gentagne koordinater i streg
        if (i === 0 ||
            !(coordinates[i][0].toFixed(3) === coordinates[i - 1][0].toFixed(3) &&
                coordinates[i][1].toFixed(3) === coordinates[i - 1][1].toFixed(3))) {
            formattedCoordinates.push("(" + coordinates[i][1].toFixed(3) + ", " + coordinates[i][0].toFixed(3) + ")");
        }
    }

    return formattedCoordinates.join(", ");
}