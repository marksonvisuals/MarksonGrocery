$(document).ready(function() {

    $('.cartSizeBtn').click(function() {
        var customerId = $('#customerId').val();
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

});