using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.MirrorofKalandraMinions;

public class Starforge : ModProjectile, ILocalizedModType, IModType
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

	public ref float TimerToBoom => ref base.Projectile.ai[0];

	public ref float DrawSpin => ref base.Projectile.ai[1];

	public ref float Oscillation => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 12000;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.minionSlots = 1f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = MirrorofKalandra.Purple_IFrames;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.width = (base.Projectile.height = 92);
		base.Projectile.minion = true;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		CheckMinionExistence();
		if (Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Main.rand.NextVector2Circular(base.Projectile.width / 2, base.Projectile.height / 2), Main.rand.NextVector2Circular(4f, 4f), Color.DarkMagenta, Color.Magenta, Main.rand.NextFloat(0.9f, 1.1f), Main.rand.NextFloat(140f, 150f)));
			int flavorDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 272, 0f, 0f, 0, default(Color), 0.6f);
			Main.dust[flavorDust].noGravity = true;
		}
		if (Target != null)
		{
			float distanceToTarget = base.Projectile.Distance(Target.Center) + 0.01f;
			base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2() * (MirrorofKalandra.Purple_MinRamSpeed + 12f / (distanceToTarget * 0.01f));
			base.Projectile.velocity = Vector2.Clamp(base.Projectile.velocity, Vector2.One * (0f - MirrorofKalandra.Purple_MaxRamSpeed), Vector2.One * MirrorofKalandra.Purple_MaxRamSpeed);
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(Target.Center), 0.001f * distanceToTarget);
			TimerToBoom++;
		}
		else if (Target == null)
		{
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, Owner.Center + base.Projectile.rotation.ToRotationVector2() * (MirrorofKalandra.IdleDistanceFromPlayer + MirrorofKalandra.IdleDistanceFromPlayer * (MathF.Sin(Oscillation) / MirrorofKalandra.OscillationRange)), 0.4f);
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp((float)Math.PI * -2f / 3f, 0.15f);
			Oscillation += MirrorofKalandra.OscillationSpeed;
		}
	}

	public void CheckMinionExistence()
	{
		Owner.AddBuff(ModContent.BuffType<KalandraMirrorBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<Starforge>())
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
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (TimerToBoom >= MirrorofKalandra.Purple_BlastFireRate && Main.myPlayer == base.Projectile.owner)
		{
			int blast = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<StarforgeBlast>(), (int)((float)base.Projectile.damage * MirrorofKalandra.Purple_BlastDMGModifier), base.Projectile.knockBack, Owner.whoAmI);
			if (Main.projectile.IndexInRange(blast))
			{
				Main.projectile[blast].originalDamage = (int)((float)base.Projectile.originalDamage / MirrorofKalandra.Purple_BlastDMGModifier);
			}
			TimerToBoom = 0f;
		}
		GeneralParticleHandler.SpawnParticle(new SparkParticle(Vector2.Lerp(base.Projectile.Center, target.Center, 0.8f), -base.Projectile.velocity * 0.01f, affectedByGravity: false, 20, Main.rand.NextFloat(1.2f, 1.8f), Color.Purple));
		SoundStyle soundStyle = CommonCalamitySounds.SwiftSliceSound with
		{
			PitchVariance = 0.5f
		};
		SoundEngine.PlaySound(soundStyle with
		{
			Volume = 0.2f
		}, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		DrawSpin -= MathHelper.ToRadians(MirrorofKalandra.Purple_SpinSpeed);
		float rotation = ((Target != null) ? DrawSpin : (base.Projectile.rotation + (float)Math.PI - (float)Math.PI / 4f));
		if (CalamityClientConfig.Instance.Afterimages && Target != null)
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				Color purple = Color.Purple;
				((Color)(ref purple)).A = 125;
				Color afterimageDrawColor = purple * base.Projectile.Opacity * (1f - (float)i / (float)base.Projectile.oldPos.Length);
				Vector2 afterimageDrawPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
				Main.EntitySpriteDraw(texture, afterimageDrawPosition, frame, afterimageDrawColor, rotation, origin, base.Projectile.scale, (SpriteEffects)1);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), rotation, origin, base.Projectile.scale, (SpriteEffects)1);
		return false;
	}
}
