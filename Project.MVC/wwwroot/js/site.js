// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#submitButton').prop('disabled', true);;
    var isValidText = false;
    var isValidSelect = false;
    $('input[type="text"]').on('keyup', function () {
        isValidText = true;
        $('input[type="text"]').each(function () {
            if ($.trim($(this).val()) === '') {
                isValidText = false;
                return false;
            }
        });

        $('#submitButton').prop('disabled', !isValidText || !isValidSelect);
    });
    $('#MakeId, #MakeId').on('change keyup', function () {
        isValidSelect = true;

        // Check if the select menu has a selected option
        if ($('#MakeId').val() === '') {
            isValidSelect = false;
        }

        // Check if the input field is empty
        if ($('#MakeId').val().trim() === '') {
            isValidSelect = false;
        }

        // Enable or disable the button based on validation result
        $('#submitButton').prop('disabled', !isValidText || !isValidSelect);;
    });
});