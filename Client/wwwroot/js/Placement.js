$(document).ready(function () {
    loadPlacementData();
});

function loadPlacementData() {
    $('#placementTable').DataTable({
        ajax: {
            url: "https://localhost:7273/api/placements/GetEmployeeClientName",
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
            { data: "startDate" },          
            { data: "endDate" },
            { data: "employeeName" },          
            { data: "clientName" },          
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="ShowUpdate('${row.guid}')" data-bs-toggle="modal" data-bs-target="#modalUpdatePlacement" class="btn btn-primary"> Update </button>` +
                        `   <button onclick="deletePlacement('${row.guid}')" class="btn btn-secondary"> Delete </button>`;
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
}

function addPlacement() {
    var placementData = {
        startDate: $("#startDate").val(),
        endDate: $("#endDate").val(),
        employeeGuid: $("#employeeGuid").val(),
        clientGuid: $("#clientGuid").val(),
    };

    $.ajax({
        url: "https://localhost:7273/api/placements",
        type: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(placementData),
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

function deletePlacement(guid) {
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
                url: "https://localhost:7273/api/placements?guid=" + guid,
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
        url: "https://localhost:7273/api/placements/" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidUpd").val(result.data.guid);
        $("#startDateUpd").val(result.data.startDate);
        $("#endDateUpd").val(result.data.endDate);
        $("#employeeGuidUpd").val(result.data.employeeGuid);
        $("#clientGuidUpd").val(result.data.clientGuid);

        $("#modalemp2").modal("show");
    }).fail((error) => {
        alert("Failed to fetch placement data. Please try again.");
        console.log(error)
    });
    
}

function UpdatePlacement() {

    let data = {
        startDate: $("#startDateUpd").val(),
        endDate: $("#endDateUpd").val(),
        employeeGuid: $("#employeeGuidUpd").val(),
        clientGuid: $("#clientGuidUpd").val(),
    };
    $.ajax({
        url: "https://localhost:7273/api/placements",
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