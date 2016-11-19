﻿function refreshEvents() {
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
    $("#todoListAjaxTarget").on("click", ".completeTodoBtn", function (event) {
        var todoActionsBtn = $(this).find(".todoActionsBtn")[0];
        // short-circuit if this event has bubbled up from the todoActionsBtn (which is inside (un)complete todo buttons)
        if (todoActionsBtn === event.target || $.contains(todoActionsBtn, event.target)) {
            return;
        };
        var url = jsViewBag.urlCompleteTodo,
            requestData = {
                TodoId: this.getAttribute("data-estd-todo-id"),
                __RequestVerificationToken: jsViewBag.csrfToken
            }
        $.post(url, requestData)
            .done(todoListReplacingAjaxSubmitOnDone);
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".uncompleteTodoBtn", function (event) {
        var todoActionsBtn = $(this).find(".todoActionsBtn")[0];
        // short-circuit if this event has bubbled up from the todoActionsBtn (which is inside (un)complete todo buttons)
        if (todoActionsBtn === event.target || $.contains(todoActionsBtn, event.target)) {
            return;
        };
        var url = jsViewBag.urlUncompleteTodo,
            requestData = {
            TodoId: this.getAttribute("data-estd-todo-id"),
            __RequestVerificationToken: jsViewBag.csrfToken
        }
        $.post(url, requestData)
            .done(todoListReplacingAjaxSubmitOnDone);
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".moveTodoUpBtn", function () {
        var url = jsViewBag.urlChangeTodoPosition,
            requestData = {
                TodoId: this.getAttribute("data-estd-todo-id"),
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
                TodoId: this.getAttribute("data-estd-todo-id"),
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
                TodoId: this.getAttribute("data-estd-todo-id"),
                __RequestVerificationToken: jsViewBag.csrfToken
            }
        $.post(url, requestData)
            .done(todoListReplacingAjaxSubmitOnDone);
    });

    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".todoActionsBtn", function (event1) {
        var $panelDefault = $(this).closest(".todoPanelDefault"),
            $actionsPanel = $panelDefault.next(),
            $bothPanels = $panelDefault.add($actionsPanel),
            actionsBtnGroup = $actionsPanel.find(".todoActionsPanelBtnGroup")[0],
            eventNamespace = "click.todoActionsPanelClose" + event1.timeStamp.toString().replace(".", "");
        $bothPanels.toggle();
        
        // eventNamespace is unique (probably) so each instance of this handler can only detach itself
        $(document).on(eventNamespace, function (event2) {
            // short-circuit when handler is triggered by event that created it
            if (event1.originalEvent === event2.originalEvent) {
                return;
            };
            if (!$.contains(actionsBtnGroup, event.target)) {
                $bothPanels.toggle();
            }
            $(document).off(eventNamespace);
        });
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".renameTodoBtn", function () {
        var $actionsPanel = $(this).closest(".todoActionsPanel");
        var $renamePanel = $actionsPanel.next();
        $actionsPanel.add($renamePanel).toggle();
        $renamePanel.find(".renameTodoBtnClickFocusTarget").first().focus();
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("blur", ".renameTodoForm", function (event) {
        if (!(event.relatedTarget && $.contains(this, event.relatedTarget))) {
            var $renamePanel = $(this).closest(".todoRenamePanel");
            $renamePanel.prev().prev().add($renamePanel).toggle();
        }
    });
});