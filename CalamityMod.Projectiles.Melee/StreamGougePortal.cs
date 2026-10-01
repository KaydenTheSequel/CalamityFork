using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class StreamGougePortal : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle SpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/SwiftSlice")
	{
		PitchVariance = 0.1f
	};

	public new string LocalizationCategory => "Projectiles.Melee";

	public Vector2 SpearCenter
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			return Vector2.Lerp(base.Projectile.Center + base.Projectile.velocity + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 42f, base.Projectile.Center, 1f - base.Projectile.scale);
		}
	}

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 30;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			float volume = Utils.Remap(Main.LocalPlayer.Distance(base.Projectile.Center), 600f, 100f, 0.12f, 0.5f);
			SoundStyle style = SpawnSound with
			{
				Volume = volume
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.localAI[0] = 1f;
		}
		float spearOutwardness = MathHelper.Lerp(4f, 52f, Utils.GetLerpValue(0f, 18f, Time, clamped: true));
		base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * spearOutwardness;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 8f, Time, clamped: true) * Utils.GetLerpValue(0f, 10f, base.Projectile.timeLeft, clamped: true);
		base.Projectile.scale = base.Projectile.Opacity;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		Time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/StreamGouge", (AssetRequestMode)2).Value;
		Vector2 spearDrawPosition = SpearCenter - Main.screenPosition;
		Vector2 spearOrigin = texture.Size() * 0.5f;
		Color spearColor = Main.hslToRgb((float)base.Projectile.identity % 10f / 40f + 0.67f, 1f, 0.5f);
		((Color)(ref spearColor)).A = 0;
		for (int i = 0; i < 2; i++)
		{
			Main.EntitySpriteDraw(texture, spearDrawPosition, null, spearColor * base.Projectile.Opacity, base.Projectile.rotation, spearOrigin, base.Projectile.scale, (SpriteEffects)0);
		}
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/StreamGougePortal", (AssetRequestMode)2).Value;
		Vector2 origin = value.Size() * 0.5f;
		Color baseColor = Color.White;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float rotation = Main.GlobalTimeWrappedHourly * 6f + (float)base.Projectile.identity * 1.45f;
		Color color = Color.Lerp(baseColor, Color.Black, 0.55f).MultiplyRGB(Color.DarkGray) * base.Projectile.Opacity;
		Color color2 = color;
		((Color)(ref color2)).A = 0;
		Main.EntitySpriteDraw(value, drawPosition, null, color2, rotation, origin, base.Projectile.scale * 1.2f, (SpriteEffects)0);
		color2 = color;
		((Color)(ref color2)).A = 0;
		Main.EntitySpriteDraw(value, drawPosition, null, color2, 0f - rotation, origin, base.Projectile.scale * 1.2f, (SpriteEffects)0);
		color = Color.Lerp(baseColor, Color.Cyan, 0.55f) * base.Projectile.Opacity * 1.4f;
		color2 = color;
		((Color)(ref color2)).A = 0;
		Main.EntitySpriteDraw(value, drawPosition, null, color2, rotation * 0.6f, origin, base.Projectile.scale * 1.2f, (SpriteEffects)0);
		color = Color.Lerp(baseColor, Color.Fuchsia, 0.55f) * base.Projectile.Opacity * 1.4f;
		color2 = color;
		((Color)(ref color2)).A = 0;
		Main.EntitySpriteDraw(value, drawPosition, null, color2, rotation * -0.7f, origin, base.Projectile.scale * 1.2f, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 300);
		SoundEngine.PlaySound(in SoundID.Item74, target.Center);
		if (!Main.dedServ)
		{
			Color impactColor = Color.Lerp(Color.Cyan, Color.White, Main.rand.NextFloat(0.3f, 0.64f));
			GeneralParticleHandler.SpawnParticle(new ImpactParticle(Vector2.Lerp(base.Projectile.Center, target.Center, 0.65f), 0.1f, 20, Main.rand.NextFloat(0.4f, 0.5f), impactColor));
			for (int i = 0; i < 20; i++)
			{
				StreamGougeMetaball.SpawnParticle(target.Center + Main.rand.NextVector2Circular(30f, 30f), Main.rand.NextVector2Circular(3f, 3f), 60f);
				float scale = MathHelper.Lerp(24f, 64f, CalamityUtils.Convert01To010((float)i / 19f));
				Vector2 position = target.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * MathHelper.Lerp(-40f, 90f, (float)i / 19f);
				Vector2 particleVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.23000000417232513) * Main.rand.NextFloat(2.5f, 9f);
				StreamGougeMetaball.SpawnParticle(position, particleVelocity, scale);
			}
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		float _ = 0f;
		Vector2 start = SpearCenter - base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * base.Projectile.scale * 50f;
		Vector2 end = SpearCenter + base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * base.Projectile.scale * 50f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 15f, ref _);
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}
}
