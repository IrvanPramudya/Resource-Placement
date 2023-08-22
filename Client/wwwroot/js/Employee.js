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
    let role =""
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

function addEmployee() {
    var employeeData = {
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        email: $("#email").val(),
        phoneNumber: $("#phoneNumber").val(),
        gender: parseInt($("#gender").val()),
        status: parseInt($("#status").val()),
        skill: $("#skill").val()
    };

    $.ajax({
        url: "https://localhost:7273/api/employees",
        type: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(employeeData),
    }).done(function (result) {
        Swal.fire({
            title: 'Success',
            text: 'Data has been successfully inserted',
            icon: 'success'
        }).then(function () {
            location.reload();
        });
    }).fail(function (error) {
        Swal.fire({
            title: 'Oops',
            text: 'Failed to insert data. Please try again.',
            icon: 'error'
        });
    });
}

function deleteEmployee(guid) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'Changes cannot be reverted!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Delete Data'
    }).then(function (result) {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:7273/api/employees?guid=" + guid,
                type: "DELETE",
            }).done(function (result) {
                Swal.fire({
                    title: 'Deleted',
                    text: 'Data has been successfully deleted',
                    icon: 'success'
                }).then(function () {
                    location.reload();
                });
            }).fail(function (error) {
                alert("Failed to delete data. Please try again!");
            });
        }
    });
}
function ShowUpdateEmployee(guid) {
    $.ajax({
        url: "https://localhost:7273/api/employees/" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidUpd").val(result.data.guid);
        $("#nikUpd").val(result.data.nik);
        $("#firstNameUpd").val(result.data.firstName);
        $("#lastNameUpd").val(result.data.lastName);
        $("#emailUpd").val(result.data.email);
        $("#phoneNumberUpd").val(result.data.phoneNumber);
        $("#genderUpd").val(result.data.gender);
        $("#statusUpd").val(result.data.status);
        // Melakukan penyesuaian untuk nilai gender
        /*if (result.data.gender === 0) {
            $("input[name='genderUpd'][value='Female']").prop("checked", true);
        } else {
            $("input[name='genderUpd'][value='Male']").prop("checked", true);
        }
        if (result.data.status === 0) {
            $("input[name='statusUpd'][value='Active']").prop("checked", true);
        } else {
            $("input[name='statusUpd'][value='Idle']").prop("checked", true);
        }*/
        $("#skillUpd").val(result.data.skill);

        $("#modalemp2").modal("show");
    }).fail((error) => {
        alert("Failed to fetch employee data. Please try again.");
        console.log(error)
    });
    
}

function UpdateEmployee() {


    let data = {
        guid: $("#guidUpd").val(),
        nik: $("#nikUpd").val(),
        firstName: $("#firstNameUpd").val(),
        lastName: $("#lastNameUpd").val(),
        email: $("#emailUpd").val(),
        phoneNumber: $("#phoneNumberUpd").val(), 
        gender: parseInt($("#genderUpd").val()), 
        status: parseInt($("#statusUpd").val()), 
        skill: $("#skillUpd").val(),
    };
    $.ajax({
        url: "https://localhost:7273/api/employees",
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(data)
    }).done((result) => {
        Swal.fire({
            title: 'Success',
            text: 'Data has been successfully updated',
            icon: 'success'
        }).then(function () {
            location.reload();
        });
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to insert data! Please try again.'
        })
        console.log(error)
    })
}