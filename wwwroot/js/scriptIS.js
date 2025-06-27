document.addEventListener('DOMContentLoaded', () => {
    // This script ensures a preloader is shown for a set duration
    // before the main content, specifically the login container, is revealed.

    // Get references to the preloader and the login container elements.
    const preloader = document.querySelector('.preloader');
    // Note: 'container' should likely be '.container' if it's a class, or '#container' if an ID.
    // Assuming it's a class for this example, common in CSS frameworks.
    const loginContainer = document.querySelector('.container'); 

    // Basic check to ensure required elements exist before proceeding.
    if (!preloader) {
        console.warn("Preloader element with class '.preloader' not found. Preloader functionality will be skipped.");
        if (loginContainer) {
            loginContainer.classList.remove('hidden');
            loginContainer.style.display = 'flex';
            loginContainer.style.position = 'fixed'; // Ensure initial visibility if no preloader.
        }
        return; // Exit if preloader is missing.
    }

    if (!loginContainer) {
        console.error("Login container element with class '.container' not found. Page display may be affected.");
        // We will still hide the preloader even if the container is missing,
        // to avoid an infinite loading screen.
    }

    // Set a timeout to hide the preloader and display the login container.
    // The preloader will be visible for 2 seconds.
    setTimeout(() => {
        // Hide the preloader by setting its display to 'none'.
        preloader.style.display = 'none';

        // If the login container exists, make it visible.
        if (loginContainer) {
            loginContainer.classList.remove('hidden'); // Remove a class that might initially hide it (e.g., from CSS).
            loginContainer.style.display = 'flex';     // Apply Flexbox display for layout.
            loginContainer.style.position = 'fixed';    // Keep it fixed on the screen, useful for full-page overlays.
        }
    }, 2000); // 2000 milliseconds = 2 seconds.
});