using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.MirrorofKalandraMinions;

public class WindRipper : ModProjectile, ILocalizedModType, IModType
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

	public ref float Oscillation => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		Main.projFrames[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.Projectile.minionSlots = 1f;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.width = (base.Projectile.height = 84);
		base.Projectile.minion = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		CheckMinionExistence();
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, Owner.Center + ((float)Math.PI * -7f / 8f).ToRotationVector2() * (MirrorofKalandra.IdleDistanceFromPlayer + MirrorofKalandra.IdleDistanceFromPlayer * (MathF.Sin(Oscillation) / MirrorofKalandra.OscillationRange)), 0.4f);
		base.Projectile.velocity = Vector2.Zero;
		Oscillation += MirrorofKalandra.OscillationSpeed;
		if (Target != null)
		{
			DoAnimation();
			ShootTarget();
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(base.Projectile.Center, Target, MirrorofKalandra.Wind_ArrowSpeed, MirrorofKalandra.Wind_ArrowSpeedMult).ToRotation(), 0.2f);
		}
		else
		{
			base.Projectile.frame = 0;
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards((float)Math.PI * -7f / 8f, 0.2f);
		}
	}

	public void DoAnimation()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % MirrorofKalandra.Wind_BowChargeTime == 0)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
	}

	public void ShootTarget()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.frame == 5 && base.Projectile.frameCounter % MirrorofKalandra.Wind_BowChargeTime == 0 && Main.myPlayer == base.Projectile.owner)
		{
			_ = base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * (float)(base.Projectile.width / 2);
			int arrow = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(base.Projectile.Center, Target, MirrorofKalandra.Wind_ArrowSpeed, MirrorofKalandra.Wind_ArrowSpeedMult), ModContent.ProjectileType<WindRipperArrow>(), base.Projectile.damage, base.Projectile.knockBack, Owner.whoAmI);
			if (Main.projectile.IndexInRange(arrow))
			{
				Main.projectile[arrow].originalDamage = base.Projectile.originalDamage;
			}
			SoundEngine.PlaySound(in SoundID.Item5, Owner.Center);
			base.Projectile.netUpdate = true;
		}
	}

	public void CheckMinionExistence()
	{
		Owner.AddBuff(ModContent.BuffType<KalandraMirrorBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<WindRipper>())
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

	public override bool? CanDamage()
	{
		return false;
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
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(origin: frame.Size() * 0.5f, texture: value, position: drawPosition, sourceRectangle: frame, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		return false;
	}
}
