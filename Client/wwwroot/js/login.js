function LoginEmployee() {
    var obj = new Object();
    var valueRe = $("#inputEmail").val();
    if (ValidateEmail(valueRe) == true) {
        obj.Email = valueRe;
    } else {
        obj.Phone = valueRe;
    }
    obj.Password = $("#inputPassword").val();
    console.log(obj);
    $.ajax({
        url: "/Login/actionLogin",
        type: "POST",
        data: obj,
        success: function (response) {
            if (response.result.idtoken == null && response.result.statusCode == 0) {
                console.log(response);
                Swal.fire(
                    'Opps!',    
                    'Email / Phone / Password kurang tepat.',
                    'error'
                )
            } else {
                console.log(response);
                Swal.fire({
                    title: 'Yeay!! Login Berhasil',
                    html:
                        'Mohon Tunggu <br>' +
                        '<strong></strong> detik <br>'+
                        'Direct to Dashboard Employee',
                    icon: 'success',
                    timer: 3000,
                    showConfirmButton: false,
                    allowOutsideClick: false,
                    didOpen: () => {
                        timerInterval = setInterval(() => {
                            Swal.getHtmlContainer().querySelector('strong')
                                .textContent = (Swal.getTimerLeft() / 1000)
                                    .toFixed(0)
                        }, 100)
                    },
                    willClose: () => {
                        clearInterval(timerInterval)
                        //alert('done');
                        window.location.href = '/dashboard';
                    }
                })
            }
        },
        error: function (response) {
            console.log(response);
            Swal.fire(
                'Opps!',
                'Sepertinya terjadi kesalahan, periksa kembali!',
                'error'
            )
        }
    });
}
function ValidateEmail(input) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(input)) {
        return (true)
    }
    return (false)
}
