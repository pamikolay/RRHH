$(document).ready(function () {
    $(':input[type="submit"]').prop('disabled', true);

    //$("#proviceSelect").change(function () {
    //    fillCombo("citySelect", $("#proviceSelect").val());
    //});
    

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
