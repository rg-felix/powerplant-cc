# How to build and launch
- Open the .sln in Visual Studio 2019
- Press F5 (or go to the menu Debug then Run)

# How to test the API
- the url of the API is http://localhost:8888/CodingChallenge/ProductionPlan
- if you have a favorite tool to test API, use that
	- add the header "Content-Type: application/json"
	- use the content of a payload file as the content of the request
	- send a POST to the API

Otherwise, use the command line tool curl, you can download dit from https://curl.se/
```curl -X POST http://localhost:8888/CodingChallenge/ProductionPlan -H "Content-Type: application/json" -d @payload1.json```

# Extra challenge
- the url to test with the CO2 is http://localhost:8888/CodingChallenge/ProductionPlanWithCo2
```curl -X POST http://localhost:8888/CodingChallenge/ProductionPlanWithCo2 -H "Content-Type: application/json" -d @payload1.json```

# About
As requested in the exercise, I didn't read documentation on other solvers.
The algorythm I went for was inspired by pathfinding and the visitor pattern.