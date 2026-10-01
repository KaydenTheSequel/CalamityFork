using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.MirrorofKalandraMinions;

public class AtzirisDisfavor : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer ModdedOwner => Owner.Calamity();

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center.MinionHoming(MirrorofKalandra.TargetDistanceDetection, Owner);
		}
	}

	public ref float DrawSpin => ref base.Projectile.ai[0];

	public ref float Oscillation => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 12000;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.minionSlots = 1f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = MirrorofKalandra.Axe_IFrames;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.width = (base.Projectile.height = 112);
		base.Projectile.minion = true;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		CheckMinionExistence();
		if (Main.rand.NextBool(3))
		{
			int flavorDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 228, 0f, 0f, 0, default(Color), 1.2f);
			Main.dust[flavorDust].noGravity = true;
		}
		if (Target != null)
		{
			float distanceToTarget = base.Projectile.Distance(Target.Center) + 0.01f;
			base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2() * (MirrorofKalandra.Axe_MinRamSpeed + 12f / (distanceToTarget * 0.01f));
			base.Projectile.velocity = Vector2.Clamp(base.Projectile.velocity, Vector2.One * (0f - MirrorofKalandra.Axe_MaxRamSpeed), Vector2.One * MirrorofKalandra.Axe_MaxRamSpeed);
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(Target.Center), 0.001f * distanceToTarget);
			GeneralParticleHandler.SpawnParticle(new SemiCircularSmearVFX(base.Projectile.Center + (DrawSpin + (float)Math.PI).ToRotationVector2() * 10f, Color.OrangeRed, DrawSpin + (float)Math.PI / 2f, base.Projectile.scale * 0.8f, Vector2.One));
			if (base.Projectile.soundDelay <= 0)
			{
				SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/LoudSwingWoosh");
				soundStyle.Pitch = -0.8f;
				soundStyle.PitchVariance = 1f;
				soundStyle.Volume = 0.6f;
				SoundStyle swing = soundStyle;
				SoundEngine.PlaySound(in swing, base.Projectile.Center);
				base.Projectile.soundDelay = 15;
				base.Projectile.netUpdate = true;
			}
		}
		else if (Target == null)
		{
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, Owner.Center + base.Projectile.rotation.ToRotationVector2() * (MirrorofKalandra.IdleDistanceFromPlayer + MirrorofKalandra.IdleDistanceFromPlayer * (MathF.Sin(Oscillation) / MirrorofKalandra.OscillationRange)), 0.3f);
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(-(float)Math.PI / 2f, 0.1f);
			Oscillation += MirrorofKalandra.OscillationSpeed;
		}
	}

	public void CheckMinionExistence()
	{
		Owner.AddBuff(ModContent.BuffType<KalandraMirrorBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<AtzirisDisfavor>())
		{
			if (Owner.dead)
			{
				ModdedOwner.KalandraMirror = false;
			}
			if (ModdedOwner.KalandraMirror)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new ImpactParticle(Vector2.Lerp(base.Projectile.Center, target.Center, 0.8f), (float)Main.rand.NextBool().ToDirectionInt() * Main.rand.NextFloat(0.08f, 0.12f), 20, 1f, Color.Gold));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		DrawSpin += MathHelper.ToRadians(MirrorofKalandra.Axe_SpinSpeed);
		Main.EntitySpriteDraw(rotation: (Target != null) ? DrawSpin : (base.Projectile.rotation + (float)Math.PI / 4f), texture: value, position: drawPosition, sourceRectangle: frame, color: base.Projectile.GetAlpha(lightColor), origin: origin, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		return false;
	}
}
