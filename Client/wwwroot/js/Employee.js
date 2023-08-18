﻿$.ajax({
    url: "https://localhost:7273/api/employees/GetCountedGender",
    type: "GET"
}).done((result) => {
    const ctx = document.getElementById('GenderChart');
    new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ['Female', 'Male'],
            datasets: [{
                label: 'Gender',
                data: [result.data.countFemale, result.data.countMale],
                borderWidth: 0,
                backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(54, 162, 235)',
                ],
                hoverOffset: 4
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
})
$.ajax({
    url: "https://localhost:7273/api/employees/GetCountedStatus",
    type: "GET"
}).done((result) => {
    const ctx = document.getElementById('StatusChart');
    new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ['Idle', 'Site'],
            datasets: [{
                label: 'Status',
                data: [result.data.countIdle, result.data.countSite],
                borderWidth: 0,
                backgroundColor: [
                    '#18df7b',
                    '#254636',
                ],
                hoverOffset: 4
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
})
$.ajax({
    url: "https://localhost:7273/api/employees/CountEmployee",
    type: "GET"
}).done((result) => {
    let employee = `<h3> ${result.data} </h3>`

    $("#count-employee").html(employee)
})
