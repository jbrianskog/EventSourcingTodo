// This needs to run after jquery.validate.js and *before* jquery.validate.unobtrusive.js if you want to set jquery.validate's
// validator defaults (i.e. before any calls to $.validate()). Unobtrusive executes $.validate()
// which bakes in the validator settings for each form that is in the DOM when it runs. Subsequent
// executions of $.validate() will ignore forms it's already run against, failing to apply the
// defaults if they are set after jquery.validate.unobtrusive.js loads.

// Alternatively to setting defaults (or as an override), the validator settings can be set manually
// for each form with e.g. $(selector).data("validator").settings.submitHandler.

// When creating jquery.validate forms dynamically (e.g. with Ajax), after the new form has been created...
// 1. If you didn't use setDefaults(), set the validator settings manually. 
// 2. Run $(selector).validate() for vanilla jquery.validate forms, or $.validator.unobtrusive.parse(selector) for Unobtrusive forms.

function AjaxJQValSubmitHandler(form) {
    var $form = $(form),
        url = $form.attr("action"),
        requestData = $form.serialize(),
        done = window[form.getAttribute("data-estd-ajax-jqval-submit-on-done")];

    $.post(url, requestData)
        .done(function (responseData) {
            done(responseData, form);
        });
}

var origDefaultHighlight = $.validator.defaults.highlight;
var origDefaultUnhighlight = $.validator.defaults.unhighlight;

function highlightFormGroupValidityClasses(element) {
    $(element).closest(".form-group, form").find("[data-estd-form-group-valid-class]").addBack().each(function () {
        $(this).removeClass(this.getAttribute("data-estd-form-group-valid-class"))
    });
    $(element).closest(".form-group, form").find("[data-estd-form-group-invalid-class]").addBack().each(function () {
        $(this).addClass(this.getAttribute("data-estd-form-group-invalid-class"))
    });
}

function unhighlightFormGroupValidityClasses(element) {
    $(element).closest(".form-group, form").find("[data-estd-form-group-invalid-class]").addBack().each(function () {
        $(this).removeClass(this.getAttribute("data-estd-form-group-invalid-class"))
    });
    $(element).closest(".form-group, form").find("[data-estd-form-group-valid-class]").addBack().each(function () {
        $(this).addClass(this.getAttribute("data-estd-form-group-valid-class"))
    });
}

$.validator.setDefaults({
    highlight : function (element, errorClass, validClass) {
        origDefaultHighlight(element, errorClass, validClass);
        highlightFormGroupValidityClasses(element);
    },
    unhighlight : function (element, errorClass, validClass) {
        origDefaultUnhighlight(element, errorClass, validClass);
        unhighlightFormGroupValidityClasses(element);
    },
    submitHandler: AjaxJQValSubmitHandler
});