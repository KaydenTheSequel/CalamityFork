using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class OmnibladeSwing : ModProjectile, ILocalizedModType, IModType
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Omniblade>();

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 308;
		base.Projectile.height = 184;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 3;
	}

	public override void AI()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 3;
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.Kill();
		}
		Vector2 playerRotatedPoint = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (!Owner.CantUseHoldout())
			{
				HandleChannelMovement(playerRotatedPoint);
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.rotation = ((Owner.gravDir == -1f) ? ((float)Math.PI) : 0f);
		base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt();
		base.Projectile.spriteDirection = base.Projectile.direction * (int)Owner.gravDir;
		if (base.Projectile.direction == 1)
		{
			base.Projectile.Left = Owner.MountedCenter;
		}
		else
		{
			base.Projectile.Right = Owner.MountedCenter;
		}
		base.Projectile.position.X += ((base.Projectile.spriteDirection == 1) ? (-92f) : 96f) * Owner.gravDir;
		base.Projectile.position.Y -= 80f * Owner.gravDir;
		Owner.ChangeDir(base.Projectile.direction);
		base.Projectile.timeLeft = 2;
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
	}

	public void HandleChannelMovement(Vector2 playerRotatedPoint)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		Vector2 newVelocity = Vector2.UnitX * (float)(Main.MouseWorld.X > playerRotatedPoint.X).ToDirectionInt();
		if (base.Projectile.velocity.X != newVelocity.X || base.Projectile.velocity.Y != newVelocity.Y)
		{
			base.Projectile.netUpdate = true;
		}
		base.Projectile.velocity = newVelocity;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<WhisperingDeath>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<WhisperingDeath>(), 300);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 170);
	}

	public override bool? CanDamage()
	{
		return base.Projectile.frameCounter > 6;
	}
}
