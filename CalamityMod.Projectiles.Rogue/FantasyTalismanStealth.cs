using System;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class FantasyTalismanStealth : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Rogue/FantasyTalismanProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = 1;
		base.AIType = 242;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 175, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		base.Projectile.StickyProjAI(4);
		if (base.Projectile.ai[0] == 1f && base.Projectile.timeLeft % 20 == 0)
		{
			Projectile ghost = CalamityUtils.SpawnOrb(base.Projectile, (int)((float)base.Projectile.damage * 0.3f), 356, 1000f, 4f);
			if (ghost.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				ghost.DamageType = RogueDamageClass.Instance;
				ghost.penetrate = 1;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		base.Projectile.ModifyHitNPCSticky(6);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int j = 0; j <= 3; j++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(12f, 12f), ModContent.ProjectileType<LostSoulFriendly>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
		{
			((Rectangle)(ref targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return null;
	}
}
