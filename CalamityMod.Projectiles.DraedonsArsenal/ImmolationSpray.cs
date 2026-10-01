using System;
using CalamityMod.Effects;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class ImmolationSpray : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 15;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 3;
		base.Projectile.penetrate = 4;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (time > 5f && targetDist < 1400f)
		{
			base.Projectile.velocity.Y += 0.18f;
			base.Projectile.velocity.X *= 0.9835f;
			if (Main.rand.NextBool(3))
			{
				Vector2 relativePosition = base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f);
				float speed = Main.rand.NextFloat(0.2f, 0.7f);
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(relativePosition, -base.Projectile.velocity * speed, affectedByGravity: false, 7, Main.rand.NextFloat(0.4f, 0.7f), ArsenalEffects.ArsenalPlasmaColor));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalPlasmaDust, -base.Projectile.velocity);
				dust.scale = Main.rand.NextFloat(0.4f, 1.1f);
				dust.velocity = Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.1f, 0.7f);
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalPlasmaColor;
			}
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = HolofibreImmolator.PlasmaSound with
		{
			Volume = 0.5f,
			Pitch = Main.rand.NextFloat(-0.1f, 0.1f)
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.8f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		if (Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center) < 1400f)
		{
			Vector2 vel = oldVelocity.SafeNormalize(Vector2.UnitX);
			int dustStyle = ArsenalEffects.ArsenalPlasmaDust;
			for (int i = 0; i < 6; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + vel * 5f, dustStyle, (-vel * Main.rand.NextFloat(5f, 8f)).RotatedByRandom(0.699999988079071));
				dust.scale = Main.rand.NextFloat(0.7f, 1.3f);
				dust.noGravity = false;
				dust.color = ArsenalEffects.ArsenalPlasmaColor;
				dust.fadeIn = 1.2f;
			}
		}
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if (time < 1f)
		{
			return false;
		}
		Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2);
		float squash = Utils.GetLerpValue(-3f, 10f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
		for (int i = 0; i < 2; i++)
		{
			Texture2D value = tex.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition;
			Color arsenalPlasmaColor = ArsenalEffects.ArsenalPlasmaColor;
			((Color)(ref arsenalPlasmaColor)).A = 0;
			Main.EntitySpriteDraw(value, position, null, arsenalPlasmaColor * 0.6f, base.Projectile.rotation, tex.Size() * 0.5f, new Vector2(0.4f, squash) * 0.045f * ((i == 0) ? 0.6f : 1f), (SpriteEffects)0);
		}
		return false;
	}
}
