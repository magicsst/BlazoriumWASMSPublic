window.ShowToastr = function (type, message) {
    if (type == "success") {
        toastr.success(message);
    }
    if (type == "error") {
        toastr.error(message);
    }
}

// Check Toastr 'Other Options' for usage
// https://github.com/CodeSeven/toastr