Matt Partridge Console App

Please extract the project to a directory of C:\Visul Studio Projects\
The project has been written with VS2017 version 15.4.3

There are three xml dataset files included with this application for the offline version.

If the offline version is played then the choice of artists is Bob Dylan; Madonna; The Beatles, if online this restriction does not apply.

Design Decisions

I wanted to make the application as loosely coupled as possible.
To that end all the required interfaces are in the Infrastructure project and i have programmed against interfaces instead of concrete classes where possible.

I have used object state to manage the concrete implementation, for example the EvaluateRepository method of the GameController class determines the repository to use at runtime.

I wanted to demonstrate different implementations of intefaces so created three games that all implement IGame, two of which aslo implement IGamblingGame.

Instantiation of these games and the repository is managed by the GameController.

I wanted each game to demonstrate something different, to that end.

Global Thermo Nuclear War - simply runs on a background thread, updating the console over time.

Play Your PlayCount Right and Wheel of Fortune demonstrate different implementaions of the IGamblingGame interface.

WOPR.DataLayer

This is a singleton class that holds objects containing either the deserialised contents of one of the xml files or the results of the call to the LastFM API.
It holds a the list of available games.

WOPR.Infrastructure

This contains all interfaces and any common classes used within the application.
The Maybe class' purpose is to eliminate NULL Reference Exceptions - it will always return a list that has either 1 or 0 items.
I've also included a string extension method that simplifies some of the code in the WheelOfFortune class' UpdateGame method.

WOPR.Repositories

This contains concrete implementations of IRepository allowing the datasource to be determined at runtime.

WOPR.BusinessLogic

This controls the application through the singleton GameController class.
This instantiates a concrete implementation of each object that implements IGame.
These classes use GameState to manage the state of each game, though this is far from perfect as there is more branching logic in each class than I would like, and given time i would hope to be able to simplify these classes futher.











