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

function hide_post(postId) {
    if (!confirm("Do you really want to hide this post?"))
        retur0n;
    let req = new XMLHttpRequest();
    req.open("post", "/hidepost");
    req.send(postId);
    alert("Request sent!");
    location.reload();
}

function likeToggle(postId, ele) {
    let req = new XMLHttpRequest();
    req.open("post", `/like?post=${postId}`);
    req.send(postId);
    ele.classList.toggle("liked");
    ele.classList.toggle("like");

    node = ele.parentElement.parentElement.childNodes[1].childNodes[1]

    if (ele.classList.contains("liked")) {
        node.innerHTML = increase(node.innerHTML);
    }
    if (ele.classList.contains("like")) {
        node.innerHTML = decrease(node.innerHTML);
    }
    ele.onclick = null;
    setTimeout(() => {
        ele.onclick = function () { likeToggle(postId,ele); }
    }, 1000);
}
function increase(str) {
    tokens = str.split(" ");
    inc = parseInt(tokens[0]) + 1;
    return inc + " " + tokens[1];
}

function decrease(str) {
    tokens = str.split(" ");
    inc = parseInt(tokens[0]) - 1;
    return inc + " " + tokens[1];
}
