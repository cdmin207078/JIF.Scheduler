
$(function () {
    //iCheck for checkbox and radio inputs
    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue'
    });
});


// 检查页面是否需要弹出错误提醒
$(function () {
    var exp = $('#JIFExceptionMessage');
    if (exp.length) {
        $.alert({
            type: 'red',
            title: '失败',
            content: exp.text()
        });
    }
});