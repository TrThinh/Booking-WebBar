document.getElementById("reservationForm").addEventListener("submit", function (event) {
    event.preventDefault(); // Ngăn việc submit form và refresh trang

    var formData = new FormData(this);
    formData.append("TableList", JSON.stringify(tableList)); // Gửi dữ liệu TableList

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
                console.log("Success:", data.message); // Log thông báo thành công ra console
            } else {
                resultMessage.className = "alert alert-danger";
                resultMessage.innerText = data.message;

                // Hiển thị chi tiết lỗi nếu có
                if (data.errors && data.errors.length > 0) {
                    console.log("Error details:", data.errors); // Log lỗi chi tiết ra console
                    data.errors.forEach(error => {
                        console.log("Error:", error); // Hiển thị từng lỗi
                    });
                }

                console.log("Error:", data.message); // Log thông báo lỗi ra console
            }

            resultMessage.style.display = "block";
        })
        .catch(error => {
            console.error("Error during form submission:", error);
        });
});
