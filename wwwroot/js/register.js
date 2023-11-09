function form_check_and_submit() {
    if (repeat_password_check() == false) {
        alert("Passwords need to be at least 6 characters and match");
        return false;
    }
    if (required_field_checks() == false) {
        alert("Please fill all required fields");
        return false;
    }
    document.getElementById("register-form").submit();
    return true;
}

function repeat_password_check() {
    let pwd = document.getElementById("pwd").value;
    let pwdre = document.getElementById("pwd-repeat").value;
    return pwd == pwdre && pwd.length > 5;
}

function required_field_checks() {
    if (document.getElementById("fullname").value == null || document.getElementById("fullname").value == "")
        return false;
    if (document.getElementById("username").value == null || document.getElementById("username").value == "")
        return false;
    return true;
}