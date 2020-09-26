jQuery(function ($) {
    $.validator.addMethod('date',
        function (value, element) {

            if (this.optional(element)) {
                return true;
            }
            var ok_array = [
                'DD.MM.YYYY',
                'DD.MM.YYYY HH:mm',
                'DD.MM.YYYY HH:mm:ss',
                'MM.YYYY'
            ].filter(function (type_date) {
                return moment(value, type_date, true).isValid();
            });

            return !!ok_array[0];

    });

    $.validator.addMethod('number', function (value, element) {
        return this.optional(element) || /^-?(?:\d+)(?:(\.|,)\d+)?$/.test(value);
    });

    $.validator.addMethod("enforcetrue", function (value, element, param) {
        return element.checked;
    });

    $.validator.unobtrusive.adapters.addBool("enforcetrue");

    $.validator.methods.range = function (value, element, param) {
        return this.optional(element) || (Number(value.replace(',', '.')) >= Number(param[0]) && Number(value.replace(',', '.')) <= Number(param[1]));
    }
});