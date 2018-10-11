$(document).ready(function () {

    $('#checkAll').click(function () {
        $('input:checkbox').prop('checked', this.checked);
    });

    $("#deleteBulk").click(function () {
        getValueUsingParentTag();
    });

    function getValueUsingParentTag() {
        var chkArray = [];

        /* look for all checkboxes in .cards and check if they are checked, and then take their values and push into an array */
        $(".col-md-8 input:checked").each(function () {
            chkArray.push($(this).val());
        });

        /* join the array separated by the comma */
        var selected;
        selected = chkArray.join(',');

        /* add selected value to hidden fields if it contains values */
        if (selected.length > 0) {
            $("#vehiclesIdToDelete").val(selected);
        } else {
            alert("Markera åtminstone ett fordon");
        }
    }
});
function carLoan() {
    var amount = document.getElementById('amount').value;
    var interest_rate = document.getElementById('interest_rate').value;
    var months = document.getElementById('months').value;
    var interest = amount * (interest_rate * .01) / months;
    var payment = (amount / months + interest).toFixed(2);
    payment = payment.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    document.getElementById('payment').innerHTML = "<strong>Månadskostnad från:</strong> " + payment+ " Kr (inkl. moms)";
}
function newElement() {

    var inputValue = document.getElementById("myInput").value;



    if (inputValue === '') {
        document.getElementById("myInput").value = "Ingen extrautrustning";
    }
    document.getElementById("myInput").value = "";



    for (i = 0; i < close.length; i++) {
        close[i].onclick = function () {
            var div = this.parentElement;
            div.style.display = "none";
        };
    }
    var newOne = inputValue + '|';
    $('#attrtext').append(newOne);
}
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
        title: 'BolindersBil - Anläggning Jönkoping',
        url: 'https://goo.gl/maps/R8aL5HBkq132'
    });
    google.maps.event.addListener(marker, 'click', function () {
        window.open(marker.url);
    });
    var marker2 = new google.maps.Marker({
        map: map,
        position: myLating2,
        title: 'BolindersBil - Anläggning Göteborg',
        url: 'https://goo.gl/maps/WNFVam65dfC2'

    });
    google.maps.event.addListener(marker2, 'click', function () {
        window.open(marker2.url);
    });
    var marker3 = new google.maps.Marker({
        map: map,
        position: myLating3,
        title: 'BolindersBil - Anläggning Värnamo',
        url: 'https://goo.gl/maps/e8BYHiAqFpQ2'
    });
    google.maps.event.addListener(marker3, 'click', function () {
        window.open(marker3.url);
    });
}
function success() {
        if(document.getElementById("textsend").value==="") {
            document.getElementById('send').disabled = true;
        } else {
            document.getElementById('send').disabled = false;
    }
}