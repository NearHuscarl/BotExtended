# BotExtended

<!-- Shrink 25x15
Expand 50x30 -->

This script adds a wide variety of bots to spice up combat experience. It is currently
under development and maybe full of bugs :bug:

<!-- ## Features
- **Large variety of bots**: 60+ new bots for you to fight. No more having to fight hard and expert bots all the time.
- **Bot faction:** Bots will be spawned in many different factions: Thug, Police, Soldier, Assassin, Zombie... to keep you engaging every round.
- **Bosses with special abilities:** Multiple bosses that have special abilities and unique starting weapons to ramp up challenge if you are getting bored of winning expert bots in vanilla.
- **Reaction with dialogue:** Bots can have dialogues depend on the context and evironment around it
- **Special weapons**: Some bots have special weapons that can not be found otherwise. Kill it and get the big reward or die trying! -->

## Getting Started

- Download [this file](src/BotExtended/BotExtended.txt).
- Move it to `%USERPROFILE%\Documents\Superfighters Deluxe\Scripts\`
- Install Python 3 if you don't have it on your computer.
- Run `setup_script.py`

```bash
python scripts/setup_script.py 'C:\Program Files (x86)\Steam\steamapps\common\Superfighters Deluxe'
```

## Powerups

- See the full list of all melee powerups [here](./docs/POWERUPS_MELEE.md).

- See the full list of all ranged powerups [here](./docs/POWERUPS_RANGED.md).

## Factions

- See the full list of all factions [here](./docs/FACTIONS.md).

## Bots

### Assassin Melee

<div>
  <img src='./docs/Images/Assassin_1.png' />
  <img src='./docs/Images/Assassin_2.png' />
  <img src='./docs/Images/Assassin_3.png' />
  <img src='./docs/Images/Assassin_4.png' />
  <img src='./docs/Images/Assassin_5.png' />
  <img src='./docs/Images/Assassin_6.png' />
</div>

**Stats**

| **Health**    | Below Normal                                                                                                        |
| :------------ | :------------------------------------------------------------------------------------------------------------------ |
| **Speed**     | Very Fast                                                                                                           |
| **AI**        | Jogger + Melee Hard                                                                                                 |
| **Weapons**   | ![katana]                                                                                                           |
| **Factions**  | [Assassin][fassassin]                                                                                               |
| **Abilities** | All assassins in a team target one specific enemy at a time. The targeted player has an indicator above their head. |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

<!-- GIF: show assassin gang rape -->

### Assassin Range

They are the same as [Assassin Melee](#Assassin-Melee) except for the following stats:

**Stats**

| **AI**      | Jogger + Range Hard |
| :---------- | :------------------ |
| **Weapons** | ![uzi]              |

### Agent

<div>
  <img src='./docs/Images/Agent_1.png' />
  <img src='./docs/Images/Agent_2.png' />
</div>

**Stats**

| **Health**       | Below Normal                            |
| :--------------- | :-------------------------------------- |
| **AI**           | Hard                                    |
| **Search Items** | Secondary                               |
| **Factions**     | [Agent][fagent], [MetroCop][fmetrocop]. |

**Weapons**

| Gears                    | Powerup  |
| :----------------------- | :------- |
| ![pistol]                | [Poison] |
| ![pistol] ![Lazer]       | [Poison] |
| ![baton]                 |          |
| ![ShockBaton]            |          |
| ![Magnum] ![Lazer]       | [Poison] |
| ![baton] ![Uzi] ![Lazer] | [Poison] |
| ![DarkShotgun] ![Lazer]  | [Poison] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Bandido

<div>
  <img src='./docs/Images/Bandido_1.png' />
  <img src='./docs/Images/Bandido_2.png' />
  <img src='./docs/Images/Bandido_3.png' />
  <img src='./docs/Images/Bandido_4.png' />
  <img src='./docs/Images/Bandido_5.png' />
  <img src='./docs/Images/Bandido_6.png' />
  <img src='./docs/Images/Bandido_7.png' />
</div>

**Stats**

| **Health**        | Below Normal                                                                     |
| :---------------- | :------------------------------------------------------------------------------- |
| **AI**            | Cowboy                                                                           |
| **Infinite Ammo** | True                                                                             |
| **Factions**      | [Bandido][fbandido], [Cowboy][fcowboy].                                          |
| **Abilities**     | Spawns an ammo stash on death that shoots 500 stray bullets in random direction. |

**Weapons**

| Gears                           | Powerup |
| :------------------------------ | :------ |
| ![machete] ![revolver]          |         |
| ![knife] ![carbine] ![revolver] |         |
| ![knife] ![shotgun] ![pistol]   |         |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

<!-- GIF: show ammo stash and stray bullets -->

### Biker

<div>
  <img src='./docs/Images/Biker_1.png' />
  <img src='./docs/Images/Biker_2.png' />
  <img src='./docs/Images/Biker_3.png' />
  <img src='./docs/Images/Biker_4.png' />
  <img src='./docs/Images/Biker_5.png' />
  <img src='./docs/Images/Biker_6.png' />
  <img src='./docs/Images/Biker_7.png' />
  <img src='./docs/Images/Biker_8.png' />
  <img src='./docs/Images/Biker_9.png' />
  <img src='./docs/Images/Biker_10.png' />
  <img src='./docs/Images/Biker_11.png' />
  <img src='./docs/Images/Biker_12.png' />
</div>

**Stats**

| **Health**    | Below Normal                                                                                                                                                                                            |
| :------------ | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **AI**        | Grunt                                                                                                                                                                                                   |
| **Weapons**   | ![leadpipe] ![chain] ![knife]                                                                                                                                                                           |
| **Factions**  | [Biker][fbiker], [Punk][fpunk], [Thug][fthug].                                                                                                                                                          |
| **Abilities** | <li>Has a gather spot, the gather spot is changed every 42 seconds.</li><li>Gains 4 health after dealing melee damage.</li><li>Has 3% chance of stealing an enemy's weapon after a successful hit.</li> |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

<!-- GIF: show gather spot -->

### Biker Hulk

Same profiles as [Biker].

**Stats**

| **Health**    | Strong                                  |
| :------------ | :-------------------------------------- |
| **Speed**     | Slow                                    |
| **AI**        | Hulk                                    |
| **Factions**  | [Biker][fbiker], [Stripper][fstripper]. |
| **Abilities** | Same as [Biker].                        |

**Weapons**

| Gears   | Powerup    |
| :------ | :--------- |
| ![fist] | [Breaking] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Very Low     |
| **Melee Damage Dealt**      | Above Normal |
| **Melee Force**             | Strong       |
| **Size**                    | Very Big     |

</details>

### Bodyguard

<div>
  <img src='./docs/Images/Bodyguard.png' />
</div>

**Stats**

| **Health**   | Below Normal                                |
| :----------- | :------------------------------------------ |
| **AI**       | Grunt                                       |
| **Weapons**  | ![pistol]                                   |
| **Factions** | [Stripper][fstripper], [Kingpin][fkingpin]. |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Bodyguard 2

Same as [Bodyguard] except for the following stats:

**Stats**

| **Weapons**  | ![tommygun]         |
| :----------- | :------------------ |
| **Factions** | [Kingpin][fkingpin] |

### Clown Bodyguard

<div>
  <img src='./docs/Images/ClownBodyguard_1.png' />
  <img src='./docs/Images/ClownBodyguard_2.png' />
  <img src='./docs/Images/ClownBodyguard_3.png' />
  <img src='./docs/Images/ClownBodyguard_4.png' />
</div>

**Stats**

| **Health**   | Below Normal                     |
| :----------- | :------------------------------- |
| **AI**       | Grunt                            |
| **Weapons**  | ![Katana] ![Knife] ![Axe] ![Bat] |
| **Factions** | [Clown][fclown]                  |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Clown Boxer

<div>
  <img src='./docs/Images/ClownBoxer.png' />
</div>

**Stats**

| **Health**   | Above Normal    |
| :----------- | :-------------- |
| **AI**       | Hulk            |
| **Factions** | [Clown][fclown] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Very Low     |
| **Melee Damage Dealt**      | Above Normal |
| **Melee Force**             | Strong       |
| **Size**                    | Very Big     |

</details>

### Clown Cowboy

<div>
  <img src='./docs/Images/ClownCowboy.png' />
</div>

**Stats**

| **Health**    | Below Normal                                                                                                                                                                   |
| :------------ | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Speed**     | Above Normal                                                                                                                                                                   |
| **AI**        | Cowboy                                                                                                                                                                         |
| **Factions**  | [Clown][fclown]                                                                                                                                                                |
| **Abilities** | <li>Has 15% chance of disarming the enemy's weapon after a successful shot.</li><li>Has 1% chance of disarming and destroying the enemy's weapon after a successful shot.</li> |

**Weapons**

| Gears       | Powerup |
| :---------- | :------ |
| ![Revolver] | [Blast] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Above Normal |
| **Melee Damage Dealt**      | Fairly Low   |
| **Size**                    | Small        |

</details>

<!-- GIF: show cowboy disarming -->

### Clown Gangster

<div>
  <img src='./docs/Images/ClownGangster.png' />
</div>

**Stats**

| **Health**   | Below Normal    |
| :----------- | :-------------- |
| **AI**       | Grunt           |
| **Factions** | [Clown][fclown] |

**Weapons**

| Gears       | Powerup |
| :---------- | :------ |
| ![TommyGun] | [Blast] |
| ![Shotgun]  | [Blast] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Cowboy

<div>
  <img src='./docs/Images/Cowboy_1.png' />
  <img src='./docs/Images/Cowboy_2.png' />
  <img src='./docs/Images/Cowboy_3.png' />
  <img src='./docs/Images/Cowboy_4.png' />
  <img src='./docs/Images/Cowboy_5.png' />
  <img src='./docs/Images/Cowboy_6.png' />
  <img src='./docs/Images/Cowboy_7.png' />
</div>

**Stats**

| **Health**    | Below Normal                                                                                                                                                                   |
| :------------ | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Speed**     | Above Normal                                                                                                                                                                   |
| **AI**        | Cowboy                                                                                                                                                                         |
| **Weapons**   | ![Sawedoff] ![Shotgun] ![Revolver] ![Magnum]                                                                                                                                   |
| **Factions**  | [Cowboy][fcowboy]                                                                                                                                                              |
| **Abilities** | <li>Has 15% chance of disarming the enemy's weapon after a successful shot.</li><li>Has 1% chance of disarming and destroying the enemy's weapon after a successful shot.</li> |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Above Normal |
| **Melee Damage Dealt**      | Fairly Low   |
| **Size**                    | Small        |

</details>

<!-- GIF: show cowboy disarming -->

### Cyborg

<div>
  <img src='./docs/Images/Cyborg_1.png' />
  <img src='./docs/Images/Cyborg_2.png' />
  <img src='./docs/Images/Cyborg_3.png' />
  <img src='./docs/Images/Cyborg_4.png' />
  <img src='./docs/Images/Cyborg_5.png' />
  <img src='./docs/Images/Cyborg_6.png' />
</div>

**Stats**

| **Health**          | Below Normal    |
| :------------------ | :-------------- |
| **AI**              | Grunt           |
| **Zombie Immunity** | True            |
| **Factions**        | [Robot][frobot] |

**Weapons**

| Gears            | Powerup  |
| :--------------- | :------- |
| ![MachinePistol] | [Homing] |
| ![Pistol]        | [Homing] |
| ![Pistol45]      | [Homing] |
| ![DarkShotgun]   | [Homing] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

<!-- GIF: show near death effect -->

### Elf

<div>
  <img src='./docs/Images/Elf_1.png' />
  <img src='./docs/Images/Elf_2.png' />
</div>

**Stats**

| **Health**   | Below Normal                                                            |
| :----------- | :---------------------------------------------------------------------- |
| **AI**       | Grunt                                                                   |
| **Weapons**  | ![Knife] ![Chain] ![MP50] ![Shotgun] ![Flamethrower] ![Uzi] ![Flaregun] |
| **Factions** | [Santa][fsanta]                                                         |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Engineer

<div>
  <img src='./docs/Images/Engineer_1.png' />
  <img src='./docs/Images/Engineer_2.png' />
  <img src='./docs/Images/Engineer_3.png' />
  <img src='./docs/Images/Engineer_4.png' />
</div>

**Stats**

| **Health**    | Below Normal                                 |
| :------------ | :------------------------------------------- |
| **AI**        | Grunt                                        |
| **Factions**  | [Engineer][fengineer]                        |
| **Abilities** | Builds an automatic turret every 12 seconds. |

**Weapons**

| Gears                  | Powerup |
| :--------------------- | :------ |
| ![LeadPipe]            |         |
| ![Pipe]                |         |
| ![Hammer]              |         |
| ![LeadPipe] ![Shotgun] |         |
| ![Pipe] ![Pistol]      |         |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Normal       |

</details>

<!-- GIF: show automatic turret -->

### Farmer

<div>
  <img src='./docs/Images/Farmer_1.png' />
  <img src='./docs/Images/Farmer_2.png' />
  <img src='./docs/Images/Farmer_3.png' />
</div>

**Stats**

| **Health**    | Below Normal                                                                                                                                            |
| :------------ | :------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **AI**        | Grunt                                                                                                                                                   |
| **Weapons**   | ![SawedOff] ![Shotgun]                                                                                                                                  |
| **Factions**  | [Farmer][ffarmer], [Hunter][fhunter].                                                                                                                   |
| **Abilities** | Spawns 6 chickens, each has 1 HP and 2 ATK, the chickens guard and attack any enemy nearby. Has 25% chance of making the enemy fall after pecking them. |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

<!-- GIF: show chicken -->

### Gangster

<div>
  <img src='./docs/Images/Gangster_1.png' />
  <img src='./docs/Images/Gangster_2.png' />
  <img src='./docs/Images/Gangster_3.png' />
  <img src='./docs/Images/Gangster_4.png' />
  <img src='./docs/Images/Gangster_5.png' />
  <img src='./docs/Images/Gangster_6.png' />
  <img src='./docs/Images/Gangster_7.png' />
  <img src='./docs/Images/Gangster_8.png' />
  <img src='./docs/Images/Gangster_9.png' />
  <img src='./docs/Images/Gangster_10.png' />
  <img src='./docs/Images/Gangster_11.png' />
  <img src='./docs/Images/Gangster_12.png' />
</div>

**Stats**

| **Health**    | Below Normal                                                                                                                                                                                                                                                                                                                                                            |
| :------------ | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **AI**        | Grunt                                                                                                                                                                                                                                                                                                                                                                   |
| **Weapons**   | ![Bat] ![Bottle] ![Uzi] ![Pistol] ![Revolver] ![Shotgun] ![Sawedoff] ![MP50]                                                                                                                                                                                                                                                                                            |
| **Factions**  | [Gangster][fgangster]                                                                                                                                                                                                                                                                                                                                                   |
| **Abilities** | <li>Gathers at one spot and setups a camp in each team.</li><li>Camp spawns a new [Gangster] or [Gangster Hulk] every 12 seconds.</li><li>Has 3% chance of spawning a boss ([Kingpin], [Bobby] or [Jo]) intead.</li><li>Gangsters with less than 30 HP flee back to the tent to heal.</li><li>Go out and attack other teams once the tent has 7 members and above.</li> |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

<!-- GIF: show camp spawn gangster and start attacking -->

### Gangster Hulk

<div>
  <img src='./docs/Images/GangsterHulk_1.png' />
  <img src='./docs/Images/GangsterHulk_2.png' />
  <img src='./docs/Images/GangsterHulk_3.png' />
</div>

**Stats**

| **Health**    | Strong                                                             |
| :------------ | :----------------------------------------------------------------- |
| **Speed**     | Slow                                                               |
| **AI**        | Hulk                                                               |
| **Factions**  | [Gangster][fgangster], [Stripper][fstripper], [Kingpin][fkingpin]. |
| **Abilities** | Same as [Gangster].                                                |

**Weapons**

| Gears   | Powerup    |
| :------ | :--------- |
| ![fist] | [Breaking] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Very Low     |
| **Melee Damage Dealt**      | Above Normal |
| **Melee Force**             | Strong       |
| **Size**                    | Very Big     |

</details>

### Gardener

<div>
  <img src='./docs/Images/Gardener_1.png' />
  <img src='./docs/Images/Gardener_2.png' />
</div>

**Stats**

| **Health**   | Below Normal       |
| :----------- | :----------------- |
| **AI**       | Grunt              |
| **Weapons**  | ![teapot] ![Knife] |
| **Factions** | [Farmer][ffarmer]  |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Hunter

<div>
  <img src='./docs/Images/Hunter_1.png' />
  <img src='./docs/Images/Hunter_2.png' />
</div>

**Stats**

| **Health**        | Weak                                  |
| :---------------- | :------------------------------------ |
| **Speed**         | Slow                                  |
| **AI**            | Hunter                                |
| **Infinite Ammo** | True                                  |
| **Search Items**  | Health                                |
| **Factions**      | [Hunter][fhunter], [Farmer][ffarmer]. |
| **Abilities**     | Prioritize hunting bears over humans. |

**Weapons**

| Gears              | Powerup |
| :----------------- | :------ |
| ![Knife] ![bowwpn] | [Bow]   |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                            | Value        |
| :------------------------------- | :----------- |
| **Projectile Damage Dealt**      | Fairly High  |
| **Projectile Crit Chance Dealt** | Fairly High  |
| **Melee Damage Dealt**           | Fairly Low   |
| **Size**                         | Below Normal |

</details>

### Lumberjack

<div>
  <img src='./docs/Images/Lumberjack_1.png' />
  <img src='./docs/Images/Lumberjack_2.png' />
</div>

**Stats**

| **Health**   | Strong            |
| :----------- | :---------------- |
| **Speed**    | Slow              |
| **AI**       | Hulk              |
| **Factions** | [Farmer][ffarmer] |

**Weapons**

| Gears                   | Powerup |
| :---------------------- | :------ |
| ![Chainsaw]             |         |
| ![Axe] ![StrengthBoost] | [Gib]   |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Very Low     |
| **Melee Damage Dealt**      | Above Normal |
| **Melee Force**             | Strong       |
| **Size**                    | Very Big     |

</details>

### MetroCop

<div>
  <img src='./docs/Images/MetroCop_1.png' />
  <img src='./docs/Images/MetroCop_2.png' />
  <img src='./docs/Images/MetroCop_3.png' />
</div>

**Stats**

| **Health**    | Below Normal                               |
| :------------ | :----------------------------------------- |
| **AI**        | Grunt                                      |
| **Factions**  | [MetroCop][fmetrocop]                      |
| **Abilities** | Spawns up to 2 LaserSweepers in each team. |

**Weapons**

| Gears                                 | Powerup |
| :------------------------------------ | :------ |
| ![ShockBaton] ![SMG] ![Lazer]         |         |
| ![ShockBaton] ![DarkShotgun] ![Lazer] |         |
| ![Assault] ![Lazer]                   |         |
| ![DarkShotgun] ![Lazer]               |         |
| ![SMG] ![Lazer]                       |         |
| ![ShockBaton] ![Lazer]                |         |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

<!-- GIF: show laser sweeper -->

### Mutant

<div>
  <img src='./docs/Images/Mutant_1.png' />
  <img src='./docs/Images/Mutant_2.png' />
  <img src='./docs/Images/Mutant_3.png' />
  <img src='./docs/Images/Mutant_4.png' />
  <img src='./docs/Images/Mutant_5.png' />
  <img src='./docs/Images/Mutant_6.png' />
  <img src='./docs/Images/Mutant_7.png' />
  <img src='./docs/Images/Mutant_8.png' />
</div>

**Stats**

| **Health**    | Strong                                                                         |
| :------------ | :----------------------------------------------------------------------------- |
| **AI**        | Grunt                                                                          |
| **Factions**  | [Mutant][fmutant]                                                              |
| **Abilities** | Split into 2 twins on dealth once. Each twin has 50% health, deals 50% damage. |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                   | Value                |
| :---------------------- | :------------------- |
| **Impact Damage Taken** | Extremely Vulnerable |
| **Size**                | Big                  |

</details>

<!-- GIF: split when on dealth -->

### Nazi Hulk

<div>
  <img src='./docs/Images/NaziHulk.png' />
</div>

**Stats**

| **Health**   | Strong        |
| :----------- | :------------ |
| **Speed**    | Slow          |
| **AI**       | Hulk          |
| **Factions** | [Nazi][fnazi] |

**Weapons**

| Gears             | Powerup    |
| :---------------- | :--------- |
| ![fist] ![Pistol] | [Breaking] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Very Low     |
| **Melee Damage Dealt**      | Above Normal |
| **Melee Force**             | Strong       |
| **Size**                    | Very Big     |

</details>

### Nazi Soldier

<div>
  <img src='./docs/Images/NaziSoldier_1.png' />
  <img src='./docs/Images/NaziSoldier_2.png' />
  <img src='./docs/Images/NaziSoldier_3.png' />
</div>

**Stats**

| **Health**   | Below Normal  |
| :----------- | :------------ |
| **AI**       | Grunt         |
| **Factions** | [Nazi][fnazi] |

**Weapons**

| Gears                       | Powerup |
| :-------------------------- | :------ |
| ![MP50]                     |         |
| ![MP50] ![Grenade]          |         |
| ![Knife] ![MP50] ![Grenade] |         |
| ![Carbine]                  |         |
| ![Knife] ![Carbine]         |         |
| ![Carbine] ![Grenade]       |         |
| ![Pistol]                   |         |
| ![Knife] ![Pistol]          |         |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Police

<div>
  <img src='./docs/Images/PoliceOfficer_1.png' />
  <img src='./docs/Images/PoliceOfficer_2.png' />
  <img src='./docs/Images/PoliceOfficer_3.png' />
  <img src='./docs/Images/PoliceOfficer_4.png' />
  <img src='./docs/Images/PoliceOfficer_5.png' />
  <img src='./docs/Images/PoliceOfficer_6.png' />
  <img src='./docs/Images/PoliceOfficer_7.png' />
</div>

**Stats**

| **Health**   | Below Normal                                   |
| :----------- | :--------------------------------------------- |
| **AI**       | Grunt                                          |
| **Factions** | [Police][fpolice], [Police SWAT][fpoliceswat]. |

**Weapons**

| Gears                | Powerup |
| :------------------- | :------ |
| ![Baton]             |         |
| ![Baton] ![Pistol]   | [Taser] |
| ![Baton] ![Shotgun]  |         |
| ![Baton] ![Revolver] | [Taser] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Police SWAT

<div>
  <img src='./docs/Images/SWAT_1.png' />
  <img src='./docs/Images/SWAT_2.png' />
</div>

**Stats**

| **Health**   | Below Normal                                   |
| :----------- | :--------------------------------------------- |
| **AI**       | Grunt                                          |
| **Factions** | [Police][fpolice], [Police SWAT][fpoliceswat]. |

**Weapons**

| Gears                                | Powerup   |
| :----------------------------------- | :-------- |
| ![Knife] ![Pistol45] ![C4]           | [Termite] |
| ![Knife] ![MachinePistol] ![Grenade] | [Termite] |
| ![Knife] ![Assault]                  | [Termite] |
| ![Knife] ![SMG]                      | [Termite] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Lab Assistant

<div>
  <img src='./docs/Images/LabAssistant_1.png' />
  <img src='./docs/Images/LabAssistant_2.png' />
</div>

**Stats**

| **Health**   | Below Normal            |
| :----------- | :---------------------- |
| **AI**       | Grunt                   |
| **Factions** | [Scientist][fscientist] |

**Weapons**

| Gears                      | Powerup            |
| :------------------------- | :----------------- |
| ![Pistol]                  | [Precise Bouncing] |
| ![Pistol] ![StrengthBoost] | [Precise Bouncing] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Scientist

<div>
  <img src='./docs/Images/Scientist_1.png' />
  <img src='./docs/Images/Scientist_2.png' />
</div>

**Stats**

| **Health**   | Below Normal            |
| :----------- | :---------------------- |
| **AI**       | Grunt                   |
| **Factions** | [Scientist][fscientist] |

**Weapons**

| Gears       | Powerup            |
| :---------- | :----------------- |
| ![Revolver] | [Precise Bouncing] |
| ![Pistol]   | [Precise Bouncing] |
| ![Pistol45] | [Precise Bouncing] |
| ![LeadPipe] |                    |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Survivor

<div>
  <img src='./docs/Images/Survivor_1.png' />
  <img src='./docs/Images/Survivor_2.png' />
  <img src='./docs/Images/Survivor_3.png' />
  <img src='./docs/Images/Survivor_4.png' />
  <img src='./docs/Images/Survivor_5.png' />
  <img src='./docs/Images/Survivor_6.png' />
</div>

**Stats**

| **Health**    | Weak                                                           |
| :------------ | :------------------------------------------------------------- |
| **AI**        | Normal                                                         |
| **Weapons**   | ![SMG] ![Knife] ![Machete] ![SawedOff] ![Revolver] ![LeadPipe] |
| **Factions**  | [Survivor][fsurvivor]                                          |
| **Abilities** | Turns into zombie on dealth due to infection.                  |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Sniper

<div>
  <img src='./docs/Images/Sniper_1.png' />
  <img src='./docs/Images/Sniper_2.png' />
</div>

**Stats**

| **Health**       | Weak                                    |
| :--------------- | :-------------------------------------- |
| **Speed**        | Slow                                    |
| **AI**           | Sniper                                  |
| **Search Items** | Primary                                 |
| **Factions**     | [Soldier][fsoldier], [Sniper][fsniper]. |

**Weapons**

| Gears                       | Powerup |
| :-------------------------- | :------ |
| ![Knife] ![Sniper]          |         |
| ![SilencedPistol] ![sniper] |         |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                            | Value        |
| :------------------------------- | :----------- |
| **Projectile Damage Dealt**      | Fairly High  |
| **Projectile Crit Chance Dealt** | Fairly High  |
| **Melee Damage Dealt**           | Fairly Low   |
| **Size**                         | Below Normal |

</details>

### Soldier

<div>
  <img src='./docs/Images/Soldier_1.png' />
  <img src='./docs/Images/Soldier_2.png' />
  <img src='./docs/Images/Soldier_3.png' />
  <img src='./docs/Images/Soldier_4.png' />
  <img src='./docs/Images/Soldier_5.png' />
  <img src='./docs/Images/Soldier_6.png' />
  <img src='./docs/Images/Soldier_7.png' />
  <img src='./docs/Images/Soldier_8.png' />
</div>

**Stats**

| **Health**       | Below Normal        |
| :--------------- | :------------------ |
| **AI**           | Soldier             |
| **Search Items** | Primary             |
| **Factions**     | [Soldier][fsoldier] |

**Weapons**

| Gears                | Powerup       |
| :------------------- | :------------ |
| ![Pistol]            | [Penetration] |
| ![Shotgun] ![Pistol] | [Penetration] |
| ![Assault] ![Pistol] | [Penetration] |
| ![SMG] ![Pistol]     | [Penetration] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Spacer

<div>
  <img src='./docs/Images/Spacer_1.png' />
  <img src='./docs/Images/Spacer_2.png' />
  <img src='./docs/Images/Spacer_3.png' />
  <img src='./docs/Images/Spacer_4.png' />
  <img src='./docs/Images/Spacer_5.png' />
  <img src='./docs/Images/Spacer_6.png' />
  <img src='./docs/Images/Spacer_7.png' />
  <img src='./docs/Images/Spacer_8.png' />
</div>

**Stats**

| **Health**   | Below Normal      |
| :----------- | :---------------- |
| **AI**       | Grunt             |
| **Factions** | [Spacer][fspacer] |

**Weapons**

| Gears                     | Powerup |
| :------------------------ | :------ |
| ![ShockBaton]             |         |
| ![LeadPipe]               |         |
| ![Assault] ![Lazer]       | [Gauss] |
| ![Pistol] ![Lazer]        | [Gauss] |
| ![MachinePistol] ![Lazer] | [Gauss] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Space Sniper

<div>
  <img src='./docs/Images/SpaceSniper_1.png' />
  <img src='./docs/Images/SpaceSniper_2.png' />
  <img src='./docs/Images/SpaceSniper_3.png' />
</div>

**Stats**

| **Health**       | Weak                                             |
| :--------------- | :----------------------------------------------- |
| **Speed**        | Slow                                             |
| **AI**           | Sniper                                           |
| **Search Items** | Primary                                          |
| **Factions**     | [Space Sniper][fspacesniper], [Spacer][fspacer]. |

**Weapons**

| Gears               | Powerup |
| :------------------ | :------ |
| ![Sniper]           | [Gauss] |
| ![sniper] ![Pistol] | [Gauss] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                            | Value        |
| :------------------------------- | :----------- |
| **Projectile Damage Dealt**      | Normal       |
| **Projectile Crit Chance Dealt** | Normal       |
| **Melee Damage Dealt**           | Fairly Low   |
| **Size**                         | Below Normal |

</details>

### Stripper

<div>
  <img src='./docs/Images/Stripper_1.png' />
  <img src='./docs/Images/Stripper_2.png' />
  <img src='./docs/Images/Stripper_3.png' />
  <img src='./docs/Images/Stripper_4.png' />
  <img src='./docs/Images/Stripper_5.png' />
  <img src='./docs/Images/Stripper_6.png' />
  <img src='./docs/Images/Stripper_7.png' />
  <img src='./docs/Images/Stripper_8.png' />
  <img src='./docs/Images/Stripper_9.png' />
</div>

**Stats**

| **Health**        | Weak                                                      |
| :---------------- | :-------------------------------------------------------- |
| **Speed**         | Above Normal                                              |
| **AI**            | Hard                                                      |
| **Infinite Ammo** | True                                                      |
| **Search Items**  | Makeshift, Health                                         |
| **Factions**      | [Stripper][fstripper]                                     |
| **Abilities**     | Recruits one of the hulk bots to follow and protect them. |

**Weapons**

| Gears            | Powerup   |
| :--------------- | :-------- |
| ![MachinePistol] | [Tearing] |
| ![Revolver]      | [Tearing] |
| ![Pistol]        | [Tearing] |
| ![Sawedoff]      | [Tearing] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                            | Value        |
| :------------------------------- | :----------- |
| **Projectile Damage Dealt**      | Fairly High  |
| **Projectile Crit Chance Dealt** | Fairly High  |
| **Melee Damage Dealt**           | Fairly Low   |
| **Fire Damage Taken**            | Vulnerable   |
| **Size**                         | Below Normal |

</details>

### Thug

<div>
  <img src='./docs/Images/Thug_1.png' />
  <img src='./docs/Images/Thug_2.png' />
  <img src='./docs/Images/Thug_3.png' />
  <img src='./docs/Images/Thug_4.png' />
  <img src='./docs/Images/Thug_5.png' />
  <img src='./docs/Images/Thug_6.png' />
  <img src='./docs/Images/Thug_7.png' />
  <img src='./docs/Images/Thug_8.png' />
  <img src='./docs/Images/Thug_9.png' />
  <img src='./docs/Images/Thug_10.png' />
  <img src='./docs/Images/Thug_11.png' />
  <img src='./docs/Images/Thug_12.png' />
  <img src='./docs/Images/Thug_13.png' />
  <img src='./docs/Images/Thug_14.png' />
  <img src='./docs/Images/Thug_15.png' />
  <img src='./docs/Images/Thug_16.png' />
  <img src='./docs/Images/Thug_17.png' />
  <img src='./docs/Images/Thug_18.png' />
</div>

**Stats**

| **Health**       | Below Normal                                                                                                                        |
| :--------------- | :---------------------------------------------------------------------------------------------------------------------------------- |
| **AI**           | Grunt                                                                                                                               |
| **Search Items** | Primary, Secondary, Melee                                                                                                           |
| **Weapons**      | ![Bat] ![LeadPipe] ![Hammer] ![Chain] ![MachinePistol]                                                                              |
| **Factions**     | [Thug][fthug]                                                                                                                       |
| **Abilities**    | <li>x3 damage to objects.</li><li>Targets and loots objects if not in danger.</li><li>Discovers new weapons in looted objects.</li> |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Thug Hulk

<div>
  <img src='./docs/Images/ThugHulk_1.png' />
  <img src='./docs/Images/ThugHulk_2.png' />
  <img src='./docs/Images/ThugHulk_3.png' />
</div>

**Stats**

| **Health**    | Strong                                                       |
| :------------ | :----------------------------------------------------------- |
| **Speed**     | Slow                                                         |
| **AI**        | Hulk                                                         |
| **Factions**  | [Gangster][fgangster], [Stripper][fstripper], [Thug][fthug]. |
| **Abilities** | Same as [Thug].                                              |

**Weapons**

| Gears       | Powerup    |
| :---------- | :--------- |
| ![fist]     | [Breaking] |
| ![LeadPipe] | [Breaking] |
| ![Pipe]     | [Breaking] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Very Low     |
| **Melee Damage Dealt**      | Above Normal |
| **Melee Force**             | Strong       |
| **Size**                    | Very Big     |

</details>

### Punk

<div>
  <img src='./docs/Images/Punk_1.png' />
  <img src='./docs/Images/Punk_2.png' />
  <img src='./docs/Images/Punk_3.png' />
  <img src='./docs/Images/Punk_4.png' />
  <img src='./docs/Images/Punk_5.png' />
  <img src='./docs/Images/Punk_6.png' />
  <img src='./docs/Images/Punk_7.png' />
  <img src='./docs/Images/Punk_8.png' />
  <img src='./docs/Images/Punk_9.png' />
  <img src='./docs/Images/Punk_10.png' />
  <img src='./docs/Images/Punk_11.png' />
  <img src='./docs/Images/Punk_12.png' />
  <img src='./docs/Images/Punk_13.png' />
  <img src='./docs/Images/Punk_14.png' />
</div>

**Stats**

| **Health**        | Below Normal  |
| :---------------- | :------------ |
| **AI**            | Grunt         |
| **Infinite Ammo** | True          |
| **Factions**      | [Punk][fpunk] |

**Weapons**

| Gears            | Powerup     |
| :--------------- | :---------- |
| ![Bat] ![Pistol] | [Knockback] |
| ![Knife] ![Uzi]  | [Knockback] |
| ![LeadPipe]      |             |
| ![Baseball]      |             |
| ![Pistol45]      | [Knockback] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Below Normal |
| **Melee Damage Dealt**      | Below Normal |
| **Size**                    | Below Normal |

</details>

### Punk Hulk

<div>
  <img src='./docs/Images/PunkHulk_1.png' />
  <img src='./docs/Images/PunkHulk_2.png' />
  <img src='./docs/Images/PunkHulk_3.png' />
  <img src='./docs/Images/PunkHulk_4.png' />
  <img src='./docs/Images/PunkHulk_5.png' />
</div>

**Stats**

| **Health**   | Strong                                |
| :----------- | :------------------------------------ |
| **Speed**    | Slow                                  |
| **AI**       | Hulk                                  |
| **Factions** | [Stripper][fstripper], [Punk][fpunk]. |

**Weapons**

| Gears       | Powerup    |
| :---------- | :--------- |
| ![fist]     | [Breaking] |
| ![LeadPipe] | [Breaking] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Dealt** | Very Low     |
| **Melee Damage Dealt**      | Above Normal |
| **Melee Force**             | Strong       |
| **Size**                    | Very Big     |

</details>

### Zombie

A basic common zombie.

<div>
  <img src='./docs/Images/Zombie_1.png' />
  <img src='./docs/Images/Zombie_2.png' />
</div>

**Stats**

| **Health**    | Weak                                                                                                                                                         |
| :------------ | :----------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Run Speed** | Slow                                                                                                                                                         |
| **AI**        | ZombieSlow                                                                                                                                                   |
| **Factions**  | [Zombie][fzombie], [Zombie Mutated][fzombiemutated], [Zombie Boss][fzombieboss].                                                                             |
| **Abilities** | <li>Infects the enemies after a successful punch.</li><li>Infected players turn into zombie on dealth. The only exception is when their body are burnt.</li> |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value        |
| :--------------------- | :----------- |
| **Melee Damage Dealt** | Low          |
| **Size**               | Below Normal |

</details>

### Zombie Agent

<div>
  <img src='./docs/Images/ZombieAgent.png' />
</div>

A common zombie which is the same as [Zombie] except for the following stats:

| **Weapons** | ![Pistol] ![SilencedPistol] ![SilencedUzi] |
| :---------- | :----------------------------------------- |

### Zombie Bruiser

<div>
  <img src='./docs/Images/ZombieBruiser.png' />
</div>

A mutated zombie.

**Stats**

| **Health**    | Above Normal                                                                     |
| :------------ | :------------------------------------------------------------------------------- |
| **Speed**     | Slow                                                                             |
| **AI**        | ZombieHulk                                                                       |
| **Factions**  | [Zombie][fzombie], [Zombie Mutated][fzombiemutated], [Zombie Boss][fzombieboss]. |
| **Abilities** | Same as [Zombie].                                                                |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value         |
| :--------------------- | :------------ |
| **Melee Damage Dealt** | Above Normal  |
| **Melee Force**        | Strong        |
| **Size**               | Extremely Big |

</details>

### Zombie Child

<div>
  <img src='./docs/Images/ZombieChild_1.png' />
  <img src='./docs/Images/ZombieChild_2.png' />
</div>

A mutated zombie.

**Stats**

| **Health**    | Extremely Weak                                                                   |
| :------------ | :------------------------------------------------------------------------------- |
| **Speed**     | Fast                                                                             |
| **AI**        | ZombieFast                                                                       |
| **Factions**  | [Zombie][fzombie], [Zombie Mutated][fzombiemutated], [Zombie Boss][fzombieboss]. |
| **Abilities** | Same as [Zombie].                                                                |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value      |
| :--------------------- | :--------- |
| **Melee Damage Dealt** | Low        |
| **Melee Force**        | Weak       |
| **Size**               | Very Small |

</details>

### Zombie Fat

<div>
  <img src='./docs/Images/ZombieFat.png' />
</div>

A mutated zombie.

**Stats**

| **Health**    | Embarrassingly Weak                                                                                                    |
| :------------ | :--------------------------------------------------------------------------------------------------------------------- |
| **Speed**     | Barely Any                                                                                                             |
| **AI**        | ZombieSlow                                                                                                             |
| **Factions**  | [Zombie][fzombie], [Zombie Mutated][fzombiemutated], [Zombie Boss][fzombieboss].                                       |
| **Abilities** | Same as [Zombie]. On its dealth, the body is exploded into pieces and infects all players inside the explosion radius. |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value       |
| :--------------------- | :---------- |
| **Melee Damage Dealt** | Fairly High |
| **Size**               | Chonky      |

</details>

### Zombie Flamer

<div>
  <img src='./docs/Images/ZombieFlamer.png' />
</div>

A mutated zombie.

**Stats**

| **Health**    | Extremely Weak                                                                   |
| :------------ | :------------------------------------------------------------------------------- |
| **Speed**     | Fast                                                                             |
| **AI**        | ZombieFast                                                                       |
| **Factions**  | [Zombie][fzombie], [Zombie Mutated][fzombiemutated], [Zombie Boss][fzombieboss]. |
| **Abilities** | Same as [Zombie]. Is set on max fire on startup.                                 |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value           |
| :--------------------- | :-------------- |
| **Fire Damage Taken**  | Ultra Resistant |
| **Melee Damage Dealt** | Very Low        |
| **Size**               | Below Normal    |

</details>

### Zombie Gangster

<div>
  <img src='./docs/Images/ZombieGangster_1.png' />
  <img src='./docs/Images/ZombieGangster_2.png' />
  <img src='./docs/Images/ZombieGangster_3.png' />
  <img src='./docs/Images/ZombieGangster_4.png' />
  <img src='./docs/Images/ZombieGangster_5.png' />
</div>

A common zombie which is the same as [Zombie] except for the following stats:

| **Weapons** | ![TommyGun] ![Shotgun] ![Revolver] ![Pistol] |
| :---------- | :------------------------------------------- |

### Zombie Ninja

<div>
  <img src='./docs/Images/ZombieNinja.png' />
</div>

A common zombie which is the same as [Zombie] except for the following stats:

| **Weapons** | ![Katana] |
| :---------- | :-------- |

### Zombie Police

<div>
  <img src='./docs/Images/ZombiePolice_1.png' />
  <img src='./docs/Images/ZombiePolice_2.png' />
</div>

A common zombie which is the same as [Zombie] except for the following stats:

| **Weapons** | ![Baton] ![Revolver] |
| :---------- | :------------------- |

### Zombie Soldier

<div>
  <img src='./docs/Images/ZombieSoldier_1.png' />
  <img src='./docs/Images/ZombieSoldier_2.png' />
</div>

A common zombie which is the same as [Zombie] except for the following stats:

| **Weapons** | ![SMG] ![Assault] ![Shotgun] ![Grenade] ![MineWpn] |
| :---------- | :------------------------------------------------- |

### Zombie Thug

<div>
  <img src='./docs/Images/ZombieThug_1.png' />
  <img src='./docs/Images/ZombieThug_2.png' />
</div>

A common zombie which is the same as [Zombie] except for the following stats:

| **Weapons** | ![Bat] ![Knife] ![Pistol] ![Molotov] |
| :---------- | :----------------------------------- |

### Zombie Worker

<div>
  <img src='./docs/Images/ZombieWorker.png' />
</div>

A common zombie which is the same as [Zombie] except for the following stats:

| **Weapons** | ![Pipe] ![Hammer] ![Axe] ![Chainsaw] |
| :---------- | :----------------------------------- |

## Bosses

They are bots that are a bit harder to beat.

### Agent 79

<div>
  <img src='./docs/Images/Agent79.png' />
</div>

**Stats**

| **Health**        | Strong             |
| :---------------- | :----------------- |
| **AI**            | Expert             |
| **Infinite Ammo** | True               |
| **Search Items**  | Secondary, Health. |
| **Factions**      | [Agent][fagent]    |

**Weapons**

| Gears                                           | Powerup  |
| :---------------------------------------------- | :------- |
| ![Knife] ![Bazooka] ![Pistol] ![C4] ![Slowmo10] | [Object] |

### Amos

<div>
  <img src='./docs/Images/Amos.png' />
</div>

**Stats**

| **Health**       | Very Strong       |
| :--------------- | :---------------- |
| **Speed**        | Below Normal      |
| **AI**           | Hard              |
| **Search Items** | Primary           |
| **Factions**     | [Spacer][fspacer] |

**Weapons**

| Gears                    | Powerup |
| :----------------------- | :------ |
| ![DarkShotgun] ![Pistol] | [Gauss] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                   | Value            |
| :---------------------- | :--------------- |
| **Melee Damage Dealt**  | High             |
| **Impact Damage Taken** | Fairly Resistant |
| **Melee Force**         | Above Normal     |
| **Size**                | Big              |

</details>

### Ass Kicker

<img src='./docs/Images/AssKicker.png' />

**Stats**

| **Health**        | Strong                          |
| :---------------- | :------------------------------ |
| **Speed**         | Extremely Fast                  |
| **AI**            | AssKicker                       |
| **Infinite Ammo** | True                            |
| **Search Items**  | Melee                           |
| **Weapons**       | ![Baton]                        |
| **Factions**      | [Assassin][fassassin]           |
| **Abilities**     | Kicks the enemy into the space. |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value        |
| :--------------------- | :----------- |
| **Melee Damage Dealt** | Above Normal |
| **Energy Recharge**    | Quick        |
| **Melee Force**        | One Punch    |
| **Size**               | Big          |

</details>

### Balista

<img src='./docs/Images/Balista.png' />

**Stats**

| **Health**        | Very Strong                                                                                     |
| :---------------- | :---------------------------------------------------------------------------------------------- |
| **Speed**         | Fast                                                                                            |
| **AI**            | Hard                                                                                            |
| **Infinite Ammo** | True                                                                                            |
| **Search Items**  | All                                                                                             |
| **Factions**      | [Punk][fpunk]                                                                                   |
| **Abilities**     | Can equip 2 primary weapons: **Assault Rifle** and **Grenade Launcher** with [Spinner] powerup. |

**Weapons**

| Gears                                   | Powerup   |
| :-------------------------------------- | :-------- |
| ![Chain] ![Assault] ![Uzi] ![GLauncher] | [Spinner] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value        |
| :--------------------- | :----------- |
| **Melee Damage Dealt** | Fairly Low   |
| **Melee Force**        | Above Normal |
| **Size**               | Above Normal |

</details>

### Balloonatic

<img src='./docs/Images/Balloonatic.png' />

**Stats**

| **Health**        | Very Strong                 |
| :---------------- | :-------------------------- |
| **AI**            | Hard                        |
| **Infinite Ammo** | True                        |
| **Search Items**  | Primary, Health.            |
| **Factions**      | [Clown][fclown]             |
| **Abilities**     | Has balloons for no reason. |

**Weapons**

| Gears                  | Powerup  |
| :--------------------- | :------- |
| ![Knife] ![SMG] ![Uzi] | [Helium] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                   | Value          |
| :---------------------- | :------------- |
| **Impact Damage Taken** | Very Resistant |
| **Size**                | Very Big       |

</details>

### Bazooka Jane

<img src='./docs/Images/BazookaJane.png' />

**Stats**

| **Health**       | Strong                                             |
| :--------------- | :------------------------------------------------- |
| **Speed**        | Slow                                               |
| **AI**           | Hard                                               |
| **Search Items** | Health, Streetsweeper, Melee, Powerups, Secondary. |
| **Factions**     | [Soldier][fsoldier]                                |

**Weapons**

| Gears                                     | Powerup           |
| :---------------------------------------- | :---------------- |
| ![Knife] ![Bazooka] ![Pistol] ![Slowmo10] | [Suicide Fighter] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats    | Value        |
| :------- | :----------- |
| **Size** | Above Normal |

</details>

### Beast

<img src='./docs/Images/Beast.png' />

**Stats**

| **Health**        | Extremely Strong                 |
| :---------------- | :------------------------------- |
| **AI**            | Hard                             |
| **Infinite Ammo** | True                             |
| **Search Items**  | Health, Streetsweeper, Powerups. |
| **Factions**      | [Thug][fthug]                    |

**Weapons**

| Gears                        | Powerup      |
| :--------------------------- | :----------- |
| ![Machete] ![C4] ![Slowmo10] | [Earthquake] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value         |
| :--------------------- | :------------ |
| **Energy Consumption** | 0             |
| **Size**               | Extremely Big |

</details>

### Berserker

<img src='./docs/Images/Berserker.png' />

**Stats**

| **Health**       | Above Normal                                                                                                                                                                      |
| :--------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Speed**        | Hacker                                                                                                                                                                            |
| **AI**           | God                                                                                                                                                                               |
| **Search Items** | Melee                                                                                                                                                                             |
| **Faction**      | [Mutant][fmutant]                                                                                                                                                                 |
| **Abilities**    | <li>Has extremely high melee damage that can one-hit kill almost all life forms.</li><li>Health is reduced gradually over time.</li><li>Gains 5 HP after executing an enemy.</li> |

**Weapons**

| Gears  | Powerup |
| :----- | :------ |
| ![Axe] | [Gib]   |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value        |
| :--------------------- | :----------- |
| **Melee Damage Dealt** | Ultra High   |
| **Melee Force**        | Ultra Strong |
| **Size**               | Big          |

</details>

### Big Mutant

<div>
  <img src='./docs/Images/Mutant_1.png' />
  <img src='./docs/Images/Mutant_2.png' />
  <img src='./docs/Images/Mutant_3.png' />
  <img src='./docs/Images/Mutant_4.png' />
  <img src='./docs/Images/Mutant_5.png' />
  <img src='./docs/Images/Mutant_6.png' />
  <img src='./docs/Images/Mutant_7.png' />
  <img src='./docs/Images/Mutant_8.png' />
</div>

**Stats**

| **Health**       | Ultra Strong      |
| :--------------- | :---------------- |
| **AI**           | MeleeHard         |
| **Search Items** | Health, Powerups. |
| **Faction**      | [Mutant][fmutant] |

**Weapons**

| Gears  | Powerup     |
| :----- | :---------- |
| ![Axe] | [Splitting] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                   | Value                |
| :---------------------- | :------------------- |
| **Impact Damage Taken** | Extremely Vulnerable |
| **Size**                | Very Big             |

</details>

### Bobby

<div>
  <img src='./docs/Images/Bobby.png' />
</div>

**Stats**

| **Health**        | Strong                                      |
| :---------------- | :------------------------------------------ |
| **Speed**         | Above Normal                                |
| **AI**            | Hard                                        |
| **Infinite Ammo** | True                                        |
| **Search Items**  | Secondary, Health, Streetsweeper, Powerups. |
| **Faction**       | [Thug][fthug]                               |

**Weapons**

| Gears                | Powerup       |
| :------------------- | :------------ |
| ![Knife] ![Shotgun]  | [Scattershot] |
| ![Knife] ![Sawedoff] | [Scattershot] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                       | Value        |
| :-------------------------- | :----------- |
| **Projectile Damage Taken** | Resistant    |
| **Projectile Damage Dealt** | Fairly High  |
| **Size**                    | Above Normal |

</details>

### Boffin

<div>
  <img src='./docs/Images/Boffin.png' />
</div>

**Stats**

| **Health**        | Very Strong                      |
| :---------------- | :------------------------------- |
| **AI**            | Hard                             |
| **Infinite Ammo** | True                             |
| **Search Items**  | Health, Streetsweeper, Powerups. |
| **Faction**       | [Scientist][fscientist]          |
| **Abilities**     | Is immune to shrinking.          |

**Weapons**

| Gears                    | Powerup     |
| :----------------------- | :---------- |
| ![GLauncher] ![Slowmo10] | [Shrinking] |

### Chairman

<div>
  <img src='./docs/Images/Chairman.png' />
</div>

**Stats**

| **Health**        | Very Strong                  |
| :---------------- | :--------------------------- |
| **Run Speed**     | Hacker                       |
| **Sprint Speed**  | Fast                         |
| **AI**            | Melee Expert                 |
| **Infinite Ammo** | True                         |
| **Search Items**  | Makeshift, Health, Powerups. |
| **Faction**       | [Stripper][fstripper]        |

**Weapons**

| Gears                      | Powerup    |
| :------------------------- | :--------- |
| ![Chair] ![C4] ![Slowmo10] | [Pushback] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats           | Value        |
| :-------------- | :----------- |
| **Melee Force** | Strong       |
| **Size**        | Above Normal |

</details>

### Cindy

<div>
  <img src='./docs/Images/Cindy.png' />
</div>

**Stats**

| **Health**        | Above Normal                                |
| :---------------- | :------------------------------------------ |
| **Speed**         | Fast                                        |
| **AI**            | Expert                                      |
| **Infinite Ammo** | True                                        |
| **Search Items**  | Secondary, Streetsweeper, Powerups, Health. |
| **Faction**       | [Police][fpolice]                           |
| **Abilities**     | Equips handguns with [Stun] powerup.        |

**Weapons**

| Gears                    | Powerup |
| :----------------------- | :------ |
| ![ShockBaton] ![Pistol]  | [Stun]  |
| ![ShockBaton] ![Assault] | [Stun]  |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats           | Value        |
| :-------------- | :----------- |
| **Energy**      | High         |
| **Melee Force** | Above Normal |

</details>

### Demolitionist

<div>
  <img src='./docs/Images/Demolitionist.png' />
</div>

**Stats**

| **Health**        | Strong                 |
| :---------------- | :--------------------- |
| **Speed**         | Barely Any             |
| **AI**            | Range Hard             |
| **Infinite Ammo** | True                   |
| **Search Items**  | Primary, Health.       |
| **Weapons**       | ![Sniper] ![GLauncher] |
| **Faction**       | None                   |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                            | Value        |
| :------------------------------- | :----------- |
| **Projectile Damage Dealt**      | Ultra High   |
| **Projectile Crit Chance Dealt** | Ultra High   |
| **Melee Damage Dealt**           | Very High    |
| **Size**                         | Below Normal |

</details>

### Demoman

<div>
  <img src='./docs/Images/Demoman.png' />
</div>

**Stats**

| **Health**        | Very Strong                  |
| :---------------- | :--------------------------- |
| **Speed**         | Below Normal                 |
| **AI**            | Expert                       |
| **Infinite Ammo** | True                         |
| **Search Items**  | Makeshift, Health, Powerups. |
| **Faction**       | [Nazi][fnazi]                |

**Weapons**

| Gears                   | Powerup |
| :---------------------- | :------ |
| ![GLauncher] ![Machete] | [Mine]  |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                      | Value               |
| :------------------------- | :------------------ |
| **Explosion Damage Taken** | Extremely Resistant |
| **Melee Damage Dealt**     | High                |
| **Melee Force**            | Strong              |
| **Size**                   | Big                 |

</details>

### Firebug

<div>
  <img src='./docs/Images/Firebug.png' />
</div>

**Stats**

| **Health**        | Very Strong              |
| :---------------- | :----------------------- |
| **AI**            | Expert                   |
| **Infinite Ammo** | True                     |
| **Search Items**  | Melee, Health, Powerups. |
| **Faction**       | [Punk][fpunk]            |
| **Abilities**     | Is immune to fire.       |

**Weapons**

| Gears                    | Powerup |
| :----------------------- | :------ |
| ![Pipe] ![MachinePistol] | [Fire]  |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                 | Value      |
| :-------------------- | :--------- |
| **Fire Damage Taken** | Unbeatable |
| **Size**              | Big        |

</details>

### Fireman

<div>
  <img src='./docs/Images/Fireman.png' />
</div>

**Stats**

| **Health**        | Strong                    |
| :---------------- | :------------------------ |
| **Speed**         | Fast                      |
| **AI**            | Assassin Melee            |
| **Infinite Ammo** | True                      |
| **Faction**       | [Pyromaniac][fpyromaniac] |

**Weapons**

| Gears             | Powerup      |
| :---------------- | :----------- |
| ![Axe] ![Molotov] | [Fire Trail] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value               |
| :--------------------- | :------------------ |
| **Fire Damage Taken**  | Extremely Resistant |
| **Melee Damage Taken** | Resistant           |

</details>

### Fritzliebe

<div>
  <img src='./docs/Images/Fritzliebe.png' />
</div>

**Stats**

| **Health**        | Very Strong      |
| :---------------- | :--------------- |
| **AI**            | Expert           |
| **Infinite Ammo** | True             |
| **Search Items**  | Primary, Health. |
| **Faction**       | [Nazi][fnazi]    |

**Weapons**

| Gears     | Powerup     |
| :-------- | :---------- |
| ![Magnum] | [Lightning] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats    | Value        |
| :------- | :----------- |
| **Size** | Below Normal |

</details>

### Funnyman

<div>
  <img src='./docs/Images/Funnyman.png' />
</div>

**Stats**

| **Health**       | Strong            |
| :--------------- | :---------------- |
| **AI**           | Expert            |
| **Search Items** | Health, Powerups. |
| **Faction**      | [Clown][fclown]   |

**Weapons**

| Gears               | Powerup   |
| :------------------ | :-------- |
| ![Fist]             | [Megaton] |
| ![Fist] ![TommyGun] | [Megaton] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value        |
| :--------------------- | :----------- |
| **Melee Damage Taken** | Resistant    |
| **Size**               | Above Normal |

</details>

### Jo

<div>
  <img src='./docs/Images/Jo.png' />
</div>

**Stats**

| **Health**       | Extremely Strong   |
| :--------------- | :----------------- |
| **AI**           | Melee Expert       |
| **Search Items** | Makeshift, Health. |
| **Faction**      | [Biker][fbiker]    |

**Weapons**

| Gears                 | Powerup   |
| :-------------------- | :-------- |
| ![Bottle] ![Slowmo10] | [Hurling] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats           | Value  |
| :-------------- | :----- |
| **Melee Force** | Strong |
| **Size**        | Big    |

</details>

### Hacker

<div>
  <img src='./docs/Images/Hacker_1.png' />
  <img src='./docs/Images/Hacker_2.png' />
</div>

**Stats**

| **Health**  | Above Normal |
| :---------- | :----------- |
| **Speed**   | Above Normal |
| **AI**      | Hacker       |
| **Faction** | None         |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                  | Value |
| :--------------------- | :---- |
| **Energy Consumption** | None  |

</details>

### Handler

<div>
  <img src='./docs/Images/Handler.png' />
</div>

**Stats**

| **Health**        | Strong                                |
| :---------------- | :------------------------------------ |
| **Speed**         | Above Normal                          |
| **AI**            | Hard                                  |
| **Infinite Ammo** | True                                  |
| **Search Items**  | Health, Powerups, Primary, Secondary. |
| **Faction**       | [Farmer][ffarmer]                     |

**Weapons**

| Gears                | Powerup        |
| :------------------- | :------------- |
| ![Whip] ![GLauncher] | [Suicide Dove] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats          | Value     |
| :------------- | :-------- |
| **Max Energy** | Very High |

</details>

### Hawkeye

<div>
  <img src='./docs/Images/Hawkeye.png' />
</div>

**Stats**

| **Health**        | Above Normal                     |
| :---------------- | :------------------------------- |
| **AI**            | Range Expert                     |
| **Infinite Ammo** | True                             |
| **Search Items**  | Health, Powerups, Streetsweeper. |
| **Faction**       | [Sniper][fsniper]                |

**Weapons**

| Gears              | Powerup       |
| :----------------- | :------------ |
| ![Sniper] ![Lazer] | [Penetration] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                            | Value     |
| :------------------------------- | :-------- |
| **Projectile Crit Chance Dealt** | Very High |
| **Size**                         | Big       |

</details>

### Heavy Soldier

<div>
  <img src='./docs/Images/HeavySoldier.png' />
</div>

**Stats**

| **Health**        | Above Normal        |
| :---------------- | :------------------ |
| **Speed**         | Slow                |
| **AI**            | Soldier             |
| **Infinite Ammo** | True                |
| **Search Items**  | Primary             |
| **Faction**       | [Soldier][fsoldier] |

**Weapons**

| Gears            | Powerup              |
| :--------------- | :------------------- |
| ![M60] ![Pistol] | [Double Penetration] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats    | Value        |
| :------- | :----------- |
| **Size** | Above Normal |

</details>

### Hitman

<div>
  <img src='./docs/Images/Hitman_1.png' />
  <img src='./docs/Images/Hitman_2.png' />
</div>

**Stats**

| **Health**       | Strong                                                                                                                                         |
| :--------------- | :--------------------------------------------------------------------------------------------------------------------------------------------- |
| **Run Speed**    | Below Normal                                                                                                                                   |
| **Sprint Speed** | Fast                                                                                                                                           |
| **AI**           | Expert                                                                                                                                         |
| **Search Items** | All                                                                                                                                            |
| **Faction**      | [Agent][fagent]                                                                                                                                |
| **Abilities**    | <li>Disappears inside portal and shows up behind the enemy.</li><li>Ranged weapons deals x4 damage in the back, x0.5 damage in the front.</li> |

**Weapons**

| Gears             | Powerup     |
| :---------------- | :---------- |
| ![Assault]        | [Precision] |
| ![DarkShotgun]    |             |
| ![SilencedPistol] | [Precision] |

### Huntsman

<div>
  <img src='./docs/Images/Huntsman.png' />
</div>

**Stats**

| **Health**        | Strong                       |
| :---------------- | :--------------------------- |
| **AI**            | Range Expert                 |
| **Infinite Ammo** | True                         |
| **Search Items**  | Makeshift, Health, Powerups. |
| **Faction**       | [Hunter][fhunter]            |

**Weapons**

| Gears                      | Powerup   |
| :------------------------- | :-------- |
| ![Sniper] ![MachinePistol] | [Trigger] |
| ![Magnum]                  | [Trigger] |

<details>
  <summary>
    <strong>Other Stats</strong>
  </summary>

| Stats                            | Value        |
| :------------------------------- | :----------- |
| **Projectile Crit Chance Dealt** | High         |
| **Size**                         | Above Normal |

</details>

### Spy

<img src='./docs/Images/Spy.png' />

## Script Commands

BotExtended can be played using the default settings without you having to touch the command line. But if you want to customize or try out different bots, weapons or factions, this section is for you.

By default, the game spawns a random faction (See the faction list [here](#list-of-factions)) in team 4 in every match.

All of the commands start with `/botextended` or `/be`. Some commands may require one or more arguments.

### `help`

Usage: `/<botextended|be> help|h|?`

Print all other commands and arguments you can pass to BotExtended.

### `version`

Usage: `/<botextended|be> version|v`

Print the current version of BotExtended.

### `listfaction`

Usage: `/<botextended|be> [listfaction|lf]`

List all of the available `BotFaction`s. A `BotFaction` consists of one or many different bots in a specific theme, for example: Assassin, Police, Zombie... Some `BotFaction`s only have one bot, usually a boss.

A boss is a much stronger bot and often come with special abilities: Mecha boss with bulletproof armor, Demolitionist who can one shot one kill. They are much harder to kill and is equivalent with several expert bots.

`BotFaction`s whose names prefixed with `Boss` will have at least one boss, sometimes spawn with its minions. (Kinpin and bodyguards, BadSanta and his Elks)

```
/be lf
```

#### List of factions

<details>
  <summary>Click to open</summary>

```
0: None
1: Assassin
2: Agent
3: Bandido
4: Biker
5: Clown
6: Cowboy
7: Engineer
8: Farmer
9: Gangster
10: Hunter
11: MetroCop
12: Mutant
13: Nazi
14: Police
15: PoliceSWAT
16: Pyromaniac
17: Robot
18: Scientist
19: Sniper
20: Soldier
21: Spacer
22: SpaceSniper
23: Stripper
24: Survivor
25: Thug
26: Punk
27: Zombie
28: ZombieMutated
200: Boss_Demolitionist
201: Boss_Agent79
202: Boss_Amos
203: Boss_Balista
204: Boss_Balloonatic
205: Boss_BazookaJane
206: Boss_Beast
207: Boss_Berserker
208: Boss_BigMutant
209: Boss_Bobby
210: Boss_Boffin
211: Boss_Chairman
212: Boss_Cindy
213: Boss_PoliceChief
214: Boss_Funnyman
215: Boss_Jo
216: Boss_Hacker
217: Boss_Handler
218: Boss_HeavySoldier
219: Boss_Hitman
220: Boss_Incinerator
221: Boss_Firebug
222: Boss_Fireman
223: Boss_Kingpin
224: Boss_MadScientist
225: Boss_Kriegbar
226: Boss_Meatgrinder
227: Boss_Mecha
228: Boss_MetroCop
229: Boss_MirrorMan
230: Boss_Nadja
231: Boss_Napoleon
232: Boss_Ninja
233: Boss_President
234: Boss_Quillhogg
235: Boss_Rambo
236: Boss_Raze
237: Boss_Reznor
238: Boss_Santa
239: Boss_Sheriff
240: Boss_Smoker
241: Boss_Survivalist
242: Boss_Translucent
243: Boss_Teddybear
244: Boss_ZombieFighter
245: Boss_ZombieEater
```

</details>

### `listbot`

Usage: `/<botextended|be> [listbot|lb]`

List all available `BotType`s.

`BotType` describes a specific bot's appearance, stats and behavior. Each `BotType` has different outfits, starting weapons, modifiers, AI behaviors and special abilities. A `BotFaction` must have multiple `BotType`s in the same theme. For example, Clown `BotFaction` can spawn ClownCowboy, ClownGangster and ClownBoxer `BotType`, but cannot spawn Gangster.

```
/be lb
```

<details>
    <summary>Result</summary>

```
0: None
1: AssassinMelee
2: AssassinRange
3: Agent
4: Bandido
5: Biker
6: BikerHulk
7: Bodyguard
8: Bodyguard2
9: Cyborg
10: LabAssistant
11: Scientist
12: ClownBodyguard
13: ClownBoxer
14: ClownCowboy
15: ClownGangster
16: Cowboy
17: Elf
18: Engineer
19: Farmer
20: Gangster
21: GangsterHulk
22: Gardener
23: Hunter
24: Lumberjack
25: MetroCop
26: Mutant
27: NaziLabAssistant
28: NaziHulk
29: NaziScientist
30: NaziSoldier
31: Police
32: PoliceSWAT
33: Survivor
34: Sniper
35: Soldier
36: Spacer
37: SpaceSniper
38: Stripper
39: SuicideDwarf
40: Thug
41: ThugHulk
42: Punk
43: PunkHulk
44: Zombie
45: ZombieAgent
46: ZombieBruiser
47: ZombieChild
48: ZombieFat
49: ZombieFlamer
50: ZombieGangster
51: ZombieNinja
52: ZombiePolice
53: ZombiePrussian
54: ZombieSoldier
55: ZombieThug
56: ZombieWorker
57: Agent79
58: Amos
59: Balista
60: Balloonatic
61: BazookaJane
62: Beast
63: Berserker
64: BigMutant
65: Bobby
66: Boffin
67: Chairman
68: Cindy
69: Demolitionist
70: Jo
71: Fireman
72: Firebug
73: Fritzliebe
74: Funnyman
75: Hacker
76: Handler
77: HeavySoldier
78: Hitman
79: Incinerator
80: Pyromaniac
81: Kingpin
82: Kriegbar
83: MetroCop2
84: Meatgrinder
85: Mecha
86: MirrorMan
87: Nadja
88: Napoleon
89: Ninja
90: PoliceChief
91: President
92: Quillhogg
93: Rambo
94: Raze
95: Reznor
96: Santa
97: Sheriff
98: Smoker
99: SSOfficer
100: Survivalist
101: Teddybear
102: Translucent
103: Babybear
104: ZombieEater
105: ZombieFighter
```

</details>

### `listrangedpowerup`

Usage: `/<botextended|be> [listrangedpowerup|lrp]`

List all ranged powerups. See [`setweapon`](#setweapon) command for further detail.

```
/be lrp
```

<details>
    <summary>Result</summary>

```
0: None
1: Blackhole
2: Blast
3: Bow
4: Delay
5: Dormant
6: DoublePenetration
7: DoubleTrouble
8: Fatigue
9: Fire
10: Helium
11: Hunting
12: Homing
13: Gauss
14: Grapeshot
15: Gravity
16: GravityDE
17: InfiniteBouncing
18: Minigun
19: Molotov
20: Lightning
21: Object
22: Penetration
23: Poison
24: Present
25: Riding
26: Shotgun
27: Shrapnel
28: Shrinking
29: Smoke
30: Stun
31: Spinner
32: Steak
33: StickyBomb
34: SuicideDove
35: SuicideFighter
36: Taser
37: Tearing
38: Termite
39: Welding
```

</details>

### `listmeleepowerup`

Usage: `/<botextended|be> [listmeleepowerup|lmp]`

List all melee powerups. See [`setweapon`](#setweapon) command for further detail.

```
/be lrp
```

<details>
    <summary>Result</summary>

```
0: None
1: Breaking
2: Earthquake
3: FireTrail
4: Hurling
5: Gib
6: GroundBreaker
7: GroundSlam
8: Megaton
9: Pushback
10: Serious
11: Slide
12: Splitting
```

</details>

### `findfaction`

Usage: `/<botextended|be> [findfaction|ff] <query>`

Find all bot factions that match query

```
/be ff zombie
```

```
--BotExtended find results--
14: Zombie
15: ZombieHard
213: Boss_Zombie
```

### `settings`

Usage: `/<botextended|be> [settings|s]`

Display current script settings

```
/be s
```

### `create`

Usage: `/<botextended|be> [create|c] <BotType> [1|2|3|4|_] [Count]`

Spawn one or a group of bots. First argument is `BotType`. See [`listbot`](#listbot) to list all `BotType`s. This is the only required argument.

The second argument is the team to spawn from `t1` to `t4`, `t0` is independent. Default to independent.

Third argument is the number of bot to spawn. Default to 1.

_Example_:

Spawn one funnyman

```
/be c funnyman
```

Spawn 5 metrocops at Team 2

```
/be c 34 t2 5
```

Spawn 3 bandidos at team independent

```
/be c bandido t0 3
```

Spawn 1 cowboy at team 2

```
/be c cowboy t2
```

Spawn 2 cowboy at team independent

```
/be c bandido 2
```

### `botcount`

Usage: `/<botextended|be> [botcount|bc] <1-10>`

Set the maximum bot count for a round. Be aware the number of bots will be capped based on 2 factors. The number of available `SpawnPlayer`s in the map and the `botcount` itself. So when you set `botcount` to 8 it will only spawn all 8 bots if the map is big enough to have all of the `SpawnPlayer`s needed.

How it works: On startup of every round, the script will search for the number of `SpawnPlayer` tiles in the map, and subtract the number of players and regular bots that have already spawned. That number then is capped again if it exceeds the `botcount` limit.

```
/be bc 2
```

### `faction`

Usage: `/<botextended|be> [Team] [faction|f] [-e] <names|indexes|all>`

Select a list of `BotFaction`s by either name or index to randomly spawn on startup. Each faction is separated by a space. `all` argument means randomizing all `BotFaction`s. Add -e flag before the argument list to exclude those `BotFaction`s instead. Select `none` to disable spawning bot to that team. Team arguments ranges from `t1` to `t4`

```bash
/be f sniper thug boss_jo boss_hacker
```

You can use the index instead of `BotFaction` name to shorten the command. See [`listfaction`](#listfaction) for more detail.

```
/be f 11 13 202 203
```

Select all factions.

```
/be f all
```

Select all except zombie factions.

```
/be f -e Zombie ZombieHard Boss_Zombie
```

Don't spawn bots on team 3.

```
/be f t3 none
```

To select one `BotFaction` to spawn every round, simply select that `BotFaction` only and set [`factionrotation`](#factionrotation) to `0` to disable rotation.

```bash
/be fr 0
/be f biker
```

### `factionrotation`

Usage: `/<botextended|be> [factionrotation|fr] <0-10>`

Set faction rotation interval for every n rounds. Set `0` to disable rotation

```
/be fr 4
```

### `nextfaction`

Usage: `/<botextended|be> [nextfaction|nf]`

Change the faction in the current faction rotation to the next faction

```
/be nf
```

### `setplayer`

Usage: `/<botextended|be> [setplayer|sp] <player> <BotType>`

Set player outfit, weapons and modifiers to the same as `BotType`. Player argument can be either name or slot index. Type `/listplayers`
to see the list of players and their respective indexes. this settings will be saved permanently. To reset, replace `BotType` value with `None`.
Use [`clearplsettings`](#clearplsettings) to reset settings for all players.

Set player 0 to funnyman

```
/be sp 0 funnyman
```

Reset player Spud to normal

```
/be sp spud none
```

### `setweapon`

Usage: `/<botextended|be> [setweapon|sw] <player> <WeaponItem> <Powerup>`

Give specific player a powerup weapon.

Give player 'your_name' an electroshock magnum.

```
/be setweapon your_name magnum stun
```

Give player 'near' hurl powerup. None weapon means barehand.

```
/be setweapon near none hurl
```

Same command as above but use indexes to shorten the command.

```
/be sw 0 1 1
```

Remove powerup. Magnum now fires normal rounds.

```
/be sw near magnum none
```

Remove all powerup weapons that in the player's inventory (not apply until the next round).

```
/be sw near none
```

### `clearplsettings`

Usage: `/<botextended|be> [clearplsettings|cp]`

Clear all player settings.

## Development

- Clone [ScriptLinker](https://github.com/nearhuscarl/scriptlinker) project on my github.
- Build in DEBUG mode. The global hotkeys doesn't work in RELEASE mode for some reasons but I'm too lazy to fix it now.
- Open ScriptLinker > Script > New, then enter the _Name_, _Entry Point_ and _Project Directory_. Remind me to add a way to read the script metedata in the future.

![New Script](./docs/NewScript.png)

- Open [this](./src) solution in _Visual Studio_.
- In BotExtended project, Click Reference > Add Reference > Browse > Select `...\Steam\steamapps\common\Superfighters Deluxe\SFD.GameScriptInterface.dll`
- Start coding and press `F8` to save change.

<!-- bot type -->

[biker]: #biker
[bodyguard]: #bodyguard
[gangster]: #gangster
[gangster hulk]: #gangster-hulk
[kingpin]: #kingpin
[bobby]: #bobby
[jo]: #jo
[thug]: #Thug
[zombie]: #Zombie

<!-- Factions -->

[fassassin]: ./docs/FACTIONS.md#assassin
[fagent]: ./docs/FACTIONS.md#agent
[fbandido]: ./docs/FACTIONS.md#bandido
[fbiker]: ./docs/FACTIONS.md#biker
[fcowboy]: ./docs/FACTIONS.md#cowboy
[fclown]: ./docs/FACTIONS.md#clown
[fengineer]: ./docs/FACTIONS.md#engineer
[ffarmer]: ./docs/FACTIONS.md#farmer
[fgangster]: ./docs/FACTIONS.md#gangster
[fhunter]: ./docs/FACTIONS.md#hunter
[fmetrocop]: ./docs/FACTIONS.md#metrocop
[fmutant]: ./docs/FACTIONS.md#mutant
[fnazi]: ./docs/FACTIONS.md#nazi
[fpolice]: ./docs/FACTIONS.md#Police
[fpoliceswat]: ./docs/FACTIONS.md#police-swat
[fpunk]: ./docs/FACTIONS.md#punk
[fpyromaniac]: ./docs/FACTIONS.md#pyromaniac
[fscientist]: ./docs/FACTIONS.md#scientist
[fsniper]: ./docs/FACTIONS.md#Sniper
[fsoldier]: ./docs/FACTIONS.md#soldier
[fspacer]: ./docs/FACTIONS.md#spacer
[fspacesniper]: ./docs/FACTIONS.md#space-sniper
[fstripper]: ./docs/FACTIONS.md#stripper
[fsurvivor]: ./docs/FACTIONS.md#survivor
[fthug]: ./docs/FACTIONS.md#thug
[fzombie]: ./docs/FACTIONS.md#zombie
[fzombiemutated]: ./docs/FACTIONS.md#zombie-mutated
[fkingpin]: ./docs/FACTIONS.md#kingpin
[frobot]: ./docs/FACTIONS.md#robot
[fsanta]: ./docs/FACTIONS.md#santa
[fzombieboss]: ./docs/FACTIONS.md#zombie-boss

<!-- Melee Powerups -->

[breaking]: ./docs/POWERUPS_MELEE.md#breaking
[earthquake]: ./docs/POWERUPS_MELEE.md#earthquake
[fire trail]: ./docs/POWERUPS_MELEE.md#fire-trail
[hurling]: ./docs/POWERUPS_MELEE.md#hurling
[gib]: ./docs/POWERUPS_MELEE.md#gib
[megaton]: ./docs/POWERUPS_MELEE.md#megaton
[pushback]: ./docs/POWERUPS_MELEE.md#pushback
[splitting]: ./docs/POWERUPS_MELEE.md#splitting

<!-- Ranged Powerups -->

[blast]: ./docs/POWERUPS_RANGED.md#blast
[bow]: ./docs/POWERUPS_RANGED.md#bow
[double penetration]: ./docs/POWERUPS_RANGED.md#double-penetration
[fire]: ./docs/POWERUPS_RANGED.md#fire
[helium]: ./docs/POWERUPS_RANGED.md#helium
[homing]: ./docs/POWERUPS_RANGED.md#homing
[gauss]: ./docs/POWERUPS_RANGED.md#gauss-gun
[knockback]: ./docs/POWERUPS_RANGED.md#knockback
[lightning]: ./docs/POWERUPS_RANGED.md#lightning
[mine]: ./docs/POWERUPS_RANGED.md#mine
[object]: ./docs/POWERUPS_RANGED.md#object-gun
[penetration]: ./docs/POWERUPS_RANGED.md#penetration
[poison]: ./docs/POWERUPS_RANGED.md#poison
[precise bouncing]: ./docs/POWERUPS_RANGED.md#precise-bouncing
[precision]: ./docs/POWERUPS_RANGED.md#precision
[scattershot]: ./docs/POWERUPS_RANGED.md#scattershot
[shrinking]: ./docs/POWERUPS_RANGED.md#shrinking
[spinner]: ./docs/POWERUPS_RANGED.md#spinner
[stun]: ./docs/POWERUPS_RANGED.md#stun
[suicide dove]: ./docs/POWERUPS_RANGED.md#suicide-dove
[suicide fighter]: ./docs/POWERUPS_RANGED.md#suicide-fighter
[taser]: ./docs/POWERUPS_RANGED.md#taser-gun
[tearing]: ./docs/POWERUPS_RANGED.md#tearing
[termite]: ./docs/POWERUPS_RANGED.md#termite
[trigger]: ./docs/POWERUPS_RANGED.md#trigger

<!-- weapons -->

[assault]: ./docs/Images/Weapons/Assault.png
[axe]: ./docs/Images/Weapons/Axe.png
[bat]: ./docs/Images/Weapons/Bat.png
[baseball]: ./docs/Images/Weapons/Baseball.png
[baton]: ./docs/Images/Weapons/Baton.png
[bazooka]: ./docs/Images/Weapons/Bazooka.png
[bottle]: ./docs/Images/Weapons/Bottle.png
[bowwpn]: ./docs/Images/Weapons/Bow.png
[c4]: ./docs/Images/Weapons/C4.png
[carbine]: ./docs/Images/Weapons/Carbine.png
[chain]: ./docs/Images/Weapons/Chain.png
[chainsaw]: ./docs/Images/Weapons/Chainsaw.png
[chair]: ./docs/Images/Weapons/Chair.png
[darkshotgun]: ./docs/Images/Weapons/DarkShotgun.png
[flamethrower]: ./docs/Images/Weapons/Flamethrower.png
[flaregun]: ./docs/Images/Weapons/FlareGun.png
[fist]: ./docs/Images/Weapons/Fist.png
[glauncher]: ./docs/Images/Weapons/GLauncher.png
[grenade]: ./docs/Images/Weapons/Grenade.png
[hammer]: ./docs/Images/Weapons/Hammer.png
[katana]: ./docs/Images/Weapons/Katana.png
[knife]: ./docs/Images/Weapons/Knife.png
[lazer]: ./docs/Images/Weapons/Lazer.png
[leadpipe]: ./docs/Images/Weapons/LeadPipe.png
[m60]: ./docs/Images/Weapons/M60.png
[machete]: ./docs/Images/Weapons/Machete.png
[machinepistol]: ./docs/Images/Weapons/MachinePistol.png
[magnum]: ./docs/Images/Weapons/Magnum.png
[minewpn]: ./docs/Images/Weapons/Mine.png
[molotov]: ./docs/Images/Weapons/Molotov.png
[mp50]: ./docs/Images/Weapons/MP50.png
[pipe]: ./docs/Images/Weapons/Pipe.png
[pistol]: ./docs/Images/Weapons/Pistol.png
[pistol45]: ./docs/Images/Weapons/Pistol45.png
[revolver]: ./docs/Images/Weapons/Revolver.png
[sawedoff]: ./docs/Images/Weapons/SawedOff.png
[shockbaton]: ./docs/Images/Weapons/ShockBaton.png
[shotgun]: ./docs/Images/Weapons/Shotgun.png
[silencedpistol]: ./docs/Images/Weapons/SilencedPistol.png
[silenceduzi]: ./docs/Images/Weapons/SilencedUzi.png
[slowmo10]: ./docs/Images/Weapons/SloMo10.png
[smg]: ./docs/Images/Weapons/SMG.png
[sniper]: ./docs/Images/Weapons/Sniper.png
[strengthboost]: ./docs/Images/Weapons/StrengthBoost.png
[teapot]: ./docs/Images/Weapons/Teapot.png
[tommygun]: ./docs/Images/Weapons/TommyGun.png
[uzi]: ./docs/Images/Weapons/Uzi.png
[whip]: ./docs/Images/Weapons/Whip.png
