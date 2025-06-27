document.addEventListener('DOMContentLoaded', function() {
    // 1. Sidebar Toggle Functionality
    let btn = document.querySelector("#btn"); // Selects the button that toggles the sidebar
    let sidebar = document.querySelector(".sidebar"); // Selects the sidebar element
 
    // Check if both elements exist to prevent errors
    if (btn && sidebar) {
        btn.onclick = function() {
            sidebar.classList.toggle("active"); // Toggles the 'active' class to show/hide the sidebar
        };
    } else {
        console.error("Sidebar or button element not found for toggle functionality.");
    }
 
    // 2. Modal (Popup) Opening Functionality
    // Selects all elements that have a 'data-modal' attribute
    const modalTriggers = document.querySelectorAll('[data-modal]');
    
    modalTriggers.forEach(trigger => {
        trigger.addEventListener('click', function(e) {
            e.preventDefault(); // Prevents the default action (e.g., navigating if it's an <a> tag)

            // Get the ID from the 'data-modal' attribute (e.g., 'myModal')
            const modalId = this.getAttribute('data-modal');
            // Construct the full ID for the modal backdrop (e.g., 'myModal-modal')
            const modal = document.getElementById(`${modalId}-modal`);
            
            // If the modal element is found, add the 'active' class to display it
            if (modal) {
                modal.classList.add('active');
            } else {
                console.error(`Modal with ID '${modalId}-modal' not found.`);
            }
        });
    });
    
    // 3. Modal Closing Functionality (by clicking close button)
    // Selects all elements with the 'close-modal' class (these are typically buttons inside modals)
    const closeButtons = document.querySelectorAll('.close-modal');
    closeButtons.forEach(button => {
        button.addEventListener('click', function() {
            // Find the closest parent element with the class 'modal-backdrop' (the modal itself)
            const modal = this.closest('.modal-backdrop');
            // If found, remove the 'active' class to hide the modal
            if (modal) {
                modal.classList.remove('active');
            }
        });
    });
    
    // 4. Prevent Modal Closing when clicking inside modal content
    // Selects all elements with the 'modal-content' class
    const modalContents = document.querySelectorAll('.modal-content');
    modalContents.forEach(content => {
        content.addEventListener('click', function(e) {
            e.stopPropagation(); // Stops the click event from "bubbling up" to the modal backdrop,
                               // preventing the modal from closing if you click inside its content.
        });
    });
    
    // 5. Logout Link Handler
    // Selects the logout link by its ID
    const logoutLink = document.getElementById('logout-link');
    if (logoutLink) {
        logoutLink.addEventListener('click', function(e) {
            e.preventDefault(); // Prevents the default link navigation

            // --- KEY CHANGE FOR ASP.NET CORE MVC ---
            // The original path '../views/loginEasyCodeIS.html' is a relative path to a static HTML file.
            // In ASP.NET Core MVC, you'll typically redirect to an action on a Controller.
            //
            // Option A: Redirect to an MVC Login Action (recommended)
            // This is the standard way to handle logout in MVC, often involving
            // server-side logic to clear authentication cookies/sessions.
            window.location.href = '/Auth/Login'; // Assuming you have an 'AuthController' with a 'Login' action.
                                               // Or adjust to whatever your login route is.

            // Option B: Redirect to a static HTML file within wwwroot
            // If you truly have a static login HTML file in wwwroot/loginEasyCodeIS.html
            // window.location.href = '/loginEasyCodeIS.html';
            //
            // Option C: Redirect to the Home page or default login route
            // window.location.href = '/'; // Or a specific login page: window.location.href = '/Login';
        });
    } else {
        console.error("Logout link element with ID 'logout-link' not found.");
    }
});