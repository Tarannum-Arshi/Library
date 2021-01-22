var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admins/Home/GetAll"

        },
        "columns": [
            { "data": "bookName"},
            { "data": "author" },
            { "data": "stock" },
            { "data": "availableStock" },
            { "data": "bookid", "render": function (data)
                {
                    return `
                            <div class="text-center">
                                <a href="/Admins/Home/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    Edit
                                </a>
                                <a href="/Admins/Home/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer">
                                    Delete
                                </a>
                            </div>  
                            `;
                 },
                "width": "40%"
            }
        ]
    });
}

