var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admins/Home/GetAlls"

        },
        "columns": [
            { "data": "name" },
            { "data": "bookName" },
            {
                "data": "issueId", "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admins/Home/ApproveRequest/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    Approve
                                </a>
                            </div>  
                            `;
                },
                "width": "40%"
            }
        ]
    });
}