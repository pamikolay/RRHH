$(document).ready(function(){
    $('.botonEditar').on('click', function () {
        var elementoActual=$(this);
        var idBusqueda;
        elementoActual=elementoActual.parent();		//voy al padre (el p)
        elementoActual=elementoActual.parent();		//voy al padre (el div)
        idBusqueda = elementoActual.children("span")[0].textContent;    //paso el valor del id
        Number(idBusqueda);     //lo convierto a int
        $('#datoID').val(idBusqueda);
        //darclick();
        //$('#pasarDatos').on('click', function () { $(this).submit(); });
        document.forms[0].submit();
    });
});