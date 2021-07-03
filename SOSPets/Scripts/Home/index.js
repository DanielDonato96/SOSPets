var myIndex = 0;

$(function () {
    
    if ($('.carousel-item').length > 1) setCarouselPropagandas(); 
});


function setCarouselPropagandas() {
    var i;
    var x = $(".carousel-item");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    myIndex++;
    if (myIndex > x.length) { myIndex = 1 }
    x[myIndex - 1].style.display = "block";
    setTimeout(setCarouselPropagandas, 5000);
}