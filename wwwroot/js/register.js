function form_check_and_submit() {
    if (repeat_password_check() == false) {
        alert("Passwords need to be at least 6 characters and match");
        return false;
    }    
    return true;
}

function repeat_password_check() {
    let pwd = document.getElementById("pwd").value;
    let pwdre = document.getElementById("pwd-repeat").value;
    return pwd == pwdre && pwd.length > 5;
}