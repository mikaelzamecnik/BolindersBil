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
        $(".card input:checked").each(function () {
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