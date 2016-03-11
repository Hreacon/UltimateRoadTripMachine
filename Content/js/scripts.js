// To do the carousel:
// On opening the maximg div, store the source of the image and the nth child it is
// On Click right - go to the next nth child img of the gallery
// on click left - go to the prev child of the gallery

function fixImageAddClick()
{
  console.log("Fix Image Add Click");
  $(".clickhandler").each(function() {
    if($(".clickhandler").parent().attr('data-id') > 0)
    {
      var uniqueId=Math.floor((Math.random()*1000000) + 1);
      $(".clickhandler").parent().attr('data-id', uniqueId);
      console.log("UniqueId: "+uniqueId);
    }
    var img = $(this).find("img");
    console.log(img);
    if(img.complete || img.readyState == 4)
    {
      // image cached!
      fixImageSize(img);
    }
    img.on('load', function() {
      fixImageSize(img);
    });
    // Add MaxImg click handler
    $(this).click(function() {
      console.log("MaxImg:");
      var img = $(this).children("img");
      var src = img.attr('src');
      // carousel info
      $(".maximg").attr('data-gid', $(this).parent().parent().find(".results-header").attr('data-id'));
      $(".maximg").attr('data-index', img.attr('data-index'));
      if( window.innerWidth > 1000 ) {
        $('.maximg .maximginner').attr('top', '5%');
      }
      var width = 0;
      var height = 0;
      var aspect = img.attr('data-aspect');
      if( aspect > 1 ) {
        width = window.innerWidth - window.innerWidth*.2;
        height = width * aspect;
      } else {
        height = window.innerHeight - window.innerHeight*.1;
        width = height * aspect;
      }
      var maximgImg = $(".maximginner").find("img");
      maximgImg.attr('src', src);
      maximgImg.attr('height', height );
      maximgImg.attr('width', width );
      $('.maximg').css('display', 'flex');
      $(".left-arrow").click(function(e) {
        e.stopPropagation();
        var galleryId = $(".maximg").attr('data-gid');
        var index = $(".maximg").attr('data-index');
        index--;
        if(index > 5) index = 0;
        if(index < 0) index = 5;
        var targetImg = $("[data-id="+galleryId+"]").parent().find("[data-index="+index+"]");
        var newsrc = targetImg.attr('src');
        var newAspect = targetImg.attr('data-aspect');
        console.log("left arrow: id: " + galleryId + ", Index: " + index + ", newsrc: " + newsrc);
        $(".maximg").attr('data-index', index);
        if( newAspect > 1 ) {
          width = window.innerWidth - window.innerWidth*.2;
          height = width * newAspect;
        } else {
          height = window.innerHeight - window.innerHeight*.1;
          width = height * newAspect;
        }
        console.log("dimensions "+width+"x"+height);
        var maximgImg = $(".maximginner").find("img");
        maximgImg.attr('src', newsrc);
        maximgImg.attr('height', height );
        maximgImg.attr('width', width );
      });
      $(".right-arrow").click(function(e) {
        e.stopPropagation();
        var galleryId = $(".maximg").attr('data-gid');
        var index = $(".maximg").attr('data-index');
        index++;
        if(index > 5) index = 0;
        if(index < 0) index = 5;
        var targetImg = $("[data-id="+galleryId+"]").parent().find("[data-index="+index+"]");
        var newsrc = targetImg.attr('src');
        var newAspect = targetImg.attr('data-aspect');
        console.log("right arrow: id: " + galleryId + ", Index: " + index + ", newsrc: " + newsrc);
        $(".maximg").attr('data-index', index);
        if( newAspect > 1 ) {
          width = window.innerWidth - window.innerWidth*.2;
          height = width * newAspect;
        } else {
          height = window.innerHeight - window.innerHeight*.1;
          width = height * newAspect;
        }
        console.log("dimensions "+width+"x"+height);
        var maximgImg = $(".maximginner").find("img");
        maximgImg.attr('src', newsrc);
        maximgImg.attr('height', height );
        maximgImg.attr('width', width );
      });
    }); // end maximg clickhandler
    $(this).removeClass("clickhandler");
  });
  $(".moveUpHandler").each(function() {
    $(this).click(function() {
      var href = "/moveUp/" + $(this).parent().attr("data-id");
      console.log("AJAX Sending Move/Delete");
      var href2 = "/roadTrip/" + $("#roadTripId").val();
      $.ajax(href).done(function() {
        window.location.href = href2;
      });
      var stop = $(this).parent().parent().parent();
      stop.insertBefore(stop.prev());
    });
    $(this).removeClass("moveUpHandler");
  });
  $(".moveDownHandler").each(function() {
    $(this).click(function() {
      var href = "/moveDown/" + $(this).parent().attr("data-id");
      console.log("AJAX Sending Move/Delete");
      var href2 = "/roadTrip/" + $("#roadTripId").val();
      $.ajax(href).done(function() {
        window.location.href = href2;
      });
      var stop = $(this).parent().parent().parent();
      stop.insertAfter(stop.next());
    });
    $(this).removeClass("moveDownHandler");
  });
  $(".deleteHandler").each(function() {
    $(this).click(function() {
      if(confirm("Are you sure you want to delete this stop?"))
      {
        var href = "/deleteDestination/" + $(this).parent().attr("data-id");
        console.log("AJAX Sending Move/Delete");
        $.ajax(href);
        $(this).parent().parent().remove();
      }
    });
    $(this).removeClass("deleteHandler");
  });
  $('.maximg').click(function() {
    $(this).hide();
  });
  indexImages();
} // end fix image add click
function indexImages()
{
  console.log("Indexing images");
  $(".stop").each(function() {
    var i = 0;
    $(this).find("img").each(function() {
      $(this).attr('data-index', i);
      i++;
    });
  });
  // count stops
  // if the first stop, hide up arrow
  // if the last stop, hide down arrow
  // if no stops, do nothing
  $(".fa").show();
  $(".stop").first().find(".fa-arrow-circle-up").hide();
  $(".stop").last().find(".fa-arrow-circle-down").hide();
}
function fixImageSize(img)
{
  
  var aspect = img.width() / img.height();
  img.attr("data-aspect", aspect);
  console.log("Aspect Ratio: " + aspect);
  img.css('position', 'relative');

  if(aspect < 1) {
    img.width(img.parent().height() * aspect-4);
  } else {
    // for landscape set the width and let the height figure itself out and set margin to center it vertically
    img.width(img.parent().width()-4);
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
}

$(document).ready(function() {
  $("#commandLine").submit(function(event) { // command enter
    var commandLine = $("input[name=command]");
    $("#commandLine input[type=submit]").prop('disabled', true);
    var command = commandLine.val();
    commandLine.val("Loading......");
    commandLine.prop('disabled', true);
    event.preventDefault();
    
    var href = "/addStop";
    if(command.trim().length > 0) { // command is good, do ajax
      console.log("Command sent to server at " + href + " with command " + command);
      var roadTripId = 0;
      roadTripId = $("#roadTripId").val();
      $.post(href, {
          command: command,
          roadTripId: roadTripId,
        }).done(function(data, status) {
          console.log("Data returned from server");
          $(".content").append("<div id='scrollToHere'>"+data+"</div>");
          if($(".content").children().length>0)
            $(".nameTrip").show();
          fixImageAddClick();
          var commandLine = $("input[name=command]");
          document.getElementById('scrollToHere').scrollIntoView();
          $("#scrollToHere").attr('id', '');
          commandLine.focus();
          window.scrollBy(0,-200);
          commandLine.val("");
        }
      ).fail(function(){
        commandLine.val("Failure..");
      }).always(function() {
        commandLine.prop('disabled', false);
        $("#commandLine input[type=submit]").prop('disabled', false);
      });
    } else {
      console.log("Command not sent, no command found");
    }
  });
  
    $(".nameTrip").click(function() {
      var name = prompt("What do you want to name your Road Trip?");
      $.post("/nameTrip", {
        id: $("#roadTripId").val(),
        name: name,
      }).done(function() {
        $(".nameTrip").find("button").text(name);
      });
    });
  // fix image sizes on window resize
  $(window).resize(function() {
    $("img[data-aspect]").each(function() {
      fixImageSize($(this));
    });
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

 $(document).ready(function(){
  $('.sliding-panel-button,.sliding-panel-fade-screen,.sliding-panel-close').on('click touchstart',function (e) {
    $('.sliding-panel-content,.sliding-panel-fade-screen').toggleClass('is-visible');
    e.preventDefault();
  });
});
