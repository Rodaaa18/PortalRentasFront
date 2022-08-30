function ajax(type, url, jsonData, msgAjax, msgProgreso, functionSuccess, functionError) {
    ajaxShowBack(msgAjax, msgProgreso);
    jQuery.support.cors = true;
    $.ajax({
        type: type,
        url: url,
        async: true,
        data: JSON.stringify(jsonData),
        contentType: "application/json",
        dataType: "json",
        cache: false,
        crossDomain: true,
        success: function (retorno) {
            ajaxHideBack();
            if (retorno.TipoError == 2) {
                window.location = urlContent + retorno.MensajeError;
            }
            else {
                if (retorno.Success) {
                    functionSuccess(retorno);
                }
                else {
                    if (retorno.TipoError == 1) {
                        //error
                        msjError("Error", retorno.MensajeError, urlContent + retorno.UrlImagen, functionError);
                    }
                    else if (retorno.TipoError == 3) {
                        //alerta
                        msjWarning("Alerta", retorno.MensajeError, urlContent + retorno.UrlImagen, functionError);
                    }
                    else if (retorno.TipoError == 4) {
                        //info
                        msjInfo("Informaci&oacute;n", retorno.MensajeError, urlContent + retorno.UrlImagen, functionError);
                    }
                }
            }
        },
        error: function (objeto, error, objeto2) {
            ajaxHideBack();
            msjWarning("Alerta", mensajeErrorConexionAjax, urlContent + urlImagenErrorConexionAjax, null);
        }
    });
}

function ajaxSinBloqueo(type, url, jsonData, msgAjax, msgProgreso, functionSuccess, functionError) {
    jQuery.support.cors = true;
    $.ajax({
        type: type,
        url: url,
        async: true,
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        crossDomain: true,
        success: function (retorno) {
            if (retorno.TipoError == 2) {
                window.location = urlContent + retorno.MensajeError;
            }
            else {
                if (retorno.Success) {
                    functionSuccess(retorno);
                }
                else {
                    if (retorno.TipoError == 1) {
                        //error
                        msjError("Error", retorno.MensajeError, urlContent + retorno.UrlImagen, functionError);
                    }
                    else if (retorno.TipoError == 3) {
                        //alerta
                        msjWarning("Alerta", retorno.MensajeError, urlContent + retorno.UrlImagen, functionError);
                    }
                    else if (retorno.TipoError == 4) {
                        //info
                        msjInfo("Información", retorno.MensajeError, urlContent + retorno.UrlImagen, functionError);
                    }
                }
            }
        },
        error: function (objeto, error, objeto2) {
            msjWarning("Alerta", mensajeErrorConexionAjax, urlContent + urlImagenErrorConexionAjax, null);
        }
    });
}

function ajaxShowBack(msgAjax, msgProgreso) {
	$.blockUI({
        blockMsgClass: 'alertBox',
        message: '<div class="spinner-border text-primary" role="status">' +
                 '   <span class= "sr-only" >' + msgAjax + '</span>' +
                 '   </div>' +
                 '<h5>' + msgProgreso + '</h5>' });
}
function procesarRespuesta(retorno) {
    if (retorno.TipoError == 2) {
        window.location = urlContent + retorno.MensajeError;
    }
    else {
        if (retorno.Success) {
            return retorno.InnerObject;
        }
        else {
            /* Error */
            var msj = "";
            var title = "";
            if (retorno.InnerObject != null && retorno.InnerObject != 'undefined') {
                var item = 0;

                for (item in retorno.InnerObject) {
                    msj += retorno.InnerObject[item] + "<br />";
                }

                title = retorno.MensajeError;
            }
            else {
                msj = "<b>Se gener&oacute; un error:</b><br />" + retorno.MensajeError;
                title = "Error";
            }

            msjError(title, msj);
            return null;
        }
    }
}

function ajaxHideBack() {
    $.unblockUI();
}