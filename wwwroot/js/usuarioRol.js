document.addEventListener('DOMContentLoaded', function() {
    // Get a reference to the form element
    const usuarioForm = document.getElementById('usuarioForm');

    // Check if the form exists before attaching the event listener
    if (usuarioForm) {
        usuarioForm.addEventListener('submit', function(e) {
            e.preventDefault(); // Prevent the default form submission behavior

            // Gather data from the form fields
            const usuarioData = {
                documento: document.getElementById('Documento').value,
                nombre: document.getElementById('Nombre').value,
                email: document.getElementById('Email').value,
                celular: document.getElementById('Celular').value,
                rol: document.getElementById('rol').value
            };

            console.log("Rol seleccionado:", usuarioData.rol); // Log the selected role for debugging

            // --- KEY CHANGE FOR ASP.NET CORE MVC ---
            // Replace the jQuery AJAX call with a standard fetch API call.
            // The URL needs to point to your C# Controller action.
            // Example: '/Usuarios/RegisterUserWithRole'
            fetch('/Usuarios/RegisterUserWithRole', { // Adjust this URL to your actual C# route
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json' // Indicate that we are sending JSON data
                },
                body: JSON.stringify(usuarioData) // Convert the JavaScript object to a JSON string
            })
            .then(response => {
                // Check if the HTTP response was successful (status code 200-299)
                if (!response.ok) {
                    // If not successful, try to parse the error message from the response body (if JSON)
                    return response.json().then(errorData => {
                        throw new Error(errorData.message || `Server error: ${response.status} ${response.statusText}`);
                    }).catch(() => {
                        // Fallback if response is not JSON or cannot be parsed
                        throw new Error(`HTTP error! status: ${response.status} ${response.statusText}`);
                    });
                }
                return response.json(); // Parse the JSON response from the C# backend
            })
            .then(data => {
                // Assuming your C# backend returns JSON like { success: true, message: "..." } or { success: false, message: "..." }
                if (data.success) {
                    alert(data.message || 'Usuario registrado exitosamente.'); // Use message from backend if available
                    usuarioForm.reset(); // Reset the form fields
                } else {
                    alert(data.message || 'Error al registrar el usuario.'); // Use error message from backend
                }
            })
            .catch(error => {
                console.error("Error al registrar usuario:", error);
                alert("Ocurrió un error al registrar el usuario: " + error.message); // Display the error to the user
            });
        });
    } else {
        console.warn("Form with ID 'usuarioForm' not found. User registration functionality might not work.");
    }
});