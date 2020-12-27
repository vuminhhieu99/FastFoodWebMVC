var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/Shop/Index";
        });

        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtquantity');
            var cartList = [];
            var dcCode = $('#txtdiscountCode').val();
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Amount: $(item).val(),
                    Products: {
                        id_product: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/ShopCart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/ShopCart/Index?discountCode=" + dcCode;
                    }
                }
            });      

        });

        $('#btnDeleteAll').off('click').on('click', function () {
           
            $.ajax({
                url: '/ShopCart/DeleteAll',
              
                dataType: 'json',
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/ShopCart/Index/";
                    }
                }
            });

        });

        $('#checkout').on('click', function () {
            var listProduct = $('.txtquantity');
            var cartList = [];
            var dcCode = $('#txtdiscountCode').val();
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Amount: $(item).val(),
                    Products: {
                        id_product: $(item).data('id')
                    }
                });
            });
            var bill = {
                Item: cartList,
                Id_customer: $('#txtCustomer').data('id'),
                Subtotal : $('#SubTotal').text(),
                Total: $('#Total').text(),
                DiscountCode: $('#txtdiscountCode').val()
            }

            $.ajax({
                url: '/ShopCheckout/AddBill',
                data: { billModel: JSON.stringify(bill) },
                dataType: 'json',
                type: "POST",                
            }).done(function (res) {
                window.location.href = res.newUrl;
            }).fail(function (xhr, a, error) {
                console.log(error);
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
            var textIDparent = "#row_" + $button.parent().find('input').data('id');
            var textPrice = $(textIDparent).find('.shoping__cart__price').data('price');
            var IntoMoney = newVal * textPrice;
            $(textIDparent).find('.shoping__cart__total').replaceWith('<td class="shoping__cart__total">' + IntoMoney + ' đ</td >');
            var SubTotal = 0;
            $.each($('.shoping__cart__total'), function (i, item) {
                var textSubTotal = item.textContent;
                textSubTotal = textSubTotal.replace(" đ", "");
                SubTotal = SubTotal + Number(textSubTotal);
            });
            
            $('#SubTotal').replaceWith('<span id="SubTotal">' + SubTotal + '</span>');
            $('#txtTotal').replaceWith('<span id="txtTotal">' + SubTotal + ' đ</span>');
            $('#Total').replaceWith('<span id="Total">' + SubTotal + '</span>');
            
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
            var textIDparent = "#row_" + $(this).data('id');
            var textPrice = $(textIDparent).find('.shoping__cart__price').data('price');
            var IntoMoney = newVal * textPrice;
            $(textIDparent).find('.shoping__cart__total').replaceWith('<td class="shoping__cart__total">' + IntoMoney + ' đ</td >');
           
        });
    }
}
cart.init();