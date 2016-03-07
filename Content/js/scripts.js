$(document).ready(function() {
  $("#testa").submit(function(event) {
    event.preventDefault();
    var href = "/test";
    var command = $("input[name='command']").val();
    $.post(href, {
        command: command 
      }, function(data, status) {
          $(".data").html("Command: " + data);
      }
    );
  });
});