# BotExtended

This script adds a wide variety of bots to spice up combat. It is currently
under development and maybe full of bugs :bug:

## Getting Started

- Download [this file](src/BotExtended/BotExtended.txt)
- Move it to `%USERPROFILE%\Documents\Superfighters Deluxe\Scripts\`

## Script Commands

BotExtended can be played using the default settings without you having to touch the commandline. But if you want to further customize or break stuff, this section is for you.

All of the commands start with `/botextended` or `/be`. Some commands may require one or more arguments.

### `help`

Usage: `/<botextended|be> help|h|?`

Print all other commands and arguments you can pass to BotExtended.

### `version`

Usage: `/<botextended|be> version|v`

Print the current version of BotExtended.

### `listgroup`

Usage: `/<botextended|be> [listgroup|lg]`

List all of the available `BotGroup`s. A `BotGroup` consists of one or many different bots in a specific theme, for example: Assassin, Police, Zombie... Some `BotGroup`s only have one bot, usually a boss.

A boss is a much stronger bot, some have special abilities: Mecha boss with bulletproof armor, Demolitionist who can one shot one kill. They are much harder to kill and is equivalent with several expert bots.

`BotGroup`s whose names prefixed with `Boss` will have at least one boss, sometimes spawn with its minions. (Kinpin and bodyguards, BadSanta and his Elks)

```
/be lg
```


<details>
  <summary>Result</summary>

```
0: Assassin
1: Agent
2: Bandido
3: Biker
4: Clown
5: Cowboy
6: Gangster
7: Marauder
8: MetroCop
9: Police
10: PoliceSWAT
11: Sniper
12: Soldier
13: Thug
14: Zombie
15: ZombieHard
200: Boss_Demolitionist
201: Boss_Funnyman
202: Boss_Jo
203: Boss_Hacker
204: Boss_Incinerator
205: Boss_Kingpin
206: Boss_MadScientist
207: Boss_Meatgrinder
208: Boss_Mecha
209: Boss_MetroCop
210: Boss_Ninja
211: Boss_Santa
212: Boss_Teddybear
213: Boss_Zombie
```
</details>


### `listbot`

Usage: `/<botextended|be> [listbot|lb]`

List all available `BotType`s.

`BotType` describes a specific bot apperance, stats and behavior. Each `BotType` has different outfits, starting weapons, modifiers, AI behaviors and special abilities. A `BotGroup` must have multiple `BotType`s in the same theme. For example, Clown `BotGroup` can spawn ClownCowboy, ClownGangster and ClownBoxer `BotType`, but cannot spawn Gangster.

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
4: Agent2
5: Bandido
6: Biker
7: BikerHulk
8: Bodyguard
9: Bodyguard2
10: ClownBodyguard
11: ClownBoxer
12: ClownCowboy
13: ClownGangster
14: Cowboy
15: Demolitionist
16: Elf
17: Hacker
18: Jo
19: Fritzliebe
20: Funnyman
21: Gangster
22: GangsterHulk
23: Incinerator
24: Kingpin
25: Kriegb
26: MarauderBiker
27: MarauderCrazy
28: MarauderNaked
29: MarauderRifleman
30: MarauderRobber
31: MarauderTough
32: Meatgrinder
33: Mecha
34: MetroCop
35: MetroCop2
36: Mutant
37: NaziLabAssistant
38: NaziMuscleSoldier
39: NaziScientist
40: NaziSoldier
41: SSOfficer
42: Ninja
43: Police
44: PoliceSWAT
45: Santa
46: Sniper
47: Soldier
48: Soldier2
49: Teddybear
50: Babybear
51: Thug
52: ThugHulk
53: Zombie
54: ZombieAgent
55: ZombieBruiser
56: ZombieChild
57: ZombieFat
58: ZombieFighter
59: ZombieFlamer
60: ZombieGangster
61: ZombieNinja
62: ZombiePolice
63: ZombiePrussian
64: BaronVonHauptstein
65: ZombieSoldier
66: ZombieThug
67: ZombieWorker
```
</details>

### `find`

Usage: `/<botextended|be> [find|f|/] <query>`

Find all bot groups that match query

```
/be f zombie
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

### `spawn`

Usage: `/<botextended|be> [spawn|sp] <BotType> [1|2|3|4|_] [Count]`

Spawn one or a group of bots. First argument is `BotType`. See [`listbot`](#listbot) to list all `BotType`s. This is the only required argument

Second argument is team to spawn to from `1` to `4`, `_` is indenpendent. Default to indenpendent

Third argument is number of bot to spawn. Default to 1

*Example*:

Spawn one funnyman

```
/be sp funnyman
```

Spawn 5 metrocops at Team 2

```
/be sp 34 2 5
```

Spawn 3 bandidos independently

```
/be sp bandido _ 3
```

### `botcount`

Usage: `/<botextended|be> [botcount|bc] <1-10>`

Set the maximum bot count for a round. Be aware the number of bots will be capped based on 2 factors. The number of available `SpawnPlayer`s in the map and the `botcount` itself. So when you set `botcount` to 8 it will only spawn all 8 bots if the map is big enough to have all of the `SpawnPlayer`s needed.

How it works: On startup of every round, the script will search for the number of `SpawnPlayer` tiles in the map, and substract the number of players and regular bots that already spawned. That number then is capped again if it exceeds the `botcount` limit.

```
/be bc 2
```

### `random`

Usage: `/<botextended|be> [random|r] <0|1>`

Whether to randomize *all* `BotGroup`s or not.

If the first argument is 1, randomize *all* `BotGroup`s and select one for each rounds. Otherwise, a list of user-defined `BotGroup`s will be randomized instead. The user-defined `BotGroup`s can be customized by [`group`](#group) command.

```
/be r 0
/be r 1
```

### `group`

Usage: `/<botextended|be> [group|g] <group names|indexes>`

Select a list of `BotGroup`s by either name or index to randomly spawn on startup. Each group name is seperated by a space. This option will be disregarded if [`random`](#random) is set to 1 (randomize all `BotGroup`s)

```bash
/be g sniper thug boss_jo boss_hacker
```

You can use index for `BotGroup` name to shorten the command. See [`listgroup`](#listgroup) for more detail.

```
/be r 11 13 202 203
```

To select one single `BotGroup` to spawn for every round, simply turn off [`random`](#random) option and select that `BotGroup` only

```bash
/be r 0
/be g biker
```

### `decorate`

Usage: `/<botextended|be> [decorate|d] <player> <BotType>`

Change player outfit, weapons and modifiers to the same as `BotType`. Player argument can be either name or slot index. Type `/listplayers`
to see the list of players and their respective indexes.

```
/be d 0 funnyman
```