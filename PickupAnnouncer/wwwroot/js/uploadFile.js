$(document).ready(function () {
    $("#registrationFile").fileinput({
        showPreview: false,
        allowedFileExtensions: ["csv"],
        elErrorContainer: "#errorBlock"
    });
});