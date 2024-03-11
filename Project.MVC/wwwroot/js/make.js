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