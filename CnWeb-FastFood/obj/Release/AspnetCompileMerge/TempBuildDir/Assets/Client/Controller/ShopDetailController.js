var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        
        $('#AddToCard').off('click').on('click', function () {                 
            var product = {
                Products: {
                    id_product: $('.txtquantity').data('id')
                },
                Amount: $('.txtquantity').val()     
            };
            

            $.ajax({
                url: '/ShopCart/AddToDetail',
                data: { product: JSON.stringify(product) },
                dataType: 'json',
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        $('#txtCountCart').empty();
                        $('#txtCountCart').append(res.count);
                        $('#txtTotal').empty();
                        $('#txtTotal').append(res.total);
                        alert("thêm thành công");
                       // window.location.href = "/ShopCart/Index?discountCode=" + dcCode;
                    }
                }
            });

        });

        /*-------------------
      Quantity change
  --------------------- */
        var proQty = $('.pro-qty');
        proQty.prepend('<span class="dec qtybtn">-</span>');
        proQty.append('<span class="inc qtybtn">+</span>');
        proQty.on('click', '.qtybtn', function () {
            var $button = $(this);
            var oldValue = $button.parent().find('input').val();
            if ($button.hasClass('inc')) {
                var newVal = parseFloat(oldValue) + 1;
                if (newVal > 10) {
                    alert("Số lượng mua tối đa là 10");
                    newVal = 10;
                }
            } else {
                // Don't allow decrementing below zero
                if (oldValue > 0) {
                    var newVal = parseFloat(oldValue) - 1;
                } else {
                    newVal = 0;
                }
            }
            $button.parent().find('input').val(newVal);            
            var textPrice = $('.txtquantity').data('price');
            var IntoMoney = newVal * textPrice;
            $('#txttotal').replaceWith('<div class="product__details__price" id="txttotal">' + IntoMoney+' đ</div>');
        });

        $('.txtquantity').on('change', function () {
            var newVal = this.value;
            if (newVal < 0) {
                newVal = 0;
            }
            else if (newVal > 10) {
                alert("Số lượng mua tối đa là 10");
                newVal = 10;
            }
            this.value = newVal;
            var textPrice = $(this).data('price');
            var IntoMoney = newVal * textPrice;
            $('#txttotal').replaceWith('<div class="product__details__price" id="txttotal">' + IntoMoney + ' đ</div>');
        });
    }
}
cart.init();