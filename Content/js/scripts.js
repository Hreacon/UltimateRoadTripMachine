function fixImages()
{
  console.log("Fix Images");
  $(".clickhandler").each(function() {
    var img = $(this).find("img");
    img.on('load', function() {
      var aspect = img.width() / img.height();
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
      var src = $(this).children("img").attr('src');
    $('.maximg img').attr('src', src);
      if( window.innerWidth > 1000 ) {
        $('.maximg div').attr('top', '5%');
      }
      $('.maximg img').attr('width', window.innerWidth - window.innerWidth*.2);
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

$(document).ready(function() {
    $('.overlay').click(function () {
        $('.overlay iframe').css("pointer-events", "auto");
    });

    $( ".overlay" ).mouseleave(function() {
      $('.overlay iframe').css("pointer-events", "none");
    });
 }); 
