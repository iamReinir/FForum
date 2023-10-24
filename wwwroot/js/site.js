const imageUploadInput = document.querySelector('#image-upload');

// Get the image preview element
const imagePreview = document.querySelector('#image-preview');

// Add an event listener to the input element
imageUploadInput.addEventListener('change', () => {
    // Get the uploaded image file
    const imageFile = imageUploadInput.files[0];

    // Create a new FileReader object
    const reader = new FileReader();

    // Read the image file
    reader.onload = function () {
        // Set the image preview source to the image data
        imagePreview.src = reader.result;
    };

    // Read the image file as a data URL
    reader.readAsDataURL(imageFile);
});