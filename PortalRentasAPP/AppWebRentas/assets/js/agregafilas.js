// Define Variables de la UI
const tabla = document.querySelector("#docurequerida");
const agregaBtn = document.getElementById("agregafila");
const eliminaBtn = document.getElementsByClassName("table");
const fila = document.querySelector("#fila");

// Carga event listeners
loadEventListeners();

// Load all event listeners
function loadEventListeners() {
  // Add task event
  agregaBtn.addEventListener("click", agregaFila);
  // Clear task event
  for (var i = 0; i < eliminaBtn.length; i++) {
    eliminaBtn[i].addEventListener("click", eliminaFila);
  }
}

// Agrega Fila
function agregaFila(e) {
  // Crea elementos
  const tr = document.createElement("tr");
  // const th = document.createElement("th");
  const td = document.createElement("td");
  const input = document.createElement("input");
  input.className = "input-tabla";
  const td2 = document.createElement("td");
  const input2 = document.createElement("input");
  input2.className = "input-tabla";
  const td3 = document.createElement("td");
  const input3 = document.createElement("input");
  input3.className = "input-tabla";
  const td4 = document.createElement("td");
  const button = document.createElement("button");
  button.innerHTML = "x";
  button.className = "eliminafila btn btn-outline-danger btn-sm";
  button.setAttribute("id", "eliminafila");
  // Columna 1
  // tr.appendChild(th);
  tr.appendChild(td);
  td.appendChild(input);

  // Columna 2
  tr.appendChild(td2);
  td2.appendChild(input2);

  // Columna 3
  tr.appendChild(td3);
  td3.appendChild(input3);

  tr.appendChild(td4);
  td4.appendChild(button);

  //Agrega Fila a Tabla
  tabla.appendChild(tr);
  e.preventDefault();
}

// Eliminar Task
function eliminaFila(e) {
  if (e.target.classList.contains("eliminafila")) {
    if (confirm("Estás seguro?")) {
      e.target.parentElement.parentElement.remove();
    }
  }
}
