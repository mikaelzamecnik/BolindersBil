function initMap() {
    var myLatLng = { lat: 57.697770, lng: 14.184420 };
    var myLating2 = { lat: 57.918940, lng: 12.066450 };
    var myLating3 = { lat: 57.189060, lng: 14.037122 };

    // Create a map object and specify the DOM element
    // for display.
    var map = new google.maps.Map(document.getElementById("map"), {
        center: myLatLng,
        zoom: 8
    });

    // Create a marker and set its position.
    var marker = new google.maps.Marker({
        map: map,
        position: myLatLng,
        title: 'BolindersBil - Anlägning Jönkoping',
        url: 'https://goo.gl/maps/R8aL5HBkq132'
    });
    google.maps.event.addListener(marker, 'click', function () {
        window.open(marker.url);
    });
    var marker2 = new google.maps.Marker({
        map: map,
        position: myLating2,
        title: 'BolindersBil - Anlägning Göteborg',
        url: 'https://goo.gl/maps/WNFVam65dfC2'

    });
    google.maps.event.addListener(marker2, 'click', function () {
        window.open(marker2.url);
    });
    var marker3 = new google.maps.Marker({
        map: map,
        position: myLating3,
        title: 'BolindersBil - Anlägning Värnamo',
        url: 'https://goo.gl/maps/e8BYHiAqFpQ2'
    });
    google.maps.event.addListener(marker3, 'click', function () {
        window.open(marker3.url);
    });
}