var settingsmenu = document.querySelector(".settings-menu");
var comment = document.querySelector(".content-comment");
function settingsMenuToggle() {
    settingsmenu.classList.toggle("settings-menu-height");
}
function commentToggle(element) {
    var postId = element;
    var node = document.getElementById(postId);
    node.classList.toggle("content-comment-height");
    var getComment = new XMLHttpRequest();
    getComment.open("get", "/comment?id="+element);
    getComment.onreadystatechange = function () {
        if (getComment.readyState === XMLHttpRequest.DONE && getComment.status === 200) { 
            console.log(getComment.responseText);
        }
    }
    getComment.send();

}
