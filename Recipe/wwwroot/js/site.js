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
            successFunc(res)
            if (load['toggle']) {
                let delay = load['delay'];
                spinner(false, delay||500)
            }
        },
        error: function (err) {
            console.log(err)
        }
    })
}