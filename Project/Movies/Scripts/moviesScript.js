function setPage() {

    var x = $(window).width();
    var y = $(window).height();
    var basicX = 1280;
    var basicY = 800;
    var resultX = x / basicX;
    var resultY = y / basicY;
    var smallerXY;
    if (x < y) {
        smallerXY = x;
    }
    else if (x > y) {
        smallerXY = y;
    }

    console.log("smaller:" + smallerXY + " resultX:" + resultX + " resultY:" + resultY);

    if (x > basicX || x == basicX) {
        /***Main Bar***/
        var MainBarNameHeight = 80;
        var MainBarNameWidth = 400;
        var TransparentNameHeight = 80;
        var TransparentNameWidth = 400;
        var MainBarHeight = 80;
        var MainBarLeft = MainBarNameWidth - 1;
        var MainBarListTop = 50;
        var MainBarTextSize = 20;
        var MenuArrowSize = 10;

        /***Search***/
        var SearchBoxWidth = 500;
        var SearchBoxHeight = 100;
        var SearchBoxThickness = 15;
        var SearchBoxTop = ((y - MainBarHeight) / 2) - (SearchBoxHeight / 2);
        var SearchBoxLeft = (x / 2) - (SearchBoxWidth / 2);
        var SearchFieldWidth = 520;
        var SearchFieldLeft = (x / 2) - (SearchFieldWidth / 2);
        var SearchFieldTop = MainBarHeight + 10;
        var MoviesResultTop = SearchFieldTop + 30;
        var PeopleResultTop = $("#moviesResult").height() + MoviesResultTop + 30;

        /***Footer***/
        var FooterHeight = 240;
        var CopyrightSize = 12;
        var FooterTop;

        if ($('body').height() < 770) {
            FooterTop = 770;
        }
        else if ($('body').height() > 770) {
            FooterTop = $('body').height();
        }

        /***Movies comparison***/
        var similarMovieTop = $("mainMovie").height() + 100;
    }

    /*else if(x < basicX)
	{
		var MainBarNameHeight = 80 * resultY;
		var MainBarNameWidth = 400 * resultX;
		var TransparentNameHeight = 80 * resultY;
		var TransparentNameWidth = 400 * resultX;
		var MainBarHeight = 80 * resultY;
		var MainBarLeft = (MainBarNameWidth) * resultX;
		var MainBarListTop = 50 * resultY;
		var MainBarTextSize = 20 * smallerXY;
		var MenuArrowSize = 10 * resultY;
		var SearchBoxWidth = 500 * resultX;
		var SearchBoxHeight = 100 * resultY;
		var SearchBoxThickness = 15;
		var SearchBoxTop = ((y - MainBarHeight)/2)-(SearchBoxHeight/2);
		var SearchBoxLeft = ((x/2)-(SearchBoxWidth/2)) * resultX;
		var FooterHeight = 180 * resultY;
		var FotterTop = 770 * resultY;
		var CopyrightSize = 12 * smallerXY;
	}*/

    $("#MainBarNameHeight").css({ "height": MainBarNameHeight, "width": MainBarNameWidth });
    $(".TransparentName").css({ "height": TransparentNameHeight, "width": TransparentNameWidth });
    $("#MainBar").css({ "height": MainBarHeight, "left": MainBarLeft });
    $("#MainBarList").css({ "top": MainBarListTop });
    $("a.MainBarListLink").css({ "font-size": MainBarTextSize });
    $(".MenuArrow").css({ "height": MenuArrowSize, "width": (2 * MenuArrowSize) });

    $("#SearchBox").css({ "height": SearchBoxHeight, "width": SearchBoxWidth, "border-width": SearchBoxThickness, "top": SearchBoxTop, "left": SearchBoxLeft });
    $("#searchField").css({ "width": SearchFieldWidth, "top": SearchFieldTop, "left": SearchFieldLeft });
    $("#moviesResult").css({ "width": SearchFieldWidth, "top": MoviesResultTop, "left": SearchFieldLeft });
    $("#peopleResult").css({ "width": SearchFieldWidth, "top": PeopleResultTop, "left": SearchFieldLeft });

    $(".formFields").css({ "left": centerWidth(".formFields"), "top": centerHeight(".formFields") });

    $(".similarMovie").css({ "top": similarMovieTop });

    $(".Footer").css({ "top": FooterTop, "height": FooterHeight });
    $("#Copyright").css({ "font-size": CopyrightSize });

    $(".centerMe").css({ "left": centerWidth(".centerMe"), "top": centerHeight(".centerMe") });

}

function centerWidth(id) {

    var x = $(window).width();

    var objectLeft = (x / 2) - (($(id).width()) / 2);

    return objectLeft;
}

function centerHeight(id) {

    var y = $(window).height();

    var objectTop = ((y - 80) / 2) - (($(id).height()) / 2);

    return objectTop;
}

function writeSize() {

    var x = $(window).width();
    var y = $(window).height();

    console.log(x, y);
}

function prevent() {

    var e = $('#MainBarList');
    var jWindow = $(window);
    var offsetTop = e.offset().top;
    var positionTop = e.position().top;
    console.log("dddd");

    jWindow.resize(function () {
        /* if(jWindow.scrollTop() > offsetTop)
             e.css({'position':'fixed', 'top':0});
         else
             e.css({'position':'relative', 'top':positionTop});
         */

        if ($(window).width() < 930)
            e.css({ 'position': 'relative', 'left': '420px', 'right': '' });
        else
            e.css({ 'position': 'fixed', 'left': '', 'right': '100px;' });

    });
}

function showBlur() {

    $(document).keyup(function (e) {

        var display = $("#SearchBlur").css('display');

        if (e.keyCode == 27 && display != "none") {

            $("#SearchBlur").fadeOut(500);

            $('#searchInput').val("");

            if ($('#searchInput').val().length == 0) {

                $("#incentive").css({ "display": "block" });
            }

            $(".SearchResult").remove();

            yesScroll();
        }
    });

    $("#SearchBlur").fadeIn(800);

    $("#CloseSearchBlur").click(function () {

        $("#SearchBlur").fadeOut(500);

        $('#searchInput').val("");

        if ($('#searchInput').val().length == 0) {

            $("#incentive").css({ "display": "block" });
        }

        $(".SearchResult").remove();

        yesScroll();

    });
}

function checkWritting() {

    $(document).bind('keyup', function (e) {

        var keyValue = String.fromCharCode(e.keyCode).toLowerCase();

        /* $('#searchInput').val($('#searchInput').val() + keyValue); */

        if ($('#searchInput').val().length != 0) {

            $("#incentive").css({ "display": "none" });
        }

        if ($('#searchInput').val().length == 0) {
            $('#searchInput').val(keyValue);
        }
        console.log("okok: " + e.keyCode);
        searchMovie($('#searchInput').val());
    });

    specialCharacter();
}

function searchMovie(subtitle) {
    console.log("substring: " + subtitle);
    var table = new Array();
    var title = subtitle;
    var moviePack = new movie(title);
    table[0] = moviePack;

    $.ajax({
        type: 'POST',
        url: '/Search/glassSearchSubstring',
        data: JSON.stringify(table),
        contentType: 'application/json',
        dataType: 'json',
        success: function (data) {
            $(".SearchResult").remove();
            for (var i = 0; i < data.length; i++) {

                if (data[i].pictureId == "") {
                    data[i].pictureId = "defaultPoster";
                }
                $("#moviesResult").append("<div class='SearchResult'><a href=/Movie/Show/" + data[i].id + " class='oneResult'><img class='searchPoster' src='/Content/images/uploaded" + data[i].pictureId + ".png'><p class='oneResultP'>" + data[i].title + "</p></a></div>");
            }
        },
        error: function (err) {
            alert('error - ' + err);
        }
    });
}

function movie(title) {

    this.id = null;
    this.title = title;
    this.releaseDate = null;
    this.pictureId = null;
}

function specialCharacter() {

    $(document).keyup(function (e) {

        if (e.keyCode == 8) {

            var lastKey = $('#searchInput').val();

            lastKey = lastKey.slice(0, -1);

            $('#searchInput').val(lastKey);
            searchMovie($('#searchInput').val());
        }

        if (e.keyCode == 13) {

           /* console.log("<__/enter"); */
        }

        if ($('#searchInput').val().length == 0) {

            $("#incentive").css({ "display": "block" });
        }
    });
}

function noScroll() {

    var top = $(window).scrollTop();
    var left = $(window).scrollLeft()

    $('body').css('overflow', 'hidden');

    $(window).scroll(function () {
        $(this).scrollTop(top).scrollLeft(left);
    });
}

function yesScroll() {

    $('body').css('overflow', 'auto');
    $(window).unbind('scroll');
}

function searchOnBar() {
    
    $("#SearchBarLink").click(function () {

        $("#searchInput").val("");
        checkScrollAndGo();
    });
}

function checkScrollAndGo() {

    var scrollTop = $(window).scrollTop();

    if (scrollTop == 0) {
        showBlur();
        noScroll();

        var input = $("#searchInput");
        input[0].selectionStart = input[0].selectionEnd = input.val().length;
    }
    else {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        setTimeout(checkScrollAndGo, 200);
    }
}


$(document).ready(function () {
    setPage();
    writeSize();
    //prevent();
    searchOnBar();
    checkWritting();


    $(window).resize(function () {
        setPage();
        writeSize();
    });

});

