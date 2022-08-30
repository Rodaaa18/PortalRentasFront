const tramite = document.getElementById("tramites");
const botones = document.getElementsByClassName("btntramite");

loadEventListeners();

function loadEventListeners() {
  tramite.addEventListener("click", activarBtn);
}

function activarBtn() {
  for (var i = 0; i < botones.length; i++) {
    botones[i].classList.remove("disabled");
  }
}
