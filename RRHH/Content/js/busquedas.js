$(document).ready(function(){
    $('.botonEditar').on('click', function () {
        var elementoActual=$(this);
        var idBusqueda;
        elementoActual=elementoActual.parent();		//voy al padre (el p)
        elementoActual=elementoActual.parent();		//voy al padre (el div)
        idBusqueda = elementoActual.children("span")[0].textContent;    //paso el valor del id
        Number(idBusqueda);     //lo convierto a int
        $('#datoIDeditar').val(idBusqueda);
        document.forms[0].submit();     //voy al evento click del boton submit para enviar el ID de la busqueda al JobController
    });

    $('.botonPostularse').on('click', function () {
        var elementoActual = $(this);
        var idBusqueda;
        elementoActual = elementoActual.parent();		//voy al padre (el p)
        elementoActual = elementoActual.parent();		//voy al padre (el div)
        idBusqueda = elementoActual.children("span")[0].textContent;    //paso el valor del id
        Number(idBusqueda);     //lo convierto a int
        $('#datoIDpostularse').val(idBusqueda);
        document.forms[1].submit();     //voy al evento click del boton submit para enviar el ID de la busqueda al JobController
    });
});