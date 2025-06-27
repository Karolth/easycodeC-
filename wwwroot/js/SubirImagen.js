document.addEventListener("DOMContentLoaded", function() {
    // Get references to the necessary DOM elements
    const inputImagenes = document.getElementById("imagenes"); // The hidden file input
    const previewContainer = document.getElementById("previewContainer"); // Where image previews are shown
    const fileInfo = document.getElementById("fileInfo"); // Text element to show file count/size
    const dropZone = document.getElementById("dropZone"); // The area for drag-and-drop

    // --- 1. Open File Selector on Drop Zone Click ---
    // Clicking anywhere on the drop zone area will trigger the hidden file input.
    if (dropZone && inputImagenes) {
        dropZone.addEventListener("click", function() {
            inputImagenes.click();
        });
    } else {
        console.warn("Required elements (dropZone or inputImagenes) not found for click-to-open functionality.");
    }

    // --- 2. Drag-and-Drop Event Handling ---
    // Prevent default browser behavior for drag events (e.g., opening file in new tab).
    ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
        if (dropZone) {
            dropZone.addEventListener(eventName, preventDefaults, false);
        }
    });

    function preventDefaults(e) {
        e.preventDefault();
        e.stopPropagation();
    }

    // Add visual feedback when files are dragged over the drop zone.
    ['dragenter', 'dragover'].forEach(eventName => {
        if (dropZone) {
            dropZone.addEventListener(eventName, highlight, false);
        }
    });

    // Remove visual feedback when files leave or are dropped on the drop zone.
    ['dragleave', 'drop'].forEach(eventName => {
        if (dropZone) {
            dropZone.addEventListener(eventName, unhighlight, false);
        }
    });

    function highlight() {
        if (dropZone) {
            dropZone.classList.add('dragover'); // Add a class for styling (e.g., border color change)
        }
    }

    function unhighlight() {
        if (dropZone) {
            dropZone.classList.remove('dragover'); // Remove the styling class
        }
    }

    // Handle files that are dropped onto the drop zone.
    if (dropZone && inputImagenes) {
        dropZone.addEventListener('drop', handleDrop, false);
    }

    function handleDrop(e) {
        const dt = e.dataTransfer;
        const files = dt.files;
        // Assign dropped files to the file input element so they can be processed
        // as if they were selected normally.
        inputImagenes.files = files;
        handleFiles(files); // Process the files for preview and info
    }

    // --- 3. Handle File Input Changes ---
    // Process files selected via the traditional file input dialog or dropped files.
    if (inputImagenes) {
        inputImagenes.addEventListener("change", function(event) {
            handleFiles(event.target.files);
        });
    } else {
        console.warn("Input element with ID 'imagenes' not found. File selection functionality might not work.");
    }

    /**
     * Processes selected files: validates them, displays previews, and updates file information.
     * @param {FileList} files - The FileList object containing selected or dropped files.
     */
    function handleFiles(files) {
        // Clear previous previews
        if (previewContainer) {
            previewContainer.innerHTML = "";
        }

        // Handle case where no files are selected
        if (files.length === 0) {
            if (fileInfo) {
                fileInfo.textContent = "No hay archivos seleccionados";
            }
            return;
        }

        let validFilesCount = 0;
        let totalSize = 0;
        // Define allowed image types and maximum size (2MB)
        const validTypes = ["image/jpeg", "image/png", "image/gif"];
        const maxSize = 2 * 1024 * 1024; // 2 MB in bytes

        // Iterate through each selected file
        for (const archivo of files) {
            // Validate file type
            if (!validTypes.includes(archivo.type)) {
                alert("Solo se permiten imágenes JPG, PNG o GIF.");
                continue; // Skip this file and go to the next
            }

            // Validate file size
            if (archivo.size > maxSize) {
                alert(`El archivo ${archivo.name} es demasiado grande (Máximo 2MB).`);
                continue; // Skip this file
            }

            // If validation passes, count it as a valid file and add to total size
            validFilesCount++;
            totalSize += archivo.size;

            // Create a FileReader to read the file content and display a preview
            const reader = new FileReader();
            reader.onload = function(e) {
                if (previewContainer) {
                    const imgContainer = document.createElement("div");
                    imgContainer.style.position = "relative";
                    imgContainer.style.margin = "5px";
                    imgContainer.style.display = "inline-block"; // For side-by-side previews

                    const img = document.createElement("img");
                    img.src = e.target.result; // The base64 encoded image data
                    img.alt = archivo.name;
                    img.style.maxWidth = "150px";
                    img.style.height = "100px";
                    img.style.objectFit = "cover"; // Maintain aspect ratio within bounds
                    img.style.border = "1px solid #ddd";
                    imgContainer.appendChild(img);

                    const nameLabel = document.createElement("div");
                    nameLabel.textContent = archivo.name.length > 15 ? archivo.name.substring(0, 12) + "..." : archivo.name;
                    nameLabel.title = archivo.name; // Show full name on hover
                    nameLabel.style.fontSize = "12px";
                    nameLabel.style.textAlign = "center";
                    nameLabel.style.marginTop = "5px";
                    nameLabel.style.wordBreak = "break-all"; // Prevent long names from breaking layout
                    imgContainer.appendChild(nameLabel);

                    previewContainer.appendChild(imgContainer);
                }
            };
            reader.readAsDataURL(archivo); // Read the file as a Data URL (base64 string)
        }

        // Update the file information display
        if (fileInfo) {
            const totalSizeMB = (totalSize / (1024 * 1024)).toFixed(2);
            fileInfo.textContent = `${validFilesCount} archivo(s) seleccionado(s) - Total: ${totalSizeMB}MB`;
        }
    }
});