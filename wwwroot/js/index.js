$(document).ready(function () {

    console.log("Javascript loaded")

    var theForm = $("#theForm");
    theForm.hide();

    let button = $("#buyButton");
    button.on("click", () => console.log("Buying Item"));

    let productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("You clicked on " + $(this).text());
    });

    let $loginToogle = $("#login-toggle");
    let $popupForm = $(".popup-form");

    $loginToogle.on("click", () => $popupForm.fadeToggle(600));

});