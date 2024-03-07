// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#submitButton').prop('disabled', true);
    
    $('input[type="text"]').on('keyup', function () {
        var isValid = true;
        $('input[type="text"]').each(function () {
            if ($.trim($(this).val()) === '') {
                isValid = false;
                return false;
            }
        });

        $('#submitButton').prop('disabled', !isValid);
    });
});