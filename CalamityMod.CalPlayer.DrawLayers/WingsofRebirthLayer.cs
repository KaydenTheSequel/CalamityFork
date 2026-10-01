using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class WingsofRebirthLayer : PlayerDrawLayer
{
	public static Asset<Texture2D> yharwingTexture;

	public override void Load()
	{
		yharwingTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Accessories/Wings/WingsofRebirth_Wings_Real", (AssetRequestMode)2);
	}

	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.Wings);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		return drawInfo.drawPlayer.wings == EquipLoader.GetEquipSlot(base.Mod, "WingsofRebirth", EquipType.Wings);
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		if (!drawPlayer.dead)
		{
			Texture2D texture = yharwingTexture.Value;
			Vector2 Position = drawInfo.Position;
			Vector2 pos = default(Vector2);
			((Vector2)(ref pos))._002Ector((float)(int)(Position.X - Main.screenPosition.X + (float)(drawPlayer.width / 2) - (float)(2 * drawPlayer.direction)), (float)(int)(Position.Y - Main.screenPosition.Y + ((float)(drawPlayer.height / 2) + drawPlayer.HeightOffsetVisual / 2f) - 2f * drawPlayer.gravDir));
			Color color = Lighting.GetColor((int)drawPlayer.Center.X / 16, (int)drawPlayer.Center.Y / 16, Color.White) * (1f - drawInfo.shadow);
			DrawData d = new DrawData(texture, pos, texture.Frame(1, 9, 0, drawInfo.drawPlayer.wingFrame), color, 0f, new Vector2((float)(texture.Width / 2), (float)(texture.Height / 18)), 1f, drawInfo.playerEffect);
			d.shader = drawInfo.drawPlayer.cWings;
			drawInfo.DrawDataCache.Add(d);
		}
	}
}
