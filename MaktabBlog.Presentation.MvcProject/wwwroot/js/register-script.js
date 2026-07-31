const passwordToggle = document.getElementById("passwordToggle");
const repasswordToggle = document.getElementById("repasswordToggle");
const nationalIdInput = document.getElementById("national-id");
const nationalIdError = document.getElementById("nationalIdError");
const passwordInput = document.getElementById("password");
const repasswordInput = document.getElementById("repassword");
const passwordError = document.getElementById("passwordError");
const repasswordError = document.getElementById("repasswordError");
const signUpBtn = document.getElementById("sign-up");
const firstNameInput = document.getElementById("first-name");
const firstNameError = document.getElementById("firstNameError");
const lastNameError = document.getElementById("lastNameError");
const ageError = document.getElementById("ageError");
const lastNameInput = document.getElementById("last-name");
const ageInput = document.getElementById("age");
const toastEl = document.getElementById("error-toast");
const toastBody = document.getElementById("error-toast-body");
const successToastBody = document.getElementById("success-toast-body");
const successToastEl = document.getElementById("success-toast");
const bsToast = new bootstrap.Toast(toastEl);
const successBsToast = new bootstrap.Toast(successToastEl);

passwordToggle.addEventListener("mousedown", (e) => {
    passwordInput.setAttribute("type", "text");
});
passwordToggle.addEventListener("mouseup", (e) => {
    passwordInput.setAttribute("type", "password");
});
repasswordToggle.addEventListener("mousedown", (e) => {
    repasswordInput.setAttribute("type", "text");
});
repasswordToggle.addEventListener("mouseup", (e) => {
    repasswordInput.setAttribute("type", "password");
});

signUpBtn.addEventListener("click", async (e) => {
    let hasError = false;
    if (!isValidNameInput(firstNameInput.value)) {
        firstNameError.innerText = "Please enter a valid first name";
        firstNameError.style.opacity = 100;

        setTimeout(() => {
            firstNameError.style.opacity = 0;
            firstNameError.style.transition = "0.5s";
        }, 2000);

        hasError = true;
    }

    if (!isValidNameInput(lastNameInput.value)) {
        lastNameError.innerText = "Please enter a valid last name";
        lastNameError.style.opacity = 100;

        setTimeout(() => {
            lastNameError.style.opacity = 0;
            lastNameError.style.transition = "0.5s";
        }, 2000);

        hasError = true;
    }

    if (ageInput.value <= 0) {
        ageError.innerText = "Please enter a valid age";
        ageError.style.opacity = 100;

        setTimeout(() => {
            ageError.style.opacity = 0;
            ageError.style.transition = "0.5s";
        }, 2000);

        hasError = true;
    }

    if (!isValidNationalId(nationalIdInput.value)) {
        nationalIdError.innerText = "Please enter a valid national id";
        nationalIdError.style.opacity = 100;

        setTimeout(() => {
            nationalIdError.style.opacity = 0;
            nationalIdError.style.transition = "0.5s";
        }, 2000);

        hasError = true;
    }

    if (!isValidPassword(passwordInput.value)) {
        passwordError.innerText = "Please enter a valid password";
        passwordError.style.opacity = 100;

        setTimeout(() => {
            passwordError.style.opacity = 0;
            passwordError.style.transition = "0.5s";
        }, 2000);

        hasError = true;
    }

    if (passwordInput.value !== repasswordInput.value) {
        repasswordError.innerText = "Password does not match";
        repasswordError.style.opacity = 100;

        setTimeout(() => {
            repasswordError.style.opacity = 0;
            repasswordError.style.transition = "0.5s";
        }, 2000);

        hasError = true;
    }

    if (hasError) {
        e.preventDefault();
    }
    
/*
    const url = `http://localhost:5089/api/auth/register`;
    const payload = {
        firstName: firstNameInput.value,
        lastName: lastNameInput.value,
        nationalId: nationalIdInput.value,
        age: ageInput.value,
        password: passwordInput.value,
    };

    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
    });

    const responseData = await response.json();
    console.log(responseData);

    if (!response.ok) {
        toastBody.innerText = responseData.error.message;
        bsToast.show();
        return;
    }
    successToastBody.innerText = "Registration successfull!";
    successBsToast.show();

    await setTimeout(() => {}, 2000);

    window.location.href = "../login/index.html";*/
});

function isValidNationalId(nationalId) {
    if (nationalId.length !== 10) return false;

    return true;
}

function isValidNameInput(value) {
    if (value === undefined) return false;

    if (value.trim().length <= 3) return false;

    return true;
}

function isValidPassword(password) {
    if (password.length < 8) return false;

    if (password.toLowerCase() == password) return false;

    return true;
}
