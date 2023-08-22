
$(document).ready(function () {
    loadRoleData();
});

function loadRoleData() {
    $('#roleTable').DataTable({
        ajax: {
            url: "https://localhost:7273/api/roles",
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
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="ShowUpdateRole('${row.guid}')" data-bs-toggle="modal" data-bs-target="#modalUpdateRole" class="btn btn-primary"> Update </button>` +
                        `   <button onclick="deleteRole('${row.guid}')" class="btn btn-secondary"> Delete </button>`;
                }
            }
        ],
        dom: "Blfrtip",
        buttons: [
            {
                extend: 'colvis',
                postfixButtons: ['colvisRestore'],
                collectionLayout: 'fixed two-column',
                className: 'btn btn-primary'
            }
            , 'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
}

function addRole() {
    var roleData = {
        name: $("#name").val()
    };

    $.ajax({
        url: "https://localhost:7273/api/roles",
        type: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(roleData),
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

function deleteRole(guid) {
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
                url: "https://localhost:7273/api/roles?guid=" + guid,
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
function ShowUpdateRole(guid) {
    $.ajax({
        url: "https://localhost:7273/api/roles/" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidUpd").val(result.data.guid);
        $("#nameUpd").val(result.data.name);
    }).fail((error) => {
        alert("Failed to fetch role data. Please try again.");
        console.log(error)
    });

}

function UpdateRole() {


    let data = {
        guid: $("#guidUpd").val(),
        name: $("#nameUpd").val(),
    };
    $.ajax({
        url: "https://localhost:7273/api/roles",
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(data)
    }).done((result) => {
        Swal.fire({
            title: 'Success',
            text: 'Data has been successfully Updated',
            icon: 'success'
        }).then(() => {
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