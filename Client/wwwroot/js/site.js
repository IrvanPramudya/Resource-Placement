$.ajax({
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
    url: "https://localhost:7273/api/accountroles/GetCountAllRole",
    type: "GET"
}).done((result) => {
    console.log(result.data)
    let role = ""
    $.each(result.data, (key, val) => {
        role += `<li>
                                <i class='bx bx-show-alt'></i>
                                <span class="info">
                                    <h3>
                                        ${val.countRole}
                                    </h3>
                                    <p>${val.roleName}</p>
                                </span>
                            </li>`
    })
    $("#Adding-Role").html(role)
})

$(document).ready(function () {
    loadEmployeeData();

    $("#saveEmployeeBtn").click(function () {
        addEmployee();
    });
});

function loadEmployeeData() {
    $('#employeeTable').DataTable({
        ajax: {
            url: "https://localhost:7273/api/employees",
            dataType: "JSON",
            dataSrc: "data"
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { data: "nik" },
            {
                data: null,
                render: function (data, type, row) {
                    return row.firstName + " " + row.lastName;
                }
            },
            { data: "email" },
            { data: "phoneNumber" },
            {
                data: "gender",
                render: function (data, type, row) {
                    return data === 0 ? "Female" : "Male";
                }
            },
            {
                data: "status",
                render: function (data, type, row) {
                    return data === 0 ? "Idle" : "Active";
                }
            },
            { data: "skill" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="ShowUpdateEmployee('${row.guid}')" data-bs-toggle="modal" data-bs-target="#modalUpdateEmployee" class="btn btn-primary"> Update </button>` +
                        `   <button onclick="deleteEmployee('${row.guid}')" class="btn btn-secondary"> Delete </button>`;
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
}
