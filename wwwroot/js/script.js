var settingsmenu = document.querySelector(".settings-menu");
var comment = document.querySelector(".content-comment");
function settingsMenuToggle() {
    settingsmenu.classList.toggle("settings-menu-height");
}
function commentToggle(postId) { 
    let showComment = "cmtShow";
    var node = document.getElementById(postId);
    node.classList.toggle(showComment);
    if (node.classList.contains(showComment)) {
        commentLoad(postId);                       
    }
    else {                
        commentClear(postId);
    }
    return;
}

function commentLoad(postId) {
    var node = document.getElementById(postId);
    var getComment = new XMLHttpRequest();
    getComment.open("get", "/comment?id=" + postId);
    getComment.onreadystatechange = function () {
        if (getComment.readyState === XMLHttpRequest.DONE && getComment.status === 200) {
            const obj = JSON.parse(getComment.responseText);
            commentPopulate(postId, obj);
        }
    }
    getComment.send();
}

function commentClear(postId) {
    document.getElementById(postId).classList.remove("content-comment-height");
    setTimeout(() => {
        var node = document.getElementById(postId).getElementsByClassName("output-comment")[0];
        node.innerHTML = null;
    }, 1000);
}
function commentPopulate(postId, list) {
    var node = document.getElementById(postId).getElementsByClassName("output-comment")[0];
    for (var item of list) {                
        var cmt = document.createElement("div");        
        cmt.innerHTML = 
        `<div class="user-profile">
        <img src="https://static-00.iconduck.com/assets.00/user-icon-2048x2048-ihoxz4vq.png"/>
            <div class="info-comment">
                <p style="margin-bottom:2px">${item["Displayname"]}</p>
                <span>${item["Content"]}</span>
            </div>`;
        node.appendChild(cmt);
    }
    document.getElementById(postId).classList.add("content-comment-height");
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

function sendComment(ele, postID) {
    let texta = ele.parentElement.childNodes[1];
    let cmtRequest = new XMLHttpRequest();
    cmtRequest.open("post", "/comment?id=" + postID);
    cmtRequest.send(texta.value);
    setTimeout(() => {
        commentClear(postID);
        commentLoad(postID);
        texta.value = null;
    }, 500);
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
