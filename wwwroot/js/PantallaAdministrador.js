document.addEventListener('DOMContentLoaded', function() {
    // Add event listener to the admin button to show information
    const adminButton = document.querySelector('.admin-button');
    if (adminButton) {
        adminButton.addEventListener('click', mostrarInformacionAdmin);
    } else {
        console.warn("Element with class 'admin-button' not found. Admin info functionality might not work.");
    }

    // Add event listener to the close button/area of the overlay
    const overlay = document.getElementById("overlay");
    if (overlay) {
        // You might have a specific close button inside the overlay,
        // or you can click anywhere on the overlay to close it.
        // Assuming there's a close button or just click on the overlay itself.
        overlay.addEventListener('click', function(event) {
            // Check if the click was directly on the overlay itself, not its content
            if (event.target === overlay) {
                cerrarInformacionAdmin();
            }
        });
        // If you have a dedicated close button within the overlay, you'd add:
        // const closeButtonInsideOverlay = document.getElementById('closeAdminInfoBtn'); // Example ID
        // if (closeButtonInsideOverlay) {
        //     closeButtonInsideOverlay.addEventListener('click', cerrarInformacionAdmin);
        // }
    } else {
        console.warn("Element with ID 'overlay' not found. Admin info functionality might not work.");
    }
});


/**
 * Fetches and displays administrator information in a designated overlay.
 * In an ASP.NET Core MVC context, this function would typically make an
 * AJAX request to a C# controller to retrieve dynamic admin data.
 */
function mostrarInformacionAdmin() {
    // --- KEY CHANGE FOR ASP.NET CORE MVC ---
    // Instead of hardcoding 'adminInfo', you'd typically fetch it from your backend.
    // This makes the data dynamic and secure.

    // Example: Fetch admin info from a C# Controller
    // Assuming you have a 'UsersController' with an action like 'GetCurrentAdminInfo'
    fetch('/Users/GetCurrentAdminInfo') // Adjust this URL to your C# route
        .then(response => {
            if (!response.ok) {
                // If the response is not OK (e.g., 401 Unauthorized, 404 Not Found, 500 Internal Error)
                throw new Error(`Error al obtener la información del administrador: ${response.status} ${response.statusText}`);
            }
            return response.json(); // Expecting JSON data from the C# controller
        })
        .then(adminInfo => {
            // Check if adminInfo contains expected properties
            if (adminInfo) {
                // Display the information in the respective span or div elements
                // Using nullish coalescing operator (?? 'N/A') for robustness
                // in case a property is missing from the backend response.
                document.getElementById("nombreAdmin").textContent = adminInfo.nombre ?? 'N/A';
                document.getElementById("apellidosAdmin").textContent = adminInfo.apellidos ?? 'N/A';
                document.getElementById("documentoAdmin").textContent = adminInfo.documento ?? 'N/A';
                document.getElementById("emailAdmin").textContent = adminInfo.email ?? 'N/A';
                document.getElementById("celularAdmin").textContent = adminInfo.celular ?? 'N/A';

                // Hide the administrator button
                const adminButton = document.querySelector('.admin-button');
                if (adminButton) {
                    adminButton.style.display = 'none';
                }

                // Show the overlay
                const overlay = document.getElementById("overlay");
                if (overlay) {
                    overlay.style.display = "flex"; // Using 'flex' if it's a flex container for centering
                }
            } else {
                console.error("No admin information received from the server.");
                alert("No se pudo cargar la información del administrador.");
            }
        })
        .catch(error => {
            console.error("Error fetching admin info:", error);
            alert("No se pudo cargar la información del administrador. Por favor, intente de nuevo.");
            // Optionally, re-enable the admin button or show a message on the UI
        });

    // Original hardcoded data (REMOVE this block after implementing fetch)
    /*
    const adminInfo = {
        nombre: "Nombre del Administrador",
        apellidos: "Apellidos del Administrador",
        documento: "123456789",
        email: "admin@example.com",
        celular: "123-456-7890"
    };

    document.getElementById("nombreAdmin").textContent = adminInfo.nombre;
    document.getElementById("apellidosAdmin").textContent = adminInfo.apellidos;
    document.getElementById("documentoAdmin").textContent = adminInfo.documento;
    document.getElementById("emailAdmin").textContent = adminInfo.email;
    document.getElementById("celularAdmin").textContent = adminInfo.celular;

    document.querySelector('.admin-button').style.display = 'none';
    document.getElementById("overlay").style.display = "flex";
    */
}

/**
 * Hides the administrator information overlay and re-shows the admin button.
 */
function cerrarInformacionAdmin() {
    // Show the administrator button
    const adminButton = document.querySelector('.admin-button');
    if (adminButton) {
        adminButton.style.display = 'block';
    }

    // Hide the overlay
    const overlay = document.getElementById("overlay");
    if (overlay) {
        overlay.style.display = "none";
    }
}