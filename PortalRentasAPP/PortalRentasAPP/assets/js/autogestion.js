/**
* Theme: Manejo del paginador
* Author: Mario
* Module/App: autogestion Js
*/
$('#exampleModal').on('shown.bs.modal', function () {
  $('#myInput').trigger('focus')
})

/**$().ready(function () {
    paginar(1);
});

function paginar(num)
{
    var num_registros = document.getElementById("ModeloCant").innerHTML;
    console.log(num_registros);
    $('.search-performer').attr('href', '/Inmuebles/Index?dateRange=' + $('.date-picker').val()) + ",num_pagina=" + num + ",num_registros=" + num_registros;
    $('.search-performer')[0].click();
}
*/