# ChallengePlus

## Challenges

### Chonky

Players are huge, extremely slow, have high melee damage and very strong melee forces.

### Kickass

Players have huge melee forces.

### Minesweeper

Spawns mine randomly every 2 seconds.

### Tiny

Players are tiny, very fast, have low melee damage and weak melee forces.

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
