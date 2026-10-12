document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll("[data-telefono-list]").forEach((lista) => {
        const formulario = lista.closest("form");
        const botonAgregar = formulario.querySelector("[data-telefono-add]");

        const actualizarIndices = () => {
            lista.querySelectorAll("[data-telefono-row] input").forEach((entrada, indice) => {
                entrada.name = `Telefonos[${indice}]`;
            });
        };

        botonAgregar.addEventListener("click", () => {
            const fila = document.createElement("div");
            fila.className = "input-group mb-2";
            fila.setAttribute("data-telefono-row", "");
            fila.innerHTML = '<input type="tel" class="form-control" autocomplete="tel" /><button type="button" class="btn btn-outline-danger" data-telefono-remove aria-label="Quitar teléfono">Quitar</button>';
            lista.appendChild(fila);
            actualizarIndices();
            fila.querySelector("input").focus();
        });

        lista.addEventListener("click", (evento) => {
            const botonQuitar = evento.target.closest("[data-telefono-remove]");
            if (botonQuitar) {
                botonQuitar.closest("[data-telefono-row]").remove();
                actualizarIndices();
            }
        });

        formulario.addEventListener("submit", actualizarIndices);
    });
});
