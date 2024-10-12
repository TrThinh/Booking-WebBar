async function fetchBookingsData() {
    // Gọi đến endpoint trả về JSON
    const response = await fetch('/Admin/Dashboard/GetBookingsPerTable');
    if (!response.ok) {
        console.error("Failed to fetch bookings data:", response.statusText);
        return [];
    }
    const data = await response.json();
    if (!data || data.length === 0) {
        console.warn("No booking data returned from API.");
    }
    return data;
}

async function renderChart() {
    const bookingData = await fetchBookingsData();

    const labels = bookingData.map(item => item.tableName);
    const counts = bookingData.map(item => item.count);

    const ctx = document.getElementById('bookingsTableChart').getContext('2d');
    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: '# of Bookings',
                data: counts,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

async function fetchBookingSummary() {
    try {
        const response = await fetch('/Admin/Dashboard/GetBookingSummary');

        // Kiểm tra phản hồi
        if (!response.ok) {
            console.error("Failed to fetch booking summary:", response.statusText);
            return { TotalBookings: 0, TotalPrice: 0 };
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error("Error fetching booking summary:", error);
        return { TotalBookings: 0, TotalPrice: 0 };
    }
}

async function renderBookingSummary() {
    const summaryData = await fetchBookingSummary();

    if (summaryData && typeof summaryData === 'object' &&
        summaryData.hasOwnProperty('totalBookings') &&
        summaryData.hasOwnProperty('totalPrice')) {

        document.getElementById('totalBookings').innerText = summaryData.totalBookings;
        document.getElementById('totalPrice').innerText = summaryData.totalPrice.toLocaleString('vi-VN');
    } else {
        console.warn("Invalid booking summary data:", summaryData);
        document.getElementById('totalBookings').innerText = 0;
        document.getElementById('totalPrice').innerText = "0 VND";
    }
}

document.addEventListener('DOMContentLoaded', renderBookingSummary);
renderChart();