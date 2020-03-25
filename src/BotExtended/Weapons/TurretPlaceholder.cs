using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Weapons
{
    class TurretPlaceholder
    {
        public int UniqueID { get { return m_components.First().UniqueID; } }
        public Vector2 Position { get; private set; }
        public Area GetAABB() { return m_components.First().GetAABB(); }
        public bool IsRemoved { get; private set; }

        public IObject RepresentedObject { get { return m_components.First(); } }

        private List<IObject> m_components = new List<IObject>();
        public TurretDirection Direction { get; private set; }
        private IObject m_ground;

        private Vector2 m_placeholderBgPosition;
        private List<IObject> m_progressIndicators = new List<IObject>();

        public PlayerTeam Team { get; private set; }
        public IPlayer OriginalBuilder { get; private set; }

        private float m_buildProgress = 0f;
        // 0-1, 1 is finished
        public float BuildProgress
        {
            get { return m_buildProgress; }
            set
            {
                if (m_buildProgress == value) return;
                var progress = -1;

                if (value >= .8 && m_buildProgress < .8)
                    progress = 4;
                else if (value >= .6 && m_buildProgress < .6)
                    progress = 3;
                else if (value >= .4 && m_buildProgress < .4)
                    progress = 2;
                else if (value >= .2 && m_buildProgress < .2)
                    progress = 1;
                else if (m_buildProgress == 0)
                    progress = 0;

                if (progress > -1)
                {
                    foreach(var indicator in m_progressIndicators)
                        indicator.Remove();
                    m_progressIndicators.Clear();

                    var rows = (int)Math.Ceiling(progress / 2.0);

                    for (var r = 0; r < rows; r++)
                    {
                        var indicator = Game.CreateObject("BgFrame00B", m_placeholderBgPosition - Vector2.UnitY * 8 * r);
                        int c;

                        if (r == rows - 1) c = progress % 2 == 1 ? 1 : 2;
                        else c = 2;

                        indicator.SetColor1("BgGray");
                        indicator.SetSizeFactor(new Point(c, 1));
                        m_progressIndicators.Add(indicator);
                    }
                }

                m_buildProgress = value;
            }
        }

        public TurretPlaceholder(Vector2 worldPosition, TurretDirection direction, IPlayer builder)
        {
            OriginalBuilder = builder;
            Team = builder.GetTeam();
            Direction = direction;
            IsRemoved = false;

            var dir = (direction == TurretDirection.Left) ? -1 : 1;

            var ux = Vector2.UnitX * -dir;
            var uy = Vector2.UnitY;

            // worldPosition works best when get from player.GetWorldPosition()
            Position = worldPosition - ux * 10 + uy * 9;
            m_placeholderBgPosition = Position - Vector2.UnitX * (Direction > 0 ? 5 : 3) - uy * 1;
            var legLeft1Position = Position - ux * 3 + uy * 1;
            var legLeft2Position = Position - ux * 6 - uy * 5;
            var legRight1Position = Position + ux * 1 - uy * 5;
            var legRight2Position = Position + ux * 5 - uy * 13;
            var legMiddle1Position = Position - ux * 1.8f - uy * 3;
            var legMiddle2Position = Position - ux * 1.8f - uy * 6;

            var placeholderBg = Game.CreateObject("BgFrame00B", m_placeholderBgPosition);
            var legMiddle1 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legMiddle1Position, -dir * 0.41f);
            var legMiddle2 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legMiddle2Position, -dir * 0.41f);
            var legLeft1 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legLeft1Position);
            var legLeft2 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legLeft2Position);
            var legRight1 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legRight1Position, MathHelper.PI);
            var legRight2 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legRight2Position, MathHelper.PI);
            m_ground = Turret.GetGround(Position);

            legMiddle1.SetEnabled(false);
            legMiddle2.SetEnabled(false);
            legLeft1.SetEnabled(false);
            legLeft2.SetEnabled(false);
            legRight1.SetEnabled(false);
            legRight2.SetEnabled(false);

            legLeft1.SetFaceDirection(-dir);
            legLeft2.SetFaceDirection(-dir);
            legRight1.SetFaceDirection(dir);
            legRight2.SetFaceDirection(dir);
            legMiddle1.SetFaceDirection(-dir);
            legMiddle2.SetFaceDirection(-dir);

            placeholderBg.SetSizeFactor(new Point(2, 2));

            m_components.Add(placeholderBg); legMiddle1.CustomID = "PlaceholderBg";
            m_components.Add(legMiddle1); legMiddle1.CustomID = "LegMiddle1";
            m_components.Add(legMiddle2); legMiddle2.CustomID = "LegMiddle2";
            m_components.Add(legLeft1); legLeft1.CustomID = "LegLeft1";
            m_components.Add(legLeft2); legLeft2.CustomID = "LegLeft2";
            m_components.Add(legRight1); legRight1.CustomID = "LegRight1";
            m_components.Add(legRight2); legRight2.CustomID = "LegRight2";
        }

        public void Update(float elapsed)
        {
            if (m_ground.GetBodyType() == BodyType.Dynamic)
                Remove();
        }

        public void Remove()
        {
            if (IsRemoved) return;
            IsRemoved = true;

            foreach (var component in m_components)
            {
                component.Remove();
            }
            foreach (var indicator in m_progressIndicators)
            {
                indicator.Remove();
            }
            WeaponManager.RemoveTurretPlaceholder(UniqueID);
        }
    }
}
