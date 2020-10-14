(function ($) {
    $("#game").hide();
    $("#end").hide();

    let counter;
    let startTime;
    let totalTime;
    let timePerTarget;

    $("#start .btn, #end .btn").click(function () {
        counter = 1;
        $("#targetsLeft").text("Targets left: " + (15-counter));
        startTime = new Date().getTime();
        $("#start").hide();
        $("#end").hide();
        $("#game").show();
        aimChange();
    });

    $("#aim").click(function () {
        counter++;
        $("#targetsLeft").text("Targets left: " + (15 - counter));
        aimChange();
    });


    function aimChange() {
        if (counter <= 14) {
            let fromTop = (Math.floor(Math.random() * 400) + 1);
            let fromLeft = (Math.floor(Math.random() * 860) + 1);

            $("#aim").css("top", fromTop + "px");
            $("#aim").css("left", fromLeft + "px");
        } else {
            totalTime = new Date().getTime() - startTime;
            timePerTarget = (totalTime / 14).toFixed(3);
            $("#game").hide();
            $("#end").show();
            $("#totalTimeSmall").text((totalTime / 1000).toFixed(1) + " sec");
            $("#timePerTargetSmall").text(timePerTarget + " ms");
            submitScore(timePerTarget);
        }
    }

    function submitScore(timePerTarget) {
        $.ajax({
            type: "POST",
            url: "/Index",
            contentType: 'application/json',
            data: timePerTarget,
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() }
        });
    }
})(jQuery);