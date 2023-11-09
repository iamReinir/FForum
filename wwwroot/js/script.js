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
    var commentButton = document.getElementById(`comment-btn-${postId}`);
    commentButton.firstElementChild.classList.remove("fa-message");
    commentButton.firstElementChild.classList.add("fa-spinner");
    commentButton.firstElementChild.classList.add("fa-spin");
    var temp = commentButton.onclick;
    commentButton.onclick = null;
    getComment.onreadystatechange = function () {
        if (getComment.readyState === XMLHttpRequest.DONE && getComment.status === 200) {
            const obj = JSON.parse(getComment.responseText);
            commentButton.firstElementChild.classList.add("fa-message");
            commentButton.firstElementChild.classList.remove("fa-spinner");
            commentButton.firstElementChild.classList.remove("fa-spin");
            commentButton.onclick = temp;
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
    }, 350);
}

function HTMLcomment(item) {
    let imageSrc = "https://static-00.iconduck.com/assets.00/user-icon-2048x2048-ihoxz4vq.png";
    if (item["Base64String"] != null && item["Base64String"] !="" ) {
        imageSrc = `data:image/jpeg;base64,${item["Base64String"]}`
    }
    return `<div class="user-profile">
                <img src="${imageSrc}"/>
                <div class="info-comment">
                    <p style="margin-bottom:2px">${item["Displayname"]}</p>
                    <span>${item["Content"]}</span>
                </div>
            </div>`;
}
function commentPopulate(postId, list, pageNum = 1, pageCount = 1) {
    var node = document.getElementById(postId).getElementsByClassName("output-comment")[0];
    for (var item of list) {                
        var cmt = document.createElement("div");        
        cmt.innerHTML = HTMLcomment(item);        
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
    texta.value = null;
    texta.placeholder = "It might take some time before your comment is posted.";
    setTimeout(() => {
        texta.placeholder = "Write your comment here...";
    }, 5000);
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

let pageCount = 0;
// Create an IntersectionObserver object
const observer = new IntersectionObserver((entries, observer) => {
    // Check if the element is visible
    if (entries[0].isIntersecting) {
        // Run the script
        lazyLoadPosts(++pageCount);
    }
});

function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) { return pair[1]; }
    }
    return (false);
}
function lazyLoadPosts(page) {    
    let req = new XMLHttpRequest();
    var container = document.getElementById('posts');
    var search = getQueryVariable("search");
    if (container.classList.contains("dealt")) return;
    req.open('GET', `/get_post?page=${page}&search=${search}`);
    req.send();    
    container.classList.toggle("dealt");
    req.onreadystatechange = (ev) => {
        if (req.readyState != XMLHttpRequest.DONE) return;
        if (req.status == 200 && req.response != "") {
            console.log("Lazy load page " + page);            
            container.removeChild(container.lastElementChild);
            container.innerHTML += req.response;
            checkAll();
            var node = document.createElement("div");
            node.classList.add("loader");
            //node.scrollIntoView(lazyLoadPosts(page + 1));
            container.appendChild(node);
            observer.observe(document.getElementsByClassName("loader")[0]);            
        }
        if (req.status == 205) {
            container.lastElementChild.classList.remove("loader");
            container.lastElementChild.innerHTML = "No more post..."
            console.log("End of page");            
        }
        container.classList.toggle("dealt");
    }

}
function checkLike(postID) {
    var req = new XMLHttpRequest();
    req.open("GET", `/like?id=${postID}`);
    req.send();
    req.onreadystatechange = ev => {        
        if (req.status == 200 && req.responseText == "1") {
            document.getElementById(`like-btn-${postID}`).classList.add("liked");
            document.getElementById(`like-btn-${postID}`).classList.remove("like");
        }

    };
}

function checkAll() {
    var docs = document.getElementsByClassName("like-btn");
    for(var doc of docs)
    {
        var id = doc.id.split("-")[2];
        checkLike(id);
    }
}

observer.observe(document.getElementsByClassName("loader")[0]);