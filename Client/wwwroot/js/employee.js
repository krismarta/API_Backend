$(document).ready(function () {

    tableEmployee = $("#listemployee").DataTable({
        "ajax": {
            "url": "/employees/getall",
            "dataSrc": ""
        },
        "pageLength": 5,
        "columns": [
            {
                "data": "id",
                    render: function(data, type, row, meta) {
                        return meta.row + 1;
                    }
            },
            {
                "data" : "nik"
            },
            {
                "data": null,
                render: function (data, type, row, meta) {
                    return row['firstName'] +" "+ row['lastName']
                }
            },
            {
                "data": null,
                render: function (data, type, row, meta) {
                    var phones = row['phone'];
                    var tmp = "";
                    if (phones.substring(0, 1) == "0") {
                       tmp = phones.replace(phones.substring(0,1), "+62");
                    } else {
                        tmp = phones;
                    }
                    return tmp
                }
            },
            {
                "data": null,
                render: function (data, type, row, meta) {
                    var birthdate = row['birthDate'];
                    var tmp = new Date(birthdate);
                    
                    return ((tmp.getMonth() > 8) ? (tmp.getMonth() + 1) : ('0' + (tmp.getMonth() + 1))) + '/' + ((tmp.getDate() > 9) ? tmp.getDate() : ('0' + tmp.getDate())) + '/' + tmp.getFullYear();
                }
            },
            {
                "data": null,
                render: function (data, type, row, meta) {
                    return formatRupiah(row['salary']);
                }
            },
            {
                "data" : "email"
            },
            {
                "data": null,
                render: function (data, type, row, meta) {
                    if (row['gender'] == 0) {
                        return "Male"
                    } else {
                        return "Female"
                    }
                    
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    var niks = row['nik'];
                    return `<div class="btn-group">
                                    <button type="button" class="btn btn-sm btn-circle btn-warning" data-bs-toggle="modal" onclick="updateEmployee('${niks}')" data-bs-target="#detailCharacterModal">
                                        <span class="fas fa-edit"></span>
                                    </button>
                                    &nbsp;
                                    <button type="button" class="btn btn-sm btn-circle btn-danger" onclick="DeleteEmployee('${niks}')" id="btndeleteEmployee">
                                        <span class="fas fa-trash"></span>
                                    </button>
                                </div>
                            `;
                },
                "orderable" : false
            }
        ],
        dom: "lBfrtip",
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'csv',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },  
        ]
        
    });

    //$('.dt-buttons').addClass('btn btn-info');
    $('.dt-button').removeClass().addClass("btn btn-info");
    
 
    var date_input = $('input[id="inputBirth"]'); 
    var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
    var options = {
        format: 'yyyy-mm-dd',
        container: container,
        todayHighlight: true,
        autoclose: true,
    };
    date_input.datepicker(options);

});
 
(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            $("#btnSaveEmployee").click(function () {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    var data_action = $(this).attr("data-name");
                    if (data_action == "insert") {
                        InsertEmployee();
                    } else if (data_action == "update") {
                        console.log("langsung Update bray");
                        var nik = $(this).attr("data-nik");

                        UpdateActionEmployee(nik);
                    }
                    console.log(data_action);
                }
                
                form.classList.add('was-validated');
                
            })

        });
    }, false);
})();

//testing radio button get value
function getgender() {
    var value = "";
    var dd = $("input[type='radio'][name='inputGenders']:checked").val(); //getvalue
    var selectdd = $("input[type='radio'][name=inputGenders]").attr('checked', false); //setvalue
    console.log(dd);
}


function InsertEmployee() {
    var obj = new Object();
    obj.NIK = $("#inputNik").val();
    obj.FirstName = $("#inputFirst").val();
    obj.LastName = $("#inputLastname").val();
    obj.Phone = $("#inputPhone").val();
    obj.Birthdate = $("#inputBirth").val();
    obj.Salary = $("#inputSalary").val();
    obj.Email = $("#inputEmail").val();
    obj.gender = $("input[type='radio'][name='inputGenders']:checked").val(); //getvalue
    console.log(obj);

    $.ajax({
        url: "/employees/post",
        type: "POST",
        data: obj
    }).done((result) => {
        Swal.fire(
            'Yeayy',
            "Data Berhasil ditambahkan",
            'success'
        )
        tableEmployee.ajax.reload();
        $('.createEmployee').modal('hide');

    }).fail((error) => {
        Swal.fire(
            'Opps!',
            'Sepertinya terjadi kesalahan, periksa kembali!',
            'error'
        )
    })
}

function DeleteEmployee(nik) {
    Swal.fire({
        title: 'Yakin ingin dihapus?',
        text: "Data akan dihapus dari database.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yakieen dong!',
        cancelButtonText:'Engga jadi'

    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "employees/delete/" + nik,
                type: "DELETE",
                
            }).done((result) => {
                Swal.fire(
                    'Yeayy',
                    'Data Berhasil dihapus',
                    'success'
                )
                tableEmployee.ajax.reload();
                
                $('.createEmployee').modal('hide');

            }).fail((error) => {

                Swal.fire(
                    'Opps!',
                    'Sepertinya terjadi kesalahan, periksa kembali!',
                    'error'
                )
            })
        }
    })
}

function showCreateEmployee() {
    $('.createEmployee').modal('show');
    clearFormEmployee();
}

function clearFormEmployee() {
    var value = "";
    $('#labelText').html("Create New Employee");
    $('#inputNik').val("");
    $('#inputNik').removeAttr('readonly');
    $('#inputFirst').val("");
    $('#inputLastname').val("");
    $('#inputEmail').val("");
    $('#inputPhone').val("");
    $('#inputSalary').val("");
     $("input[type='radio'][name=inputGenders]").attr('checked', false); //setvalue
    $('#inputBirth').val("");
    $('#btnSaveEmployee').attr('data-name', 'insert').html("<span class='fas fa-save'>&nbsp;</span>Save New Employee")
    $('#btnSaveEmployee').removeAttr('data-nik');
    $('#fEmployee').removeClass('was-validated')
}

function updateEmployee(nik) {
    $.ajax({
        url: "/employees/get/"+nik
    }).done((result) => {
        console.log(result)
        $('.createEmployee').modal('show');
        $('#fEmployee').removeClass('was-validated')
        $('#labelText').html("Update Employee");
        $('#inputNik').prop('readonly', true);
        $('#inputNik').val(nik).readonly;
        $('#inputFirst').val(result.firstName);
        $('#inputLastname').val(result.lastName);
        $('#inputEmail').val(result.email);
        $('#inputPhone').val(result.phone);
        $('#inputSalary').val(result.salary);
        $("input[type='radio'][name=inputGenders][value=" + result.gender + "]").attr('checked', 'checked'); //setvalue
        var birthdate = result.birthDate;
        var tmp = new Date(birthdate);
        var birthdate_simply = tmp.getFullYear() + '-' + ((tmp.getMonth() > 8) ? (tmp.getMonth() + 1) : ('0' + (tmp.getMonth() + 1))) + '-' + ((tmp.getDate() > 9) ? tmp.getDate() : ('0' + tmp.getDate()));
        $('#inputBirth').val(birthdate_simply);
        $('#btnSaveEmployee').attr('data-name','update').html("<span class='fas fa-save'>&nbsp;</span>Update Employee")
        $('#btnSaveEmployee').attr('data-nik',nik);
    }).fail((error) => {
        console.log(error);
    });
}
function UpdateActionEmployee(nik) {
    var obj = new Object();
    obj.Nik = $("#inputNik").val();
    obj.FirstName = $("#inputFirst").val();
    obj.LastName = $("#inputLastname").val();
    obj.Phone = $("#inputPhone").val();
    obj.Birthdate = $("#inputBirth").val();
    obj.Salary = $("#inputSalary").val();
    obj.Email = $("#inputEmail").val();
    obj.gender = $("input[type='radio'][name='inputGenders']:checked").val(); //getvalue
    console.log(JSON.stringify(obj));

    console.log(nik + " " + JSON.stringify(obj));
    $.ajax({
        url: "employees/Put/"+nik,
        type: "PUT",
        data: obj
    }).done((result) => {
        Swal.fire(
            'Yeayy',
            'Data ' + nik + ' Berhasil diubah',
            'success'
        )
        tableEmployee.ajax.reload();
        $('.createEmployee').modal('hide');

    }).fail((error) => {

        Swal.fire(
            'Opps!',
            'Sepertinya terjadi kesalahan, periksa kembali!',
            'error'
        )
    });
}
function formatRupiah(angka) {
    const format = angka.toString().split('').reverse().join('');
    const convert = format.match(/\d{1,3}/g);
    const rupiah = 'Rp ' + convert.join('.').split('').reverse().join('')
    return rupiah;
}



