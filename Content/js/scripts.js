$(document).ready(function() {
  $("#tesat").submit(function(event) {
    event.preventDefault();
    var href = "/getPage";
    var command = $("input[name='command']").val();
    href = href;
    $.post(href, {
        command: command 
      }, function(data, status) {
          $(".data").html(data);
      }
    );
  });
});


