/*
*
* Custom quantity input
* Element requires following HTML structure
* <div class="quantity"><input type="number"/></div>
* 
*
*/
"use strict";
function CreateObjectiveRequest() {
    let objective = JSON.stringify({
        name: $("#Name").val(),
        description: $("#Description").val(),
        performers: $("#Performers").val(),
        estimateTime: $("#TotalEstimateTime").val(),
        factTime: 0,
        objectiveStatus: 0,
        CreatedTime: $("#CreatedTime").val()
    });
    console.log(objective);
    $.ajax({
        url: "/api/Home/",
        data: objective,
        type: "POST",
        contentType: "application/json",
        success: function (result) {
            refreshNode(result);
        },
        error: function (result) {
            console.log(result);
            alert("Ошибка добавления, заполните все поля!");
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

function refreshNode(data) {
    if (data.parent === "#") {
        $(".tree-view").jstree(true).refresh();
    } else {
        let node = $(".tree-view").jstree(true).get_node(data.parent);
        $(".tree-view").refresh_node(node);
    }

}
