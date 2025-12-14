function contarYLimitarTexto(idTxt, idContador, max) {
    var txt = document.getElementById(idTxt);
    var cont = document.getElementById(idContador);
    if (!txt || !cont) return;

    if (txt.value.length > max) {
        txt.value = txt.value.substring(0, max);
    }

    var length = txt.value.length;
    cont.textContent = length + "/" + max;

    // Reset clases
    cont.classList.remove("warning", "danger");

    if (length >= max) {
        cont.classList.add("danger");
    } else if (length >= max * 0.8) {
        cont.classList.add("warning");
    }
}

document.addEventListener("DOMContentLoaded", function () {
    contarYLimitarTexto('txtDescripcion', 'contadorDescripcion', 150);
});