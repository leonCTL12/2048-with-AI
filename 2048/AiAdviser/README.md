# 2048 Game with AI Assistant

## Overview

This project is a command-line implementation of the classic 2048 game, a single-player sliding tile puzzle where players combine tiles with the same value to create larger tiles, aiming to reach the 2048 tile while avoiding a game-over state. The game includes an AI assistant that suggests optimal moves to help players maximize their chances of winning.

## Features

- **Command-Line Interface**: Play the 2048 game directly in the terminal with a text-based board visualization.
- **Game Mechanics**: Supports merging tiles in four directions (Up, Left, Right, Down) and adds a random 2 or 4 tile after each valid move.
- **AI Assistant**: Provides move suggestions using a random simulation-based AI to avoid game-over and aim for the 2048 tile.

## AI Implementation

The AI assistant uses a **Random Simulation AI** to suggest the best move based on the current board state. The algorithm works as follows:

1. **For Each Direction (Up, Left, Right, Down)**:
- Run 1000 simulations, each starting with the chosen direction.
- In each simulation:
    - Execute the initial move in the chosen direction.
    - Perform up to 20 random moves or stop if the game ends (win or loss).
    - Calculate a score for the simulation using:
      FinalScore = GameScore + (MaxTileValue × 100) + (EmptyCellCount × 500)

- `GameScore`: Sum of values of merged tiles.
- `MaxTileValue`: Highest tile value on the board.
- `EmptyCellCount`: Number of empty cells on the board.
- Compute the average score across all simulations for that direction.

2. **Move Selection**:
- Choose the direction with the highest average score as the recommended move.


## How to Play
Board Visualization: The game displays a 4x4 grid where numbers represent tiles, and * represents empty cells.
Controls:
Use arrow keys to move tiles (Up, Down, Left, Right).
Press a to get an AI-suggested move.
Press q to quit the game.
Objective: Merge tiles to create a 2048 tile while keeping the board open to avoid a game-over state.
Scoring: Each merge adds the value of the new tile to the score (e.g., merging two 2s into a 4 adds 4 points).
Example Gameplay
Starting board:


```
2 2 2 *
* 2 * 2
2 * 2 *
2 2 * 2
```

Press "a" to get an AI suggestion (e.g., "Left").
Choose a direction to move tiles, merging identical values and adding a new tile (2 or 4).
Continue until you reach a 2048 tile (win) or no moves are possible (loss).

## Acknowledgments
Inspired by the classic 2048 game created by Gabriele Cirulli.