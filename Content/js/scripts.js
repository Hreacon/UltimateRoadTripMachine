$(document).ready(function() {
  $("#maptest").submit(function(event) {
    event.preventDefault();
    var href = "/map";
    var start = $("#maptest input[name='start']").val();
    href = href;
    $.post(href, {
        start: start 
      }, function(data, status) {
          $(".content").html(data);
      }
    );
  });
  $("#maptestdirections").submit(function(event) {
    event.preventDefault();
    var href = "/mapDirections";
    var start = $("#maptestdirections input[name='start']").val();
    var end = $("#maptestdirections input[name='end']").val();
    href = href;
    $.post(href, {
        start: start 
      }, function(data, status) {
          $(".content").html(data);
      }
    );
  });
});


