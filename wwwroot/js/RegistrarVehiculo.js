document.addEventListener("DOMContentLoaded", function () {
    // --- Load Vehicle Types into Dropdown ---
    // This function fetches available vehicle types from the backend
    // and populates a <select> element with the ID 'tipoVehiculo'.

    // Define the backend endpoint for fetching vehicle types.
    // In ASP.NET Core MVC, this will be a C# Controller action.
    // Example: '/Vehiculos/GetVehicleTypes'
    fetch("/Vehiculos/GetVehicleTypes") // Adjust this URL to your C# route
        .then(response => {
            // Check if the HTTP response was successful.
            if (!response.ok) {
                // If not, throw an error to be caught by the .catch() block.
                throw new Error(`Error al obtener tipos de vehículo: ${response.status} ${response.statusText}`);
            }
            return response.json(); // Parse the JSON response.
        })
        .then(data => {
            // Ensure the select element exists.
            const selectTipo = document.getElementById("tipoVehiculo");
            if (!selectTipo) {
                console.error("Select element with ID 'tipoVehiculo' not found.");
                return;
            }

            // Check if the backend returned a successful response with types.
            // Assumes C# returns { success: true, TipoVehiculo: [...] }
            if (data.success && Array.isArray(data.TipoVehiculo) && data.TipoVehiculo.length > 0) {
                selectTipo.innerHTML = "<option value=''>Seleccione un tipo</option>"; // Clear and add default option

                // Populate the dropdown with vehicle types.
                data.TipoVehiculo.forEach(tipo => {
                    const option = document.createElement("option");
                    // 'IdTipoVehiculo' and 'Tipo' should match property names in your C# model.
                    option.value = tipo.IdTipoVehiculo;
                    option.text = tipo.Tipo;
                    selectTipo.appendChild(option);
                });
            } else {
                console.error("Error en la respuesta para tipos de vehículo:", data.message || "No hay tipos de vehículo.");
                // Add a disabled option if no types are found or an error occurs.
                selectTipo.innerHTML = "<option value='' disabled selected>No se encontraron tipos de vehículo</option>";
            }
        })
        .catch(error => {
            console.error("Error en la petición de tipos de vehículo:", error);
            const selectTipo = document.getElementById("tipoVehiculo");
            if (selectTipo) {
                selectTipo.innerHTML = "<option value='' disabled selected>Error al cargar tipos</option>";
            }
        });

    // Make the 'registrarVehiculo' function globally accessible
    // if it's called directly from an HTML 'onclick' attribute.
    window.registrarVehiculo = registrarVehiculo;
});

/**
 * Registers a new vehicle.
 * This function sends vehicle data to an ASP.NET Core MVC C# controller.
 */
function registrarVehiculo() {
    // Retrieve user ID and type from localStorage.
    const idUsuario = localStorage.getItem("Id");
    const tipoUsuario = localStorage.getItem("Tipo");

    // Basic validation: ensure user information is available.
    if (!idUsuario) {
        alert("Error: No se ha iniciado sesión o no se pudo recuperar el ID del usuario.");
        return;
    }

    // Get form field values for the vehicle.
    const placa = document.getElementById("Placa").value.trim();
    const idTipoVehiculo = document.getElementById("tipoVehiculo").value;

    // Client-side validation for required fields.
    if (placa === "" || idTipoVehiculo === "") {
        alert("Por favor, complete todos los campos (Placa y Tipo de Vehículo).");
        return;
    }

    // Determine if the user is an 'aprendiz' or 'usuario' for backend mapping.
    const idAprendiz = tipoUsuario === "aprendiz" ? idUsuario : null;
    const idUsuarioFinal = tipoUsuario === "usuario" ? idUsuario : null;

    // --- KEY CHANGE FOR ASP.NET CORE MVC ---
    // Update the fetch URL to your C# controller action.
    // Assuming a 'VehiculosController' with a 'RegisterVehicle' action.
    fetch("/Vehiculos/RegisterVehicle", { // Adjust this URL to your C# route
        method: "POST",
        headers: {
            "Content-Type": "application/json" // Sending data as JSON is recommended
        },
        body: JSON.stringify({ // Convert JavaScript object to JSON string
            Placa: placa,
            IdTipoVehiculo: parseInt(idTipoVehiculo), // Ensure it's an integer for C# model
            IdUsuarioFinal: idUsuarioFinal ? parseInt(idUsuarioFinal) : null,
            IdAprendiz: idAprendiz ? parseInt(idAprendiz) : null
        })
    })
    .then(response => {
        if (!response.ok) {
            // Attempt to parse JSON error message if available, otherwise use status text.
            return response.json().then(errorData => {
                throw new Error(errorData.message || `Server error: ${response.status} ${response.statusText}`);
            }).catch(() => {
                throw new Error(`HTTP error! status: ${response.status} ${response.statusText}`);
            });
        }
        return response.json(); // Assuming C# returns JSON, e.g., { success: true, message: "..." }
    })
    .then(data => {
        // Update a message element if it exists.
        const mensajeElement = document.getElementById("mensaje");
        if (mensajeElement) {
            mensajeElement.innerText = data.message || "Operación completada.";
        }
        alert(data.message || "¡Vehículo registrado exitosamente!");

        // Recargar the vehicle table after successful registration.
        // This assumes `cargarVehiculos` is defined globally or in a parent frame.
        if (typeof cargarVehiculos === 'function') {
            cargarVehiculos();
        } else if (window.parent && typeof window.parent.cargarVehiculos === 'function') {
            window.parent.cargarVehiculos();
        } else {
            console.warn("Function 'cargarVehiculos' not found. Vehicle table might not refresh.");
        }

        // Close the form if the 'cerrarFormulario' function exists.
        if (typeof cerrarFormulario === 'function') {
            cerrarFormulario();
        } else {
            console.warn("Function 'cerrarFormulario' not found. Form might not close automatically.");
        }
    })
    .catch(error => {
        console.error("Error en el registro del vehículo:", error);
        alert("Error en el registro del vehículo: " + error.message);
    });
}