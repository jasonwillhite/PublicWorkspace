$(function () {
    $('#HomeLink').removeClass('active');
    $('#OrderLink').addClass('active');
    $('#AdminLink').removeClass('active');
});

function addItemToOrder(id) {
    alterOrder(id, "AddItemToOrder");
}

function removeItemFromOrder(id) {
    alterOrder(id, "RemoveItemFromOrder");
}

function addDiscountToOrder(id) {
    alterOrder(id, "AddDiscountToOrder");
}

function editTaxOnOrder(checkBox, id) {
    if (checkBox === null || checkBox === 'undefined' || typeof(checkBox) === 'undefined') {
        return;
    }

    if (checkBox.checked === true) {
        alterOrder(id, "AddTaxToOrder");
    }
    else {
        alterOrder(id, "RemoveTaxFromOrder");
    }
}

function alterOrder(id, action) {
    $.ajax({
        type: "POST",
        url: "/Order/" + action,
        data: JSON.stringify({ "id": id }),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $('#orderSummaryDiv').html(result);
        }
    });
}

function clearOrder() {
    if (confirm('Are you sure you want to clear the order') === true) {
        alterOrder(-1, "ClearOrder");
    }
}

function submitOrder() {
    if (confirm('Are you sure you are ready to submit the order?') === true) {
        $.ajax({
            type: "POST", 
            url: "/Order/SubmitOrder",
            data: JSON.stringify({ "id": -1 }),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                window.location.href = result.Value;
            }
        });
    }
}