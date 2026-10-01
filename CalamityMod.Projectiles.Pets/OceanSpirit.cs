using CalamityMod.CalPlayer;
using CalamityMod.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class OceanSpirit : ModProjectile, ILocalizedModType, IModType
{
	private bool underwater;

	private int sleepyTimer;

	private int lightLevel;

	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 17;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.LightPet[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 38;
		base.Projectile.height = 58;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.sirenPet = false;
		}
		if (modPlayer.sirenPet)
		{
			base.Projectile.timeLeft = 2;
		}
		bool sleepy = sleepyTimer >= 180;
		if (!sleepy)
		{
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 6)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
		}
		if (underwater)
		{
			if (base.Projectile.frame >= 8)
			{
				base.Projectile.frame = 0;
			}
		}
		else if (base.Projectile.frame >= 16)
		{
			base.Projectile.frame = (sleepy ? 16 : 8);
		}
		underwater = player.Calamity().countsAsAnyWet;
		if (underwater)
		{
			if (Main.LocalPlayer.Calamity().ZoneAbyss)
			{
				EnhancedDarknessSystem.lights.Add(new EnhancedDarknessSystem.LightSource
				{
					center = base.Projectile.Center,
					rotation = 0f,
					scale = 6f,
					texture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value
				});
			}
			if (base.Projectile.frame == 16)
			{
				base.Projectile.frame = 0;
			}
			if (sleepyTimer > 0)
			{
				sleepyTimer--;
			}
			if (base.Projectile.localAI[0] == 0f)
			{
				lightLevel = 0;
			}
			else
			{
				lightLevel = 1;
			}
		}
		else if (!sleepy)
		{
			sleepyTimer++;
			lightLevel = 1;
		}
		else
		{
			lightLevel = 2;
			base.Projectile.frame = 16;
		}
		switch (lightLevel)
		{
		case 0:
			Lighting.AddLight(base.Projectile.Center, 0f, 2f, 2.5f);
			break;
		case 1:
			Lighting.AddLight(base.Projectile.Center, 0f, 1.32f, 1.65f);
			break;
		case 2:
			Lighting.AddLight(base.Projectile.Center, 0f, 0.5f, 0.7f);
			break;
		}
		float velAdjustment = 0.2f;
		float speedLimit = 5f;
		Vector2 playerVec = player.Center - base.Projectile.Center;
		playerVec.Y += player.gfxOffY;
		if (player.controlLeft && !sleepy)
		{
			playerVec.X -= 120f;
		}
		else if (player.controlRight && !sleepy)
		{
			playerVec.X += 120f;
		}
		if (player.controlDown && !sleepy)
		{
			playerVec.Y += 120f;
		}
		else
		{
			if (player.controlUp && !sleepy)
			{
				playerVec.Y -= 120f;
			}
			playerVec.Y -= 60f;
		}
		if (base.Projectile.velocity.X < -0.25f || (player.controlLeft && !sleepy))
		{
			base.Projectile.direction = -1;
		}
		else if (base.Projectile.velocity.X > 0.25f || (player.controlRight && !sleepy))
		{
			base.Projectile.direction = 1;
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		float playerDist = ((Vector2)(ref playerVec)).Length();
		if (playerDist > 1000f)
		{
			base.Projectile.position.X += playerVec.X;
			base.Projectile.position.Y += playerVec.Y;
		}
		if (base.Projectile.localAI[0] == 1f)
		{
			if (playerDist < 10f && ((Vector2)(ref player.velocity)).Length() < speedLimit && player.velocity.Y == 0f)
			{
				base.Projectile.localAI[0] = 0f;
			}
			speedLimit = 12f;
			if (playerDist < speedLimit)
			{
				base.Projectile.velocity = playerVec;
			}
			else
			{
				playerDist = speedLimit / playerDist;
				base.Projectile.velocity = playerVec * playerDist;
			}
			base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
			return;
		}
		if (playerDist > 200f)
		{
			base.Projectile.localAI[0] = 1f;
		}
		if (playerDist < 10f)
		{
			base.Projectile.velocity.X = playerVec.X;
			base.Projectile.velocity.Y = playerVec.Y;
			base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
			if (playerDist < speedLimit)
			{
				Projectile projectile = base.Projectile;
				projectile.position += base.Projectile.velocity;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0f;
				velAdjustment = 0f;
			}
		}
		playerDist = speedLimit / playerDist;
		playerVec *= playerDist;
		if (base.Projectile.velocity.X < playerVec.X)
		{
			base.Projectile.velocity.X += velAdjustment;
			if (base.Projectile.velocity.X < 0f)
			{
				base.Projectile.velocity.X *= 0.99f;
			}
		}
		if (base.Projectile.velocity.X > playerVec.X)
		{
			base.Projectile.velocity.X -= velAdjustment;
			if (base.Projectile.velocity.X > 0f)
			{
				base.Projectile.velocity.X *= 0.99f;
			}
		}
		if (base.Projectile.velocity.Y < playerVec.Y)
		{
			base.Projectile.velocity.Y += velAdjustment;
			if (base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y *= 0.99f;
			}
		}
		if (base.Projectile.velocity.Y > playerVec.Y)
		{
			base.Projectile.velocity.Y -= velAdjustment;
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
}
