using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ChromaticFlare : ModProjectile, ILocalizedModType, IModType
{
	public static int Lifetime = 90;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/ExtraTextures/SmallGreyscaleCircle";

	public static float MinimumSpeed => 4f;

	public static float MaximumSpeed => 24f;

	public static float HomingRange => 720f;

	public static float HomingSpeedMult => 1.6f;

	public static int StickDuration => 2;

	public static int MaxStick => 6;

	public static float StickyDamageFalloff => 0.5f;

	public bool StickToEnemies => base.Projectile.ai[2] == 1f;

	public bool StickToTiles => base.Projectile.ai[2] == 2f;

	public ref float StoredVelocity => ref base.Projectile.ai[0];

	public ref float Direction => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 18;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = Lifetime * base.Projectile.MaxUpdates;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		if (StickToTiles)
		{
			return;
		}
		if (StickToEnemies)
		{
			base.Projectile.ai[2] = 1f;
			base.Projectile.StickyProjAI(StickDuration * base.Projectile.MaxUpdates);
			return;
		}
		float effectiveVelocity = MathHelper.Clamp(StoredVelocity, MinimumSpeed, MaximumSpeed);
		int targetNPC = base.Projectile.FindTargetWithLineOfSight(HomingRange);
		if (targetNPC != -1 && base.Projectile.timeLeft < (Lifetime - 20) * base.Projectile.MaxUpdates)
		{
			Vector2 destination = Main.npc[targetNPC].Center;
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(destination) * effectiveVelocity * HomingSpeedMult;
			if (base.Projectile.Distance(destination) <= 160f)
			{
				return;
			}
		}
		Projectile projectile = base.Projectile;
		projectile.position += (Vector2.UnitY * MathF.Sin((float)base.Projectile.timeLeft * (float)Math.PI * 0.05f) * effectiveVelocity * Direction).RotatedBy(base.Projectile.velocity.ToRotation());
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 1200);
		if ((StickToEnemies || StickToTiles) && base.Projectile.damage > 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * StickyDamageFalloff);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (StickToEnemies)
		{
			base.Projectile.ModifyHitNPCSticky(MaxStick);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		if (StickToTiles || StickToEnemies)
		{
			base.Projectile.velocity = oldVelocity * 0.95f;
			Projectile projectile = base.Projectile;
			projectile.position -= base.Projectile.velocity;
			if (StickToEnemies)
			{
				base.Projectile.ai[2] = 2f;
				base.Projectile.timeLeft = StickDuration * 60 * base.Projectile.MaxUpdates;
			}
			return false;
		}
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 8; i++)
		{
			Vector2 smokeVel = Main.rand.NextVector2Circular(16f, 16f);
			Color smokeColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.8f);
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, smokeVel, smokeColor, Color.Black, Main.rand.NextFloat(0.6f, 1.6f), 200 - Main.rand.Next(60), 0.1f));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		Texture2D lightTexture = TextureAssets.Projectile[base.Type].Value;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			Color val = Main.hslToRgb(0.5f + 0.5f * (float)i / (float)base.Projectile.oldPos.Length * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f), 1f, 0.6f);
			Vector2 drawPosition = base.Projectile.oldPos[i] + lightTexture.Size() * 0.5f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY) + new Vector2(-15f, -15f);
			Color outerColor = val * 2f;
			Color innerColor = Color.Lerp(val, Color.White, 0.2f) * 0.5f;
			float intensity = 0.9f + 0.15f * MathF.Cos(Main.GlobalTimeWrappedHourly % 60f * ((float)Math.PI * 2f));
			intensity *= MathHelper.Lerp(0.15f, 1f, 1f - (float)i / (float)base.Projectile.oldPos.Length);
			Vector2 outerScale = new Vector2(1.25f) * intensity;
			Vector2 innerScale = new Vector2(1.25f) * intensity * 0.7f;
			outerColor *= intensity * base.Projectile.scale;
			innerColor *= intensity * base.Projectile.scale;
			Main.EntitySpriteDraw(lightTexture, drawPosition, null, outerColor, 0f, lightTexture.Size() * 0.5f, outerScale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(lightTexture, drawPosition, null, innerColor, 0f, lightTexture.Size() * 0.5f, innerScale * 0.6f, (SpriteEffects)0);
		}
		return false;
	}
}
