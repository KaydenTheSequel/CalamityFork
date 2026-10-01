using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class Orbacle : ModProjectile, ILocalizedModType, IModType
{
	private static int Lifetime = 40;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/ExtraTextures/TinyGreyscaleCircle";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 origin = value.Size() * 0.5f;
		Color color = Color.Goldenrod;
		Main.EntitySpriteDraw(value, drawPosition, null, Color.Gold * 0.2f, base.Projectile.rotation, origin, base.Projectile.scale * 1.6f, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, drawPosition, null, color, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override void AI()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		int dustType = (Main.rand.NextBool(3) ? 244 : 246);
		float scale = 0.8f + Main.rand.NextFloat(0.6f);
		int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType);
		Main.dust[idx].noGravity = true;
		Main.dust[idx].velocity = base.Projectile.velocity / 3f;
		Main.dust[idx].scale = scale;
		if (base.Projectile.timeLeft < 38)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.85f;
		}
		if (base.Projectile.timeLeft == 1)
		{
			for (int i = 0; i < 14; i++)
			{
				dustType = (Main.rand.NextBool(3) ? 244 : 246);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustType, Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.5f, 0.9f));
				dust.noGravity = true;
				dust.scale = 0.6f;
			}
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.Gold, new Vector2(1f, 1f), 0f, 0f, 0.4f, 8));
		}
	}
}
