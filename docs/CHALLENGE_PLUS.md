# ChallengePlus

## Challenges

### Athelete

- Players explode if standing for too long.
- All players have infinite energy, deal very low melee damage.

### Bouncing

All guns have bouncing powerup.

### BuffGun

- All guns deal x8 damage, melee weapons deal x0.1 damage.
- Has 99% chance of spawning gun crates.

Bot Support: Yes.

- Bots do not search for melee weapons.
- Bots are kind of passive until they draw ranged weapons.

### BuffMelee

All melee weapons (including fist) deal x8 damage, guns deal x0.1 damage.

Bot Support: Yes. Bots only use melee weapons.

### Chonky

Players are huge, extremely slow, have high melee damage and very strong melee forces.

### Crit

All projectiles deal critical damage 100% of the time.

### Danger

Dynamic objects are explosion barrels, small objects are propane tanks or gas cans.

### Disease

Players' health reduce gradually, deal damage to heal.

### Drug

All players have unlimited strengthboost and speedboost.

### FastBullet

All projectiles have max velocity.

### Fire

All guns have fire powerup. Projectiles deal 1% damage.

### InfiniteAmmo

Guns never run out of ammo.

### Kickass

Players have huge melee forces.

### LootBox

Dynamic objects have 50% health. Destroyed objects spawn weapons.

### Minesweeper

Spawns mine randomly every 2 seconds.

Bot Support: No.

### Moonwalk

(EXPERIMENTAL) Players can only walk backward and face the wrong direction.

Bot Support: Fuck no lol.

### Nuclear

Explodes and destroys all entities in a radius on dealth.

### Precision

All guns have laser and pinpoint accuracy.

### SlowBullet

All projectiles have min velocity.

### Sniper

- All players start with a sniper, all projectiles have infinite bouncing and fire powerups.
- Snipers can only walk.

### SpecificWeapon

All supply crates spawn a specific weapon.

### StrongObject

Dynamic objects have infinite health.

### Switcharoo

Players swap body on contact.

### Tiny

Players are tiny, very fast, have low melee damage and weak melee forces.

### Trap

Dynamic objects have 50% health. Destroyed objects spawn either cooked grenades, molotovs or mines.

### Unstable

Players have 15% chance of exploding after taking damage.

### Weak

Players are weak. Damage taken x3.

### WeakObject

Dynamic objects have 20 health max.

## Script Commands

BotExtended can be played using the default settings without you having to touch the command line. But if you want to customize or try out different bots, weapons or factions, this section is for you.

By default, the game spawns a random faction (See the faction list [here](#list-of-factions)) in team 4 in every match.

All of the commands start with `/botextended` or `/be`. Some commands may require one or more arguments.

### `help`

Usage: `/<challengeplus|cp> help|h|?`

Print all other commands and arguments.

### `version`

Usage: `/<challengeplus|cp> version|v`

Print the current version of the script.

### `listchallenges`

Usage: `/<challengeplus|cp> [listchallenges|lc]`

List all of the available Challenges.

```
/cp lc
```

### `settings`

Usage: `/<challengeplus|cp> [settings|s]`

Display the current script's settings

```
/cp s
```

### `enabledchallenges`

Usage: `/<challengeplus|cp> [enabledchallenges|ec] [-e] <names|indexes|all>`

Set enabled challenges to play with by either name or index. Each challenge name is separated by a space. `all` argument means randomizing all challenges. Add -e flag before the argument list to exclude those challenges instead. Set to `none` to disable challenge.

```bash
/cp ec tiny chonky
```

You can use the index instead of challenge's name to shorten the command. See [`listchallenges`](#listchallenges) for more detail.

```
/cp ec 3 5
```

Select all challenges.

```
/cp ec all
```

Select all except chonky.

```
/cp ec -e chonky
```

Select none to disable the challenge.

```
/cp ec none
```

### `rotationinterval`

Usage: `/<challengeplus|cp> [rotationinterval|ri] <0-10>`

Set challenge rotation interval for every n rounds. Set `0` to disable rotation

```
/cp ri 4
```

### `nextchallenge`

Usage: `/<challengeplus|cp> [nextchallenge|nc]`

Reset the rotation interval and skip to the next challenge.

```
/cp nc
```
