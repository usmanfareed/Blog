$(document).ready(function() {
 
  var owl = $("#owl-demo");
 
  owl.owlCarousel({
     
      itemsCustom : [
        [0, 1],
        [500, 2],
        [750, 3],
        [992,1],
        [1600, 1]
      ],
    lazyLoad : true,
	autoPlay : true,
	stopOnHover : true,
    navigation : false
 
  });
$('#style-switcher').styleSwitcher(); 
});  