using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class Cyclone : ModProjectile, ILocalizedModType, IModType
{
	public int dustVortex;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 56;
		base.Projectile.height = 56;
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 2;
		base.Projectile.penetrate = 2;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.PreventTileCollisionUntilHitboxIsOutsideOfTiles(base.Projectile);
		base.Projectile.rotation += 2.5f;
		base.Projectile.alpha -= 5;
		base.Projectile.ai[1]++;
		if (base.Projectile.alpha < 50)
		{
			base.Projectile.alpha = 50;
			if (base.Projectile.ai[1] >= 15f)
			{
				for (int i = 1; i <= 6; i++)
				{
					Vector2 dustspeed = Utils.RotatedBy(new Vector2(3f, 3f), (double)MathHelper.ToRadians((float)dustVortex), default(Vector2));
					int d = Dust.NewDust(base.Projectile.Center, base.Projectile.width / 2, base.Projectile.height / 2, 31, dustspeed.X, dustspeed.Y, 200, new Color(232, 251, 250, 200), 1.3f);
					Main.dust[d].noGravity = true;
					Main.dust[d].velocity = dustspeed;
					dustVortex += 60;
				}
				dustVortex -= 355;
				base.Projectile.ai[1] = 0f;
			}
		}
		float num472 = base.Projectile.Center.X;
		float num473 = base.Projectile.Center.Y;
		float num474 = 600f;
		for (int j = 0; j < Main.maxNPCs; j++)
		{
			NPC npc = Main.npc[j];
			if (!npc.CanBeChasedBy(base.Projectile) || !Collision.CanHit(base.Projectile.Center, 1, 1, npc.Center, 1, 1) || CalamityPlayer.areThereAnyDamnBosses)
			{
				continue;
			}
			float npcCenterX = npc.position.X + (float)(npc.width / 2);
			float npcCenterY = npc.position.Y + (float)(npc.height / 2);
			if (Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcCenterX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcCenterY) < num474)
			{
				if (npc.position.X < num472)
				{
					npc.velocity.X += 0.05f;
				}
				else
				{
					npc.velocity.X -= 0.05f;
				}
				if (npc.position.Y < num473)
				{
					npc.velocity.Y += 0.05f;
				}
				else
				{
					npc.velocity.Y -= 0.05f;
				}
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(204, 255, 255, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item60 with
		{
			Volume = SoundID.Item60.Volume * 0.6f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i <= 360; i += 3)
		{
			Vector2 dustspeed = Utils.RotatedBy(new Vector2(3f, 3f), (double)MathHelper.ToRadians((float)i), default(Vector2));
			int d = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 31, dustspeed.X, dustspeed.Y, 200, new Color(232, 251, 250, 200), 1.4f);
			Main.dust[d].noGravity = true;
			Main.dust[d].position = base.Projectile.Center;
			Main.dust[d].velocity = dustspeed;
		}
	}
}
