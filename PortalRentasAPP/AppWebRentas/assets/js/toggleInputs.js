//Bloquear Inputs
const fieldset1 = document.getElementById("fieldset1");
const fieldset2 = document.getElementsByClassName("input-tabla");
for (var i = 0; i < fieldset2.length; i++) {
  fieldset2[i].disabled = true;
}

fieldset1.disabled = true;

//Agarrar Botones Editar y Crear Nuevo
const btnToggle1 = document.getElementsByClassName("activar-inputs");
const btnToggle2 = document.getElementsByClassName("desactivar-inputs");

loadEventListeners();

function loadEventListeners() {
  for (var i = 0; i < btnToggle1.length; i++) {
    btnToggle1[i].addEventListener("click", activarFieldset);
  }

  for (var i = 0; i < btnToggle2.length; i++) {
    btnToggle2[i].addEventListener("click", desactivarFieldset);
  }
}

//Activar los inputs
function activarFieldset(e) {
  fieldset1.disabled = false;
  for (var i = 0; i < fieldset2.length; i++) {
    fieldset2[i].disabled = false;
  }

  e.preventDefault();
}

//Desactivar los inputs cuando se clickean los botones de cancelar or guardar
function desactivarFieldset(e) {
  fieldset1.disabled = true;
  for (var i = 0; i < fieldset2.length; i++) {
    fieldset2[i].disabled = true;
  }

  e.preventDefault();
}
