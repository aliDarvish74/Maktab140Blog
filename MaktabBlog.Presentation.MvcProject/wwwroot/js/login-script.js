const passwordToggle = document.getElementById("passwordToggle");
const emailInput = document.getElementById("NationalId");
const emailError = document.getElementById("emailError");
const passwordInput = document.getElementById("Password");
const passwordError = document.getElementById("passwordError");
const signInBtn = document.getElementById("sign-in");
const toastEl = document.getElementById("error-toast");
const toastBody = document.getElementById("error-toast-body");
const bsToast = new bootstrap.Toast(toastEl);

passwordToggle.addEventListener("mousedown", (e) => {
    passwordInput.setAttribute("type", "text");
});
passwordToggle.addEventListener("mouseup", (e) => {
    passwordInput.setAttribute("type", "password");
});

signInBtn.addEventListener("click", async (e) => {

    if (!isValidNationalId(emailInput.value)) {
        emailError.innerText = "Please enter a valid national id";
        emailError.style.opacity = 100;

        setTimeout(() => {
            emailError.style.opacity = 0;
            emailError.style.transition = "0.5s";
        }, 1500);

        e.preventDefault();
    }
/*
    const url = `http://localhost:5089/api/auth/login`;
    const payload = {
        username: emailInput.value,
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

    console.log(response.status);

    if (response.status === 401) {
        toastBody.innerText = responseData.error.message;
        bsToast.show();
    }

    if (response.ok) {
        window.location.href = "../index.html";
    }*/
});

function isValidNationalId(nationalId) {
    if (nationalId.length !== 10) return false;

    return true;
}

function showToast(message) {
    toastBody.innerText = message;
    bsToast.show();
}
