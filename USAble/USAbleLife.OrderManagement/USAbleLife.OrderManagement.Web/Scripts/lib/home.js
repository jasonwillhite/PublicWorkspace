$(function () {
    $('#HomeLink').addClass('active');
    $('#OrderLink').removeClass('active');
    $('#AdminLink').removeClass('active');
});

function getOrderSummary(mealOrderId) {
    $.ajax({
        type: "POST",
        url: "/Home/GetOrderSummary",
        data: JSON.stringify({ "mealOrderId": mealOrderId }),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $('#orderSummaryDiv').html(result);
        }
    });
}

$('.orderItem').click(function () {
    $('.orderItem').each(function () {
        this.style.background = 'white';
    });

    $(this)[0].style.background = 'rgba(144, 238, 144, .5)';
});

$(".orderItem").hover(
  function () {
      if ($(this)[0].style.background.toString() === 'rgba(144, 238, 144, 0.5)') {
          return;
      }

      $(this)[0].style.background = '#f1f1f1';
  }, function () {
      if ($(this)[0].style.background.toString() !== 'rgba(144, 238, 144, 0.5)') {
          $(this)[0].style.background = 'white';
      }
  }
);

