$modalContainer = $('.modal-container');

function modalController(action) {
    if (action == 'close') {
        $modalContainer.removeClass('active');
        $modalContainer.find('.modal-wrap').removeClass('active');
        $modalContainer.find('.overlay').removeClass('active');
    } else {
        $modalContainer.addClass('active');
        $modalContainer.find('.modal-wrap' + action).addClass('active');
        $modalContainer.find('.overlay').addClass('active');
    }
}

$('[data-modal]').click(function () {
    var target = $(this).attr('data-modal');

    modalController(target);
});

var error_label = document.getElementById('error_label');
var password_hash = document.getElementById('password_hash');
var old_password = document.getElementById('password');
var new_password1 = document.getElementById('password2');
var new_password2 = document.getElementById('password3');
var confirmButton = document.getElementById('confirmButton');

function ChangePassword() {
    if (password_hash.value == old_password.value) {
        if ((new_password1.value == new_password2.value) && new_password1 != null && new_password1 != "") {
            error_label.innerHTML = "Success";
            confirmButton.click();
        } else {
            error_label.innerHTML = "Error, your new passwords is not the same!"
        }
    } else {
        error_label.innerHTML = "Invalid password, please enter again carefully."
    }
}

var cardNumber = document.getElementById('cardNumber');
var cardTerm = document.getElementById('cardTerm');
var cardCVV = document.getElementById('cardCVV');
var email = document.getElementById('email');

var confirmBuyBtn = document.getElementById('btnBuyTicketsHiden');
var errors_span = document.getElementById('errors_span');

function BuyTicket() {
    let check = true;
    let cardNumber_t = /^([0-9]{16})$/;
    let cardCVV_t = /^([0-9]{3}$)/;
    let email_t = /^([0-9a-zA-Z]{3,}@[a-z.]{4}$)/;

    let err_str = "";
    var now = new Date();
    var ms = Date.parse(cardTerm);

    if (!cardNumber_t.test(cardNumber.value)) {
        check = false;
        err_str += "Invalid card number!\n"
    }
    if (now >= ms) {
        check = false;
        err_str += "Invalid date!\n";
    }
    if (!cardCVV_t.test(cardCVV.value)) {
        check = false;
        err_str += "Invalid CVV!\n"
    }
    if (!email_t.test(email.value)) {
        check = false;
        err_str += "Invalid email!\n"
    }

    if (check) {
        confirmBuyBtn.click();
    }

}

