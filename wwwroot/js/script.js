var settingsmenu = document.querySelector(".settings-menu");
var darkBtn = document.getElementById("dark-btn");
function settingsMenuToggle() {
    settingsmenu.classList.toggle("settings-menu-height");
}
darkBtn.onclick = function () {
    darkBtn.classList.toggle("dark-btn-on");
    document.body.classList.toggle("dark-theme");
    if (localStorage.getItem("theme") == "light") {
        localStorage.setItem("theme", "dark");
    }
    else {
        localStorage.setItem("theme", "light");
    }
}
if (localStorage.setItem("theme") == "light") {
    darkBtn.classList.remove("dark-btn-on");
    document.body.classList.remove("dark-theme");
}
else if (localStorage.setItem("theme") == "dark") {
    darkBtn.classList.add("dark-btn-on");
    document.body.classList.add("dark-theme");
}
else {
    localStorage.setItem("theme", "light");
}


function delete_post(postId) {
    if (!confirm("Do you really want to delete this post?"))
        return;
    let req = new XMLHttpRequest();
    req.open("post", "/deletepost");
    req.send(postId);
    alert("Request sent!");
    location.reload();
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