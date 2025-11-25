function mostrarVistaPrevia(input, imgId) {
    if (input.files && input.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            const img = document.getElementById(imgId);
            if (img) {
                img.src = e.target.result;
            } else {
                console.error("No se encontró la imagen con id:", imgId);
            }
        };

        reader.readAsDataURL(input.files[0]); // convierte la imagen a base64 para mostrarla
    }
}