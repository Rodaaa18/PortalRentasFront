const categoria = document.getElementById("nombreCategoria");
const boton = document.getElementById("nuevaCat");

loadEventListeners();

function loadEventListeners() {
  categoria.addEventListener("input", activarBtn);
}

function activarBtn() {
  //   for (var i = 0; i < botones.length; i++) {
  //     botones[i].classList.remove("disabled");
  //   }
  boton.classList.remove("disabled");
}
