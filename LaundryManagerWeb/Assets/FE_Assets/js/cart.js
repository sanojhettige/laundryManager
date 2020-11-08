var check = false;

function changeVal(el) {
    var qt = parseFloat(el.parent().children(".item-qty").val());
    var price = parseFloat(el.parent().children(".unitPrice").val());
    alert(price);
    var eq = Math.round(price * qt * 100) / 100;

    el.parent().children(".full-price").html("Rs. "+eq);

    changeTotal();
}

function changeTotal() {

    var price = 0;

    $(".full-price").each(function (index) {
        price += parseFloat($(".full-price").eq(index).html());
    });

    price = Math.round(price * 100) / 100;
    var tax = Math.round(price * 0.05 * 100) / 100
    var shipping = parseFloat($(".shipping span").html());
    var fullPrice = Math.round((price + tax + shipping) * 100) / 100;

    if (price == 0) {
        fullPrice = 0;
    }

    $(".subtotal span").html(price);
    $(".tax span").html(tax);
    $(".total span").html(fullPrice);
}

$(document).ready(function () {

    $(".remove").click(function () {
        var el = $(this);
        el.parent().parent().addClass("removed");
        window.setTimeout(
            function () {
                el.parent().parent().slideUp('fast', function () {
                    el.parent().parent().remove();
                    if ($(".product").length == 0) {
                        if (check) {
                            alert('a');
                        } else {
                            $("#cart").html("<h1>No products!</h1>");
                        }
                    } 
                    changeTotal();
                });
            }, 200);
    });

    $(".item-qty").change(function () {
        const val = $(this).val();
        alert(val);
        //$(this).parent().children(".qt").html(parseInt(val));

        $(this).parent().children(".full-price").addClass("added");

        var el = $(this);
        window.setTimeout(function () { el.parent().children(".full-price").removeClass("added"); changeVal(el); }, 150);
    });

    window.setTimeout(function () { $(".is-open").removeClass("is-open") }, 1200);

    $(".btn").click(function () {
        check = true;
        $(".remove").click();
    });
});