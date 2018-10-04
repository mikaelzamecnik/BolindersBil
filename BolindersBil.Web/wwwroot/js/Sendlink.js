function success() {
        if(document.getElementById("textsend").value==="") { 
            document.getElementById('send').disabled = true; 
        } else { 
            document.getElementById('send').disabled = false;
        }
}
    