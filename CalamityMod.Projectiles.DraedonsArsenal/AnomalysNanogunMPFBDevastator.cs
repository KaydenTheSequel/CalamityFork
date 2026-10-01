using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class AnomalysNanogunMPFBDevastator : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 44;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.375f / (float)Math.PI);
			Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 16f;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, 267, Vector2.Zero);
			dust.color = Color.Cyan;
			dust.noGravity = true;
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center - offset, 267, Vector2.Zero);
			dust2.color = Color.Cyan;
			dust2.noGravity = true;
			Lighting.AddLight(base.Projectile.Center, 0f, base.Projectile.Opacity * 0.7f / 255f, base.Projectile.Opacity);
			base.Projectile.frameCounter++;
			base.Projectile.frame = base.Projectile.frameCounter / 3 % Main.projFrames[base.Type];
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int yPos = texture.Height / Main.projFrames[base.Type] * base.Projectile.frame;
		Main.EntitySpriteDraw(texture, base.Projectile.position - Main.screenPosition, (Rectangle?)new Rectangle(0, yPos, texture.Width, texture.Height / Main.projFrames[base.Type]), lightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 18; i++)
		{
			Vector2 vel = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.8f, 1.2f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + vel * 1.5f, 226, vel * Main.rand.NextFloat(0.1f, 1.2f) + new Vector2(0f, -2f));
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(0.65f, 1.2f);
		}
		SoundEngine.PlaySound(in AnomalysNanogunMPFBBoom.MPFBExplosion, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			for (int j = 0; j < 4; j++)
			{
				Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AnomalysNanogunMPFBBoom>(), (int)((float)base.Projectile.damage * 0.075f), base.Projectile.knockBack, base.Projectile.owner);
				projectile.ai[1] = Main.rand.NextFloat(110f, 200f) + (float)j * 20f;
				projectile.localAI[1] = Main.rand.NextFloat(0.18f, 0.3f);
				projectile.netUpdate = true;
			}
		}
	}
}
