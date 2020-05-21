"use strict";
function DrawTreeMenu(appSettings) {
    $(appSettings.jsTreeSelector).jstree({
        'core': {
            'data': {
                'url': function (node) {
                    return node.id === "#" ? appSettings.Urls.getRootItems : appSettings.Urls.getChildrenItems + node.id;
                },
                'data': function (node) {
                    return { 'id': node.id };
                }
            },
            "multiple": false,
            "animation": 50,
            "themes.icons": false,
            'check_callback': true
        }
    });
}

function sendFormData(appSettings) {
    let form = $(appSettings.FormSelector);
    form.on("submit", function (e) {
        e.preventDefault();
        if (form.valid()) {
            let objective = JSON.stringify({
                name: $("#Name").val(),
                parentId: $("#ParentId").val(),
                description: $("#Description").val(),
                performers: $("#Performers").val(),
                estimateTime: $("#EstimateTime").val(),
                factTime: 0,
                objectiveStatus: 0,
                createdTime: $("#CreatedTime").val()
            });
            $.ajax({
                url: form.attr("action"),
                data: objective,
                type: "POST",
                contentType: "application/json",
                success: function (result) {
                    refreshNode(result, appSettings);
                },
                error: function (result) {
                    console.log(result);
                    alert("Ошибка добавления");
                }
            });
        }
    });
}

function updateFormDataEvent(appSettings, selectedNode) {
    let form = $("#ObjectiveDetailsForm");
    let jsTreeInstance = $(appSettings.jsTreeSelector).jstree(true);
    form.on("submit", function (e) {
        e.preventDefault();
        if (form.valid()) {
            let objective = JSON.stringify({
                id: selectedNode.id,
                parentId: selectedNode.parent === "#" ? null : selectedNode.parent,
                name: $("#Name").val(),
                description: $("#Description").val(),
                performers: $("#Performers").val(),
                createdTime: $("#CreatedTime").val(),
                completedTime: $("#CompletedTime").val(),
                estimateTime: $("#EstimateTime").val(),
                factTime: $("#FactTime").val(),
                objectiveStatusType: $("input[name='ObjectiveStatusType']:checked").val(),
            });
            $.ajax({
                url: form.attr("action"),
                data: objective,
                type: "PUT",
                contentType: "application/json",
                success: function () {
                    selectedNode.parent === "#" ? jsTreeInstance.refresh() : jsTreeInstance.refresh_node(selectedNode.parent);
                },
                error: function (result) {
                    console.log(result);
                    alert("Ошибка обновления");
                }
            });
        }
    });
}

function deleteObjectiveRequest(appSettings) {
    let jsTreeInstance = $(appSettings.jsTreeSelector).jstree(true);
    let selectedNodeIdArr = jsTreeInstance.get_selected();
    let parentNode = jsTreeInstance.get_prev_dom(selectedNodeIdArr[0]);
    if (jsTreeInstance.is_parent(selectedNodeIdArr[0])) {
        alert("Нельзя удалить задачу, если у неё есть подзадачи!");
    } else {
        $.ajax({
            url: appSettings.Urls.deleteObjective,
            type: "DELETE",
            contentType: "application/json",
            data: JSON.stringify(selectedNodeIdArr[0]),
            success: function () {
                jsTreeInstance.delete_node(selectedNodeIdArr[0]);
                jsTreeInstance.select_node(parentNode);
            },
            error: function (result) {
                console.log(result);
            }
        });
    }
}

function refreshNode(newMenuItem, appSettings) {
    let jsTreeInstance = $(appSettings.jsTreeSelector).jstree(true);
    if (newMenuItem.parent === "#") {
        jsTreeInstance.refresh();
        $(appSettings.jsTreeSelector).on('refresh.jstree',
            function () {
                let newNode = jsTreeInstance.get_node(newMenuItem.id);
                jsTreeInstance.deselect_all();
                jsTreeInstance.select_node(newNode);
            });
    } else {
        let parentNode = jsTreeInstance.get_node(newMenuItem.parent);
        jsTreeInstance.refresh_node(parentNode);
        $(appSettings.jsTreeSelector).on('refresh_node.jstree',
            function (node, nodes) {
                jsTreeInstance.open_node(node, false);
                let newNode = jsTreeInstance.get_node(newMenuItem.id);
                jsTreeInstance.deselect_all();
                jsTreeInstance.select_node(newNode);
            });
    }

}
