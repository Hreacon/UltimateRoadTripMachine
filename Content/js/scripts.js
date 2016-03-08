$(document).ready(function() {
  $("#maptest").submit(function(event) {
    console.log("Map Test Form Submit");
    event.preventDefault();
    var href = "/map";
    var start = $("#maptest input[name='start']").val();
    href = href;
    $.post(href, {
        start: start 
      }, function(data, status) {
          console.log("Map Test Returned Data");
          $(".content").html(data);
      }
    );
  });
  $("#maptestdirections").submit(function(event) {
    console.log("Map Test Direction Form Submit");
    event.preventDefault();
    var href = "/mapDirections";
    var start = $("#maptestdirections input[name='start']").val();
    var end = $("#maptestdirections input[name='end']").val();
    href = href;
    $.post(href, {
        start: start,
        destination: end,
      }, function(data, status) {
          console.log("Map Test Direction Returned Data");
          $(".content").html(data);
      }
    );
  });
});


