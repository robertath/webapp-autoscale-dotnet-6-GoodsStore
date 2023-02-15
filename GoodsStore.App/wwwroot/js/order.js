class Cart {

    clickIncrement(btn)
    {
        let data = this.getData(btn);
        data.Quantity++;
        this.postQuantity(data);
    }

    clickDecrease(btn) {
        let data = this.getData(btn);
        data.Quantity--;
        this.postQuantity(data);
    }

    setQuantity(input) {
        let data = this.getData(input);
        this.postQuantity(data);
    }

    getData(element) {
        var itemLine = $(element).parents('[item-id]');
        var itemId = $(itemLine).attr('item-id');
        var newQty = $(itemLine).find('input').val();
        return {
            Id: itemId,
            Quantity: newQty
        };
    }

    postQuantity(obj) {
        var orderItem = { "Id": parseInt(obj.Id), "Quantity": parseInt(obj.Quantity) };

        let token = $('[name=__RequestVerificationToken]').val();

        let headers = {};
        headers['RequestVerificationToken'] = token;
        debugger;
        $.ajax({
            type: 'POST',
            url: '/Order/SetQuantity',
            data: JSON.stringify(orderItem),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: headers
        }).done(function (response) {
            let orderItem = response.OrderItem;
            let orderItemLine = $('[item-id=' + orderItem.Id + ']');
            orderItemLine.find('input').val(orderItem.Quantity);
            orderItemLine.find('[subtotal]').html((orderItem.SubTotal).twoplaces());

            let cartViewModel = response.CartViewModel;

            $('[itens-number]').html('Total: ' + cartViewModel.OrderItems.length + 'items');
            $('[total]').html((cartViewModel.Total).twoplaces());

            if (orderItem.Quantity == 0) {
                orderItemLine.remove();
            }
        });
    }
}

let cart;

if(cart == null)
    cart = new Cart()

Number.prototype.twoplaces = function () {
    return this.toFixed(2).replace('.', ',');
}

