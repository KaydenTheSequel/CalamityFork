using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class GemTechGreenFlechette : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = base.Projectile.MaxUpdates * 180;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.Opacity = Utils.GetLerpValue(180f, 174f, base.Projectile.timeLeft, clamped: true);
		if (base.Projectile.localAI[0] == 0f)
		{
			float initialSpeed = Main.rand.NextFloat(2.5f, 4.5f);
			for (int i = 0; i < 12; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust.velocity = ((float)Math.PI * 2f * (float)i / 12f).ToRotationVector2() * initialSpeed * Main.rand.NextFloat(0.6f, 1f);
				dust.velocity = dust.velocity.RotatedByRandom(0.3700000047683716);
				dust.scale = 1.25f;
				dust.color = Color.ForestGreen;
				dust.noGravity = true;
			}
			base.Projectile.localAI[0] = 1f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
		float initialSpeed = Main.rand.NextFloat(2.5f, 4.5f);
		for (int i = 0; i < 16; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
			dust.velocity = ((float)Math.PI * 2f * (float)i / 16f).ToRotationVector2() * initialSpeed;
			dust.scale = 1.25f;
			dust.color = Color.ForestGreen;
			dust.noGravity = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		int afterimageCount = ProjectileID.Sets.TrailCacheLength[base.Type];
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 origin = texture.Size() * 0.5f;
		for (int i = 0; i < afterimageCount; i++)
		{
			if (!(base.Projectile.oldPos[i] == Vector2.Zero))
			{
				float scaleFactor = MathHelper.Lerp(1f, 0.6f, (float)i / ((float)afterimageCount - 1f));
				Color drawColor = Color.Lerp(Color.LightGreen, Color.White, (float)i / ((float)afterimageCount - 1f));
				((Color)(ref drawColor)).A = (byte)(int)MathHelper.Lerp(105f, 0f, (float)i / ((float)afterimageCount - 1f));
				drawPosition -= base.Projectile.velocity.SafeNormalize(Vector2.Zero) * scaleFactor * 4.5f;
				Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, base.Projectile.rotation, origin, base.Projectile.scale * scaleFactor, (SpriteEffects)0);
			}
		}
		return false;
	}
}
