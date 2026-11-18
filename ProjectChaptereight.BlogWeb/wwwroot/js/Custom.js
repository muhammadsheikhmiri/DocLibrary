$(document).ready(function () {

    LoadCkEditor4();
    $.ajax({

        url: "/index/PopularPost",

        type: "get"
    }).done(function (data) {
        $("#Popular_Post").html(data);
    });

});
function LoadCkEditor4() {
    if (!document.getElementById("ckEditor4"))
        return;
    $("body").append("<script src='/ckeditor4/ckeditor/ckeditor.js'></script>");
    CKEDITOR.replace('ckEditor4', {
        customConfig:'/ckEditor4/ckEditor/config.js'
    });
}
function changePage(pageId) {
    var url = new URL(window.location.href);
    var search_params = url.searchParams;

    // Change PageId
    search_params.set('pageId', pageId);
    url.search = search_params.toString();

    // the new url string
    var new_url = url.toString();

    window.location.replace(new_url);
}