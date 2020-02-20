// Decompiled with JetBrains decompiler
// Type: SFDGameScriptInterface.BotMeleeActions

using System;

namespace SFDGameScriptInterface
{
    [Serializable]
    public struct BotMeleeActions : IEquatable<BotMeleeActions>
    {
        public ushort Attack;
        public ushort AttackCombo;
        public ushort Block;
        public ushort Kick;
        public ushort Jump;
        public ushort Wait;
        public ushort Grab;

        public ushort GetMinRegisteredValue(bool includeZeroValues = true)
        {
            if (this.Attack == (ushort)0 && this.AttackCombo == (ushort)0 && (this.Block == (ushort)0 && this.Kick == (ushort)0) && (this.Jump == (ushort)0 && this.Wait == (ushort)0 && this.Grab == (ushort)0))
                return 0;
            ushort num = ushort.MaxValue;
            if (includeZeroValues)
            {
                if ((int)num > (int)this.Attack)
                    num = this.Attack;
                if ((int)num > (int)this.AttackCombo)
                    num = this.AttackCombo;
                if ((int)num > (int)this.Block)
                    num = this.Block;
                if ((int)num > (int)this.Kick)
                    num = this.Kick;
                if ((int)num > (int)this.Jump)
                    num = this.Jump;
                if ((int)num > (int)this.Wait)
                    num = this.Wait;
                if ((int)num > (int)this.Grab)
                    num = this.Grab;
            }
            else
            {
                if ((int)num > (int)this.Attack && this.Attack > (ushort)0)
                    num = this.Attack;
                if ((int)num > (int)this.AttackCombo && this.AttackCombo > (ushort)0)
                    num = this.AttackCombo;
                if ((int)num > (int)this.Block && this.Block > (ushort)0)
                    num = this.Block;
                if ((int)num > (int)this.Kick && this.Kick > (ushort)0)
                    num = this.Kick;
                if ((int)num > (int)this.Jump && this.Jump > (ushort)0)
                    num = this.Jump;
                if ((int)num > (int)this.Wait && this.Wait > (ushort)0)
                    num = this.Wait;
                if ((int)num > (int)this.Grab && this.Grab > (ushort)0)
                    num = this.Grab;
            }
            return num;
        }

        public ushort GetMaxRegisteredValue()
        {
            ushort num = 0;
            if ((int)num < (int)this.Attack)
                num = this.Attack;
            if ((int)num < (int)this.AttackCombo)
                num = this.AttackCombo;
            if ((int)num < (int)this.Block)
                num = this.Block;
            if ((int)num < (int)this.Kick)
                num = this.Kick;
            if ((int)num < (int)this.Jump)
                num = this.Jump;
            if ((int)num < (int)this.Wait)
                num = this.Wait;
            if ((int)num < (int)this.Grab)
                num = this.Grab;
            return num;
        }

        public BotMeleeActions Multiply(float value)
        {
            BotMeleeActions botMeleeActions = this;
            botMeleeActions.Attack = BotMeleeActions.Multiply(this.Attack, value);
            botMeleeActions.AttackCombo = BotMeleeActions.Multiply(this.AttackCombo, value);
            botMeleeActions.Block = BotMeleeActions.Multiply(this.Block, value);
            botMeleeActions.Kick = BotMeleeActions.Multiply(this.Kick, value);
            botMeleeActions.Jump = BotMeleeActions.Multiply(this.Jump, value);
            botMeleeActions.Wait = BotMeleeActions.Multiply(this.Wait, value);
            botMeleeActions.Grab = BotMeleeActions.Multiply(this.Grab, value);
            return botMeleeActions;
        }

        public static ushort Multiply(ushort s, float p)
        {
            float num = (float)s * p;
            if ((double)num < 0.0)
                num = 0.0f;
            if ((double)num > (double)ushort.MaxValue)
                num = (float)ushort.MaxValue;
            return (ushort)Math.Round((double)num, 0);
        }

        public static BotMeleeActions Default
        {
            get
            {
                return new BotMeleeActions()
                {
                    Attack = 8,
                    AttackCombo = 12,
                    Block = 3,
                    Kick = 3,
                    Jump = 2,
                    Wait = 1,
                    Grab = 2
                };
            }
        }

        public static BotMeleeActions DefaultWhenHit
        {
            get
            {
                return new BotMeleeActions()
                {
                    Attack = 8,
                    AttackCombo = 12,
                    Block = 10,
                    Kick = 3,
                    Jump = 2,
                    Wait = 3,
                    Grab = 2
                };
            }
        }

        public static BotMeleeActions DefaultWhenEnraged
        {
            get
            {
                return new BotMeleeActions()
                {
                    Attack = 10,
                    AttackCombo = 14,
                    Block = 5,
                    Kick = 3,
                    Jump = 3,
                    Wait = 1,
                    Grab = 4
                };
            }
        }

        public static BotMeleeActions DefaultWhenEnragedAndHit
        {
            get
            {
                return new BotMeleeActions()
                {
                    Attack = 5,
                    AttackCombo = 5,
                    Block = 10,
                    Kick = 2,
                    Jump = 0,
                    Wait = 1,
                    Grab = 0
                };
            }
        }

        public static BotMeleeActions Blank
        {
            get
            {
                return new BotMeleeActions()
                {
                    Attack = 0,
                    AttackCombo = 0,
                    Block = 0,
                    Kick = 0,
                    Jump = 0,
                    Wait = 0,
                    Grab = 0
                };
            }
        }

        public static BotMeleeActions Easy
        {
            get
            {
                return new BotMeleeActions()
                {
                    Attack = 12,
                    AttackCombo = 20,
                    Block = 8,
                    Kick = 6,
                    Jump = 1,
                    Wait = 16,
                    Grab = 0
                };
            }
        }

        public static BotMeleeActions EasyWhenHit
        {
            get
            {
                BotMeleeActions easy = BotMeleeActions.Easy;
                easy.Wait += (ushort)4;
                easy.Block += (ushort)6;
                return easy;
            }
        }

        public static BotMeleeActions EasyWhenEnraged
        {
            get
            {
                BotMeleeActions easy = BotMeleeActions.Easy;
                easy.Block += (ushort)4;
                easy.Attack += (ushort)6;
                easy.AttackCombo += (ushort)6;
                return easy;
            }
        }

        public static BotMeleeActions EasyWhenEnragedAndHit
        {
            get
            {
                BotMeleeActions easy = BotMeleeActions.Easy;
                easy.Attack += (ushort)8;
                easy.Block += (ushort)8;
                return easy;
            }
        }

        public static BotMeleeActions Normal
        {
            get
            {
                return new BotMeleeActions()
                {
                    Attack = 14,
                    AttackCombo = 20,
                    Block = 8,
                    Kick = 6,
                    Jump = 2,
                    Wait = 12,
                    Grab = 2
                };
            }
        }

        public static BotMeleeActions NormalWhenHit
        {
            get
            {
                BotMeleeActions normal = BotMeleeActions.Normal;
                normal.Wait += (ushort)4;
                normal.Block += (ushort)10;
                return normal;
            }
        }

        public static BotMeleeActions NormalWhenEnraged
        {
            get
            {
                BotMeleeActions normal = BotMeleeActions.Normal;
                normal.Block += (ushort)4;
                normal.Attack += (ushort)6;
                normal.AttackCombo += (ushort)6;
                normal.Jump += (ushort)2;
                normal.Grab += (ushort)2;
                return normal;
            }
        }

        public static BotMeleeActions NormalWhenEnragedAndHit
        {
            get
            {
                BotMeleeActions normal = BotMeleeActions.Normal;
                normal.Attack += (ushort)10;
                normal.Block += (ushort)10;
                return normal;
            }
        }

        public static BotMeleeActions Hard
        {
            get
            {
                return new BotMeleeActions()
                {
                    Attack = 14,
                    AttackCombo = 24,
                    Block = 8,
                    Kick = 6,
                    Jump = 4,
                    Wait = 4,
                    Grab = 4
                };
            }
        }

        public static BotMeleeActions HardWhenHit
        {
            get
            {
                BotMeleeActions hard = BotMeleeActions.Hard;
                hard.Wait += (ushort)4;
                hard.Block += (ushort)12;
                return hard;
            }
        }

        public static BotMeleeActions HardWhenEnraged
        {
            get
            {
                BotMeleeActions hard = BotMeleeActions.Hard;
                hard.Block += (ushort)4;
                hard.Attack += (ushort)6;
                hard.AttackCombo += (ushort)6;
                hard.Jump += (ushort)2;
                hard.Grab += (ushort)2;
                return hard;
            }
        }

        public static BotMeleeActions HardWhenEnragedAndHit
        {
            get
            {
                BotMeleeActions hard = BotMeleeActions.Hard;
                hard.Attack += (ushort)10;
                hard.Block += (ushort)10;
                hard.Jump += (ushort)2;
                hard.Grab += (ushort)2;
                return hard;
            }
        }

        public static BotMeleeActions Expert
        {
            get
            {
                return new BotMeleeActions()
                {
                    Attack = 16,
                    AttackCombo = 24,
                    Block = 6,
                    Kick = 6,
                    Jump = 5,
                    Wait = 3,
                    Grab = 5
                };
            }
        }

        public static BotMeleeActions ExpertWhenHit
        {
            get
            {
                BotMeleeActions expert = BotMeleeActions.Expert;
                expert.Wait += (ushort)4;
                expert.Block += (ushort)12;
                return expert;
            }
        }

        public static BotMeleeActions ExpertWhenEnraged
        {
            get
            {
                BotMeleeActions expert = BotMeleeActions.Expert;
                expert.Block += (ushort)2;
                expert.Attack += (ushort)6;
                expert.AttackCombo += (ushort)6;
                expert.Wait += (ushort)2;
                expert.Jump += (ushort)2;
                expert.Grab += (ushort)2;
                return expert;
            }
        }

        public static BotMeleeActions ExpertWhenEnragedAndHit
        {
            get
            {
                BotMeleeActions expert = BotMeleeActions.Expert;
                expert.Attack += (ushort)10;
                expert.Block += (ushort)10;
                return expert;
            }
        }

        public static bool operator ==(BotMeleeActions a, BotMeleeActions b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(BotMeleeActions a, BotMeleeActions b)
        {
            return !a.Equals(b);
        }

        public bool Equals(BotMeleeActions other)
        {
            return (int)this.Attack == (int)other.Attack & (int)this.AttackCombo == (int)other.AttackCombo & (int)this.Block == (int)other.Block & (int)this.Kick == (int)other.Kick & (int)this.Jump == (int)other.Jump & (int)this.Wait == (int)other.Wait & (int)this.Grab == (int)other.Grab;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            return this.Equals((BotMeleeActions)obj);
        }

        public override int GetHashCode()
        {
            return ((int)this.Attack * 50000 + (int)this.AttackCombo * 10000 + (int)this.Block * 5000 + (int)this.Kick * 1000 + (int)this.Jump * 500 + (int)this.Wait * 100 + (int)this.Grab).GetHashCode();
        }
    }
}
