const gradeSelect = document.getElementById('name');
const salaryInput = document.getElementById('salary');

gradeSelect.addEventListener('change', function () {
    const selectedOption = gradeSelect.options[gradeSelect.selectedIndex];
    const selectedSalary = selectedOption.getAttribute('data-salary');

    salaryInput.value = selectedSalary;
});
$(document).ready(function () {
    loadGradeData();
});

$.ajax({
    url:"https://localhost:7273/api/employees"
}).done((result) => {
    let selectEmployee = ""
    $.each(result.data, (key, val) => {
        console.log(val)
        selectEmployee += ` <option value="${val.guid}">${val.firstName} ${val.lastName}</option>`
    })
    $('.employeeSelect').html(selectEmployee)
})
function loadGradeData() {
    $('#gradeTable').DataTable({
        ajax: {
            url: "https://localhost:7273/api/grades",
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
            { data: "name" },          
            { data: "salary" },          
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="ShowUpdateGrade('${row.guid}')" data-bs-toggle="modal" data-bs-target="#modalUpdateGrade" class="btn btn-primary"> Update </button>` +
                        `   <button onclick="deleteGrade('${row.guid}')" class="btn btn-secondary"> Delete </button>`;
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
}

function addGrade() {
    var gradeData = {
        guid: $("#guidEmployee").val(),
        name: $("#name").val(),
        salary: $("#salary").val(),
    };

    $.ajax({
        url: "https://localhost:7273/api/grades",
        type: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(gradeData),
    }).done(function (result) {
        Swal.fire({
            title: 'Success',
            text: 'Data has been successfully inserted',
            icon: 'success'
        }).then(function () {
            location.reload();
        });
    }).fail(function (error) {
        console.log(error)
        Swal.fire({
            title: 'Oops',
            text: 'Failed to insert data. Please try again.',
            icon: 'error'
        });
    });
}

function deleteGrade(guid) {
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
                url: "https://localhost:7273/api/grades?guid=" + guid,
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
function ShowUpdateGrade(guid) {
    $.ajax({
        url: "https://localhost:7273/api/grades/" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidUpd").val(result.data.guid);
        $("#nameUpd").val(result.data.name);
        $("#salaryUpd").val(result.data.salary);

        $("#modalemp2").modal("show");
    }).fail((error) => {
        alert("Failed to fetch grade data. Please try again.");
        console.log(error)
    });
    
}

function UpdateGrade() {

    let data = {
        guid: $("#guidUpd").val(),
        name: $("#nameUpd").val(),
        salary: $("#salaryUpd").val(),
    };
    $.ajax({
        url: "https://localhost:7273/api/grades",
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