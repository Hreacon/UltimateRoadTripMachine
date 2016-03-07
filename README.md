# UltimateRoadTripMachine
Epicodus C# Final Project
Main description

The goal is to pull up a map with directions from one stop to the next, provide pictures images and video of the location, and scroll down to get to the next location. Each “page” (as in view window of the browser) should be a destination
When the user scrolls up to see their previous destinations show them as much info as they could want. Arrow keys should scroll on desktop. Swiping should scroll on mobile.
I want the image galleries to extend off to the right if theres enough content. Somehow mix content between images and youtube vids and maps. Have a way to scroll through the content without leaving or changing the site.

In the end you have a road trip that scrolls like its already been completed, just with impersonal images and videos

Save the road trip to a database and be able to see all the road trips in snippets, select them and view the awesome road trips other people have planned 
Description of service used in MVP form:

Enter start location - IE Portland OR
Show map focused on that location
Enter first stop, or destination - IE Multnomah Falls
Show map with travel distance, time. Show pictures and video.
Ask for other destinations, upon entry scroll the page down and show more content.
Keep getting destinations until the user clicks on the end button, at which time they will be asked to provide a name for the road trip.
PAST MVP:
Offer stops every four hours for food
Track mileage and offer oil change locations
Description of the Landing Page:
Start screen
Black background with large white lettering “Please enter your start location”
Put in address, city, whatever hit enter
Show map centered on that area
Large white lettering saying enter first stop/attraction/destination


Teams
Michael Dada and Chris Kuiper, C# Back end team. Write the classes described below
Chris B, Front End Design. Design/Code the front end/views
Nicholas Jensen, Project Management, Javascript/jQuery, AJAX
Class definitions for MVP:

Destination Class:
Properties
id int identity(1,1)
roadtrip_id int
name varchar(255)
order int
Methods:
Non-Static methods:
getters/setters
save, saves to database weather it needs to insert or ipdate.
Optional - separate the update function. I don’t mind what way you choose to do it.
delete, deletes the record in the database
Static methods:
getall, gets all destinations
find, returns the destination given the id
deleteall, deletes all in the table

RoadTrip Class:
Properties
id int identity(1,1)
name varchar(255)
description text
Methods:
Non-Static Methods
Getters/Setters
save
optional update
getdestinations
Delete
change order, change the order of the events. arguments, id of item to change, place to put it in. - do this later
Static Methods
getall
Find - get a road trip by id
deleteall

Views:
List of road trips
Landing page / road trip creation page / road trip view page
The destinations should take up roughly the same size as a screen
Have small links to edit the destination
A page for editing the order of the routes 

Past MVP:
Can we make scrolling the website “snap” a little to certain places?
gallery - store the url of all the pictures and videos found. Need a C# class and some javascript
Have a way to store cars and pictures and show the rides used to go on the roadtrip
