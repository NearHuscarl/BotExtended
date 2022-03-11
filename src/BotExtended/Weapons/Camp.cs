using BotExtended.Bots;
using BotExtended.Factions;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Weapons
{
    public class Camp : Weapon
    {
        public GangsterBot Bot { get; private set; }
        public List<Bot> Members { get; set; }
        public List<Bot> AttackMembers { get; set; }
        public IPlayer Target { get; private set; }
        public Area Area { get; private set; }
        public override IEnumerable<IObject> Components { get; set; }

        public static readonly int MaxMembers = 10;
        public static readonly int MembersToStartAttacking = 7;

        public Camp(GangsterBot bot) : base(bot.Player)
        {
            Bot = bot;
            Members = new List<Bot>();
            AttackMembers = new List<Bot>();

            var body = Game.CreateObject("BgCarnivalTent01D", new Vector2(Bot.Position.X, Bot.Position.Y + 12));
            var roof = Game.CreateObject("BgCarnivalTent01B", new Vector2(Bot.Position.X, Bot.Position.Y + 36));

            body.SetColor1(Turret.GetColor(Bot.Player.GetTeam()));
            body.CustomID = "GangTentBody";
            roof.SetColor1(Turret.GetColor(Bot.Player.GetTeam()));
            roof.CustomID = "GangTentRoof";

            Area = ScriptHelper.GrowFromCenter(body.GetAABB().Center, 100, 40);
            Components = new List<IObject>() { body, roof };
            Instance = body;
            _isElapsedHeal = ScriptHelper.WithIsElapsed(555);
            _isElapsedCheckMember = ScriptHelper.WithIsElapsed(3019);
        }

        private float _spawnGangsterTime = 0;
        private Func<bool> _isElapsedHeal;
        private Func<bool> _isElapsedCheckMember;
        public override void Update(float elapsed)
        {
            //Game.DrawText(Members.Count.ToString(), Position);
            //Game.DrawArea(Area, Color.Green);
            //Members.ForEach(m => Game.DrawText(m.Player.UniqueId.ToString(), m.Position, Color.Green));
            //AttackMembers.ForEach(m => Game.DrawText(m.Player.UniqueId.ToString(), m.Position, Color.Red));

            if (ScriptHelper.IsElapsed(_spawnGangsterTime, 12000))
            {
                if (Members.Count < MaxMembers)
                {
                    var rndNum = RandomHelper.Between(0, 100);
                    var botType = RandomHelper.Boolean() ? BotType.Gangster : BotType.GangsterHulk;
                    
                    if (rndNum < 1)
                        botType = BotType.Kingpin;
                    if (1 <= rndNum && rndNum < 2)
                        botType = BotType.Bobby;
                    if (2 <= rndNum && rndNum < 3)
                        botType = BotType.Jo;
                    var bot = BotManager.SpawnBot(botType, BotFaction.Gangster, team: Team, ignoreFullSpawner: true);

                    bot.Player.SetWorldPosition(Position);
                    AddMember(bot);
                }

                if (Members.Count >= MembersToStartAttacking && AttackMembers.Count <= 2)
                {
                    var attackCount = RandomHelper.BetweenInt(1, Members.Count - 3);
                    
                    AttackMembers = Members.Where(x => x.Player.GetHealth() > 50).Take(attackCount).ToList();
                    AttackMembers.ForEach(x => x.Player.SetGuardTarget(null));
                }

                _spawnGangsterTime = Game.TotalElapsedGameTime;
            }

            if (_isElapsedHeal())
            {
                foreach (var member in Members)
                {
                    if (member.Player.GetAABB().Intersects(Area))
                        member.Player.SetHealth(member.Player.GetHealth() + 1);
                }
            }

            if (_isElapsedCheckMember())
            {
                foreach (var gangster in BotManager.GetBots())
                {
                    if (!gangster.Player.IsDead
                        && gangster.Faction == BotFaction.Gangster
                        && gangster.Player.GetTeam() == Team
                        && !Members.Any(x => x.Player.UniqueID == gangster.Player.UniqueID))
                        AddMember(gangster);
                }
                // retreat if injured
                foreach (var member in AttackMembers.ToList())
                {
                    if (member.Player.GetHealth() < 30)
                    {
                        member.Player.SetGuardTarget(Instance);
                        AttackMembers.Remove(member);
                    }
                }
                foreach (var member in Members.ToList())
                {
                    if (member.Player.IsDead) RemoveMember(member);
                }
            }
        }

        public void AddMember(Bot member)
        {
            Members.Add(member);
            member.Player.SetGuardTarget(Instance);
        }

        public void RemoveMember(Bot member)
        {
            Members.Remove(member);
            AttackMembers.Remove(member);
        }
    }
}
