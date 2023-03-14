# Bowling App

The app is designed as a simple console application that simulates a bowling game with a single player
For each game loop iteration the game advanced by one step in which the ball is rolled in the currently active frame. When the last frame is completed the game ends.
The score can be printed out at any point during the game and at the end.

The problem is programmed as a simple object oriented solution.

# Future enhancements

Some simple improvements that could be made for this console application:

- The game could be extended to include multiple players. This could be achived by keeping history of all rolls for each player in the `Frame` class.
- The application could handle parsing a previous game and showing the complete score. For example we could save the game state to a JSON file and parse it later to show the score.

## Adding a UI

A next level enhancement could be to add some kind of UI either a desktop frontend or a web application. The most preferable solution would likely be a web application that communicates with a backend service in a RESTful manner.
