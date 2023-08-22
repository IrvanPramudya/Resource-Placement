$(document).ready(function () {
    loadClientData();
    $.ajax({
        url: "https://localhost:7273/api/clients/GetAvailableClient"
    }).done(function (result) {
        // Assuming the API response contains a property named "totalEmployees"
        console.log(result)
        let getName = ""
        $.each(result.data, (key, val) => {
            console.log(result)
            getName += ` <option value="${val.accountGuid}">${val.fullName}</option>`
        })
        $('.selectName').html(getName)
    })
    $.ajax({
        url: "https://localhost:7273/api/clients"
    }).done(function (result) {
        // Assuming the API response contains a property named "totalEmployees"
        console.log(result)
        let getRole = ""
        $.each(result.data, (key, val) => {
            console.log(result)
            getRole += ` <option value="${val.guid}">${val.name}</option>`
        })
        $('.selectRole').html(getRole)
    })
});

function loadClientData() {
    $('#clientTable').DataTable({
        ajax: {
            url: "https://localhost:7273/api/clients/GetAvailableClient",
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
            { data: "isAvailable" },
            { data: "capacity" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="ShowUpdateClient('${row.guid}')" data-bs-toggle="modal" data-bs-target="#modalUpdateClient" class="btn btn-primary"> Update </button>` +
                        `   <button onclick="deleteClient('${row.guid}')" class="btn btn-secondary"> Delete </button>`;
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
    })
}

function addClient() {
    var clientData = {
        clientName: $("#clientName").val(),
        isAvailable: $("#isAvailable").val(),
        capacity: $("#capacity").val()
    };

    $.ajax({
        url: "https://localhost:7273/api/clients",
        type: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(accountroleData),
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

function deleteClient(guid) {
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
                url: "https://localhost:7273/api/clients?guid=" + guid,
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
function ShowUpdateClient(guid) {
    $.ajax({
        url: "https://localhost:7273/api/clients/" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidUpd").val(result.data.guid);
        $("#accountguidUpd").val(result.data.accountGuid);
        $("#roleguidUpd").val(result.data.roleGuid);
    }).fail((error) => {
        alert("Failed to fetch accountrole data. Please try again.");
        console.log(error)
    });

}

function UpdateClient() {


    let data = {
        guid: $("#guidUpd").val(),
        accountGuid: $("#accountguidUpd").val(),
        roleGuid: $("#roleguidUpd").val(),
    };
    $.ajax({
        url: "https://localhost:7273/api/clients",
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