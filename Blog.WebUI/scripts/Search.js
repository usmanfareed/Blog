$(document).ready(function() {
    $('#search-field').keypress(function (e) {
        if (e.which === 13) {
            e.preventDefault();
            window.location = "/search/" + $('#search-field').val();
        }
    });
});