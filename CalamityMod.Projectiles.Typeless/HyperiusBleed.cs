using CalamityMod.Items.Ammo;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class HyperiusBleed : DirectStrike, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		modifiers.HideCombatText();
		SoundStyle style = HyperiusBullet.hit with
		{
			Volume = 0.45f,
			Pitch = Main.rand.NextFloat(-0.15f, 0.15f),
			MaxInstances = 10
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}
}
