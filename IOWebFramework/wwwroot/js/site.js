function JsonBGdate(value) {
    if (!value) {
        return '';
    }

    return moment(value).format("DD.MM.YYYY");
}

//Преобразува handlebars template, който е съдържание в контейнер с подадено име
function TemplateToHtml(countainer, data) {
    var source = $(countainer).html();

    return HandlebarsToHtml(source, data);
}

//Преобразува handlebars template, 
function HandlebarsToHtml(hbTemplate, data) {
    var template = Handlebars.compile(hbTemplate);

    return template(data);
}

Handlebars.registerHelper('eachData', function (context, options) {
    var fn = options.fn, inverse = options.inverse, ctx;
    var ret = "";

    if (context && context.length > 0) {
        for (var i = 0, j = context.length; i < j; i++) {
            ctx = Object.create(context[i]);
            ctx.index = i;
            ret = ret + fn(ctx);
        }
    } else {
        ret = inverse(this);
    }
    return ret;
});

Handlebars.registerHelper("math", function (lvalue, operator, rvalue, options) {
    lvalue = parseFloat(lvalue);
    rvalue = parseFloat(rvalue);

    return {
        "+": lvalue + rvalue
    }[operator];
});

Handlebars.registerHelper("date", function (date) {
    dateValue = date;

    return moment(dateValue).format("DD.MM.YYYY");
})

Handlebars.registerHelper("dateTime", function (date) {
    dateValue = date;

    return moment(dateValue).format("DD.MM.YYYY HH:mm:ss");
})

//Зарежда съдържанието на резултата от PartialView в div-елемент
function requestContent(url, data, callback) {

    $.ajax({
        type: 'GET',
        //async: true,
        cache: false,
        url: url,
        data: data,
        success: function (data) {
            callback(data);
        }
    });
}

// Показва съобщения от JS
var messageHelper = (function () {
    function ShowErrorMessage(message) {
        toastr.error(message);
    }

    function ShowSuccessMessage(message) {
        toastr.success(message);
    }

    function ShowWarning(message) {
        toastr.warning(message);
    }

    return {
        ShowErrorMessage: ShowErrorMessage,
        ShowSuccessMessage: ShowSuccessMessage,
        ShowWarning: ShowWarning
    };
})();