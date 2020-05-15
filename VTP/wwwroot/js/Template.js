function GetStudents() {
    $('#student-list').empty();
    $.ajax({
        url: '/Documents/ChooseStudents',
        type: 'GET',
        success: function (html) {
            $('#student-list').append(html);
        }
    });
}

function template_on_submit() {
    let fields = $("form").serializeArray();
    let students = new Array();
    let colums = new Array();
    let IsSave = false;
    fields.forEach(f => {
        if (f.name === 'student') {
            students.push(parseInt(f.value, 10));
        }
        if (f.value === "colum") {
            let order = $('#' + f.name).children('input').val();
            colums.push({ ColumName: f.name, Order: order });
        }
        if (f.name === 'save') {
            IsSave = true;
        }
    });

    const file = document.getElementById("template")
    var data = new FormData();
    data.append("template", file.files[0]);
    $.ajax({
        url: '/api/DocumentApi/template',
        type: 'PUT',
        data: data,
        contentType: false,
        cache: false,
        processData: false,
        success: function (id) {
            let template = {
                StudentIds: students,
                Colums: colums,
                IsSave: IsSave,
                DocumentId: id
            };
            Create(template);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function Create(template) {
    $.ajax({
        url: '/Documents/CreateTemplate',
        type: 'POST',
        dataType: 'json',
        cache: false,
        data: JSON.stringify(template),
        contentType: "application/json;charset=utf-8"
    });
}

function Checkbox_OnClick(event) {
    if (event.currentTarget.checked) {
        $('#' + event.currentTarget.name).css('display', 'block');
    } else {
        $('#' + event.currentTarget.name).css('display', 'none');
    }
}