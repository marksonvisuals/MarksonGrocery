$(document).ready(function() {
    var customerId = $('#customerId').val();
    var total = 0.00;
    $.each($('.produceBoxes input:checked'), function(index, value) {
        total += parseFloat($('.produceBoxes input:checked').eq(index).data('price'));
    });
    updateTotal(total);

    $('.cartSizeBtn').click(function() {
        
        var cartSizeId = $(this).data('id');
        var that = $(this);

        $.ajax({
            url: '/api/cart/PutCartSize/' + customerId + '?cartSizeId=' + cartSizeId,
            type: 'PUT',
            success: function(d) {

                    $('.cartSizeBtn.btn-primary').removeClass('btn-primary').addClass('btn-secondary');
                    that.removeClass('btn-secondary').addClass('btn-primary');

            },
            error: function() {
                console.log('Error');
            }
        });
    });

    $('.produceBoxes input').change(function() {
        var that = $(this);
        var isChecked = that.is(':checked');
        var produceId = that.data('id');

        
        if (isChecked) {
            $.ajax({
                url: '/api/produce/PostItemToCart/' + customerId + '?produceId=' + produceId,
                type: 'POST',
                success: function (d) {
                    total += parseFloat(that.data('price'));
                    updateTotal(total);

                },
                error: function () {
                    that.prop('checked', false).parent().removeClass('active');
                    console.log('Error');
                }
            });
        } else {
            $.ajax({
                url: '/api/produce/DeleteItemFromCart/' + customerId + '?produceId=' + produceId,
                type: 'Delete',
                success: function (d) {
                    total -= parseFloat(that.data('price'));
                    updateTotal(total);

                },
                error: function () {
                    that.prop('checked', 'checked').parent().addClass('active');
                    console.log('Error');
                }
            });
        }
    });

    $('.checkoutBoxes').click(function () {

        var hasErrors = ValidateCheckout();

        if (!hasErrors) {
            var that = $(this);
            var type = that.data('type');
            var id = that.data('id');

            $('#submitCheckout').attr('data-checkoutid', id);

            if (type.toLowerCase().indexOf('self') >= 0) {
                $('#checkoutSentence').text('The Self-Checkout machine says...');
            } else {
                $('#checkoutSentence').text('The ' + type + ' says...');
            }

            $('#grandTotal').text($('.totalNumber').text());

            $('#checkoutModal').modal('show');
        }
    });

    $('#submitCheckout').click(function () {
        $('#errorCheckoutHolder').hide();
        var checkoutTypeId = $(this).data('checkoutid');
        $.ajax({
            url: '/api/checkout/PostCheckoutType/' + customerId + '?checkoutTypeId=' + checkoutTypeId,
            type: 'POST',
            success: function (d) {

                if (d == true) {
                    $.ajax('/home/ResetSession');
                    $('#successCheckoutHolder').show();
                    $('#submitCheckout').prop('disabled', 'disabled');
                    $('#checkoutModal').on('hidden.bs.modal', function() {
                        location.reload();
                    });
                } else {
                    $('#errorCheckoutHolder').show();
                }

            },
            error: function () {
                $('#errorCheckoutHolder').show();
                console.log('Error');
            }
        });
    });

});

function ValidateCheckout() {
    var hasErrors = false;
    $('#CartSizeError, #ProduceError').hide();

    if ($('.cartSizeBtn.btn-primary').text().toLowerCase() == 'none') {
        $('#CartSizeError').show();
        hasErrors = true;
    }

    if ($('.produceBoxes input:checked').length == 0) {
        $('#ProduceError').show();
        hasErrors = true;
    }

    return hasErrors;
}

function updateTotal(total) {
    $('.totalNumber').text(total.toFixed(2));
}