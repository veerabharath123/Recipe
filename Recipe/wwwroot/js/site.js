function spinner(flag) {
    if (flag) {
        $('.spinner-bg').removeClass('d-none').addClass('d-center')
    }
    else {
        setTimeout(() => {
            $('.spinner-bg').removeClass('d-none d-center').addClass('d-none')
        },2000)
    }
}