function showSearchWithKey() {

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

$(document).ready(function(){
    showSearchWithKey();

});