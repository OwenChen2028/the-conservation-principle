Credits:
- Owen Chen - Programming, Design (Levels)
- Ryan Peterson - Art (Sprites), Programming
- Axel O'Brien - Programming, Design (Levels)
- Tanner Hume - Programming, Design (Mechanics)
- Lia Hansen - Art (Backgrounds)
- Owen Gallagher - Music, SFX

The project should be built using Unity version 2022.3.34f1 (LTS)

The game is available to play at: https://mrmacropod.itch.io/the-conservation-principle

Description: The core gimmick of this game is that certain objects, including the player themselves, have a "size" property that can be altered by the player's size gun. The size of an object determines how it interacts with the world, with larger objects being heavier and having greater inertia. Every scalable object has a SizeManager script attached that determines if it is movable, which directions it can be scaled in, and how large or small it can become.

Upon left or right-clicking, the player fires a gun. This creates a raycast that, upon collision with either a wall or a scalable entity, generates a red or blue laser and does one of two things depending on which button was pressed:
- If the player is right-clicking, the object will shrink at a fixed rate, continuing until the object reaches its minimum size. The player can "store" mass, as shown by the percentage above their head, by shrinking other objects. The percentage above the player indicates how much mass they have stored relative to the minimum and maximum amounts of mass the player can store.
- If the player left-clicks, the mass that they are storing will be transferred to any scalable objects hit, making them larger and heavier at a fixed rate.

There are mirrors in the game that can reflect the laser beam, causing it to switch directions vertically or horizontally depending on the mirror's orientation. This is handled in the PlayerController script; the mirrors themselves are simply tagged and don't have a script. This allows the player to shoot themselves with their own beam to change their own size. Because the player is moved using forces rather than directly setting their velocity, a larger player will be slower and have a lower jump height, but will also be able to push heavier buttons. A lighter player will be able to jump higher and travel faster.

Most of the calculations relating to physical interactions between objects are made using Unity's physics engine. This means that the game handles bugs and edge cases relatively well, though sometimes it is possible to get an object to act strangely if it is scaled larger than the walls of the surrounding environment.

Screenshots:
![image](https://github.com/user-attachments/assets/301e3e0f-8406-480a-a9a0-da63404d5b13)
![image](https://github.com/user-attachments/assets/45e89b4f-e4c0-4610-b7d6-61f5340521f6)
![image](https://github.com/user-attachments/assets/60364eaa-2b0e-4704-8f6e-70ea8c88b321)
