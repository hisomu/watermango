# Watermango Challenge

Our office has 5 plants, and no one to water them. We don’t want them to die. Create a frontend and backend solution that enables a user to water our plants remotely.

## Requirements

Your application should meet the following requirements:

1.	As a user, I want to see a list of plants on a web page, as well as their watering status
2.  As a user, I want to start and stop watering of a plant. A plant takes 10 seconds to water.
3.	The system should support watering multiple plants at the same time.
4.	Plants need to rest from watering, so as a User, I should not be able to water the plant again within 30 seconds of the last watering session.
5.	As a user, I should be visually alerted if a plant hasn’t been watered for more than 6 hours.

## Running the solution

Run `dotnet run` for the backend dev server. 
Run `npm start` for the frontend dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Approach
* Created a background service along with the WebAPI to push out state updates using SignalR