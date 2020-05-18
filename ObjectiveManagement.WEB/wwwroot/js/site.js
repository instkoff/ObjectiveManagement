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
    form.on("submit", function (e) {
        e.preventDefault();
        let jsTreeInstance = $(appSettings.jsTreeSelector).jstree(true);
        let canUpdateStatus;
        jsTreeInstance.load_node(selectedNode,
            function(node, status) {
                canUpdateStatus = checkStatus(node, jsTreeInstance, true); 
            });
        if (!canUpdateStatus) {
            alert("Ошибка!");
        }
        if (form.valid()) {
            let objective = JSON.stringify({
                id: selectedNode.id,
                parentId: selectedNode.parent === "#" ? null : selectedNode.parent,
                name: $("#Name").val(),
                description: $("#Description").val(),
                performers: $("#Performers").val(),
                estimateTime: $("#EstimateTime").val(),
                factTime: $("#FactTime").val(),
                objectiveStatus: $("input[name='ObjectiveStatus']:checked").val(),
                completedTime: $("#CompletedTime").val()
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

function checkStatus(node, jsTree, status) {
    for (let i = 0; i < node.children.length; i++) {
        let fullNode = jsTree.get_node(node.children[i]);
        if (fullNode.data !== "Completed") {
            status = false;
        } else {
            status = true;
        }
        if (status === true) {
            return checkStatus(node.children[i].children, jsTree, status);
        } else {
            return false;
        }
    }
    return status;
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
