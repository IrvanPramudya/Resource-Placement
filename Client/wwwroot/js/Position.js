$(document).ready(function () {
    loadPositionData();
    $.ajax({
        url: "https://localhost:7273/api/clients"
    }).done((result) => {
        let selectClient = ""
        $.each(result.data, (key, val) => {
            console.log(val)
            selectClient += ` <option value="${val.guid}">${val.name}</option>`
        })
        $('.clientSelect').html(selectClient)
    })
});



function loadPositionData() {
    $('#positionTable').DataTable({
        ajax: {
            url: "https://localhost:7273/api/positions/GetClientName",
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
            { data: "clientName" },          
            { data: "positionName" },          
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="ShowUpdatePosition('${row.guid}')" data-bs-toggle="modal" data-bs-target="#modalUpdatePosition" class="btn btn-primary"> Update </button>` +
                        `   <button onclick="deletePosition('${row.guid}')" class="btn btn-secondary"> Delete </button>`;
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
}

function addPosition() {
    var positionData = {
        clientGuid: $("#clientGuid").val(),
        name: $("#name").val(),
    };

    $.ajax({
        url: "https://localhost:7273/api/positions",
        type: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(positionData),
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

function deletePosition(guid) {
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
                url: "https://localhost:7273/api/positions?guid=" + guid,
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
function ShowUpdatePosition(guid) {
    $.ajax({
        url: "https://localhost:7273/api/positions/" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidUpd").val(result.data.guid);
        $("#clientGuidUpd").val(result.data.clientGuid);
        $("#nameUpd").val(result.data.name);

        $("#modalemp2").modal("show");
    }).fail((error) => {
        alert("Failed to fetch position data. Please try again.");
        console.log(error)
    });
    
}

function UpdatePosition() {

    let data = {
        guid: $("#guidUpd").val(),
        clientGuid: $("#clientGuidUpd").val(),
        name: $("#nameUpd").val(),
    };
    $.ajax({
        url: "https://localhost:7273/api/positions",
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