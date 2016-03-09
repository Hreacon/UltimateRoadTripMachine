function fixImages()
{
  console.log("Fix Images");
  $(".clickhandler").each(function() {
    var img = $(this).find("img");
    img.on('load', function() {
      var aspect = img.width() / img.height();
      img.attr("data-aspect", aspect);
      console.log("Aspect Ratio: " + aspect);
      if(aspect < 1) {
        img.width($(this).parent().height() * aspect-4);
        img.css("margin-left", ($(this).parent().width() - img.width()) / 2 + "px");
      } else {
        img.width($(this).parent().width()-4);
        // img.css("margin-top", ($(this).parent().height() - img.height()) / 2 + "px");
        $(this).parent().height(img.height()+4);
      }
      // todo set margin to center img
    });
    $(this).click(function() {
      console.log("MaxImg:");
      var img = $(this).children("img");
      var src = img.attr('src');
    $('.maximg img').attr('src', src);
      if( window.innerWidth > 1000 ) {
        $('.maximg div').attr('top', '5%');
      }
      var width = 0;
      var height = 0;
      var aspect = img.attr('data-aspect');
      console.log("Aspect: " + aspect);
      if( aspect > 1 ) {
        width = window.innerWidth - window.innerWidth*.2;
        height = width * aspect;
      } else {
        height = window.innerHeight - window.innerHeight*.1;
        width = height * aspect;
      }
      $('.maximg img').attr('height', height );
      $('.maximg img').attr('width', width );
      $('.maximg').show();
    });
    $(this).removeClass("clickhandler");
  });
  $('.maximg').click(function() {
    $(this).hide();
  });
}

$(document).ready(function() {
  $("#commandLine").submit(function(event) {
    event.preventDefault();
    var href = "/addStop";
    var command = $("#commandLine input[name='command']").val();
    if(command.trim().length > 0) {
      console.log("Command sent to server at " + href + " with command " + command);
      var roadTripId = 0;
      roadTripId = $("#roadTripId").val();
      $.post(href, {
          command: command,
          roadTripId: roadTripId,
        }, function(data, status) {
            console.log("Data returned from server");
            $(".content").append(data);
            fixImages();
        }
      );
    } else { 
      console.log("Command not sent, no command found");
    }
  });
});


