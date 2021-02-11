# csci321_assignment02
[CSCI321 (Windows Programming) - Assignment 02](https://github.com/CDAGaming/csci321_assignment02)

[ Requirements ]

- [x] Add data and image resources in debug folder
	- [x] puzzle.txt (data about how the game board should load on startup)
	- [x] puzzle.jpg (image spritesheet; contains all possible state each grid on the game board can be in)

- [x] Create initial game board with data specified on data resource file
	- [x] Create 2D array of squares
	- [x] Fill squares with corresponding image from spritesheet

- [x] Create control buttons (up, down, left, and right) that will be displayed throughout the lifetime of the app
	- [x] Create buttons
	- [x] Add functionality to buttons

- [x] Label holes and balls

- [x] Validate move and check win
	- [x] Loss: a ball falls into the wrong hole
		- [x] Replace ball image with bottom-rightmost image in spritesheet
		- [x] Replace hole image with bottom-rightmost image in spritesheet
		- [x] Display "Game Over" message in a MessageBox
		- [x] Disable all control buttons
		- [ ] Display "Restart" button (optional)
	- [x] Win: all balls are in the correct holes
		- [x] Display "You Won" message in a MessageBox
		- [x] Disable all control buttons
		- [x] Display "Restart" button (optional)