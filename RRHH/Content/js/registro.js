$(document).ready(function () {
    $(':input[type="submit"]').prop('disabled', true);

    //$("#btnPreSubmit").click(function(){
    //    var x = $("#MailOk").text();
    //    var y = $("#PassOk").text();
    //    if (x != "El email se puede utilizar" || y != "El password es identico") {
    //        forms[0].submit();
    //    }
    //});

    $("#contact_form").submit(function () {
        var x = $("#MailOk").text();
        var y = $("#PassOk").text();
        if (x != "El email se puede utilizar" || y != "El password es identico") {
            if (x != "El email se puede utilizar" && y == "El password es identico")
            {
                $("#Validaciones").empty();
                $("#Validaciones").append("<span id='ValidacionesError'>El email no esta validado</span>");
                return false;
            }
            if (y != "El password es identico" && x == "El email se puede utilizar") {
                $("#Validaciones").empty();
                $("#Validaciones").append("<span id='ValidacionesError'>El password no esta correcto</span>");
                return false;
            }
            $("#Validaciones").empty();
            $("#Validaciones").append("<span id='ValidacionesError'>El email no esta validado y el password no es correcto</span>");
        } else
            $("#Validaciones").empty();
            return true;
    });
    
    $('#contact_form').bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            first_name: {
                validators: {
                        stringLength: {
                        min: 2,
                    },
                        notEmpty: {
                        message: 'Por favor ingrese su nombre'
                    }
                }
            },
             last_name: {
                validators: {
                     stringLength: {
                        min: 2,
                    },
                    notEmpty: {
                        message: 'Por favor ingrese su apellido'
                    }
                }
            },
            email: {
                validators: {
                    notEmpty: {
                        message: 'Por favor ingrese su e-mail'
                    },
                    emailAddress: {
                        message: 'Por favor ingrese una dirección valida de e-mail'
                    }
                }
            },
            password: {
                validators: {
                    stringLength: {
                        min: 4,
                    },
                    notEmpty: {
                        message: 'Por favor ingrese una contraseña de minimo 4 caracteres'
                    }
                }
            },
            phone: {
                validators: {
                    notEmpty: {
                        message: 'Por favor ingrese su telefono'
                    },
                    phone: {
                        country: 'AR',
                        message: 'Por favor ingrese un número valido con codigo de area'
                    }
                }
            },
            address: {
                validators: {
                     stringLength: {
                        min: 8,
                    },
                    notEmpty: {
                        message: 'Por favor ingrese su dirección'
                    }
                }
            },
            city_id: {
                validators: {
                    notEmpty: {
                        message: 'Por favor seleccione su ciudad'
                    }
                }
            },
            province_id: {
                validators: {
                    notEmpty: {
                        message: 'Por favor seleccione su provincia'
                    }
                }
            },
            }
        })
        .on('success.form.bv', function(e) {
            $('#success_message').slideDown({ opacity: "show" }, "slow") // Do something ...
                $('#contact_form').data('bootstrapValidator').resetForm();


            // Prevent form submission
            e.preventDefault();

            // Get the form instance
            var $form = $(e.target);

            // Get the BootstrapValidator instance
            var bv = $form.data('bootstrapValidator');

            // Use Ajax to submit form data
            $.post($form.attr('action'), $form.serialize(), function(result) {
                console.log(result);
            }, 'json');
        });
});
$("#provinceSelect").change(function () {
    $("#citySelect").empty();
    $.ajax({
        type: 'POST',
        //@*url: '@Url.Action("GetCiudades","Login")',*@
        url: $(this).data('url'),
        dataType: 'json',
        data: { id: Number($("#provinceSelect").val()) },
        success: function (ciudades) {
            $.each(ciudades, function (i, ciudad) {
                $("#citySelect").append('<option value="' + ciudad.Value + '">' +
                ciudad.Text + '</option>');

            });
        },
        //error: function (request, errorType, errorMessage) {
        //    alert(errorMessage);
        //}
    })
});
$("#ValidarEmail").click(function () {
    $.ajax({
        type: 'POST',
        url: $("#emailInput").data('url'),
        dataType: 'json',
        data: { email: String($("#emailInput").val()) },
        success: function (existe) {
            if (existe > 0)
            {
                $("#CheckMail").empty();
                $("#CheckMail").append("<span id='MailError'>El email ya se encuentra registrado</span>");
            }
            else {
                $("#CheckMail").empty();
                $("#CheckMail").append("<span id='MailOk'>El email se puede utilizar</span>");
                $("#btnSubmit").removeAttr('disabled');
                $("#Validaciones").empty();
            }
        },
        //error: function (request, errorType, errorMessage) {
        //    alert(errorMessage);
        //}
    })
});
$("#paswordInput2").change(function () {
    $.ajax({
        type: 'POST',
        url: $("#paswordInput2").data('url'),
        dataType: 'json',
        data: { pass1: String($("#paswordInput1").val()), pass2: String($("#paswordInput2").val()) },
        success: function (existe) {
            if (existe > 0) {
                $("#CheckPassword").empty();
                $("#CheckPassword").append("<span id='PassError'>Los passwords no son identicos</span>");
            }
            else {
                $("#CheckPassword").empty();
                $("#CheckPassword").append("<span id='PassOk'>El password es identico</span>");
                $("#btnSubmit").removeAttr('disabled');
                $("#Validaciones").empty();
            }
        },
        //error: function (request, errorType, errorMessage) {
        //    alert(errorMessage);
        //}
    })
});

$("#paswordInput1").change(function () {
    $.ajax({
        type: 'POST',
        url: $("#paswordInput2").data('url'),
        dataType: 'json',
        data: { pass1: String($("#paswordInput1").val()), pass2: String($("#paswordInput2").val()) },
        success: function (existe) {
            if (existe > 0) {
                $("#CheckPassword").empty();
                $("#CheckPassword").append("<span id='PassError'>Los passwords no son identicos</span>");
            }
            else {
                $("#CheckPassword").empty();
                $("#CheckPassword").append("<span id='PassOk'>El password es identico</span>");
                $("#btnSubmit").removeAttr('disabled');
                $("#Validaciones").empty();
            }
        },
        //error: function (request, errorType, errorMessage) {
        //    alert(errorMessage);
        //}
    })
});

$("#emailInput").change(function () {
    $("#CheckMail").empty();
});