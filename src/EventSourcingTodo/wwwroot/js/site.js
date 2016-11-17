function refreshEvents() {
    $.get(jsViewBag.urlEvents)
        .done(function (responseData) {
            $("#eventsAjaxTarget").html(responseData);
        });
}

function todoListReplacingAjaxSubmitOnDone(responseData) {
    refreshEvents();
    $("#todoListAjaxTarget").html(responseData);
    $.validator.unobtrusive.parse("#todoListAjaxTarget");
}

function addToDoFormSubmitOnDone(responseData, form) {
    $(form).trigger("reset.unobtrusiveValidation");
    todoListReplacingAjaxSubmitOnDone(responseData);
}

$(function () {
    jsViewBag.csrfToken = $("input[name=__RequestVerificationToken]").first().val();

    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".completeTodoBtn", function () {
        var url = jsViewBag.urlCompleteTodo,
            requestData = {
                TodoId: this.getAttribute("data-estd-todoid"),
                __RequestVerificationToken: jsViewBag.csrfToken
            }
        $.post(url, requestData)
            .done(todoListReplacingAjaxSubmitOnDone);
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".uncompleteTodoBtn", function () {
        var url = jsViewBag.urlUncompleteTodo,
            requestData = {
                TodoId: this.getAttribute("data-estd-todoid"),
                __RequestVerificationToken: jsViewBag.csrfToken
            }
        $.post(url, requestData)
            .done(todoListReplacingAjaxSubmitOnDone);
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".moveTodoUpBtn", function () {
        var url = jsViewBag.urlChangeTodoPosition,
            requestData = {
                TodoId: this.getAttribute("data-estd-todoid"),
                Offset: "-1",
                __RequestVerificationToken: jsViewBag.csrfToken
            }
        $.post(url, requestData)
            .done(todoListReplacingAjaxSubmitOnDone);
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".moveTodoDownBtn", function () {
        var url = jsViewBag.urlChangeTodoPosition,
            requestData = {
                TodoId: this.getAttribute("data-estd-todoid"),
                Offset: "1",
                __RequestVerificationToken: jsViewBag.csrfToken
            }
        $.post(url, requestData)
            .done(todoListReplacingAjaxSubmitOnDone);
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".deleteTodoBtn", function () {
        var url = jsViewBag.urlRemoveTodo,
            requestData = {
                TodoId: this.getAttribute("data-estd-todoid"),
                __RequestVerificationToken: jsViewBag.csrfToken
            }
        $.post(url, requestData)
            .done(todoListReplacingAjaxSubmitOnDone);
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".renameTodoBtn", function () {
        var $wrapper = $(this).closest(".renameTodoPanelWrapper");
        $wrapper.children(".renameTodoPanelToggleTarget").toggle();
        $wrapper.find(".renameTodoBtnClickFocusTarget").first().focus();
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("blur", ".renameTodoForm", function (event) {
        if (!(event.relatedTarget && $.contains(this, event.relatedTarget))) {
            $(this).closest(".renameTodoPanelWrapper").children(".renameTodoPanelToggleTarget").toggle()
        }
    });
});