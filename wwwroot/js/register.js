function form_check_and_submit() {
    if (repeat_password_check() == false) {
        alert("Passwords do not match");
        return;
    }
    document.getElementById("register-form").submit();
}

function repeat_password_check() {
    let pwd = document.getElementById("pwd").value;
    let pwdre = document.getElementById("pwd-repeat").value;
    return pwd == pwdre;
}
