var dataTable;

$(document).ready(function () {
    debugger;
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("GetAll?status=inprocess");
    }
    else {
        if (url.includes("pending")) {
            loadDataTable("GetAll?status=pending");
        }
        else {
            if (url.includes("completed")) {
                loadDataTable("GetAll?status=completed");
            }
            else {
                if (url.includes("rejected")) {
                    loadDataTable("GetAll?status=rejected");
                }
                else {
                    loadDataTable("GetAll?status=all");
                }
            }
        }
    }
});


function loadDataTable(url) {

    debugger;
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/order/" + url
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "name", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderStatus", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Order/Details/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "5%"
            }
        ]
    });
}