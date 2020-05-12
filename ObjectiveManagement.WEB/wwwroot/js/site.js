// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getMenusByConcat(data) {
    var dom = "";

    for (var i = 0; i < data.length; i++) {
        dom += "<li>";

        if (data[i].subObjectives.length > 0) {
            dom += "<a href=Home/id?id=" + data[i].id + ">";
            dom += "<span>" + data[i].name + "</span>";
            dom += "</a>";
            dom += "<ul>";
            dom += getMenusByConcat(data[i].subObjectives);
            dom += "</ul>";
        }
        else {
            dom += "<a href=Home/id?id=" + data[i].id + "><span>" + data[i].name + "</span></a>";
        }

        dom += "</li>";
    }

    return dom;
}  
  
