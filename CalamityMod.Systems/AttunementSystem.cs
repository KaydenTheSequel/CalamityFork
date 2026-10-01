using CalamityMod.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public sealed class AttunementSystem : ModSystem
{
	public static Attunement[] Registry;

	public static Attunement Empty => Registry[EmptyID];

	public static int EmptyID => Registry.Length - 1;

	public override void OnModLoad()
	{
		Registry = new Attunement[19]
		{
			new DefaultAttunement(),
			new HotAttunement(),
			new ColdAttunement(),
			new EvilAttunement(),
			new TrueDefaultAttunement(),
			new TrueHotAttunement(),
			new TrueColdAttunement(),
			new TrueTropicalAttunement(),
			new TrueEvilAttunement(),
			new HolyAttunement(),
			new WhirlwindAttunement(),
			new FlailBladeAttunement(),
			new SuperPogoAttunement(),
			new ShockwaveAttunement(),
			new PhoenixAttunement(),
			new AriesAttunement(),
			new PolarisAttunement(),
			new AndromedaAttunement(),
			null
		};
	}

	public override void Unload()
	{
		Registry = null;
	}

	public static Attunement FindOrNull(int index)
	{
		if (index < 0)
		{
			return Empty;
		}
		if (index >= Registry.Length)
		{
			return Empty;
		}
		return Registry[index];
	}

	public static Attunement FindOrNull(AttunementID id)
	{
		return FindOrNull((int)id);
	}
}
