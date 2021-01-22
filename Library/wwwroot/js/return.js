var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Reader/User/Return"

        },
        "columns": [
            { "data": "bookName" },
            {
                "data": "bookid", "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Reader/User/Confirm/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    Return
                                </a>

                            </div>  
                            `;
                },
                "width": "40%"
            }
        ]
    });
}