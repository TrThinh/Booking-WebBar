function loadAvailableTables() {
    var date = document.getElementById("CheckinDate").value;
    var tableSelect = document.getElementById("TableId");

    if (!date) return;

    fetch(`/reservation/getavailabletables?date=${date}`)
        .then(response => response.json())
        .then(data => {
            tableSelect.innerHTML = '<option value="">Select a table</option>';
            data.data.forEach(table => {
                var option = document.createElement("option");
                option.value = table.tableId; // Adjust if necessary based on actual data property names
                option.text = table.tableName + " - " + table.description; // Adjust if necessary
                tableSelect.appendChild(option);
            });
        })
        .catch(error => {
            console.error("Error fetching available tables:", error);
        });
}
