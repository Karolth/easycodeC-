document.addEventListener("DOMContentLoaded", function () {
    const tablaHistorial = document.getElementById("tablaHistorial");

    // Make sure the table element exists before trying to use it.
    if (!tablaHistorial) {
        console.error("The 'tablaHistorial' element was not found in the DOM.");
        return; // Exit if the element is missing
    }

    /**
     * Loads the historical data from the backend and populates the table.
     * This function assumes a C# controller (e.g., 'HistorialController')
     * with an action (e.g., 'GetHistorial') that returns a JSON array of history items.
     */
    function cargarHistorial() {
        // --- KEY CHANGE FOR ASP.NET CORE MVC ---
        // The URL '../controllers/Historial.php?action=getHistorial' needs to be
        // changed to point to your C# controller action.
        // Assuming you have a 'HistorialController' with a 'GetHistorial' action.
        // The URL could be '/Historial/GetHistorial' or '/api/Historial/GetHistorial'
        // depending on your routing setup (MVC or API controller).
        fetch("/Historial/GetHistorial") // Adjust this URL to match your C# route
            .then(response => {
                // Check if the HTTP response was successful (status code 200-299)
                if (!response.ok) {
                    // If not successful, throw an error to be caught by the .catch() block
                    throw new Error(`Error al obtener los datos: ${response.status} ${response.statusText}`);
                }
                // Parse the response body as JSON
                return response.json();
            })
            .then(data => {
                // Clear existing table rows before adding new data
                tablaHistorial.innerHTML = "";

                // Check if data is an array and has items
                if (!Array.isArray(data) || data.length === 0) {
                    // Display a message if no data is found
                    const noDataRow = document.createElement("tr");
                    noDataRow.innerHTML = `<td colspan="9" class="text-center">No hay registros en el historial.</td>`;
                    tablaHistorial.appendChild(noDataRow);
                    return;
                }

                // Iterate over each item in the received data and create a table row
                data.forEach(item => {
                    const fila = document.createElement("tr");
                    fila.innerHTML = `
                        <td>${item.Nombre || "N/A"}</td>
                        <td>${item.Documento || "N/A"}</td>
                        <td>${item.NombreMaterial || "N/A"}</td>
                        <td>${item.Referencia || "N/A"}</td>
                        <td>${item.Placa || "N/A"}</td>
                        <td>${item.TipoVehiculo || "N/A"}</td>
                        <td>${item.FechaHora || "N/A"}</td>
                        <td>${item.Movimiento || "N/A"}</td>
                    `;
                    tablaHistorial.appendChild(fila);
                });
            })
            .catch(error => {
                // Log and display any errors that occurred during the fetch operation
                console.error("Error al cargar el historial:", error);
                const errorRow = document.createElement("tr");
                errorRow.innerHTML = `<td colspan="9" class="text-center">Error al cargar el historial: ${error.message}</td>`;
                tablaHistorial.innerHTML = ""; // Ensure table is empty before adding error message
                tablaHistorial.appendChild(errorRow);
            });
    }

    // Call the function to load the history data when the DOM is fully loaded
    cargarHistorial();
});