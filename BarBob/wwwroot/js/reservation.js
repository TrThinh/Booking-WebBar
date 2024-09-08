document.getElementById("reservationForm").addEventListener("submit", function (event) {
    event.preventDefault();

    var formData = new FormData(this);
    formData.append("TableList", JSON.stringify(tableList));

    fetch('/Customer/Reservation/BookTable', {
        method: 'POST',
        body: formData,
        headers: {
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        }
    })
        .then(response => response.json())
        .then(data => {
            var resultMessage = document.getElementById("resultMessage");

            if (data.success) {
                resultMessage.className = "alert alert-success";
                resultMessage.innerText = data.message;
                console.log("Success:", data.message);
            } else {
                resultMessage.className = "alert alert-danger";
                resultMessage.innerText = data.message;

                if (data.errors && data.errors.length > 0) {
                    console.log("Error details:", data.errors);
                    data.errors.forEach(error => {
                        console.log("Error:", error);
                    });
                }

                console.log("Error:", data.message);
            }

            resultMessage.style.display = "block";
        })
        .catch(error => {
            console.error("Error during form submission:", error);
        });
});
