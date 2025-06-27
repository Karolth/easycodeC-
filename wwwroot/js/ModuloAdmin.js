document.addEventListener('DOMContentLoaded', function() {
    // --- Open Modals ---
    // Attaches click listeners to buttons that trigger modal display.
    // When clicked, the corresponding modal becomes visible, and body scrolling is disabled.

    const subirImagenBtn = document.getElementById('subirImagen');
    if (subirImagenBtn) {
        subirImagenBtn.addEventListener('click', function() {
            const modal = document.getElementById('modalSubirImagen');
            if (modal) {
                modal.style.display = 'block';
                document.body.style.overflow = 'hidden'; // Disable scrolling on the body
            } else {
                console.warn("Modal 'modalSubirImagen' not found.");
            }
        });
    } else {
        console.warn("Button 'subirImagen' not found.");
    }

    const crearFichaBtn = document.getElementById('crearFicha');
    if (crearFichaBtn) {
        crearFichaBtn.addEventListener('click', function() {
            const modal = document.getElementById('modalCrearFicha');
            if (modal) {
                modal.style.display = 'block';
                document.body.style.overflow = 'hidden';
            } else {
                console.warn("Modal 'modalCrearFicha' not found.");
            }
        });
    } else {
        console.warn("Button 'crearFicha' not found.");
    }

    const historialBtn = document.getElementById('historial');
    if (historialBtn) {
        historialBtn.addEventListener('click', function() {
            const modal = document.getElementById('modalHistorial');
            if (modal) {
                modal.style.display = 'block';
                document.body.style.overflow = 'hidden';
            } else {
                console.warn("Modal 'modalHistorial' not found.");
            }
        });
    } else {
        console.warn("Button 'historial' not found.");
    }

    // --- Close Modals (using close buttons) ---
    // Finds all elements with the class 'close' (e.g., span for 'x' button)
    // and attaches a click listener to close their corresponding modal.
    const closeButtons = document.querySelectorAll('.close');
    closeButtons.forEach(function(button) {
        button.addEventListener('click', function() {
            // The 'data-modal' attribute on the close button should hold the ID of the modal to close.
            const modalId = this.getAttribute('data-modal');
            const modal = document.getElementById(modalId);
            if (modal) {
                modal.style.display = 'none'; // Hide the modal
                document.body.style.overflow = 'auto'; // Re-enable body scrolling
            } else {
                console.warn(`Modal with ID '${modalId}' (from close button data-modal) not found.`);
            }
        });
    });

    // --- Close Modals (by clicking outside the modal content) ---
    // Listens for clicks anywhere on the window. If the click target is a modal
    // (i.e., the backdrop, not the content inside), it closes that modal.
    window.addEventListener('click', function(event) {
        const modals = document.querySelectorAll('.modal');
        modals.forEach(function(modal) {
            if (event.target === modal) { // Check if the click was directly on the modal backdrop
                modal.style.display = 'none';
                document.body.style.overflow = 'auto';
            }
        });
    });

    // --- Logout Functionality ---
    // Handles the click event for a logout button/link.
    // --- KEY CHANGE FOR ASP.NET CORE MVC ---
    // The original path "../views/loginEasyCodeIS.html" is a static HTML file.
    // In ASP.NET Core MVC, you'll typically redirect to a controller action for logout.
    const logoutBtn = document.getElementById("logoutBtn");
    if (logoutBtn) {
        logoutBtn.addEventListener("click", function() {
            // Best practice: Redirect to a C# logout action that clears server-side session/cookies.
            // Then, that action redirects to your login page.
            // Example: /Auth/Logout or /Account/Logout
            window.location.href = "/Auth/Logout"; // Adjust this to your actual C# logout route
            // For example, if your login page is at /Home/Login:
            // window.location.href = "/Home/Login"; // After server-side logout
        });
    } else {
        console.warn("Logout button with ID 'logoutBtn' not found.");
    }

    // --- Close Modals (by pressing Escape key) ---
    // Listens for the 'Escape' key press to close any currently open modal.
    document.addEventListener('keydown', function(event) {
        if (event.key === 'Escape') {
            const modals = document.querySelectorAll('.modal');
            modals.forEach(function(modal) {
                if (modal.style.display === 'block') { // Check if the modal is currently visible
                    modal.style.display = 'none';
                    document.body.style.overflow = 'auto';
                }
            });
        }
    });
});