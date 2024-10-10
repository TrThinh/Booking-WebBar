document.getElementById("reservationForm").addEventListener("submit", function (event) {
    event.preventDefault();

    var formData = new FormData(this);

    fetch('/Customer/Reservation/BookTable', {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (response.redirected) {
                window.location.href = response.url;
            } else {
                return response.json();
            }
        })
        .then(data => {
            if (data && !data.success) {
                alert(data.message);
            }
        })
        .catch(error => {
            console.error("Error during form submission:", error);
        });
});
