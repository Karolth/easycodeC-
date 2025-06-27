document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("#usuarioForm");

    // Check if the form exists to prevent errors on pages where it's not present.
    if (form) {
        form.addEventListener("submit", function (event) {
            let isValid = true; // Flag to track overall form validity

            // Get trimmed values from form fields
            const nombre = document.getElementById("Nombre").value.trim();
            const documento = document.getElementById("Documento").value.trim();
            const email = document.getElementById("Email").value.trim();
            const celular = document.getElementById("Celular").value.trim();
            const rol = document.getElementById("rol").value; // Select value, no trim needed for select

            // Regular expressions for validation
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Basic email format
            const celularRegex = /^[0-9]{10}$/; // Exactly 10 digits for phone number
            const documentoRegex = /^[0-9]+$/; // Only digits for document

            // --- Validation Checks ---

            // 1. Check if all fields are filled
            if (nombre === "" || documento === "" || email === "" || celular === "" || rol === "") {
                alert("Todos los campos son obligatorios.");
                isValid = false;
            }

            // 2. Validate email format
            // Only proceed if email is not empty to avoid double alerts
            if (email !== "" && !emailRegex.test(email)) {
                alert("Ingrese un correo válido.");
                isValid = false;
            }

            // 3. Validate cell phone number format
            // Only proceed if cellular is not empty to avoid double alerts
            if (celular !== "" && !celularRegex.test(celular)) {
                alert("El celular debe tener 10 dígitos.");
                isValid = false;
            }

            // 4. Validate document number (only digits)
            // The `oninput` attribute in your HTML already restricts this,
            // but server-side and client-side JavaScript validation is still good practice.
            // Only proceed if documento is not empty to avoid double alerts
            if (documento !== "" && !documentoRegex.test(documento)) {
                alert("El documento solo puede contener números.");
                isValid = false;
            }

            // If any validation failed, prevent the default form submission.
            // This is crucial if you are going to submit the form via AJAX.
            if (!isValid) {
                event.preventDefault(); // Stops the form from submitting normally
            } else {
                // --- ASP.NET Core MVC Integration Point ---
                // If the form is valid, you would typically:
                // 1. Prevent default submission if you plan to use AJAX (which is common in modern web apps).
                //    event.preventDefault();

                // 2. Collect form data.
                const formData = {
                    Nombre: nombre,
                    Documento: documento,
                    Email: email,
                    Celular: celular,
                    Rol: parseInt(rol) // Convert rol to integer if your C# model expects int
                };

                // 3. Send data to your C# backend using fetch or jQuery.ajax (if you included jQuery).
                //    Example using fetch:
                /*
                fetch('/Usuarios/RegistrarUsuario', { // Adjust the URL to your C# Controller/Action
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json' // Send as JSON
                    },
                    body: JSON.stringify(formData) // Convert JS object to JSON string
                })
                .then(response => {
                    if (!response.ok) {
                        // Handle HTTP errors (e.g., 400 Bad Request, 500 Internal Server Error)
                        return response.json().then(errorData => {
                            throw new Error(errorData.message || 'Error al registrar el usuario.');
                        });
                    }
                    return response.json(); // Assuming your C# controller returns JSON (e.g., { success: true, message: "..." })
                })
                .then(data => {
                    if (data.success) {
                        alert(data.message);
                        form.reset(); // Clear the form after successful submission
                        // Optional: Redirect the user or update UI
                    } else {
                        alert("Error: " + data.message);
                    }
                })
                .catch(error => {
                    console.error('Error al enviar el formulario:', error);
                    alert('Hubo un problema al intentar registrar el usuario. Por favor, intente de nuevo.');
                });
                */

                // If you are submitting the form traditionally (without AJAX),
                // remove `event.preventDefault();` and the fetch block.
                // The form's `action` attribute in the HTML would then point to your C# controller action.
            }
        });
    } else {
        console.warn("Form '#usuarioForm' not found. Ensure the HTML element exists.");
    }
});