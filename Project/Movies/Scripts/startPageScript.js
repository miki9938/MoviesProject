function showSearchWithKey() {

    $(document).keypress(function () {

        checkScrollAndGo();
        $("#incentive").css({ "display": "none" });
	}); 

	$("#SearchBox").click(function () {

	    checkScrollAndGo();
	});

}

$(document).ready(function () {

    showSearchWithKey();
});



