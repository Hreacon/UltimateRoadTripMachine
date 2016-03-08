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
  $("#commandLinea").submit(function(event) {
    event.preventDefault();
    var href = "/start";
    if($(".content").html().trim() != "")
      href="/addStop";
    var command = $("#commandLine input[name='command']").val();
    if(command.trim().length > 0) {
      console.log("Command sent to server at " + href + " with command " + command);
      var roadTripId = 0;
      if(href == "/addStop")
        roadTripId = $("#roadTripId").val();
      $.post(href, {
          command: command,
        }, function(data, status) {
            console.log("Data returned from server");
            $(".content").append(data);
        }
      );
    } else { 
      console.log("Command not sent, no command found");
    }
  });
});


