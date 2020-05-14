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
    $.ajax({
        type: "POST",
        url: "/api/Home",
        data: {
            Name: $("#Name").val(),
            Description: $("#Description").val(),
            Performers: $("#Performers").val(),
            TotalEstimateTime: $("#TotalEstimateTime").val(),
            TotalFactTime: 0,
            ObjectiveStatus: 0,
            CreatedTime: $("#CreatedTime").val(),
            //ParentId: parent
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
function UpdateObjectiveRequest() {
    $.ajax({
        type: "PUT",
        url: "/api/Home",
        data: {
            Name: $("#Name").val(),
            Description: $("#Description").val(),
            Performers: $("#Performers").val(),
            TotalEstimateTime: $("#TotalEstimateTime").val(),
            TotalFactTime: ("#"),
            ObjectiveStatus: 0,
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
