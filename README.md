# csci321_assignment02
[CSCI321 (Windows Programming) - Assignment 02](https://github.com/00bayz/csci321_assignment02)

[ Requirements ]

- [x] Add data and image resources in debug folder
	- [x] puzzle.txt (data about how the game board should load on startup)
	- [x] puzzle.jpg (image spritesheet; contains all possible state each grid on the game board can be in)

- [ ] Create initial game board with data specified on data resource file
	- [ ] Create 2D array of squares
	- [ ] Fill squares with corresponding image from spritesheet

- [ ] Create control buttons (up, down, left, and right) that will be displayed throughout the lifetime of the app
	- [x] Create buttons
	- [ ] Add functionality to buttons

- [ ] Label holes and balls

- [ ] Validate move and check win
	- [ ] Loss: a ball falls into the wrong hole
		- [ ] Replace ball image with bottom-rightmost image in spritesheet
		- [ ] Replace hole image with bottom-rightmost image in spritesheet
		- [ ] Display "Game Over" message in a MessageBox
		- [ ] Disable all control buttons
		- [ ] Display "Restart" button (optional)
	- [ ] Win: all balls are in the correct holes
		- [ ] Display "You Won" message in a MessageBox
		- [ ] Disable all control buttons
		- [ ] Display "Restart" button (optional)