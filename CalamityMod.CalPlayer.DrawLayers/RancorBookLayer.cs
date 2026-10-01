using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class RancorBookLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.ArmOverItem);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		Player drawPlayer = drawInfo.drawPlayer;
		if (drawInfo.shadow != 0f || drawPlayer.dead)
		{
			return false;
		}
		if (drawPlayer.heldProj != -1 && Main.projectile[drawPlayer.heldProj].active)
		{
			return Main.projectile[drawPlayer.heldProj].type == ModContent.ProjectileType<RancorHoldout>();
		}
		return false;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		int bookType = ModContent.ProjectileType<RancorHoldout>();
		Texture2D bookTexture = TextureAssets.Projectile[bookType].Value;
		Projectile book = Main.projectile[drawPlayer.heldProj];
		Rectangle frame = bookTexture.Frame(1, Main.projFrames[bookType], 0, book.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = drawPlayer.Center + Vector2.UnitX * (float)drawPlayer.direction * 8f - Main.screenPosition;
		Color drawColor = book.GetAlpha(Color.White);
		SpriteEffects direction = (SpriteEffects)((float)book.spriteDirection != 1f);
		DrawData bookDrawData = new DrawData(bookTexture, drawPosition, frame, drawColor, book.rotation, origin, book.scale, direction);
		drawInfo.DrawDataCache.Add(bookDrawData);
	}
}
