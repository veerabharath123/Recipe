function spinner(flag,delay=500) {
    if (flag) {
        $('.spinner-bg').removeClass('d-none').addClass('d-center')
    }
    else {
        setTimeout(() => {
            $('.spinner-bg').removeClass('d-none d-center').addClass('d-none')
        }, delay)
    }
}
function ajaxPost(baseUrl, relativeUrl, data, successFunc, load) {
    if (load['toggle']) spinner(true)
    $.ajax({
        url: baseUrl + relativeUrl,
        type: 'POST',
        data: data,
        success: function (res) {
            if (load['toggle']) {
                let delay = load['delay'];
                spinner(false, delay || 500)
            }
            successFunc(res)
        },
        error: function (err) {
            console.log(err)
        }
    })
}
function ajaxPostFile(baseUrl, relativeUrl, formdata, successFunc, load) {
    debugger;
    if (load['toggle']) spinner(true)
    $.ajax({
        url: baseUrl + relativeUrl,
        type: 'POST',
        data: formdata,
        success: function (res) {
            if (load['toggle']) {
                let delay = load['delay'];
                spinner(false, delay || 500)
            }
            successFunc(res)
        },
        error: function (err) {
            notifyBar('Failed', 'danger')
            console.log(err)
        }
    })
}
var animate = {
    slide: {
        animate:'animate-slide' },
    fade: {
        animate:'animate-fade'}}
function notify(options, callback = null) {
    let n = $('.notify-bar-bg .notify-bar').length;
    $('.notify-bar-bg').append(
        '<div class=" alert-bg-' + options['type'] + ' notify-bar mx-2" id="notify-bar'+(n+1)+'">'+options['message']+'</div>'
    )
    let animate1 = animate[options['style']].animate;
    setTimeout(() => {
        $('#notify-bar' + (n + 1)).addClass(animate1);
    },100)
    setTimeout(() => {
        $('#notify-bar' + (n + 1)).removeClass(animate1)
    }, options['duration']);
    setTimeout(() => {
        $('#notify-bar' + (n + 1)).remove();
        if (callback) callback();
    }, options['delay'] + options['duration'])
}
function notifyBar(message, type, callback = null, delay = 5000) {
    notify({
        message: message,
        type: type,
        delay: 1000,
        style: 'slide',
        duration: delay
    }, callback)
}
function checkEmail(value) {
    var pattern = /(?:((?:[\w-]+(?:\.[\w-]+)*)@(?:(?:[\w-]+\.)*\w[\w-]{0,66})\.(?:[a-z]{2,6}(?:\.[a-z]{2})?));*)/g;
    return pattern.test(value);
}
function checkPassword(value) {
    return value.length >= 8;
}