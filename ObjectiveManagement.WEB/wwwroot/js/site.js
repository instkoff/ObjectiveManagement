/*
*
* Custom quantity input
* Element requires following HTML structure
* <div class="quantity"><input type="number"/></div>
* 
*
*/

function DrawTreeMenu(treeSelector) {
    $(treeSelector).jstree({
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
            "themes.icons": false
        }
    });
}

function sendFormData(jstreeSelector, formSelector) {
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
            console.log(objective);
            $.ajax({
                url: form.attr("action"),
                data: objective,
                type: "POST",
                contentType: "application/json",
                success: function (result) {
                    refreshNode(result, jstreeSelector);
                },
                error: function (result) {
                    console.log(result);
                    alert("Ошибка добавления, заполните все поля!");
                }
            });
        }
    });
}

function UpdateObjectiveRequest() {
    $.ajax({
        type: "PUT",
        url: "/api/Home",
        data: {
            name: $("#Name").val(),
            description: $("#Description").val(),
            performers: $("#Performers").val(),
            estimateTime: $("#TotalEstimateTime").val(),
            factTime: 0,
            objectiveStatus: 0,
            CreatedTime: $("#CreatedTime").val()
        },
        success: function (result) {
            refreshNode(result);
        },
        error: function (result) {
            console.log(result);
            alert("Ошибка добавления, заполните все поля!");
        }
    });
}

function refreshNode(data, jstree) {
    if (data.parent === "#") {
        $(jstree).jstree(true).refresh();
    } else {
        let node = $(jstree).jstree(true).get_node(data.parent);
        $(jstree).jstree(true).refresh_node(node);
        $(jstree).jstree(true).open_node(node, false);
        $(jstree).jstree(true).deselect_all();
    }

}
