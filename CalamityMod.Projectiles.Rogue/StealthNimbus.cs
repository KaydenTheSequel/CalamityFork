using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class StealthNimbus : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Boss/ShadeNimbusHostile";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 54;
		base.Projectile.height = 24;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		if (base.Projectile.ai[0] == 1f)
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/StealthNimbus2", (AssetRequestMode)2).Value;
		}
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

	public override void AI()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 5)
			{
				base.Projectile.frame = 0;
			}
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] >= 3600f)
		{
			base.Projectile.alpha += 5;
			if (base.Projectile.alpha > 255)
			{
				base.Projectile.alpha = 255;
				base.Projectile.Kill();
			}
		}
		else if (base.Projectile.timeLeft % 8 == 0 && base.Projectile.owner == Main.myPlayer)
		{
			int rainSpawnX = (int)(base.Projectile.position.X + 14f + (float)Main.rand.Next(base.Projectile.width - 28));
			int rainSpawnY = (int)(base.Projectile.position.Y + (float)base.Projectile.height + 4f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), rainSpawnX, rainSpawnY, 0f, 5f, ModContent.ProjectileType<StealthRain>(), base.Projectile.damage, 0f, base.Projectile.owner, base.Projectile.ai[0]);
		}
		base.Projectile.localAI[0]++;
		if (!(base.Projectile.localAI[0] >= 10f))
		{
			return;
		}
		base.Projectile.localAI[0] = 0f;
		int projCount = 0;
		int oldestCloud = 0;
		float cloudAge = 0f;
		int projType = base.Projectile.type;
		for (int projIndex = 0; projIndex < Main.maxProjectiles; projIndex++)
		{
			Projectile proj = Main.projectile[projIndex];
			if (proj.active && proj.owner == base.Projectile.owner && proj.type == projType && proj.ai[1] < 3600f)
			{
				projCount++;
				if (proj.ai[1] > cloudAge)
				{
					oldestCloud = projIndex;
					cloudAge = proj.ai[1];
				}
			}
		}
		if (projCount >= 5)
		{
			Main.projectile[oldestCloud].netUpdate = true;
			Main.projectile[oldestCloud].ai[1] = 36000f;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
