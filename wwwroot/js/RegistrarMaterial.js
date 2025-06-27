document.addEventListener('DOMContentLoaded', function() {
    // Attach event listeners to buttons/elements that trigger the 'formulario' function
    // Assuming you have buttons with specific IDs or data attributes that call formulario('pc'), formulario('automovil'), formulario('otro')
    // Example:
    // document.getElementById('btnOpenPcForm').addEventListener('click', () => formulario('pc'));
    // document.getElementById('btnOpenOtherForm').addEventListener('click', () => formulario('otro'));
    // document.getElementById('btnOpenAutomovilForm').addEventListener('click', () => formulario('automovil'));

    // Attach event listeners to the close buttons for the modals/forms
    const closeButtons = document.querySelectorAll('.close-modal');
    closeButtons.forEach(button => {
        button.addEventListener('click', function() {
            cerrarFormulario(); // Call the function to close and clear the forms
        });
    });

    // You might also have submit buttons for each form that call their respective registration functions
    // Example:
    // const registerPcButton = document.getElementById('registerPcBtn'); // Assuming your PC form has a submit button with this ID
    // if (registerPcButton) {
    //     registerPcButton.addEventListener('click', registrarComputador);
    // }

    // const registerOtherButton = document.getElementById('registerOtherBtn'); // Assuming your 'other' form has a submit button with this ID
    // if (registerOtherButton) {
    //     registerOtherButton.addEventListener('click', registrarOtro);
    // }

    // Make functions globally accessible if they are called directly from HTML 'onclick' or 'onsubmit' attributes
    window.registrarComputador = registrarComputador;
    window.registrarOtro = registrarOtro;
    window.formulario = formulario;
    window.cerrarFormulario = cerrarFormulario;
});

/**
 * Registers a new "Computador" (computer) item.
 * This function will send data to an ASP.NET Core MVC C# controller.
 */
function registrarComputador() {
    // Retrieve user ID and type from localStorage.
    const idUsuario = localStorage.getItem("Id");
    const tipoUsuario = localStorage.getItem("Tipo");

    // Basic validation: ensure user information is available.
    if (!idUsuario) {
        alert("Error: No se pudo recuperar el ID del usuario. Por favor, realice una búsqueda primero.");
        return;
    }

    // Get form field values for the computer.
    const referencia = document.getElementById("referencia").value.trim();
    const marca = document.getElementById("marca").value.trim();
    const observaciones = document.getElementById("observaciones").value.trim();
    const idTipoMaterial = "1"; // Hardcoded ID for 'Computador'.

    // Determine if the user is an 'aprendiz' or 'usuario' for backend mapping.
    const idAprendiz = tipoUsuario === "aprendiz" ? idUsuario : null;
    const idUsuarioFinal = tipoUsuario === "usuario" ? idUsuario : null;

    // Further client-side validation for required fields
    if (!referencia || !marca) {
        alert("La referencia y la marca del computador son obligatorias.");
        return;
    }

    // --- KEY CHANGE FOR ASP.NET CORE MVC ---
    // Update the fetch URL to your C# controller action.
    // Assuming a 'MaterialesController' with a 'RegisterComputer' action.
    fetch('/Materiales/RegisterComputer', { // Adjust this URL to your C# route
        method: 'POST',
        headers: {
            // Using 'application/json' is generally better for structured data.
            // If your C# controller expects [FromForm], keep 'application/x-www-form-urlencoded'.
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ // Send as JSON
            Nombre: "Computador", // This could also be a fixed value in C# if always 'Computador'
            Referencia: referencia,
            Marca: marca,
            Observaciones: observaciones,
            IdTipoMaterial: parseInt(idTipoMaterial), // Ensure it's an integer for C# model
            IdUsuarioFinal: idUsuarioFinal ? parseInt(idUsuarioFinal) : null, // Convert to int or null
            IdAprendiz: idAprendiz ? parseInt(idAprendiz) : null // Convert to int or null
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
        const mensajeElement = document.getElementById("mensaje");
        if (mensajeElement) {
            mensajeElement.innerText = data.message || "Operación completada.";
        }
        alert(data.message || "Computador registrado correctamente");

        // Recargar la tabla de materiales after successful registration.
        // This is crucial for reflecting changes in the main view.
        if (typeof cargarMateriales === 'function') {
            cargarMateriales(); // Assumes cargarMateriales is defined globally or in a parent script.
        } else if (window.parent && typeof window.parent.cargarMateriales === 'function') {
            // If this script is in an iframe, try to call parent's function.
            window.parent.cargarMateriales();
        } else {
            console.warn("cargarMateriales function not found. Table might not refresh.");
        }

        // Close the form after successful registration.
        cerrarFormulario(); // Call the specific closing function defined below.
    })
    .catch(error => {
        console.error('Error al registrar computador:', error);
        alert('Error al registrar computador: ' + error.message);
    });
}

/**
 * Registers a new "Otro" (other) type of material.
 * This function will send data to an ASP.NET Core MVC C# controller.
 */
function registrarOtro() {
    // Retrieve user ID and type from localStorage.
    const idUsuario = localStorage.getItem("Id");
    const tipoUsuario = localStorage.getItem("Tipo");

    if (!idUsuario) {
        alert("Error: No se ha iniciado sesión o no se pudo recuperar el ID.");
        return;
    }

    // Get form field values for the 'other' item.
    const nombre = document.getElementById("NombreOtro").value.trim();
    const observaciones = document.getElementById("ObservacionesOtro").value.trim();
    const idTipoMaterial = "2"; // Hardcoded ID for 'Otro elemento'.

    // Client-side validation.
    if (nombre === "") {
        alert("El nombre del elemento es obligatorio.");
        return;
    }

    // Determine user type for backend mapping.
    const idAprendiz = tipoUsuario === "aprendiz" ? idUsuario : null;
    const idUsuarioFinal = tipoUsuario === "usuario" ? idUsuario : null;

    // --- KEY CHANGE FOR ASP.NET CORE MVC ---
    // Update the fetch URL to your C# controller action.
    // Assuming a 'MaterialesController' with a 'RegisterOtherMaterial' action.
    fetch('/Materiales/RegisterOtherMaterial', { // Adjust this URL to your C# route
        method: 'POST',
        headers: {
            'Content-Type': 'application/json' // Sending as JSON
        },
        body: JSON.stringify({ // Convert JS object to JSON string
            Nombre: nombre,
            Observaciones: observaciones,
            IdTipoMaterial: parseInt(idTipoMaterial),
            IdUsuarioFinal: idUsuarioFinal ? parseInt(idUsuarioFinal) : null,
            IdAprendiz: idAprendiz ? parseInt(idAprendiz) : null
        })
    })
    .then(response => {
        if (!response.ok) {
            return response.json().then(errorData => {
                throw new Error(errorData.message || `Server error: ${response.status} ${response.statusText}`);
            }).catch(() => {
                throw new Error(`HTTP error! status: ${response.status} ${response.statusText}`);
            });
        }
        return response.json(); // Assuming C# returns JSON
    })
    .then(data => {
        const mensajeElement = document.getElementById("mensaje");
        if (mensajeElement) {
            mensajeElement.innerText = data.message || "Operación completada.";
        }
        alert(data.message || "Elemento registrado correctamente");

        // Recargar la tabla de materiales after successful registration.
        if (typeof cargarMateriales === 'function') {
            cargarMateriales();
        } else if (window.parent && typeof window.parent.cargarMateriales === 'function') {
            window.parent.cargarMateriales();
        } else {
            console.warn("cargarMateriales function not found. Table might not refresh.");
        }

        // Close the form after registration.
        cerrarFormulario();
    })
    .catch(error => {
        console.error('Error al registrar otro elemento:', error);
        alert('Error al registrar otro elemento: ' + error.message);
    });
}

/**
 * Displays the appropriate form based on the 'tipo' (type) parameter.
 * Also checks if a user has been "searched" (ID stored in localStorage).
 * @param {string} tipo - 'pc', 'automovil', or 'otro'.
 */
function formulario(tipo) {
    const idUsuario = localStorage.getItem("Id");

    if (!idUsuario) {
        // Show a warning message if no document has been searched yet.
        const elementosDiv = document.getElementById("elementos");
        if (elementosDiv) {
            const mensajeError = document.createElement("div");
            mensajeError.classList.add("alert", "alert-danger", "text-center", "mt-3");
            mensajeError.innerText = "Debe buscar un documento antes de registrar un elemento.";
            elementosDiv.appendChild(mensajeError);

            // Remove the message after 3 seconds.
            setTimeout(() => mensajeError.remove(), 3000);
        } else {
            alert("Debe buscar un documento antes de registrar un elemento.");
        }
        return;
    }

    // Show the overlay and the specific form.
    const overlay = document.getElementById('overlay');
    const computadorForm = document.getElementById('computadorForm');
    const automovilForm = document.getElementById('automovilForm');
    const formOtro = document.getElementById('formOtro');

    if (overlay) overlay.style.display = 'block';
    if (computadorForm) computadorForm.style.display = tipo === "pc" ? 'block' : 'none';
    if (automovilForm) automovilForm.style.display = tipo === "automovil" ? 'block' : 'none';
    if (formOtro) formOtro.style.display = tipo === "otro" ? 'block' : 'none';
}

/**
 * Hides all material/vehicle registration forms and their overlay, and clears form fields.
 */
function cerrarFormulario() {
    const overlay = document.getElementById('overlay');
    const computadorForm = document.getElementById('computadorForm');
    const automovilForm = document.getElementById('automovilForm');
    const formOtro = document.getElementById('formOtro');

    if (overlay) overlay.style.display = 'none';
    if (computadorForm) computadorForm.style.display = 'none';
    if (automovilForm) automovilForm.style.display = 'none';
    if (formOtro) formOtro.style.display = 'none';

    // Clear the fields of the forms.
    // Assuming form elements have a .reset() method if they are <form> tags.
    // Otherwise, you'd reset individual fields by ID.
    const formPC = document.getElementById('formPC'); // Make sure this ID matches your <form> tag
    const Formautomovil = document.getElementById('Formautomovil'); // Make sure this ID matches your <form> tag
    const formRegistro = document.getElementById('formRegistro'); // Make sure this ID matches your <form> tag

    if (formPC) formPC.reset();
    if (Formautomovil) Formautomovil.reset();
    if (formRegistro) formRegistro.reset();

    // Also clear the general message area
    const mensajeElement = document.getElementById("mensaje");
    if (mensajeElement) {
        mensajeElement.innerText = ""; // Clear any previous messages
    }
}