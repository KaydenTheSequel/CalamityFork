using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CometQuasherMeteor : ModProjectile, ILocalizedModType, IModType
{
	public Color mainColor;

	public int fallTime;

	public NPC chosenTarget;

	public new string LocalizationCategory => "Projectiles.Melee";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 800;
		base.Projectile.penetrate = 1;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float num = Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (time % 5f == 0f && base.Projectile.extraUpdates < 12)
		{
			base.Projectile.extraUpdates++;
		}
		if (time == 0f)
		{
			chosenTarget = Owner.ClampedMouseWorld().ClosestNPCAt(700f);
			if (chosenTarget != null)
			{
				base.Projectile.velocity = (chosenTarget.Center - base.Projectile.Center + chosenTarget.velocity * 8f).SafeNormalize(Vector2.UnitX) * 3f;
			}
			else
			{
				base.Projectile.velocity = (Owner.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 3f;
			}
		}
		if (base.Projectile.numHits < 1)
		{
			if (chosenTarget == null || chosenTarget.life <= 0)
			{
				chosenTarget = Owner.ClampedMouseWorld().ClosestNPCAt(700f);
			}
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, chosenTarget, ignoreTiles: true, 0.08f, 5f, 0.99f, 0.95f, accelerate: true);
		}
		if (num < 1400f)
		{
			if (time % 11f == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(20f, 20f), -base.Projectile.velocity * 2f, affectedByGravity: false, 11, 0.03f, mainColor * Main.rand.NextFloat(0.7f, 1f), new Vector2(1f, 1f), quickShrink: true, glow: false, 0.6f));
			}
			if (time % 4f == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 1.5f), Main.rand.NextBool(4) ? Color.AliceBlue : Color.DodgerBlue, 6, Main.rand.NextFloat(0.4f, 0.9f), 0.65f, 0f, glowing: true));
			}
		}
		if (Main.rand.NextBool(13))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(20f, 20f), 278, -base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.2f, 3f));
			dust.scale = Main.rand.NextFloat(0.55f, 0.85f);
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool(3) ? Color.AliceBlue : Color.DodgerBlue);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		float num = base.Projectile.ai[1];
		if (num == 0f)
		{
			goto IL_0026;
		}
		Texture2D tex;
		if (num != 1f)
		{
			if (num != 2f)
			{
				goto IL_0026;
			}
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/CometQuasherMeteor3", (AssetRequestMode)2).Value;
		}
		else
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/CometQuasherMeteor2", (AssetRequestMode)2).Value;
		}
		goto IL_005e;
		IL_005e:
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.White, 1, tex);
		return false;
		IL_0026:
		tex = TextureAssets.Projectile[base.Type].Value;
		goto IL_005e;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.2f;
		int hitsToMinMult = 4;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
		base.Projectile.netUpdate = true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.netUpdate = true;
		Player Owner = Main.player[base.Projectile.owner];
		SoundEngine.PlaySound(in SoundID.Item89, base.Projectile.position);
		if (base.Projectile.ai[2] > 0f)
		{
			Vector2 spawnSpot = Owner.Center + new Vector2(Main.rand.NextFloat(-550f, 550f), Main.rand.NextFloat(-750f, -950f));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnSpot, Vector2.Zero, ModContent.ProjectileType<CometQuasherMeteor>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, base.Projectile.ai[2] - 1f);
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.7f);
			base.Projectile.ExpandHitboxBy((int)(128f * base.Projectile.scale));
			base.Projectile.penetrate = -1;
			base.Projectile.Damage();
		}
		for (int i = 0; i < 13; i++)
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(3.5f, 9f), affectedByGravity: false, 20, Main.rand.NextFloat(0.5f, 1f), Main.rand.NextBool(5) ? Color.AliceBlue : Color.DodgerBlue, AddativeBlend: true, needed: false, GlowCenter: false));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, mainColor * 0.7f, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 2f, 1f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * 0.7f, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 1f, 0.3f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public CometQuasherMeteor()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		mainColor = Color.DodgerBlue;
		fallTime = 60;
		base._002Ector();
	}
}
