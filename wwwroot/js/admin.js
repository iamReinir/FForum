const jsEdit = "js-edited";
function edit(ele) {
    if (ele.tagName == "TR")
        ele.classList.add(jsEdit);
    else
        edit(ele.parentElement);
}

function update() {
    if (confirm("Do you really want to update these users?") == false)
        return;
    let updateList = document.getElementsByClassName(jsEdit);
    for (let row of updateList) {
        let username = row.getElementsByClassName("Username").item(0).innerHTML.trim();
        let e = row.getElementsByClassName("Role").item(0);
        let role = e.options[e.selectedIndex].value;
        let is_banned = row.getElementsByClassName("Is_banned").item(0).checked;        
        var post = new XMLHttpRequest();
        post.open("post", "/update");
        post.send(`{"Username":"${username}","Role":${role}, "Is_banned":${is_banned}}`);
        alert("Done!");
    }    
}

