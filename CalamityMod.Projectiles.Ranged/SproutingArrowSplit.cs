using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SproutingArrowSplit : ModProjectile, ILocalizedModType, IModType
{
	public bool expanded;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Ammo/SproutingArrow";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 450;
		base.Projectile.ArmorPenetration = 8;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 1f)
		{
			if (!expanded)
			{
				base.Projectile.ExpandHitboxBy(100);
				expanded = true;
			}
		}
		else
		{
			base.Projectile.tileCollide = true;
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.LimeGreen;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.25f);
		if (base.Projectile.alpha > 0)
		{
			Vector2 center2 = base.Projectile.Center;
			Vector2? velocity = base.Projectile.velocity.RotatedByRandom(0.6) * Main.rand.NextFloat(0.05f, 1.5f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(center2, 264, velocity, 0, newColor);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.8f, 1.3f);
			dust.color = (Main.rand.NextBool(3) ? Color.MediumAquamarine : Color.Lime);
			base.Projectile.alpha -= 20;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.09f;
		}
		else if (Main.rand.NextBool())
		{
			Vector2 center3 = base.Projectile.Center;
			Vector2? velocity2 = -base.Projectile.velocity * Main.rand.NextFloat(0.05f, 0.3f);
			newColor = default(Color);
			Dust dust2 = Dust.NewDustPerfect(center3, 264, velocity2, 0, newColor);
			dust2.noGravity = true;
			dust2.scale = Main.rand.NextFloat(0.35f, 0.65f);
			dust2.color = (Main.rand.NextBool(3) ? Color.MediumAquamarine : Color.Lime);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		int Dusts = 6;
		float radians = (float)Math.PI * 2f / (float)Dusts;
		Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
		float rotRando = Main.rand.NextFloat(0.1f, 2.5f);
		for (int i = 0; i < Dusts; i++)
		{
			Vector2 dustVelocity = spinningPoint.RotatedBy(radians * (float)i).RotatedBy(0.5f * rotRando) * 3f;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 264, dustVelocity);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.65f, 0.95f);
			dust.color = (Main.rand.NextBool(3) ? Color.MediumAquamarine : Color.Lime);
		}
		SoundStyle style = SoundID.Item118 with
		{
			Pitch = 0.5f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}
}
