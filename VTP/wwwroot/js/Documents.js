const realFileBtn = document.getElementById("real-file");
const customBtn = document.getElementById("custom-btn");
var data = new FormData();


customBtn.addEventListener("click", function () {
    realFileBtn.click();
});

function SaveDocument() {
   data.append("doc", realFileBtn.files[0]);
    if (!!realFileBtn.files[0]) {
        $.ajax({
            url: '/api/DocumentApi',
            type: 'PUT',
            data: data,
            contentType: false,
            cache: false,
            processData: false,
            success: function () {
                alert('Файл загружен');
                GetAll();
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
        data = new FormData();
        document.getElementById('real-file').value = "";
    }
}

function DeleteDocument(id, name) {
    if (confirm('Удалить ' + name)) {
        $.ajax({
            url: '/api/DocumentApi',
            type: 'DELETE',
            dataType: 'json',
            cache: false,
            data: JSON.stringify(id),
            contentType: "application/json;charset=utf-8"
        });
        $('#' + id).remove();
    }
}

function GetAll() {
    $('#doc-table').empty();
    $.ajax({
        url: '/Documents/GetAll',
        type: 'Get',
        success: function (html) {
            $('#doc-table').append(html);
        }
    });
}
