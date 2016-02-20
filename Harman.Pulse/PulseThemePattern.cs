using System.Collections.Generic;

namespace Harman.Pulse
{
    public sealed class PulseThemePattern
    {
        public static readonly PulseThemePattern PulseTheme_Firework = new PulseThemePattern("PulseTheme_Firework",
            InnerEnum.PulseTheme_Firework);

        public static readonly PulseThemePattern PulseTheme_Traffic = new PulseThemePattern("PulseTheme_Traffic",
            InnerEnum.PulseTheme_Traffic);

        public static readonly PulseThemePattern PulseTheme_Star = new PulseThemePattern("PulseTheme_Star",
            InnerEnum.PulseTheme_Star);

        public static readonly PulseThemePattern PulseTheme_Wave = new PulseThemePattern("PulseTheme_Wave",
            InnerEnum.PulseTheme_Wave);

        public static readonly PulseThemePattern PulseTheme_Firefly = new PulseThemePattern("PulseTheme_Firefly",
            InnerEnum.PulseTheme_Firefly);

        public static readonly PulseThemePattern PulseTheme_Rain = new PulseThemePattern("PulseTheme_Rain",
            InnerEnum.PulseTheme_Rain);

        public static readonly PulseThemePattern PulseTheme_Fire = new PulseThemePattern("PulseTheme_Fire",
            InnerEnum.PulseTheme_Fire);

        public static readonly PulseThemePattern PulseTheme_Canvas = new PulseThemePattern("PulseTheme_Canvas",
            InnerEnum.PulseTheme_Canvas);

        public static readonly PulseThemePattern PulseTheme_Hourglass = new PulseThemePattern("PulseTheme_Hourglass",
            InnerEnum.PulseTheme_Hourglass);

        public static readonly PulseThemePattern PulseTheme_Ripple = new PulseThemePattern("PulseTheme_Ripple",
            InnerEnum.PulseTheme_Ripple);

        private static readonly IList<PulseThemePattern> valueList = new List<PulseThemePattern>();

        static PulseThemePattern()
        {
            valueList.Add(PulseTheme_Firework);
            valueList.Add(PulseTheme_Traffic);
            valueList.Add(PulseTheme_Star);
            valueList.Add(PulseTheme_Wave);
            valueList.Add(PulseTheme_Firefly);
            valueList.Add(PulseTheme_Rain);
            valueList.Add(PulseTheme_Fire);
            valueList.Add(PulseTheme_Canvas);
            valueList.Add(PulseTheme_Hourglass);
            valueList.Add(PulseTheme_Ripple);
        }

        public enum InnerEnum
        {
            PulseTheme_Firework,
            PulseTheme_Traffic,
            PulseTheme_Star,
            PulseTheme_Wave,
            PulseTheme_Firefly,
            PulseTheme_Rain,
            PulseTheme_Fire,
            PulseTheme_Canvas,
            PulseTheme_Hourglass,
            PulseTheme_Ripple
        }

        private readonly string nameValue;
        private readonly int ordinalValue;
        private readonly InnerEnum innerEnumValue;
        private static int nextOrdinal = 0;

        private PulseThemePattern(string name, InnerEnum innerEnum)
        {
            nameValue = name;
            ordinalValue = nextOrdinal++;
            innerEnumValue = innerEnum;
        }

        public static IList<PulseThemePattern> values()
        {
            return valueList;
        }

        public InnerEnum InnerEnumValue()
        {
            return innerEnumValue;
        }

        public int ordinal()
        {
            return ordinalValue;
        }

        public override string ToString()
        {
            return nameValue;
        }

        public static PulseThemePattern valueOf(string name)
        {
            foreach (PulseThemePattern enumInstance in PulseThemePattern.values())
            {
                if (enumInstance.nameValue == name)
                {
                    return enumInstance;
                }
            }
            throw new System.ArgumentException(name);
        }
    }


    /* Location:              D:\Hack\classes.jar!\com\harman\pulsesdk\PulseThemePattern.class
	 * Java compiler version: 7 (51.0)
	 * JD-Core Version:       0.7.1
	 */
}