document.addEventListener("DOMContentLoaded", function () {
    // Call the function to load the login history when the page is ready.
    cargarHistorialLogin();
});

/**
 * Fetches login history data from the backend and populates a table.
 * It assumes a C# controller (e.g., 'HistorialController') with an action
 * (e.g., 'GetLoginHistory') that returns JSON data with a 'success' flag and a 'data' array.
 */
function cargarHistorialLogin() {
    // --- KEY CHANGE FOR ASP.NET CORE MVC ---
    // The URL "../controllers/HistorialLogin.php" needs to be updated
    // to point to your C# controller action.
    // Let's assume you have a 'HistorialController' and a 'GetLoginHistory' action.
    // The path would typically be '/Historial/GetLoginHistory' or '/api/Historial/GetLoginHistory'.
    fetch("/Historial/GetLoginHistory") // Adjust this URL to match your actual C# route
        .then(response => {
            // Check if the HTTP response was successful (status code 200-299).
            if (!response.ok) {
                // If not, throw an error to be caught by the .catch() block.
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            // Parse the response body as JSON.
            return response.json();
        })
        .then(data => {
            const tbody = document.querySelector("#tablaHistorialLogin tbody");

            // Ensure the tbody exists before trying to manipulate it.
            if (!tbody) {
                console.error("The 'tablaHistorialLogin tbody' element was not found in the DOM.");
                return;
            }

            tbody.innerHTML = ""; // Clear existing table rows.

            // Check if the data indicates success and contains actual records.
            if (data.success && Array.isArray(data.data) && data.data.length > 0) {
                // Iterate over each login history item and create a table row.
                data.data.forEach(item => {
                    const fila = document.createElement("tr");
                    fila.innerHTML = `
                        <td>${item.Documento || "N/A"}</td>
                        <td>${item.Nombre || "N/A"}</td>
                        <td>${item.FechaHora || "N/A"}</td>
                        <td>${item.Rol || "N/A"}</td>
                    `;
                    tbody.appendChild(fila);
                });
            } else {
                // If no data or an error occurred, display a "No records" message.
                const fila = document.createElement("tr");
                fila.innerHTML = `<td colspan="4" class="text-center">No hay registros de inicio de sesión.</td>`;
                tbody.appendChild(fila);
            }
        })
        .catch(error => {
            // Log any errors that occurred during the fetch operation.
            console.error("Error al cargar el historial de login:", error);
            const tbody = document.querySelector("#tablaHistorialLogin tbody");
            if (tbody) {
                tbody.innerHTML = ""; // Clear table and show error message.
                const errorRow = document.createElement("tr");
                errorRow.innerHTML = `<td colspan="4" class="text-center">Error al cargar el historial de login: ${error.message}</td>`;
                tbody.appendChild(errorRow);
            }
        });
}