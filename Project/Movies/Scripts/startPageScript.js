function showSearchWithKey() {

    $(document).keyup(function (e) {

        if (e.keyCode > 32) {
            checkScrollAndGo();
            var keyValue = String.fromCharCode(e.keyCode);
            $("#searchinput").val(keyValue);
            $("#incentive").css({ "display": "none" });
        }
    }); 

	$("#SearchBox").click(function () {

	    checkScrollAndGo();
	});

}

$(document).ready(function () {

    showSearchWithKey();
});



