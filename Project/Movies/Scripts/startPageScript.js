function showSearch() {

	$(document).keypress(function (){
    
		$("html, body").animate({ scrollTop: 0 }, "slow");
   		showBlur();  		
   		noScroll();
   		
   		var input = $("#searchInput");
		input[0].selectionStart = input[0].selectionEnd = input.val().length;   		


	}); 

	$("#SearchBox").click(function(){

		//$("html, body").animate({ scrollTop: 0 }, "slow");
   		showBlur(); 		
   		noScroll();

   		var input = $("#searchInput");
		input[0].selectionStart = input[0].selectionEnd = input.val().length;
   		

	});

}

function showBlur(){


    
    $("#SearchBlur").fadeIn(800);

    $("#CloseSearchBlur").click(function () {

       	$("#SearchBlur").fadeOut(500);

       	$('#searchInput').val("");

       	if($('#searchInput').val().length == 0){

			$("#incentive").css({"display" : "block"});
		}

		yesScroll();

    });

    $(document).keyup(function(e) {

 		if (e.keyCode == 27) {

 			$("#SearchBlur").fadeOut(500);

 			$('#searchInput').val("");

 			if($('#searchInput').val().length == 0){

				$("#incentive").css({"display" : "block"});
			}

 			yesScroll();
 		}   
	});
}

function checkWritting(){

	$(document).bind('keypress', function(e) {

		var code = e.keyCode || e.which;
		
		var keyValue = String.fromCharCode(e.keyCode);

		$('#searchInput').val($('#searchInput').val() + keyValue);

		if($('#searchInput').val().length != 0){

			$("#incentive").css({"display" : "none"});
		}
	});

	specialCharacter();
}

function specialCharacter(){

	$(document).keyup(function(e) {

		if (e.keyCode == 8) {

			var lastKey = $('#searchInput').val();

			lastKey = lastKey.slice(0,-1);

			$('#searchInput').val(lastKey);
		 }

		 if (e.keyCode == 13) {

			console.log("<__/enter");
		 }  

		 if($('#searchInput').val().length == 0){

			$("#incentive").css({"display" : "block"});
		}    
	});
}

function noScroll() {

    var top = $(window).scrollTop();
    var left = $(window).scrollLeft()

      $('body').css('overflow', 'hidden');

      $(window).scroll(function(){
        $(this).scrollTop(top).scrollLeft(left);
      });
}

function yesScroll(){

	 $('body').css('overflow', 'auto');
     $(window).unbind('scroll');
}

function inputTyping(){

	$("#searchInput").keypress(function(){

		$("#incentive").css({"display" : "none"});
		
	});
}

$(document).ready(function(){
	showSearch();
	inputTyping();

});