// Wait for jQuery to load. Done after the DOM is loaded.
$(document).ready(function () {
    // Initialize Material Design.
    $.material.init();

    
});

// Setup jQuery Validate for Bootstrap integration.
jQuery.validator.setDefaults({
    highlight: function (element) {
        jQuery(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element) {
        jQuery(element).closest('.form-group').removeClass('has-error');
    },
    errorElement: 'span',
    errorClass: 'label label-danger',
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    }
});

function registerDateTimePicker(datetimepicker) {
    $(document).ready(function () {
        // Find the element.
        var item = $(datetimepicker);

        // Register the change event on the datetime picker.
        item.on("dp.change", function (e) {
            item.closest(".form-group").removeClass("is-empty");
        });

        // Start the datetime picker.
        item.datetimepicker();
    });
}