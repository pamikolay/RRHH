$(document).ready(function(){
    $('.botonEditar').on('click', function () {
        var elementoActual=$(this);
        var parrafoBusqueda;
        var tituloBusqueda;
        elementoActual=elementoActual.parent();		//voy al padre (el p)
        elementoActual=elementoActual.parent();		//voy al padre (el div)
        parrafoBusqueda=elementoActual.children("p#descripcionBusqueda");		//apunto al contenido de la noticia
        tituloBusqueda = elementoActual.children("h3");	//apunto al contenido del titulo

        $('#modalEditarBusqueda').text(parrafoBusqueda.text());	//le paso el parrafo de la noticia al modal
        if($("#modal-parrafoOculto").length == 0) {		//compruebo si existe el parrafoOculto para q no se repita
            $('#modal-bodyNoticias').append('<p id="modal-parrafoOculto" class="modal-parrafo"></p>');
        }	//si no existe lo agrego
        $('#modal-titleNoticias').text(tituloBusqueda.text());		//le paso el titulo de la noticia al modal
        $('#modalDINAMICO').modal('show');
    });
});