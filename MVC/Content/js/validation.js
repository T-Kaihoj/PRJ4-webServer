function findErrorTags() {
    $(document).ready(function () {
        // Find all elements that are invalid.
        var el = document.getElementsByClassName("field-validation-error");

        // Parse each.
        jQuery.each(el, function (index, element) {
            setErrorTags(element);
        });
    });
}

function setErrorTags(element) {
    // Find the closest form-group.
    jQuery(element).closest('.form-group').addClass('has-error');

    // Exchange the element.
    jQuery(element).addClass('label');
    jQuery(element).addClass('label-danger');
}
