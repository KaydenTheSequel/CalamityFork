using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class ConcentratedVoidAuraLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new BeforeParent(PlayerDrawLayers.Skin);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		Player drawPlayer = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = drawPlayer.Calamity();
		if (drawInfo.shadow == 0f && !drawPlayer.dead)
		{
			if (!modPlayer.voidAura)
			{
				return modPlayer.voidAuraDamage;
			}
			return true;
		}
		return false;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/VoidConcentrationAura", (AssetRequestMode)2).Value;
		Vector2 drawPos = drawInfo.Center - Main.screenPosition + new Vector2(0f, drawPlayer.gfxOffY);
		drawPos.Y -= 9f;
		SpriteEffects spriteEffects = (SpriteEffects)(drawPlayer.direction != -1);
		float scale = 1.75f;
		Rectangle frame = tex.Frame(1, 4, 0, drawPlayer.Calamity().voidFrame);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)tex.Width / 2f, (float)tex.Height / 2f / 4f);
		drawInfo.DrawDataCache.Add(new DrawData(tex, drawPos, frame, Color.White * 0.4f, 0f, origin, scale, spriteEffects));
	}
}
