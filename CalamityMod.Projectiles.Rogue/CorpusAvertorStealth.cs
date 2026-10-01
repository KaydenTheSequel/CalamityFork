using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CorpusAvertorStealth : ModProjectile, ILocalizedModType, IModType
{
	private Vector2 startPos;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/CorpusAvertor";

	private ref float Timer => ref base.Projectile.ai[0];

	private ref float Slash => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 4;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		if (startPos == Vector2.Zero)
		{
			startPos = base.Projectile.Center;
		}
		if (Timer < 120f)
		{
			Timer++;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 3f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.01f;
		if (Slash == 1f)
		{
			if (Timer > 4f)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, Vector2.Normalize(base.Projectile.velocity), affectedByGravity: false, 10, 0.055f, Color.DarkRed, new Vector2(0.66f, 1.5f), quickShrink: true, glow: false));
			}
		}
		else
		{
			int scale = (int)((Timer - 60f) * 4.25f);
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 5, 0f, 0f, 100, new Color(scale, 0, 0, 50), 2f);
			Dust obj = Main.dust[dust];
			obj.velocity *= 0f;
			Main.dust[dust].noGravity = true;
		}
		if (Timer == 60f && Slash == 0f)
		{
			SoundStyle style = CommonCalamitySounds.MeatySlashSound with
			{
				Volume = 0.4f,
				PitchVariance = 0.06f
			};
			SoundEngine.PlaySound(in style, startPos);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), startPos, base.Projectile.velocity, base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 1f);
			}
			base.Projectile.tileCollide = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (Slash == 1f)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
		if (target.IsAnEnemy(allowStatues: false) && base.Projectile.numHits == 0 && Slash == 0f)
		{
			Player player = Main.player[base.Projectile.owner];
			player.SpawnLifeStealProjectile(target, base.Projectile, 305, (int)Math.Round((double)hit.Damage * 0.025));
			if (Main.LocalPlayer.team == player.team && player.team != 0)
			{
				Main.LocalPlayer.AddBuff(ModContent.BuffType<AvertorBonus>(), CalamityUtils.SecondsToFrames(20f));
			}
		}
		if (base.Projectile.numHits > 0 && (target.life > 0 || target.realLife != -1))
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.9f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
	}

	public CorpusAvertorStealth()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		startPos = Vector2.Zero;
		base._002Ector();
	}
}
