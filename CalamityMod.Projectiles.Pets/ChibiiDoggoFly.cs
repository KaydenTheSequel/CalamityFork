using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class ChibiiDoggoFly : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 24;
		base.Projectile.height = 46;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.scale = 0.8f;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 1;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Color colorArea = Lighting.GetColor((int)(base.Projectile.Center.X / 16f), (int)(base.Projectile.Center.Y / 16f));
		Texture2D texture2D3 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/ChibiiDoggoFlyMonochrome", (AssetRequestMode)2).Value;
		int textureArea = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y3 = textureArea * base.Projectile.frame;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, y3, texture2D3.Width, textureArea);
		Vector2 origin2 = rectangle.Size() / 2f;
		int twelveCompare = 12;
		int twoConst = 2;
		for (int counter = 1; (twoConst > 0 && counter < twelveCompare) || (twoConst < 0 && counter > twelveCompare); counter += twoConst)
		{
			Color colorAlpha = colorArea;
			colorAlpha = base.Projectile.GetAlpha(colorAlpha);
			float trailColorChange = twelveCompare - counter;
			if (twoConst < 0)
			{
				trailColorChange = 1 - counter;
			}
			colorAlpha *= trailColorChange / ((float)ProjectileID.Sets.TrailCacheLength[base.Type] * 1.5f);
			Vector2 oldDrawPos = base.Projectile.oldPos[counter];
			float projRotate = base.Projectile.rotation;
			SpriteEffects effects = spriteEffects;
			Main.spriteBatch.Draw(texture2D3, oldDrawPos + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, colorAlpha, projRotate + base.Projectile.rotation * 0f * (float)(counter - 1) * (float)base.Projectile.spriteDirection, origin2, base.Projectile.scale, effects, 0f);
		}
		Main.spriteBatch.Draw(TextureAssets.Projectile[base.Type].Value, base.Projectile.position + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, lightColor, base.Projectile.rotation, origin2, base.Projectile.scale, spriteEffects, 0f);
		return false;
	}

	public override bool PreAI()
	{
		if (Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<ChibiiDoggoFly>()] > 1)
		{
			base.Projectile.hide = true;
			base.Projectile.Kill();
			Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<ChibiiDoggoFly>()]--;
			return false;
		}
		return true;
	}

	public override void AI()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (Main.player[base.Projectile.owner].Calamity().chibii)
		{
			base.Projectile.timeLeft = 2;
		}
		int byUuid = (int)base.Projectile.ai[0];
		if (Main.projectile[byUuid].active && Main.projectile[byUuid].type == ModContent.ProjectileType<ChibiiDoggo>())
		{
			base.Projectile.Center = Main.projectile[byUuid].Center;
			base.Projectile.rotation = Main.projectile[byUuid].rotation;
			base.Projectile.spriteDirection = Main.projectile[byUuid].spriteDirection;
			if (Main.projectile[byUuid].tileCollide)
			{
				base.Projectile.hide = true;
			}
			else
			{
				base.Projectile.hide = false;
			}
		}
	}
}
