function setPage(){

	var x = $(window).width();
	var y = $(window).height();
	var basicX = 1280;
	var basicY = 800;
	var resultX = x / basicX;
	var resultY = y / basicY;

	console.log(x,y);

	if(resultX > resultY || resultX == resultY){
		
		var TrasnparentNameW = (x * 400) / basicX;
		var TrasnparentNameH = (x * 80) / basicX;
		var TextSize = (x * 100)/basicY;

		var MainBarHeight = (x * 60)/basicX;		
		var MainBarListTop = (x * 23)/basicX;
		var MainBarListLeft = (x * 30)/basicX;
		var MainBarListPadding = (x * 18)/basicX;

		var FooterTrasnparentNameSize = (x * 167) / basicX;
		var FotterMenuTextSize = (x * 14)/basicX;
		var FotterMenuTextTop = (x * 27)/basicX;
		var FotterMenuTextLeft = (x * 329)/basicX;
		var FotterMenuTextPad = (x * 23)/basicX;
		var Copyright = (x * 12)/basicX;
		var CopyrightTop = (x * 224)/basicX;
		
	} else if(resultX < resultY){
		
		
		var TrasnparentNameW = (y * 400) / basicY;
		var TrasnparentNameH = (x * 80) / basicX;
		var TextSize = (y * 100)/basicY;

		var MainBarHeight = (y * 60)/basicY;		
		var MainBarListTop = (y * 23)/basicY;
		var MainBarListLeft = (y * 30)/basicY;
		var MainBarListPadding = (y * 18)/basicY;

		var FooterTrasnparentNameSize = (y * 167) / basicY;
		var FotterMenuTextSize = (y * 14)/basicY;
		var FotterMenuTextTop = (y * 27)/basicY;
		var FotterMenuTextLeft = (y * 329)/basicY;
		var FotterMenuTextPad = (y * 23)/basicY;
		var Copyright = (y * 12)/basicY;
		var CopyrightTop = (y * 224)/basicY;

	}

	var BannerHeight = y/2;
	var BoxHeight = BannerHeight * 0.66;
	var WelcomeHeight = y*0.75; // dla obrazkow w 1280 x 600
	var WelcomeSmall = y*0.625; // dla obrazkow w 1280 x 500

	var NewsField1Top = MainBarHeight + (50*resultY);
	var NewsField2Top = NewsField1Top + BannerHeight + (50*resultY);
	var NewsField3Top = NewsField2Top + BannerHeight + (50*resultY);
	var Box1Top = NewsField1Top;
	var Box2Top = Box1Top + BoxHeight + (25*resultY);
	var Box3Top = Box2Top + BoxHeight + (25*resultY);

	var Footer = NewsField3Top + WelcomeSmall + (50*resultY);
	var FooterHeight = y * 0.375;

	/*$("#NewsField1").css({"top":NewsField1Top,"height":BannerHeight});
	$("#NewsField2").css({"top":NewsField2Top,"height":BannerHeight});
	$("#NewsField3").css({"top":NewsField3Top,"height":BannerHeight});
	$("#Box1").css({"top":Box1Top,"height":BoxHeight});
	$("#Box2").css({"top":Box2Top,"height":BoxHeight});
	$("#Box3").css({"top":Box3Top,"height":BoxHeight});
    $(".Footer").css({"top":Footer,"minHeight":FooterHeight});	*/	
	$("#MainBarName").css({"height":TrasnparentNameH, "width":TrasnparentNameW});$("ul#MainBarList li").css({"font-size": TextSize + "%", "top":MainBarListTop, "left":MainBarListLeft, "padding-left":MainBarListPadding});
	$("#MainBar").css({"width":x,/* "height":MainBarHeight*/});
	$("#FooterLogo").css({"width":FooterTrasnparentNameSize, "height":FooterTrasnparentNameSize});
	$("ul#FotterMenuList li").css({"font-size": FotterMenuTextSize, "top":FotterMenuTextTop, "padding-left":FotterMenuTextPad});
	$("#Copyright").css({"font-size":Copyright});

}

function scrollPosition(){
	$(window).scroll(function () { 
	 	var y = $(window).height();
	 	y-=MainBarHeig;
      	var scrollValue = $(window).scrollTop();
	});
}

function writeSize(){

	var x = $(window).width();
	var y = $(window).height();

	console.log(x,y);
}


$(document).ready(function(){
	scrollPosition();
	setPage();
	writeSize();

	$(window).resize(function() {
		scrollPosition();
	   	setPage();
	   	writeSize();
	});
	
});





