using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class BitingEmbraceMist : ModProjectile, ILocalizedModType, IModType
{
	public Color mistColor;

	public int variant;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Particles/MediumMist";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 34);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		Vector2 size = base.Projectile.Size * base.Projectile.scale;
		return Collision.CheckAABBvAABBCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center - size / 2f, size);
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		mistColor = Main.hslToRgb(Main.rand.NextFloat(0.5f, 0.8f), 1f, 0.8f);
		variant = Main.rand.Next(3);
	}

	public override void AI()
	{
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(15) && base.Projectile.alpha <= 140)
		{
			Vector2 particlePosition = base.Projectile.Center + Main.rand.NextVector2Circular((float)base.Projectile.width * base.Projectile.scale * 0.5f, (float)base.Projectile.height * base.Projectile.scale * 0.5f);
			if (Main.rand.NextBool())
			{
				GeneralParticleHandler.SpawnParticle(new SnowflakeSparkle(particlePosition, Vector2.Zero, Color.White, new Color(75, 177, 250), Main.rand.NextFloat(0.3f, 1.5f), 40, 0.5f));
			}
			else
			{
				float scale = Main.rand.NextFloat(0.5f, 1.8f);
				GeneralParticleHandler.SpawnParticle(new CritSpark(particlePosition, Vector2.Zero, Color.White, Color.Indigo, scale, 30, 0.5f, scale * 2f));
			}
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.85f;
		Projectile projectile2 = base.Projectile;
		projectile2.position += base.Projectile.velocity;
		base.Projectile.rotation += 0.02f * (float)base.Projectile.timeLeft / 300f * ((base.Projectile.velocity.X > 0f) ? 1f : (-1f));
		if (base.Projectile.alpha < 165)
		{
			base.Projectile.scale += 0.05f;
			base.Projectile.alpha += 2;
		}
		else
		{
			base.Projectile.scale *= 0.975f;
			base.Projectile.alpha++;
		}
		if (base.Projectile.alpha >= 170)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Particles/MediumMist", (AssetRequestMode)2).Value;
		Rectangle frame = value.Frame(1, 3, 0, variant);
		Main.EntitySpriteDraw(value, base.Projectile.position - Main.screenPosition, frame, mistColor * 0.5f * ((255f - (float)base.Projectile.alpha) / 255f), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
