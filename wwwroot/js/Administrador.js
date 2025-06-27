// Sidebar functionality (no changes needed, it's purely client-side)
let btn = document.querySelector("#btn");
let sidebar = document.querySelector(".sidebar");

if (btn && sidebar) {
    btn.onclick = function () {
        sidebar.classList.toggle("active");
    };
} else {
    console.error("Sidebar or button element not found.");
}

/**
 * Registers a generic user/apprentice movement (e.g., entry/exit).
 * Assumes a C# controller named 'AdminController' with a 'RegisterMovement' action.
 * @param {string} tipo - The type of movement (e.g., 'Entrada', 'Salida').
 */
function registrar(tipo) {
    const id = localStorage.getItem("Id");
    const tipoUsuario = localStorage.getItem("Tipo");

    if (!id || !tipoUsuario) {
        alert("No se encontró información del usuario o aprendiz. Por favor, realice una búsqueda primero.");
        return;
    }

    // --- CAMBIO CLAVE PARA ASP.NET CORE MVC ---
    // La URL '../controllers/Administrador.php' se cambia a la ruta de tu controlador C#.
    // Asumimos que tienes un controlador llamado 'AdminController' con una acción 'RegisterMovement'.
    // Si usas routing por defecto o ApiController con convenciones, podría ser '/Admin/RegisterMovement'
    // o '/api/Admin/RegisterMovement' si es un controlador de API puro.
    fetch('/Admin/RegisterMovement', { // Ajusta esta ruta según tu configuración de routing en C#
        method: 'POST',
        headers: {
            // Content-Type depende de cómo esperas los datos en tu controlador C#.
            // Si usas [FromForm], 'application/x-www-form-urlencoded' es correcto.
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: `movimiento=${tipo}&id=${id}&tipoUsuario=${tipoUsuario}`
    })
    .then(response => {
        if (!response.ok) { // Manejar respuestas HTTP no exitosas
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.text(); // Esperamos texto, como en tu PHP original
    })
    .then(data => alert(data))
    .catch(error => console.error('Error al registrar movimiento:', error));
}

/**
 * Searches for a user or apprentice by document number.
 * Assumes a C# controller named 'AdminController' with a 'SearchDocument' action.
 */
function buscarDocumento() {
    var documento = document.getElementById("buscarDocumento").value;
    var mensajeBusqueda = document.getElementById("mensaje-busqueda");

    if (documento.trim() === "") {
        mensajeBusqueda.innerHTML = "Por favor, ingrese un documento.";
        return;
    }

    // --- CAMBIO CLAVE PARA ASP.NET CORE MVC ---
    // La URL '../controllers/Administrador.php' se cambia a la ruta de tu controlador C#.
    // Asumimos un controlador 'AdminController' con una acción 'SearchDocument'.
    fetch(`/Admin/SearchDocument?documento=${documento}`) // Ajusta esta ruta
        .then(response => {
            if (!response.ok) {
                // Si el servidor devuelve un 404 (NotFound) o 400 (BadRequest), lo manejamos como error.
                return response.json().then(err => { throw new Error(err.error || 'Error en la respuesta del servidor.'); });
            }
            return response.json();
        })
        .then(data => {
            if (data.error) {
                mensajeBusqueda.innerHTML = data.error;
                return;
            }

            let resultadoHTML = ""; // Iniciar vacío, sin <ul> si no es necesario

            if (data.tipo === "aprendiz") {
                localStorage.setItem("Id", data.datos.IdAprendiz);
                localStorage.setItem("Tipo", data.tipo);
                resultadoHTML += `<div style="font-size: 14px; line-height: 1.4;">`
                resultadoHTML += `<img src="${data.imagen}" alt="Foto del aprendiz" style="display: block; margin: 0 auto 15px auto; border-radius: 10px; width: 150px;">`;
                resultadoHTML += `<p><strong>Nombre:</strong> ${data.datos.Nombre}</p>`;
                resultadoHTML += `<p><strong>Rol:</strong> Aprendiz</p>`;
                resultadoHTML += `<p><strong>RH:</strong> ${data.datos.RH}</p>`;
                resultadoHTML += `<p><strong>Tipo de Programa:</strong> ${data.datos.TipoPrograma}</p>`;
                resultadoHTML += `<p><strong>Programa:</strong> ${data.datos.Programa}</p>`;
                resultadoHTML += `</div>`
            } else if (data.tipo === "usuario") {
                localStorage.setItem("Id", data.datos.IdUsuario);
                localStorage.setItem("Tipo", data.tipo);

                resultadoHTML += `<p><strong>Nombre:</strong> ${data.datos.Nombre}</p>`;
                resultadoHTML += `<p><strong>Rol:</strong> ${data.datos.Rol}</p>`;
                resultadoHTML += `<p><strong>Email:</strong> ${data.datos.Email}</p>`;
            }

            // Agregar el estado del movimiento (Entrada/Salida)
            resultadoHTML += `<p style="font-size: 14px; line-height: 1.4;"><strong>Último Movimiento:</strong> ${data.movimiento}</p>`;

            // resultadoHTML += "</ul>"; // Si iniciaste con <ul>, termina aquí.
            mensajeBusqueda.innerHTML = resultadoHTML;
            cargarMateriales();
            cargarVehiculos();
        })
        .catch(error => {
            mensajeBusqueda.innerHTML = "Error en la búsqueda: " + error.message;
            console.error("Error en buscarDocumento:", error);
        });
}

/**
 * Loads materials associated with the currently selected user/apprentice.
 * Assumes a C# controller named 'ElementsController' with a 'GetElements' action.
 */
function cargarMateriales() {
    const tbody = document.querySelector("#Material tbody");
    tbody.innerHTML = ""; // Limpiar filas existentes
    const idUsuario = localStorage.getItem("Id");
    const tipoUsuario = localStorage.getItem("Tipo");

    if (!idUsuario) {
        console.error("No se encontró ID de usuario para cargar materiales.");
        return;
    }

    // --- CAMBIO CLAVE PARA ASP.NET CORE MVC ---
    // La URL '../controllers/MostrarElemento.php' se cambia a la ruta de tu controlador C#.
    // Asumimos un controlador 'ElementsController' con una acción 'GetElements'.
    fetch(`/Elements/GetElements?idUsuario=${idUsuario}&tipoUsuario=${tipoUsuario}`) // Ajusta esta ruta
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            if (data.error) {
                console.error("Error al obtener materiales:", data.error);
                return;
            }

            // Asegúrate de que 'data' sea un array.
            // Si tu C# devuelve { "materiales": [...] }, necesitarías 'data.materiales'.
            const materiales = Array.isArray(data) ? data : (data.materiales || []);

            if (materiales.length === 0) {
                tbody.innerHTML = `
                    <tr>
                        <td colspan="6" class="text-center">No se han registrado materiales</td>
                    </tr>
                `;
                return;
            }

            materiales.forEach(material => {
                const checked = material.Estado === "Entrada" ? "checked" : "";

                const fila = `
                    <tr>
                        <td>
                            <label class="switch">
                                <input type="checkbox" class="checkbox-material" ${checked} onchange="registrarMovimientoMaterial(this, '${material.IdMaterial}')">
                                <span class="slider"></span>
                            </label>
                        </td>
                        <td><span class="etiqueta id-movimiento-material">${material.IdMaterial}</span></td>
                        <td><span class="etiqueta nombre">${material.Nombre}</span></td>
                        <td><span class="etiqueta referencia">${material.Referencia}</span></td>
                        <td><span class="etiqueta marca">${material.Marca}</span></td>
                        <td><span class="etiqueta materia">${material.Tipo}</span></td>
                    </tr>
                `;
                tbody.innerHTML += fila;
            });
        })
        .catch(error => {
            console.error("Error en la solicitud de materiales:", error);
            tbody.innerHTML = `
                <tr>
                    <td colspan="6" class="text-center">Error al cargar materiales</td>
                </tr>
            `;
        });
}

/**
 * Loads vehicles associated with the currently selected user/apprentice.
 * Assumes a C# controller named 'ElementsController' with a 'GetElements' action,
 * capable of returning vehicles based on 'tipoConsulta'.
 */
function cargarVehiculos() {
    const tbodyV = document.getElementById("tbodyVehiculo");
    tbodyV.innerHTML = "";
    const idUsuario = localStorage.getItem("Id");
    const tipoUsuario = localStorage.getItem("Tipo");
    const tipoConsulta = "vehiculo"; // Parámetro para indicar que queremos vehículos

    if (!idUsuario) {
        console.error("No se encontró ID de usuario para cargar vehículos.");
        return;
    }

    // --- CAMBIO CLAVE PARA ASP.NET CORE MVC ---
    // Similar a cargarMateriales, pero especificando tipoConsulta para vehículos.
    fetch(`/Elements/GetElements?idUsuario=${idUsuario}&tipoUsuario=${tipoUsuario}&tipoConsulta=${tipoConsulta}`) // Ajusta esta ruta
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            if (data.error) {
                console.error("Error al obtener vehículos:", data.error);
                return;
            }

            // Aquí el PHP devolvía 'data.vehiculo' para los vehículos.
            const vehiculos = Array.isArray(data.vehiculo) ? data.vehiculo : [];

            if (vehiculos.length === 0) {
                tbodyV.innerHTML = `
                <tr>
                    <td colspan="4" class="text-center">No se han registrado vehículos</td>
                </tr>
                `;
                return;
            }

            vehiculos.forEach(vehiculo => {
                const checked = vehiculo.Estado === "Entrada" ? "checked" : "";

                const fila = `
                    <tr>
                        <td>
                            <label class="switch">
                                <input type="checkbox" class="checkbox-vehiculo" ${checked} onchange="registrarMovimientoVehiculo(this, '${vehiculo.IdVehiculo}')">
                                <span class="slider"></span>
                            </label>
                        </td>
                        <td><span class="etiqueta id-movimiento-vehiculo">${vehiculo.IdVehiculo}</span></td>
                        <td><span class="etiqueta placa">${vehiculo.Placa}</span></td>
                        <td><span class="etiqueta tipo">${vehiculo.Tipo}</span></td>
                    </tr>
                `;
                tbodyV.innerHTML += fila;
            });
        })
        .catch(error => {
            console.error("Error en la solicitud de vehículos:", error);
            const filaError = `
            <tr>
                <td colspan="4" class="text-center">Error al cargar vehículos</td>
            </tr>
        `;
            tbodyV.innerHTML = filaError;
        });
}

/**
 * Registers movements for selected materials and vehicles in a single call.
 * Assumes a C# controller named 'ElementsController' with a 'RegisterElementMovements' action.
 */
async function registrarMovimientosAmbos() {
    const checkboxesMateriales = document.querySelectorAll(".checkbox-material:checked");
    const checkboxesVehiculos = document.querySelectorAll(".checkbox-vehiculo:checked");

    const idMaterial = Array.from(checkboxesMateriales).map(checkbox => {
        const fila = checkbox.closest('tr');
        return fila.querySelector('.id-movimiento-material')?.textContent;
    }).filter((id, index, self) => id && self.indexOf(id) === index); // Evita duplicados
    
    const idVehiculo = Array.from(checkboxesVehiculos).map(checkbox => {
        const fila = checkbox.closest('tr');
        return fila.querySelector('.id-movimiento-vehiculo')?.textContent;
    }).filter((id, index, self) => id && self.indexOf(id) === index); // Evita duplicados
    

    if (idMaterial.length === 0 && idVehiculo.length === 0) {
        alert("Por favor, seleccione al menos un material o vehículo.");
        return;
    }

    try {
        const body = {
            materiales: idMaterial,
            vehiculos: idVehiculo,
        };

        console.log("Enviando datos al backend:", body);

        // --- CAMBIO CLAVE PARA ASP.NET CORE MVC ---
        // La URL '../controllers/MovimientoElementos.php' se cambia a la ruta de tu controlador C#.
        // Asumimos un controlador 'ElementsController' con una acción 'RegisterElementMovements'.
        fetch('/Elements/RegisterElementMovements', { // Ajusta esta ruta
            method: 'POST',
            headers: { 'Content-Type': 'application/json' }, // Se envía JSON
            body: JSON.stringify(body) // Convertir el objeto JS a JSON string
        })
        .then(response => {
            if (!response.ok) {
                // Si la respuesta no es OK, intenta leer el error del cuerpo
                return response.json().then(err => { throw new Error(err.message || 'Error en la respuesta del servidor.'); });
            }
            return response.json(); // Esperar JSON de vuelta
        })
        .then(data => {
            if (data.success) {
                alert(data.message);
                // Si el movimiento fue exitoso, recargar las tablas para reflejar el cambio de estado
                recargarTabla("ambos");
            } else {
                alert("Error: " + data.message);
            }
        })
        .catch(error => {
            console.error("Error al registrar los movimientos:", error);
            alert("Error al registrar los movimientos.");
        });
} catch (error) {
    console.error("Error general en registrarMovimientosAmbos:", error);
    alert("Error general al registrar los movimientos.");
}
}

// Expone estas funciones globalmente para que puedan ser llamadas desde el HTML (onchange, onclick)
window.cargarMateriales = cargarMateriales;
window.cargarVehiculos = cargarVehiculos;
window.registrarMovimientosAmbos = registrarMovimientosAmbos; // Asegurarse de que esté expuesta si se usa en el HTML

/**
 * Reloads the material or vehicle tables.
 * @param {string} tipo - 'material', 'vehiculo', or 'ambos' (both).
 */
function recargarTabla(tipo) {
    if (tipo === "material" || tipo === "ambos") {
        cargarMateriales();
    }
    
    if (tipo === "vehiculo" || tipo === "ambos") {
        cargarVehiculos();
    }
}

// Listener para mensajes de otras ventanas/iframes (ej. después de un registro de elemento)
window.addEventListener('message', function(event) {
    if (event.data.action === "recargarTabla") {
        recargarTabla(event.data.tipo);
    }
});

// Nota: Las funciones 'registrarMovimientoMaterial' y 'registrarMovimientoVehiculo'
// que se usan en los onchange de los checkboxes no están definidas en el código proporcionado.
// Asegúrate de que existan y que también realicen llamadas AJAX a un endpoint C# adecuado
// si actualizan el estado de un solo material/vehículo.