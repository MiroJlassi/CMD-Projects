============================================================
       👾  MIRO'S PONG ASCII GAME DOCUMENTATION 👾
============================================================

1. **logo() Function:**
   - Displays an ASCII art logo on the console.
   - The logo is shown for 5 seconds and then clears the console.

2. **Main Game Loop:**
   - Initializes game variables like scores and initial positions.
   - Creates a grid 'L' representing the game environment.

3. **Game Grid 'L':**
   - A 2D list representing the game grid.
   - Includes a visual representation of the player, CPU, and obstacles.

4. **Player and CPU Positions:**
   - 'a' and 'b' variables represent the player and CPU positions.
   - 'x' and 'y' variables store the Ball's movement direction.

5. **Input and Game State Handling:**
   - Checks if the game has ended (max score reached).
   - Displays the result and prompts for a new game.

6. **Player Movement:** 👈
   - Uses the msvcrt module to detect keyboard input.

   // Syntax:
    # import msvcrt
    # if msvcrt.kbhit():
    #    ch = msvcrt.getch().decode("utf-8")

   - 'q' and 'a' keys are used for player movement.

7. **File Structure:**
   - 'ascii_game.py' contains the main game code.
   - 'documentation.txt' (this file) contains documentation.

8. **Note:**
   - Adjustments and enhancements may be needed based on future changes.
   - Document updates should accompany code modifications.

9. **Author:**
    - ©Miro_Jlassi

10. **Version:**
    - v1.0 (January 11, 2024)

============================================================
