var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#pay').off('click').on('click', function () {
            var Address = $('#address').val();
            var Phone = $('#phone').val();
            
            window.location.href = "/ShopCheckout/ConfirmOrder?address=" + Address + "&phone=" + Phone;                        

        });

        $('#thenReceive').change(function () {
            if ($(this).is(":checked")) {
                var val = $(this).val();
                $('.formCredit').hide("slow");
            }
            else {
                $('.formCredit').show("slow");
            }
        });

        $('#withCredit').change(function () {
            if ($(this).is(":checked")) {
                var val = $(this).val();
                $('.formCredit').show("slow");
            }
            else {
                $('.formCredit').hide("slow");
            }
        });
        
    }
}
cart.init();