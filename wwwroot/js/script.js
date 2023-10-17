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

function likeToggle(postId) {
    let req = new XMLHttpRequest();
    req.open("post", `/like?post=${postId}`);
    req.send(postId);
    btn = document.getElementById("like-btn");
    btn.innerHTML += 1;
    btn.ChildNodes[0].classList.toggle("liked");
}