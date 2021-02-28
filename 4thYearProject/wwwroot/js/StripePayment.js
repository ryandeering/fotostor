redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_hpLfBptgL9qbpWeuJgoX3AO500njVt1MOk');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};