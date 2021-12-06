$(document).ready(function () {
    $("#listemployee").DataTable({
        "ajax": {
            "url": "https://localhost:44325/api/employees",
            "dataSrc": "result"
        },
        "columns": [
            {
                "data": null,
                    render: function(data, type, row, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
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
                    return `<button type="button" class="btn btn-info" data-bs-toggle="modal" onclick="GetData('${niks}')" data-bs-target="#detailCharacterModal">
                              Detail Employee
                            </button>`;
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
    
});
/* Fungsi formatRupiah */
function formatRupiah(angka) {
    const format = angka.toString().split('').reverse().join('');
    const convert = format.match(/\d{1,3}/g);
    const rupiah = 'Rp ' + convert.join('.').split('').reverse().join('')
    return rupiah;
}

function GetData(nik) {
    $.ajax({
        url: "https://localhost:44325/api/employees/profile/" + nik
    }).done((result) => {
        console.log(result);
    }).fail((error) => {
        console.log(error);
    });
}