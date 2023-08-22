$(document).ready(function () {
    loadAccountData();

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
                    return `<button onclick="ShowUpdate('${row.guid}')" data-bs-toggle="modal" data-bs-target="#modalUpdateAccount" class="btn btn-primary"> Update </button>` +
                        `   <button onclick="deleteAccount('${row.guid}')" class="btn btn-secondary"> Delete </button>`;
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

function addAccount() {
    var accountData = {
        guid: $("#guid").val(),
        firstName: $("#password").val(),
        lastName: $("#otp").val(),
        email: $("#isUsed").val(),
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
function ShowUpdate(guid) {
    $.ajax({
        url: "https://localhost:7273/api/accounts/" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidUpd").val(result.data.guid);
        $("#otpUpd").val(result.data.otp);
        $("#isUsedUpd").val(result.data.isUsed);
        // Melakukan penyesuaian untuk nilai gender
        if (result.data.gender === 0) {
            $("input[name='gender'][value='Female']").prop("checked", true);
        } else {
            $("input[name='gender'][value='Male']").prop("checked", true);
        }
        if (result.data.status === 0) {
            $("input[name='status'][value='Active']").prop("checked", true);
        } else {
            $("input[name='status'][value='Idle']").prop("checked", true);
        }
        $("#skillUpd").val(result.data.skill);

        $("#modalemp2").modal("show");
    }).fail((error) => {
        alert("Failed to fetch account data. Please try again.");
        console.log(error)
    });

}

function UpdateAccount() {


    let data = {
        guid: $("#guidUpd").val(),
        nik: $("#nikUpd").val(),
        firstName: $("#firstNameUpd").val(),
        lastName: $("#lastNameUpd").val(),
        email: $("#emailUpd").val(),
        phoneNumber: $("#phoneNumberUpd").val(),
        gender: $("input[name='gender']:checked").val() === "Female" ? 0 : 1,
        status: $("input[name='status']:checked").val() === "Active" ? 0 : 1,
        skill: $("#skillUpd").val(),
    };
    $.ajax({
        url: "https://localhost:7273/api/accounts",
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(data)
    }).done((result) => {
        Swal.fire(
            'Data has been successfully updated!',
            'success'
        ).then(() => {
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