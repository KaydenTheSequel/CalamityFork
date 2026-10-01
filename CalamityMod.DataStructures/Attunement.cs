using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;

namespace CalamityMod.DataStructures;

public abstract class Attunement
{
	public static Attunement[] attunementArray;

	public AttunementID id;

	public Color tooltipColor;

	public Color tooltipColor2;

	public Color tooltipPassiveColor;

	public Color energyParticleEdgeColor;

	public Color energyParticleCenterColor;

	public virtual LocalizedText AttunementName => CalamityUtils.GetText("Attunement." + GetType().Name + ".Name");

	public virtual LocalizedText FunctionText => CalamityUtils.GetText("Attunement." + GetType().Name + ".Function");

	public virtual LocalizedText PassiveName
	{
		get
		{
			if ((int)id < 14)
			{
				return LocalizedText.Empty;
			}
			return CalamityUtils.GetText("Attunement." + GetType().Name + ".PassiveName");
		}
	}

	public virtual LocalizedText PassiveDesc
	{
		get
		{
			if ((int)id < 10)
			{
				return LocalizedText.Empty;
			}
			return CalamityUtils.GetText("Attunement." + GetType().Name + ".PassiveDesc");
		}
	}

	public virtual float DamageMultiplier => 1f;

	public static void Load()
	{
		attunementArray = new Attunement[19]
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

	public static void Unload()
	{
		attunementArray = null;
	}

	public virtual void ApplyStats(Item item)
	{
	}

	public virtual bool Shoot(Player player, IEntitySource source, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack, ref int Combo, ref int CanLunge, ref int PowerLungeCounter)
	{
		return true;
	}

	public virtual void PassiveEffect(Player player, IEntitySource source, ref int UseTimer, ref bool Procced, Projectile projectile = null)
	{
	}
}
