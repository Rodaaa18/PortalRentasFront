function crearGrilla(id, url, entidad, columnas, funcionFiltro, busqueda, orden, columnaOrderDefault, orderDefault, funcionTransformacion) {
    $(id).on('error.dt', function (e, settings, techNote, message) {
        console.log(message);
        msjError("Error", "Se generó un error mientras intentabamos listar las/os " + entidad + ".");
    }).DataTable({
        order: (columnaOrderDefault != null && columnaOrderDefault != 'undefined' ) ? [[columnaOrderDefault, orderDefault]] : [],
        language: {
            "processing": `<div class="spinner-border text-primary" role="status">
                            <span class= "sr-only" >Cargando..</span >
                           </div>`,
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": `<div class="spinner-border text-primary" role="status">
                            <span class= "sr-only" >Cargando..</span >
                           </div>`,
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            },
            "buttons": {
                "copy": "Copiar",
                "colvis": "Visibilidad"
            }
        },
        "processing": true,
        "filter": true,
        "serverSide": true,
        "destroy": true,
        "responsive": true,
        "info": true,
        "searching": busqueda == null ? false : busqueda,
        "paging": true,
        "sort": orden == null ? false : orden,
        "pagingType": "full_numbers",
        "lengthMenu": [[10, 20, 50], [10, 20, 50]],
        "ajax": {
            "url": urlContent + url,
            "type": "POST",
            dataFilter: function (data) {
                var json = JSON.parse(data);

                var error = false;
                if (json.TipoError == 2) {
                    window.location = urlContent + json.MensajeError;
                }
                else if (json.TipoError == 1) {
                    //error
                    msjError("Error", json.MensajeError, urlContent + json.UrlImagen, null);
                    error = true;
                }
                else if (json.TipoError == 3) {
                    //alerta
                    msjWarning("Alerta", json.MensajeError, urlContent + json.UrlImagen, null);
                    error = true;
                }
                else if (json.TipoError == 4) {
                    //info
                    msjInfo("Informaci&oacute;n", json.MensajeError, urlContent + json.UrlImagen, null);
                    error = true;
                }

                if (funcionTransformacion != null && funcionTransformacion != 'undefined') {
                    funcionTransformacion(json.InnerObject);
                }

                json.data = error ? [] : json.InnerObject.data;
                json.draw = error ? null : json.InnerObject.draw;
                json.recordsTotal = error ? 0 : json.InnerObject.recordsTotal;
                json.recordsFiltered = error ? 0 : json.InnerObject.recordsTotal;
                return JSON.stringify(json);
            },
            "data": function (d) {
                var extraParams = {}
                if (funcionFiltro != null) {
                    extraParams = funcionFiltro();
                }
                return $.extend({}, d, extraParams);
            }
        },
        "columns": columnas
    });
}

function crearGrillaSinPaginacion(id, url, entidad, columnas, funcionFiltro) {
    $(id).on('error.dt', function (e, settings, techNote, message) {
        console.log(message);
        msjError("Error", "Se generó un error mientras intentabamos listar las/os " + entidad + ".");
    }).DataTable({
        language: {
            "processing": `<div class="spinner-border text-primary" role="status">
                            <span class= "sr-only" >Cargando..</span >
                           </div>`,
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": `<div class="spinner-border text-primary" role="status">
                            <span class= "sr-only" >Cargando..</span >
                           </div>`, "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            },
            "buttons": {
                "copy": "Copiar",
                "colvis": "Visibilidad"
            }
        },
        "responsive": true,
        "processing": true,
        "serverSide": false,
        "destroy": true,
        "info": true,
        "searching": false,
        "paging": false,
        "sort": false,
        "ajax": {
            "url": urlContent + url,
            "type": "POST",
            dataFilter: function (data) {
                var json = JSON.parse(data);

                var error = false;
                if (json.TipoError == 2) {
                    window.location = urlContent + json.MensajeError;
                }
                else if (json.TipoError == 1) {
                    //error
                    msjError("Error", json.MensajeError, urlContent + json.UrlImagen, null);
                    error = true;
                }
                else if (json.TipoError == 3) {
                    //alerta
                    msjWarning("Alerta", json.MensajeError, urlContent + json.UrlImagen, null);
                    error = true;
                }
                else if (json.TipoError == 4) {
                    //info
                    msjInfo("Informaci&oacute;n", json.MensajeError, urlContent + json.UrlImagen, null);
                    error = true;
                }

                json.data = error ? [] : json.InnerObject.data;
                json.draw = error ? null : json.InnerObject.draw;
                json.recordsTotal = error ? 0 : json.InnerObject.recordsTotal;
                json.recordsFiltered = error ? 0 : json.InnerObject.recordsTotal;
                return JSON.stringify(json);
            },
            "data": function (d) {
                var extraParams = {}
                if (funcionFiltro != null) {
                    extraParams = funcionFiltro();
                }
                return $.extend({}, d, extraParams);
            }
        },
        "columns": columnas
    });
}
function ActualizarGrilla(id, reload = false) {
    $(id).DataTable().ajax.reload(null, reload);
}