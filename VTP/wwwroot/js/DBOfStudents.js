var student = 1;
var students = new Array();

function AddStudentFileds_Click() {
    $.ajax({
        url: 'AddStudentsPartial',
        type: 'GET',
        success: function (html) {
            $('#newStudents').append('<div class="input-field" id="student'
                + student + '"><button class="btn btn-danger" onclick="Remove(' + student + ')">Удалить</button>'
                + html + '</div>');
            students.push('student' + student);
            student++;
        }
    });
}

function Remove(id) {
    let index = students.indexOf('student' + id);
    students.splice(index, 1);
    $('#student'+id).remove();
}

function Save_Click() {
    let _students = new Array();
    students.forEach(s => {
        let student = {
                Id: null,
                Name: $("#" + s + " :input[name='Name']").val(),
                BornDate: new Date($("#" + s + " :input[name='BornDate']").val()),
                FamilyStatus: $("#" + s + " :input[name='FamilyStatus']").val(),
                EntryDate: new Date($("#" + s + " :input[name='EntryDate']").val()),
                GraduationDate: new Date($("#" + s + " :input[name='GraduationDate']").val()),
                FatherName: $("#" + s + " :input[name='FatherName']").val(),
                MotherName: $("#" + s + " :input[name='MotherName']").val(),
                School: $("#" + s + " :input[name='School']").val(),
                MilitaryStatus: $("#" + s + " :input[name='MilitaryStatus']").val(),
                TelNumber: $("#" + s + " :input[name='TelNumber']").val(),
                FormOfStudy: $("#" + s + " :input[name='FormOfStudy']").val(),
                ScholarshipStatus: $("#" + s + " :input[name='ScholarshipStatus']").val(),
                Group: $("#" + s + " :input[name='Groupe']").val(),
        };
            _students.push(student);
    });
    if (_students.length > 0) {
        SendStudents(_students);
    }
}

function SendStudents(studentsArray) {
    $.ajax({
        url: '/DBOfStudents/AddStudentsPost',
        type: 'POST',
        dataType: 'json',
        cache: false,
        data: JSON.stringify(studentsArray),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            window.location.href = data;
        },
        error: function () {
        }
    });
}

function Save_Student_Click(id) {
    let s = 'student';
    let student = {
            Id: id,
            Name: $("#" + s + " :input[name='Name']").val(),
            BornDate: new Date($("#" + s + " :input[name='BornDate']").val()),
            FamilyStatus: $("#" + s + " :input[name='FamilyStatus']").val(),
            EntryDate: new Date($("#" + s + " :input[name='EntryDate']").val()),
            GraduationDate: new Date($("#" + s + " :input[name='GraduationDate']").val()),
            FatherName: $("#" + s + " :input[name='FatherName']").val(),
            MotherName: $("#" + s + " :input[name='MotherName']").val(),
            School: $("#" + s + " :input[name='School']").val(),
            MilitaryStatus: $("#" + s + " :input[name='MilitaryStatus']").val(),
            TelNumber: $("#" + s + " :input[name='TelNumber']").val(),
            FormOfStudy: $("#" + s + " :input[name='FormOfStudy']").val(),
            ScholarshipStatus: $("#" + s + " :input[name='ScholarshipStatus']").val(),
            Group: $("#" + s + " :input[name='Groupe']").val(),
    };
    $.ajax({
        url: '/DBOfStudents/SaveEditStudent',
        type: 'GET',
        dataType: 'json',
        data: JSON.stringify(student),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            window.location.href = data;
        },
        error: function () {
        }
    });
}

function DeleteStudent(id, name) {
    if (confirm('Удалить ' + name)) {
        $.ajax({
            url: '/DBOfStudents/DeleteStudent',
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
    $('#students').empty();
    $.ajax({
        url: '/DBOfStudents/GetAll',
        type: 'GET',
        success: function (html) {
            $('#students').append(html);
        }
    });
}