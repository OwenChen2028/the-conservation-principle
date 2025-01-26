The Conservation Principle by Big Green Games is licensed under [CC BY-NC-ND 4.0](https://creativecommons.org/licenses/by-nc-nd/4.0/?ref=chooser-v1)

Made by Big Green Games

Credits:
- Owen Chen - Programming, Design (Levels)
- Ryan Peterson - Art (Sprites), Programming
- Axel O'Brien - Programming, Design (Levels)
- Tanner Hume - Programming, Design (Mechanics)
- Lia Hansen - Art (Backgrounds)
- Owen Gallagher - Music, SFX

The project should be built using Unity version 2022.3.34f1 (LTS)

The game is available to play at: https://elephantfanatic.itch.io/the-conservation-principle

Description:
The core gimmick in this game is that almost every object, including the player itself, have a "size" property that can be altered by the player's size gun. The size of an object determines how it interacts with the world, with larger objects being "heavier", meaning they have more inertia as if they had more mass. Every scalable object has a Size Manager script attached that determines if it is movable, what directions it can be scaled in, and how large/small it can be made. 

Upon left or right-clicking, the player fires a gun that creates a raycast that, upon collision with either a wall or a scalable entity, will create a red/blue laser and do one of two things depending on what button was pressed:
- If the player was right-clicking, the object will be shrunken at a fixed rate, and it will continue shrinking until the object reaches its minimum size. The player can "store" mass as shown by the number above its head. It cannot go beneath 0% or above 100%, and the player collects mass by shrinking other objects
- If the player left-clicks, the mass that it is storing will be inserted into whatever it is aiming at, making it larger and heavier at a fixed rate. The player will lose mass

There are mirrors in the game that can reflect the laser beam, causing it to switch directions vertically and horizontally. This is handled in the PlayerController script, the mirrors themselves don't have a script. This allows for the player to shoot the beam at itself. Because the player is moved using forces and not by directly setting its velocity, if the player is heavier, it will be slower and jump shorter, but it will also be able to push heavier buttons, and a lighter player can make jumps that would be otherwise impossible.

Most calculations are made using the game's physics engine, and the object's size alters the transform.localScale property of objects. This means that the game handles bugs and edge-cases relatively well, there are some situations where objects act unpredictably when scaled larger than their surrounding environment, but the game handles physics very well otherwise.
