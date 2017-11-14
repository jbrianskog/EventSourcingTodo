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

function completeTodoBtnHandler(e, url) {
    // only handle "enter" presses
    if (e.type === "keypress" && e.which !== 13) {
        return;
    }
    // short-circuit if this event has bubbled up from the todoActionsBtn (which is inside (un)complete todo buttons)
    var todoActionsBtn = $(e.currentTarget).find(".todoActionsBtn")[0];
    if (todoActionsBtn === e.target || $.contains(todoActionsBtn, e.target)) {
        return;
    }
    var requestData = {
        TodoId: e.currentTarget.getAttribute("data-estd-todo-id"),
        __RequestVerificationToken: jsViewBag.csrfToken
    };
    $.post(url, requestData)
        .done(todoListReplacingAjaxSubmitOnDone);
}

$(function () {
    jsViewBag.csrfToken = $("input[name=__RequestVerificationToken]").first().val();

    // Need to handle keypress here because the completeTodoBtn is an <a> without an href,
    // so tabbing to it and pressing enter doesn't trigger a click event like it would with a <button>.
    // I want completeTodoBtn to act like a <button>, but it has another <button> inside of it (todoActionsBtn),
    // so browsers and Bootstrap styling wouldn't work if it was a <button> (HTML spec says no interactive content
    // inside interactive content). By using an <a> with no href I get the a.list-group-item Bootstrap styling.
    $("#todoListAjaxTarget").on("click keypress", ".completeTodoBtn", function (e) {
        completeTodoBtnHandler(e, jsViewBag.urlCompleteTodo);
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("click keypress", ".uncompleteTodoBtn", function (e) {
        completeTodoBtnHandler(e, jsViewBag.urlUncompleteTodo);
    });

    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".moveTodoUpBtn", function () {
        var url = jsViewBag.urlChangeTodoPosition,
            requestData = {
                TodoId: this.getAttribute("data-estd-todo-id"),
                Offset: "-1",
                __RequestVerificationToken: jsViewBag.csrfToken
            };
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
            };
        $.post(url, requestData)
            .done(todoListReplacingAjaxSubmitOnDone);
    });
    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".deleteTodoBtn", function () {
        var url = jsViewBag.urlRemoveTodo,
            requestData = {
                TodoId: this.getAttribute("data-estd-todo-id"),
                __RequestVerificationToken: jsViewBag.csrfToken
            };
        $.post(url, requestData)
            .done(todoListReplacingAjaxSubmitOnDone);
    });

    // Delegated event handler
    $("#todoListAjaxTarget").on("click", ".todoActionsBtn", function (e1) {
        var $panelDefault = $(this).closest(".todoPanelDefault"),
            $actionsPanel = $panelDefault.next(),
            $bothPanels = $panelDefault.add($actionsPanel),
            actionsBtnGroup = $actionsPanel.find(".todoActionsPanelBtnGroup")[0],
            eventNamespace = "click.todoActionsPanelClose" + e1.timeStamp.toString().replace(".", "");
        $bothPanels.toggle();
        
        // eventNamespace is unique (probably) so each instance of this handler can only detach itself
        $(document).on(eventNamespace, function (e2) {
            // short-circuit when handler is triggered by event that created it
            if (e1.originalEvent === e2.originalEvent) {
                return;
            }
            if (!$.contains(actionsBtnGroup, e2.target)) {
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
    $("#todoListAjaxTarget").on("blur", ".renameTodoForm", function (e) {
        if (!(e.relatedTarget && $.contains(this, e.relatedTarget))) {
            var $renamePanel = $(this).closest(".todoRenamePanel");
            $renamePanel.prev().prev().add($renamePanel).toggle();
        }
    });
});