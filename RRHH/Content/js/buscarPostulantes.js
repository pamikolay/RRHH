$(document).ready(function () {
    $("#busquedaSelect").change(function () {
        $("#userSelect").empty();
        $.ajax({
            type: 'POST',
            url: $(this).data('url'),
            dataType: 'json',
            data: { id: Number($("#busquedaSelect").val()) },
            success: function (postulantes) {
                $.each(postulantes, function (i, postulante) {
                    $("#userSelect").append('<option value="' + postulante.Value + '">' +
                        postulante.Text + '</option>');
                });
                if (postulantes.length == 0)
                {
                    $("#userSelect").append('<option value="">No hay postulantes</option>');
                }
            },
            //error: function (request, errorType, errorMessage) {
            //    alert(errorMessage);
            //}
        })
    });
});