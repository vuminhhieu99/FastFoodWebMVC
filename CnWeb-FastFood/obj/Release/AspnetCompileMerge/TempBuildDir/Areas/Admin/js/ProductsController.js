var product = {
    init: function () {
        product.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').on('click', function (e) {
            e.preventDefault();
            var cb = $(this);
            var id = cb.data(id);
            $.ajax({
                url: "/Admin/Product/ChangeAvailability",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        //cb.prop('checked', true);
                        cb.text('có sẵn');
                    }
                    else {
                        //cb.prop('checked', false);
                        cb.text('hết');
                    }
                }
            });
        });
    }
}
product.init();