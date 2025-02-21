// Generate a random CAPTCHA code
function generateCaptcha() {
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let captcha = '';
    for (let i = 0; i < 6; i++) {
        const randomIndex = Math.floor(Math.random() * characters.length);
        captcha += characters[randomIndex];
    }
    document.getElementById('captchaCode').textContent = captcha;
}

// Validate the CAPTCHA input
document.getElementById('ticketForm').addEventListener('submit', function(event) {
    event.preventDefault();
    const captchaCode = document.getElementById('captchaCode').textContent;
    const userCaptchaInput = document.getElementById('captchaInput').value;

    if (userCaptchaInput === captchaCode) {
        alert('Ticket erfolgreich eröffnet!');
        document.getElementById('ticketForm').reset();
        generateCaptcha(); // Refresh CAPTCHA after form submission
    } else {
        alert('CAPTCHA ist falsch. Bitte versuchen Sie es erneut.');
        generateCaptcha(); // Refresh CAPTCHA if entered incorrectly
    }
});

// Cancel ticket action
function cancelTicket() {
    if (confirm("Möchten Sie das Ticket wirklich abbrechen?")) {
        document.getElementById('ticketForm').reset();
        generateCaptcha(); // Refresh CAPTCHA when form is cancelled
    }
}

// Generate CAPTCHA when the page loads
window.onload = generateCaptcha;