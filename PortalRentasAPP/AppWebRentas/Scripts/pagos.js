function Pagar(formaPago, idGrilla, payPerticVersion, mensajeCanalNoSoportado, urlImagenCanalNoSoportado, StrEfectivo, StrporcentajeDescuento, StrDominioauto = null ) {
    var url = urlContent + "DashBoard/PagoPaypertic";
    if (formaPago == 'PLUSPAGOS') {
        url = urlContent + "DashBoard/PlusPagos";
    }
    var SrtIdCtacte = "";
    var StrDetalle = "";
    var StrMonto = "";
    var StrCuenta = "";
    var contador = 0;
    //var StrEfectivo = "0";
    var StrDetalleMultiple = "";
    var StrEsAnual = "";
    var valorMonto = 0;

    $(`#${idGrilla}`).DataTable().rows().every(function () {
        var data = this.data();
        var rowIndex = this.index();
        var domCheck = $($(`#${idGrilla}`).DataTable().cell(rowIndex, 0).node()).find('input');
        if (domCheck.is(":checked")) {
            if (payPerticVersion == "2") //Version 2 api paypertic
            {
                StrMonto = data.Total;
                //console.log(StrMonto);
                StrMonto = StrMonto.replace("$ ", "").replace(".", "");
                StrMonto = StrMonto.replace(",", ".");
                //console.log(StrMonto);
                if (StrEfectivo == "2" | StrEfectivo == "3") {
                    if (data.Anual == "1") {
                        valorMonto = parseFloat(StrMonto);
                        valorMonto = valorMonto - (valorMonto * (parseFloat(StrporcentajeDescuento) / 100));
                        StrMonto = valorMonto.toFixed(2).toString();
                    }
                }
                console.log(StrMonto);

                if (contador == 0) {
                    StrEsAnual = data.Anual
                    StrDetalle = "Cuota: " + data.Cuota + " (" + data.Detalle + ")";
                    StrCuenta = data.IdCtaCte;
                }
                SrtIdCtacte += domCheck.prop("id");
                StrDetalleMultiple += (contador != 0 ? ", " : "") + "{\n                " +
                    "\"external_reference\": \"" + domCheck.prop("id") + "\",\n                " +
                    "\"concept_id\":\"" + contador.toString() + "\",\n                " +
                    "\"concept_description\":\"" + "Cuota: " + data.Cuota + " (" + data.Detalle + (StrDominioauto == null ? '' : "-" + StrDominioauto)+")" + "\",\n                " +
                    "\"amount\":" + StrMonto + "\n            }\n            ";
            }
            else {
                StrEsAnual = data.Anual;
                SrtIdCtacte = domCheck.prop("id");
                StrDetalle = "Cuota: " + data.Cuota + " (" + data.Detalle + ")";
                StrCuenta = data.IdCtaCte;
                StrMonto = data.Total;
            }
            contador++;
        }
    });
    console.log(StrDetalleMultiple);
    if (contador > 1 && payPerticVersion == "2") //Version 2 api paypertic
    {
        if (StrDetalleMultiple !== "")
            StrDetalleMultiple = "\"details\": [\n            " + StrDetalleMultiple + "         ], \n    ";
        //StrEfectivo = contador.toString();
    } else if (contador > 1 && payPerticVersion != "2") {
        return;
    } 
    if(StrEfectivo === "1") {
        SrtIdCtacte += "c";
    }
    if(StrEfectivo === "2") {
        SrtIdCtacte += "h";
    }
    if(StrEfectivo === "3") {
        SrtIdCtacte += "k";
    }

    var data = {
        srt_idCtacte: SrtIdCtacte,
        str_cuenta: StrCuenta,
        str_detalle: StrDetalle,
        str_monto: StrMonto,
        str_efectivo: StrEfectivo,
        str_dominioauto: StrDominioauto,
        str_esanual: StrEsAnual,
        str_pagomultiple: contador == 1 ? '0' : StrDetalleMultiple
    };

    ajax("post", url, data, "Aguarde un instante...", "Abriendo canal de pago...", function (data) {
        if (formaPago == "PAYPER") {
            window.open(data.InnerObject.Url, '_blank');
        }
        else if (formaPago == "PLUSPAGOS") {
            $("#div_pluspagos").append(data.InnerObject.Content);
            $("#frm_pluspagos").submit();
        }
        else {
            msjWarning("Advertencia", mensajeCanalNoSoportado, urlContent + urlImagenCanalNoSoportado);
        }
    }, null);
}

function PagarTarjeta(formaPago, idGrilla, mensajeCanalNoSoportado, urlImagenCanalNoSoportado, StrDominioauto) {
    var url = urlContent + "DashBoard/PagoPaypertic";
    if (formaPago == 'PLUSPAGOS') {
        url = urlContent + "DashBoard/PlusPagos";
    }
    var SrtIdCtacte = "";
    var StrDetalle = "";
    var StrMonto = "";
    var StrCuenta = "";
    var contador = 0;
    var StrEfectivo = "0";
    var StrEsAnual = "";

    // Recorremos todos los checkbox para contar los que estan seleccionados
    $(`#${idGrilla}`).DataTable().rows().every(function () {
        var data = this.data();
        var rowIndex = this.index();
        var domCheck = $($(`#${idGrilla}`).DataTable().cell(rowIndex, 0).node()).find('input');
        if (domCheck.is(":checked")) {
            SrtIdCtacte = domCheck.prop("id");
            StrDetalle = "Cuota: " + data.Cuota + " (" + data.Detalle + ")";
            StrCuenta = data.IdCtaCte;
            StrMonto = data.Total;
            contador++;
        }
    });

    if (contador == 1) {
        var data = {
            srt_idCtacte: SrtIdCtacte,
            str_cuenta: StrCuenta,
            str_detalle: StrDetalle,
            str_monto: StrMonto,
            str_efectivo: StrEfectivo,
            str_esanual: StrEsAnual,
            str_dominioauto: StrDominioauto
        };

        ajax("post", url, data, "Aguarde un instante...", "Abriendo canal de pago...", function (data) {
            if (formaPago == "PAYPER") {
                window.open(data.InnerObject.Url, '_blank');
            }
            else if (formaPago == "PLUSPAGOS") {
                $("#div_pluspagos").append(data.InnerObject.Content);
                $("#frm_pluspagos").submit();
            }
            else {
                msjWarning("Advertencia", mensajeCanalNoSoportado, urlContent + urlImagenCanalNoSoportado);
            }
        }, null);
    }
}

function DibujarCheckPagos(idGrilla, verPayPertic, data, type, row, meta) {
    if (data.IdCtaCte == '' && data.Pendiente == '-1') {
        return '';
    }

    if (data.Pendiente == 'DashBoard/PlusPagos') {
        var url = urlContent + "DashBoard/PlusPagos";

        return `<a>
                    <span id="btnPagarPendPlus" type="button" class="btn move">Pend.Acred.</span>
                </a>`;
    }

    if (data.Pendiente != '') {
        return `<a href="${data.Pendiente}">
                    <span id="btnPagarPend" type="button" class="btn move">Pend.Acred.</span>
                </a>`;
    }
    return `<div class="checkbox checkbox-primary">
                <input onchange="verificarPagosSeleccionados('${idGrilla}','${verPayPertic}');" id="chk${data.IdCtaCte}" type="checkbox"> 
            </div>`;
}

function verificarPagosSeleccionados(idGrilla, verPayPertic) {
    var contador = 0;
    var StrMonto = 0;
    var montoTotal = 0;
    $(`#${idGrilla}`).DataTable().rows().every(function () {
        var data = this.data();
        var rowIndex = this.index();
        var domCheck = $($(`#${idGrilla}`).DataTable().cell(rowIndex, 0).node()).find('input');
        if (domCheck.is(":checked")) {
            StrMonto = data.Total;
            StrMonto = StrMonto.replace("$ ", "");
            StrMonto = StrMonto.replace(".", "");
            StrMonto = StrMonto.replace(".", "");
            StrMonto = StrMonto.replace(",", ".");
            montoTotal += parseFloat(StrMonto);
            contador++;
        }
            
    });
    console.log(montoTotal);
    document.getElementById("montoTotal").innerHTML = "$ " + montoTotal.toFixed(2);
    if (verPayPertic == "2") //Version 2 api paypertic
    {
        if (contador == 0) {
            $('#btnPagar').attr('disabled', true);
            $('#btnPagarTar').attr('disabled', true);
            $('#btnPagarCupon').attr('disabled', true);
        } else {
            $('#btnPagar').attr('disabled', false);
            $('#btnPagarTar').attr('disabled', false);
            $('#btnPagarCupon').attr('disabled', false);
        }
    } else {
        if (contador == 1) {
            $('#btnPagar').attr('disabled', false);
            $('#btnPagarTar').attr('disabled', false);
        } else {
            $('#btnPagar').attr('disabled', true);
            $('#btnPagarTar').attr('disabled', true);
        }
    }
}