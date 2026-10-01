using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class Valkyrie : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 90;
		base.Projectile.height = 90;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.valkyrie)
		{
			base.Projectile.active = false;
			return;
		}
		if (base.Projectile.type == ModContent.ProjectileType<Valkyrie>())
		{
			if (player.dead)
			{
				modPlayer.aValkyrie = false;
			}
			if (modPlayer.aValkyrie)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 30; i++)
			{
				int spawnDust = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 59);
				Dust obj = Main.dust[spawnDust];
				obj.velocity *= 2f;
				Main.dust[spawnDust].scale *= 1.15f;
			}
			base.Projectile.localAI[0]++;
		}
		if (Math.Abs(base.Projectile.velocity.X) > 0.2f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
		base.Projectile.ChargingMinionAI(700f, 800f, 1200f, 150f, 0, 40f, 8f, 4f, new Vector2(0f, -60f), 40f, 8f, tileVision: false, ignoreTilesWhenCharging: true);
		float lightScalar = (float)Main.rand.Next(90, 111) * 0.01f;
		lightScalar *= Main.essScale;
		Lighting.AddLight(base.Projectile.Center, 0f * lightScalar, 0.2f * lightScalar, 0.45f * lightScalar);
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)framing / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}
}
