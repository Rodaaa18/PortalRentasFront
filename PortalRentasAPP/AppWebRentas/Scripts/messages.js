function msjError(title, msj, urlImagen, funCerrar) {
    agregarTexto('modal-danger', title, msj, urlImagen);
    $('#btn_aceptar').hide();
    $('#btn_cerrar').on('click', function () {
        cerrarModal(funCerrar);
    });
}

function msjSuccess(title, msj, urlImagen, funCerrar) {
    agregarTexto('modal-success', title, msj, urlImagen);
    $('#btn_aceptar').hide();
    $('#btn_cerrar').on('click', function () {
        cerrarModal(funCerrar);
    });

}

function msjInfo(title, msj, urlImagen, funCerrar) {
    agregarTexto('modal-info', title, msj, urlImagen);
    $('#btn_aceptar').hide();
    $('#btn_cerrar').on('click', function () {
        cerrarModal(funCerrar);
    });
}

function msjWarning(title, msj, urlImagen, funCerrar) {
    agregarTexto('modal-warning', title, msj, urlImagen);
    $('#btn_aceptar').hide();
    $('#btn_cerrar').on('click', function () {
        cerrarModal(funCerrar);
    });
}

function msjPregunta(title, msj, urlImagen, funAceptar, funCerrar) {
    agregarTexto('modal-primary', title, msj, urlImagen);
    $('#btn_aceptar').show();

    $('#btn_aceptar').on('click', function () {
        aceptarModal(funAceptar);
    });
    $('#btn_cerrar').on('click', function () {
        cerrarModal(funCerrar);
    });
}

function agregarTexto(modaltype, title, msj, icon) {
    $('#contenedor_modal').removeClass().addClass(modaltype);
    $('#titulo_modal').html(title);
    $('#texto_modal span').html(msj);
    $('#texto_modal img').attr("src", icon);
    $('#small_modal').modal('show');
}

function cerrarModal(funCerrar) {
    $('#small_modal').modal('hide');
    if (funCerrar != null) {
        funCerrar();
    }
}

function aceptarModal(funAceptar) {
    $('#small_modal').modal('hide');
    if (funAceptar != null) {
        funAceptar();
    }
}