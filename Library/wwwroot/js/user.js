var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Reader/User/GetAll",
        },
        "columns": [
            { "data": "bookName" },
            { "data": "author" },
            { "data": "availableStock" },
            {
                "data": "bookid", "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Reader/User/Issue/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    Issue
                                </a>
                            </div>  
                            `;
                },
                "width": "40%"
            }
        ]
    });
}
