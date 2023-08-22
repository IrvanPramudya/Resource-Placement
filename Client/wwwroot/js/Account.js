$(document).ready(function () {
    loadAccountData();

    $("#saveAccountBtn").click(function () {
        addAccount();
    });
});

function loadAccountData() {
    $('#accountTable').DataTable({
        ajax: {
            url: "https://localhost:7273/api/accounts",
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
            { data: "password" },            
            { data: "otp" },
            { data: "isUsed" },          
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="ShowUpdateAccount('${row.guid}')" data-bs-toggle="modal" data-bs-target="#modalUpdateAccount" class="btn btn-primary"> Update </button>` +
                        `   <button onclick="deleteAccount('${row.guid}')" class="btn btn-secondary"> Delete </button>`;
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
}

function addAccount() {
    var accountData = {
        password: $("#password").val(),
        otp: $("#otp").val(),
        isUsed: $("#isUsed").val()
    };

    $.ajax({
        url: "https://localhost:7273/api/accounts",
        type: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(accountData),
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

function deleteAccount(guid) {
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
                url: "https://localhost:7273/api/accounts?guid=" + guid,
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
function ShowUpdateAccount(guid) {
    $.ajax({
        url: "https://localhost:7273/api/accounts/" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidUpd").val(result.data.guid);
        $("#passwordUpd").val(result.data.password);
        $("#otpUpd").val(result.data.otp);
        $("#isUsedUpd").val(result.data.isUsed);

        $("#modalemp2").modal("show");
    }).fail((error) => {
        alert("Failed to fetch account data. Please try again.");
        console.log(error)
    });
    
}

function UpdateAccount() {


    let data = {
        guid: $("#guidUpd").val(),
        password: $("#passwordUpd").val(),
        otp: $("#otpUpd").val(),
        isUsed: $("#isUsedUpd").val(),
        email: $("#emailUpd").val()
    };
    $.ajax({
        url: "https://localhost:7273/api/accounts",
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