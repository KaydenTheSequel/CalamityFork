using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AngelicAllianceArchangel : ModProjectile, ILocalizedModType, IModType
{
	private int lifeSpan = 900;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 68;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(lifeSpan);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		lifeSpan = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (!player.Calamity().divineBless || player.dead || !player.active)
		{
			lifeSpan = 0;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
			}
			base.Projectile.localAI[0]++;
		}
		double rad = (double)base.Projectile.ai[1] * (Math.PI / 180.0);
		double dist = 300.0;
		base.Projectile.position.X = player.Center.X - (float)(int)(Math.Cos(rad) * dist) - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = player.Center.Y - (float)(int)(Math.Sin(rad) * dist) - (float)(base.Projectile.height / 2);
		base.Projectile.ai[1]++;
		if (!base.Projectile.FinalExtraUpdate())
		{
			return;
		}
		lifeSpan--;
		if (lifeSpan <= 0)
		{
			base.Projectile.alpha += 30;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.Kill();
				return;
			}
		}
		float lightScalar = Main.rand.NextFloat(0.9f, 1.1f) * Main.essScale;
		Lighting.AddLight(base.Projectile.Center, 0.3f * lightScalar, 0.26f * lightScalar, 0.15f * lightScalar);
		NPC target = base.Projectile.Center.MinionHoming(2000f, player, ignoreTiles: false, checksRange: true);
		if (target != null)
		{
			Vector2 direction = target.Center - base.Projectile.Center;
			((Vector2)(ref direction)).Normalize();
			direction *= 6f;
			if (direction.X >= 0.25f)
			{
				base.Projectile.direction = -1;
			}
			else if (direction.X < -0.25f)
			{
				base.Projectile.direction = 1;
			}
			base.Projectile.ai[0]++;
			int timerLimit = 180;
			if (base.Projectile.ai[0] > (float)timerLimit && base.Projectile.alpha < 50)
			{
				if (Main.myPlayer == base.Projectile.owner)
				{
					int type = ModContent.ProjectileType<AngelRay>();
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, direction * 0.5f, type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
		}
		else
		{
			Vector2 direction2 = player.Center - base.Projectile.Center;
			((Vector2)(ref direction2)).Normalize();
			direction2 *= 6f;
			if (direction2.X >= 0.25f)
			{
				base.Projectile.direction = -1;
			}
			else if (direction2.X < -0.25f)
			{
				base.Projectile.direction = 1;
			}
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 7)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		_ = base.Projectile.spriteDirection;
		_ = -1;
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int frameY = frameHeight * base.Projectile.frame;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, frameY, texture.Width, frameHeight), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)frameHeight / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
