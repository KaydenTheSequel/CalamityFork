using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class IceClasperMinion : BaseMinionProjectile
{
	public enum AIState
	{
		Follow,
		Ram
	}

	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<IceClasperMinion>();

	public override int AssociatedBuffTypeID => ModContent.BuffType<IceClasperBuff>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.IceClasperBool;

	public AIState State
	{
		get
		{
			return (AIState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
			SyncVariables();
		}
	}

	public ref float TimerForShooting => ref base.Projectile.ai[1];

	public ref float AfterimageInterpolant => ref base.Projectile.localAI[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		base.SetStaticDefaults();
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.coldDamage = true;
		base.Projectile.width = (base.Projectile.height = 62);
	}

	public override void MinionAI()
	{
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		switch (State)
		{
		case AIState.Follow:
			FollowState();
			break;
		case AIState.Ram:
			RamState();
			break;
		}
		base.Projectile.MinionAntiClump(0.5f);
		if (!Main.dedServ)
		{
			Color newColor;
			if (Main.rand.NextBool(10))
			{
				Vector2 center = base.Projectile.Center;
				Vector2? velocity = -base.Projectile.rotation.ToRotationVector2().RotatedByRandom(1.5707963705062866) * Main.rand.NextFloat(2f, 3f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(center, 56, velocity, 0, newColor);
				dust.customData = false;
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
			Vector2 center2 = base.Projectile.Center;
			newColor = Color.Cyan;
			Lighting.AddLight(center2, ((Color)(ref newColor)).ToVector3());
		}
	}

	public void FollowState()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.WithinRange(base.Owner.Center, 1200f))
		{
			base.Projectile.Center = base.Owner.Center;
			SyncVariables();
		}
		else if (!base.Projectile.WithinRange(base.Owner.Center, AncientIceChunk.MaxDistanceFromOwner))
		{
			base.Projectile.velocity = (base.Projectile.velocity + base.Projectile.SafeDirectionTo(base.Owner.Center)) * 0.9f;
			SyncVariables();
		}
		if (base.Target != null)
		{
			if (base.Owner.WithinRange(base.Target.Center, AncientIceChunk.DistanceToDash))
			{
				State = AIState.Ram;
			}
			else
			{
				ShootTarget();
			}
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(base.Target.Center), 0.15f);
		}
		else
		{
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.velocity.ToRotation(), 0.15f);
		}
	}

	public void RamState()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null && base.Owner.WithinRange(base.Projectile.Center, AncientIceChunk.DistanceToStopDash))
		{
			float distanceToTarget = base.Projectile.Distance(base.Target.Center) + 0.01f;
			base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2() * (AncientIceChunk.MinVelocity + 12f / (distanceToTarget * 0.01f));
			base.Projectile.velocity = Vector2.Clamp(base.Projectile.velocity, Vector2.One * -25f, Vector2.One * 25f);
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(base.Target.Center), 0.001f * distanceToTarget);
		}
		else
		{
			State = AIState.Follow;
		}
	}

	public void ShootTarget()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		TimerForShooting++;
		if (TimerForShooting >= AncientIceChunk.TimeToShoot && base.Projectile.owner == Main.myPlayer)
		{
			Vector2 velocity = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, base.Target, 25f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<IceClasperSummonProjectile>(), (int)((float)base.Projectile.damage * AncientIceChunk.ProjectileDMGMultiplier), base.Projectile.knockBack, base.Projectile.owner);
			Projectile projectile = base.Projectile;
			projectile.velocity -= velocity * 0.1f;
			if (!Main.dedServ)
			{
				SoundEngine.PlaySound(in SoundID.Item28, base.Projectile.Center);
			}
			TimerForShooting = 0f;
			SyncVariables();
		}
	}

	public void SyncVariables()
	{
		base.Projectile.ForceNetUpdate(ignoreCurrentNetSpam: false);
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		base.IFrames = AncientIceChunk.IFrames;
		base.TrailingMode = 2;
		base.TrailCacheLength = 6;
		if (!Main.dedServ)
		{
			int dustAmount = 45;
			for (int dustIndex = 0; dustIndex < dustAmount; dustIndex++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)dustIndex).ToRotationVector2() * Main.rand.NextFloat(3f, 7f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 56, velocity);
				dust.customData = false;
				dust.noGravity = true;
				dust.velocity *= 0.75f;
				dust.scale = ((Vector2)(ref velocity)).Length() * 0.2f;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return State == AIState.Ram;
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
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		AfterimageInterpolant += (((base.Target != null && base.Owner.WithinRange(base.Target.Center, 450f)) || State == AIState.Ram) ? 0.05f : (-0.05f));
		AfterimageInterpolant = MathHelper.Clamp(AfterimageInterpolant, 0f, 1f);
		float AfterimageFade = MathHelper.Lerp(0f, 1f, AfterimageInterpolant);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			Color val = default(Color);
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				((Color)(ref val))._002Ector(0.05f, 0.33f, 0.63f);
				((Color)(ref val)).A = 25;
				Color afterimageDrawColor = val * base.Projectile.Opacity * (1f - (float)i / (float)base.Projectile.oldPos.Length) * AfterimageFade;
				Vector2 afterimageDrawPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
				Main.EntitySpriteDraw(texture, afterimageDrawPosition, frame, afterimageDrawColor, base.Projectile.rotation - (float)Math.PI / 2f, origin, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation - (float)Math.PI / 2f, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
