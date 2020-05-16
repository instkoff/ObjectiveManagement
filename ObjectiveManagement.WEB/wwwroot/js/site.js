/*
*
* Custom quantity input
* Element requires following HTML structure
* <div class="quantity"><input type="number"/></div>
* 
*
*/

function DrawTreeMenu(jsTreeSelector) {
    $(jsTreeSelector).jstree({
        'core': {
            'data': {
                'url': function (node) {
                    return node.id === "#" ? appSettings.Urls.getRootItems : appSettings.Urls.getChildrenItems + "?Id=" + node.id;
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

function sendFormData(jsTreeSelector, formSelector) {
    let form = $(formSelector);
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
                    refreshNode(result, jsTreeSelector);
                },
                error: function (result) {
                    console.log(result);
                    alert("Ошибка добавления, заполните все поля!");
                }
            });
        }
    });
}

function deleteObjectiveRequest(jsTreeSelector) {
    let tree = $(jsTreeSelector).jstree(true);
    let selectedNodeIdArr = tree.get_selected();
    if (tree.is_parent(selectedNodeIdArr[0])) {
        alert("Нельзя удалить задачу, если у неё есть подзадачи!");
    } else {
        $.ajax({
            url: appSettings.Urls.deleteObjective,
            type: "DELETE",
            contentType: "application/json",
            data: JSON.stringify(selectedNodeIdArr[0]),
            success: function () {
                tree.delete_node(selectedNodeIdArr[0]);
            },
            error: function (result) {
                console.log(result);
            }
        });
    }

}

function refreshNode(newMenuItem, jsTreeSelector) {
    if (newMenuItem.parent === "#") {
        $(jsTreeSelector).jstree(true).refresh();
        $(jsTreeSelector).on('refresh.jstree',
            function () {
                let newNode = $(jsTreeSelector).jstree(true).get_node(newMenuItem.id);
                $(jsTreeSelector).jstree(true).deselect_all();
                $(jsTreeSelector).jstree(true).select_node(newNode);
            });
    } else {
        let parentNode = $(jsTreeSelector).jstree(true).get_node(newMenuItem.parent);
        $(jsTreeSelector).jstree(true).refresh_node(parentNode);
        $(jsTreeSelector).on('refresh_node.jstree',
            function (node, nodes) {
                $(jsTreeSelector).jstree(true).open_node(node, false);
                let newNode = $(jsTreeSelector).jstree(true).get_node(newMenuItem.id);
                $(jsTreeSelector).jstree(true).deselect_all();
                $(jsTreeSelector).jstree(true).select_node(newNode);
            });
    }

}
