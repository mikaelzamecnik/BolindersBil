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