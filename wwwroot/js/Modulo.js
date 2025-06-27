document.addEventListener('DOMContentLoaded', function () {
    // --- 1. Load Training Programs into Dropdown ---
    // This function fetches training program data from the backend
    // and populates a <select> element with the ID 'programaFormacion'.

    // Define the backend endpoint for fetching programs.
    // In ASP.NET Core MVC, this will be a C# Controller action.
    // Example: '/Programas/GetTrainingPrograms'
    fetch('/Programas/GetTrainingPrograms') // Adjust this URL to your C# route
        .then(response => {
            // Check if the HTTP response was successful.
            if (!response.ok) {
                // If not, throw an error to be caught by the .catch() block.
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json(); // Parse the JSON response.
        })
        .then(result => {
            const select = document.getElementById('programaFormacion');

            // Ensure the select element exists before manipulating it.
            if (!select) {
                console.error("Select element with ID 'programaFormacion' not found.");
                return;
            }

            // Check if the backend returned a successful response with data.
            // Assumes C# returns { success: true, data: [...] }
            if (result.success && Array.isArray(result.data) && result.data.length > 0) {
                // Add a default "select option" if desired
                // const defaultOption = document.createElement('option');
                // defaultOption.value = "";
                // defaultOption.textContent = "Seleccione un programa";
                // defaultOption.disabled = true;
                // defaultOption.selected = true;
                // select.appendChild(defaultOption);

                // Populate the dropdown with program data.
                result.data.forEach(programa => {
                    const option = document.createElement('option');
                    // 'IdPrograma' and 'Nombre' should match property names in your C# model.
                    option.value = programa.IdPrograma;
                    option.textContent = programa.Nombre;
                    select.appendChild(option);
                });
            } else {
                // If no programs found, add a disabled option indicating this.
                const option = document.createElement('option');
                option.textContent = 'No se encontraron programas de formación';
                option.disabled = true;
                option.selected = true; // Make it the default selected option
                select.appendChild(option);
            }
        })
        .catch(error => {
            console.error('Error al cargar los programas de formación:', error);
            const select = document.getElementById('programaFormacion');
            if (select) {
                select.innerHTML = ''; // Clear existing options
                const errorOption = document.createElement('option');
                errorOption.textContent = `Error al cargar: ${error.message}`;
                errorOption.disabled = true;
                errorOption.selected = true;
                select.appendChild(errorOption);
            }
        });

    // --- 2. Form Submission for "Ficha Completa" (File Upload + Data) ---
    const btnSubmit = document.querySelector('.btn-submit');

    // Ensure the submit button exists
    if (!btnSubmit) {
        console.error("Submit button with class 'btn-submit' not found.");
        return;
    }

    btnSubmit.addEventListener('click', function (event) {
        event.preventDefault(); // Prevent the default form submission

        const form = document.getElementById('formFichaCompleta');

        // Ensure the form exists
        if (!form) {
            console.error("Form with ID 'formFichaCompleta' not found.");
            return;
        }

        // Get form field values
        const nombrePrograma = document.getElementById('programaFormacion').value;
        const jornada = document.getElementById('jornada').value;
        const tipoPrograma = document.getElementById('tipoPrograma').value;
        const fechaInicio = document.getElementById('fechaInicio').value;
        const fechaFin = document.getElementById('fechaFin').value;
        const numeroFicha = document.getElementById('numeroFicha').value;
        const archivoExcel = document.getElementById('archivoExcel').files[0]; // Get the selected file

        // Basic client-side validation
        if (!nombrePrograma || !jornada || !tipoPrograma || !fechaInicio || !fechaFin || !numeroFicha || !archivoExcel) {
            alert('Por favor, complete todos los campos y seleccione un archivo.');
            return;
        }

        // Create a FormData object to send both form fields and the file.
        // FormData correctly sets the 'Content-Type' header to 'multipart/form-data'.
        const formData = new FormData();
        formData.append('ProgramaFormacionId', nombrePrograma); // Use ID for backend (assuming this is an ID)
        formData.append('Jornada', jornada);
        formData.append('TipoPrograma', tipoPrograma);
        formData.append('FechaInicio', fechaInicio);
        formData.append('FechaFin', fechaFin);
        formData.append('NumeroFicha', numeroFicha);
        formData.append('ArchivoExcel', archivoExcel); // The file itself

        // --- KEY CHANGE FOR ASP.NET CORE MVC ---
        // The URL '../controllers/guardar_ficha.php' needs to be updated
        // to point to your C# Controller action that handles file uploads.
        // Example: '/Fichas/SaveFichaWithApprentices'
        fetch('/Fichas/SaveFichaWithApprentices', { // Adjust this URL to your C# route
            method: 'POST',
            body: formData // FormData automatically sets correct headers
        })
        .then(response => {
            // Check if the HTTP response was successful.
            if (!response.ok) {
                // If not, try to parse error message from response body if available.
                return response.json().then(errorData => {
                    throw new Error(errorData.message || `Server error: ${response.status}`);
                }).catch(() => {
                    // Fallback if response is not JSON or cannot be parsed.
                    throw new Error(`HTTP error! status: ${response.status}`);
                });
            }
            return response.json(); // Parse the JSON response from C# backend.
        })
        .then(data => {
            // Assumes C# backend returns JSON like { success: true, message: "..." }
            if (data.success) {
                alert('Ficha creada exitosamente con aprendices.');
                // Display success message element (ensure 'mensajeExito' exists in your HTML)
                const mensajeExito = document.getElementById('mensajeExito');
                if (mensajeExito) {
                    mensajeExito.style.display = 'block';
                    // Optional: Set a timeout to hide the message
                    setTimeout(() => { mensajeExito.style.display = 'none'; }, 5000);
                }
                form.reset(); // Reset the form fields after successful submission.
            } else {
                alert('Error al crear la ficha: ' + (data.message || 'Error desconocido.'));
            }
        })
        .catch(error => {
            console.error('Error al guardar la ficha:', error);
            alert('Hubo un problema al guardar la ficha. Por favor, intente de nuevo. Detalle: ' + error.message);
        });
    });

    // --- 3. Cancel Button Functionality ---
    // Get the cancel button element
    const btnCancelar = document.getElementById('btnCancelar');
    if (btnCancelar) {
        btnCancelar.addEventListener('click', function () {
            const form = document.getElementById('formFichaCompleta');
            if (form) {
                form.reset(); // Reset the form fields.
            }
        });
    } else {
        console.warn("Cancel button with ID 'btnCancelar' not found.");
    }
});