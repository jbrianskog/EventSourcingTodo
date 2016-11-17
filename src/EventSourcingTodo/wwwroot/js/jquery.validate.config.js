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
        done = window[form.getAttribute("data-estd-AjaxJQValSubmitOnDone")];

    $.post(url, requestData)
        .done(done);
}

$.validator.setDefaults({
    submitHandler: AjaxJQValSubmitHandler
});