$(document).ready(function(){
    $('.botonEditar').on('click', function () {
        var elementoActual=$(this);
        var parrafoBusqueda;
        var tituloBusqueda;
        var idBusqueda;
        elementoActual=elementoActual.parent();		//voy al padre (el p)
        elementoActual=elementoActual.parent();		//voy al padre (el div)
        parrafoBusqueda = (elementoActual.children("p")[1]).children[1].textContent;    //paso al modal el parrafo
        tituloBusqueda = (elementoActual.children("h3")[0]).children[1].textContent;    //paso al modal el titulo
        
        idBusqueda = elementoActual.children("span")[0].textContent;    //paso el valor del id
        Number(idBusqueda);     //lo convierto a int
        $('#modalIdBusqueda').val(idBusqueda);
        
        $('#modalTituloBusqueda').val(tituloBusqueda);
        $('#modalDescripcionBusqueda').text(parrafoBusqueda);		//le paso el parrafo de la noticia al modal
        $('#modalEditarBusqueda').modal('show');
    });
});