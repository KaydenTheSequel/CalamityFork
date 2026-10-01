using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class BendyPet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.LightPet[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 38;
		base.Projectile.height = 56;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.bendyPet = false;
		}
		if (modPlayer.bendyPet)
		{
			base.Projectile.timeLeft = 2;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		player.dangerSense = true;
		player.detectCreature = true;
		if (base.Projectile.localAI[0] == 0f)
		{
			Lighting.AddLight(base.Projectile.Center, 0.8118f, 0.2529f, 1.3294f);
		}
		else
		{
			Lighting.AddLight(base.Projectile.Center, 0.4329f, 0.1349f, 0.709f);
		}
		float idleMvt = 0.2f;
		float projSpeed = 5f;
		Vector2 projVec = player.Center - base.Projectile.Center;
		projVec.Y += player.gfxOffY;
		if (player.controlLeft)
		{
			projVec.X -= 120f;
		}
		else if (player.controlRight)
		{
			projVec.X += 120f;
		}
		if (player.controlDown)
		{
			projVec.Y += 120f;
		}
		else
		{
			if (player.controlUp)
			{
				projVec.Y -= 120f;
			}
			projVec.Y -= 60f;
		}
		float playerDist = ((Vector2)(ref projVec)).Length();
		if (playerDist > 1000f)
		{
			base.Projectile.position.X += projVec.X;
			base.Projectile.position.Y += projVec.Y;
		}
		if (base.Projectile.localAI[0] == 1f)
		{
			if (playerDist < 10f && Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) < projSpeed && player.velocity.Y == 0f)
			{
				base.Projectile.localAI[0] = 0f;
			}
			projSpeed = 12f;
			if (playerDist < projSpeed)
			{
				base.Projectile.velocity.X = projVec.X;
				base.Projectile.velocity.Y = projVec.Y;
			}
			else
			{
				playerDist = projSpeed / playerDist;
				base.Projectile.velocity.X = projVec.X * playerDist;
				base.Projectile.velocity.Y = projVec.Y * playerDist;
			}
			if (base.Projectile.velocity.X > 0.5f)
			{
				base.Projectile.direction = -1;
			}
			else if (base.Projectile.velocity.X < -0.5f)
			{
				base.Projectile.direction = 1;
			}
			base.Projectile.spriteDirection = base.Projectile.direction;
			base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
			return;
		}
		if (playerDist > 200f)
		{
			base.Projectile.localAI[0] = 1f;
		}
		if (base.Projectile.velocity.X > 0.5f)
		{
			base.Projectile.direction = -1;
		}
		else if (base.Projectile.velocity.X < -0.5f)
		{
			base.Projectile.direction = 1;
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		if (playerDist < 10f)
		{
			base.Projectile.velocity.X = projVec.X;
			base.Projectile.velocity.Y = projVec.Y;
			base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
			if (playerDist < projSpeed)
			{
				Projectile projectile = base.Projectile;
				projectile.position += base.Projectile.velocity;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0f;
				idleMvt = 0f;
			}
			base.Projectile.direction = -player.direction;
		}
		playerDist = projSpeed / playerDist;
		projVec.X *= playerDist;
		projVec.Y *= playerDist;
		if (base.Projectile.velocity.X < projVec.X)
		{
			base.Projectile.velocity.X += idleMvt;
			if (base.Projectile.velocity.X < 0f)
			{
				base.Projectile.velocity.X *= 0.99f;
			}
		}
		if (base.Projectile.velocity.X > projVec.X)
		{
			base.Projectile.velocity.X -= idleMvt;
			if (base.Projectile.velocity.X > 0f)
			{
				base.Projectile.velocity.X *= 0.99f;
			}
		}
		if (base.Projectile.velocity.Y < projVec.Y)
		{
			base.Projectile.velocity.Y += idleMvt;
			if (base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y *= 0.99f;
			}
		}
		if (base.Projectile.velocity.Y > projVec.Y)
		{
			base.Projectile.velocity.Y -= idleMvt;
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y *= 0.99f;
			}
		}
		if (base.Projectile.velocity.X != 0f || base.Projectile.velocity.Y != 0f)
		{
			base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, frameHeight, texture.Width, height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)height / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/BendyPetGlow", (AssetRequestMode)2).Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Color rainbow = CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly / 2f % 1f, new Color(255, 0, 0, 50), new Color(255, 255, 0, 50), new Color(0, 255, 0, 50), new Color(0, 255, 255, 50), new Color(0, 0, 255, 50), new Color(255, 0, 255, 50));
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, frameHeight, texture.Width, height), rainbow, base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)height / 2f), base.Projectile.scale, spriteEffects, 0f);
	}
}
