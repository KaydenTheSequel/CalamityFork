using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Effects;

public class StormlionEffects
{
	public static int EnergyDust;

	public static Color EnergyColor;

	public static int FleshDust;

	public static Color FleshColor;

	public static readonly SoundStyle Hit;

	public static readonly SoundStyle Killed;

	public static readonly SoundStyle Attack;

	public static readonly SoundStyle Idle1;

	public static readonly SoundStyle Idle2;

	static StormlionEffects()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		EnergyDust = ModContent.DustType<LightDust>();
		EnergyColor = new Color(5, 187, 177);
		FleshDust = 192;
		FleshColor = new Color(171, 113, 91);
		Hit = new SoundStyle("CalamityMod/Sounds/NPCHit/StormlionAltHit");
		Killed = new SoundStyle("CalamityMod/Sounds/NPCKilled/StormlionAltDeath");
		Attack = new SoundStyle("CalamityMod/Sounds/Custom/StormlionAltShoot");
		Idle1 = new SoundStyle("CalamityMod/Sounds/Custom/StormlionAltIdle1");
		Idle2 = new SoundStyle("CalamityMod/Sounds/Custom/StormlionAltIdle2");
	}
}
