//$("#mk-header-login-button").mouseover(function () {
//    $(this).addClass('active');
//    $('#mk-header-subscribe').hide(); 
//    $('#mk-nav-search-wrapper').hide();
//    $('.mk-login-register').show();
//    $('.mk-shopping-cart-box').hide();
//});

//$("#mk-header-subscribe-button").mouseover(function () {
//    $(this).addClass('active');
//    $('.mk-login-register').hide(); 
//    $('#mk-nav-search-wrapper').hide();
//    $('#mk-header-subscribe').show(); 
//    $('.mk-shopping-cart-box').hide();
//});

$(".wiki-store-trigger").mouseover(function () {
    $(this).addClass('active');
    $('.mk-login-register').hide();
    $('#mk-header-subscribe').hide();
    var p = $(this);
    var offset = p.offset(); 
    $('.wiki-store-box').show().css("left", offset.left).css("top", offset.top + 15);
    $('#mk-nav-search-wrapper').hide();
    $('.mk-shopping-cart-box').hide();
});


$(".wiki-category-trigger").mouseover(function () { 
    $(this).addClass('active');
    $('.mk-login-register').hide();
    $('#mk-header-subscribe').hide();
    var p = $(this);
    var offset = p.offset();
    console.log(offset);
    $('.wiki-box-to-trigger').show().css("left", offset.left).css("top", offset.top+15);
    $('#mk-nav-search-wrapper').hide(); 
    $('.mk-shopping-cart-box').hide();
});

$(".mk-search-trigger").mouseover(function () {
    $(this).addClass('active');
    $('.mk-login-register').hide();
    $('#mk-header-subscribe').hide();
    $('#mk-nav-search-wrapper').show();
    $('.mk-shopping-cart-box').hide();
});

$("#searchbar").mouseout(function () {
    $('#mk-nav-search-wrapper').hide();
});

$("#work").mouseover(function () {
    $('.wiki-box-to-trigger').hide();
    $('.mk-login-register').hide();
    $('#mk-header-subscribe').hide();
    $('#mk-nav-search-wrapper').hide();
    $('.mk-shopping-cart-box').hide();
    $('.wiki-store-box').hide();
});
  
$(".shoping-cart-link").mouseover(function () {
    $(this).addClass('active');
    $('.mk-login-register').hide();
    $('#mk-header-subscribe').hide();
    $('#mk-nav-search-wrapper').hide();
    $('.mk-shopping-cart-box').show();
});

$("#home-bg").mouseover(function () { 
    $('.mk-login-register').hide();
    $('#mk-header-subscribe').hide();
    $('#mk-nav-search-wrapper').hide();
    $('.mk-shopping-cart-box').hide();
});

$("#Search").click(function () { 
    var url = "/search.aspx?search=" + $('#searchTextBox').val();
    if ($('#searchTextBox').val().length > 0) {
        window.location.href = url;
    } 
});

$(".ProductLike").click(function (event) {
    var id = $(this).attr("data-id");
    var userid = $('#userid').val();
    var url = "/api/like/";
    event.preventDefault();
    if (userid == null) {
        window.location.href = "/Account/Login/";
    }
    else
    {
    // Send the data using post 
        var dataJSON = { id: id, userid: userid };
    $.ajax({
        type: 'get',
        url: url,
        data: dataJSON,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            $("#like_" + id).html(data);
        },
        error: function (msg) {
            $('#mainimage').html(msg);
        }
    }); 
    }
});

