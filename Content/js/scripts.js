function fixImageAddClick()
{
  console.log("Fix Image Add Click");
  $(".clickhandler").each(function() {
    var img = $(this).find("img");
    img.on('load', function() {
      var aspect = img.width() / img.height();
      img.attr("data-aspect", aspect);
      console.log("Aspect Ratio: " + aspect);
      fixImageSize(img);
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
} // end fix image add click
function fixImageSize(img)
{
  var aspect = img.attr('data-aspect');
  img.css('position', 'relative');
 
  if(aspect < 1) {
    img.width(img.parent().height() * aspect-4);
    // console.log(img.parent().width()+" - "+img.width()+"/2/"+img.parent().width()+"*100 = " + ((img.parent().width() - img.width()/2)/(img.parent().width())*100) + "%");
  } else {
    // for landscape set the width and let the height figure itself out and set margin to center it vertically
    img.width(img.parent().width()-4);
    // console.log("Landscape image height: "+ img.height());
    img.height(img.width()/aspect);
    if(img.height() > img.parent().height()-10)
    {
      img.height( img.parent().height()-4 );
      img.width(img.parent().height() * aspect-4);
    }
  }
     if( window.innerWidth < 780 )
      {
        img.parent().height(img.height()+4);
        if(img.parent().width() > img.width()+5 && img.parent().height() < 300)
        {
          if((img.parent().width()-4) / aspect < 300)
          {
            img.height((img.parent().width()-4)/aspect);
          } else {
            img.height(300);
          }
            img.width(img.height()*aspect-4);
        }
      } else {
        img.parent().height("300px");
      }
      if(img.parent().width()-10 > img.width())
      {
        // center horizontally
        var margin = (((img.parent().width() - img.width())/2)/(img.parent().width())*100);
        if(margin > 2){
          img.css("margin-left", (margin-2) + "%");
          img.css("margin-top", 0);
        } else {
          img.css("margin-top", 0);
          img.css("margin-left", 0);
      }
      } else if(img.parent().height()-10 > img.height())
      {
        // center vertically 
        var margin = (((img.parent().height() - img.height())/2)/(img.parent().height())*100);
        if(margin > 2) {
          img.css("margin-top", (margin-2) + "%");
          img.css("margin-left", 0);
        } else {
          img.css("margin-top", 0);
          img.css("margin-left", 0);
      }
      } else {
          img.css("margin-top", 0);
          img.css("margin-left", 0);
      }
    // }
    
    // if(img.parent().height() - img.height() > 100)
    //   img.parent().height(img.height()+4);
    // img.height(img.width()/aspect);
    // img.css("margin-top", (img.parent().height() - img.height()) / 2 + "px");
    // if(img.height()+4<300)
    //   img.parent().height(img.height()+4);
  // todo set margin to center img
}

$(document).ready(function() {
  $("#commandLine").submit(function(event) {
    var commandLine = $("input[name=command]");
    $("#commandLine input[type=submit]").prop('disabled', true);
    console.log(commandLine);
    var command = commandLine.val();
    commandLine.val("Loading......");
    commandLine.prop('disabled', true);
    event.preventDefault();
    var href = "/addStop";
    if(command.trim().length > 0) {
      console.log("Command sent to server at " + href + " with command " + command);
      var roadTripId = 0;
      roadTripId = $("#roadTripId").val();
      $.post(href, {
          command: command,
          roadTripId: roadTripId,
        }, function(data, status) {
            console.log("Data returned from server");
            $(".content").append("<div id='scrollToHere'>"+data+"</div>");
            fixImageAddClick();
            var commandLine = $("input[name=command]");
            commandLine.prop('disabled', false);
            commandLine.val("");
            $("#commandLine input[type=submit]").prop('disabled', false);
            document.getElementById('scrollToHere').scrollIntoView();
            $("#scrollToHere").attr('id', '');
            commandLine.focus();
            window.scrollBy(0,-200);
        }
      );
    } else {
      console.log("Command not sent, no command found");
    }
  });
  $(window).resize(function() {
    $("img[data-aspect]").each(function() {
      fixImageSize($(this));
    });
  });
});

// $(document).ready(function() {
//     $('.overlay').click(function () {
//         $('.overlay iframe').css("pointer-events", "auto");
//     });
//
//     $( ".overlay" ).mouseleave(function() {
//       $('.overlay iframe').css("pointer-events", "none");
//     });
//  });
