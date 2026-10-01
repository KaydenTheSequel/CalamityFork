using CalamityMod.NPCs.StormWeaver;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Skies;

public class StormWeaverFlashSky : CustomSky
{
	public int StormWeaverHeadIndex = -1;

	public override void Update(GameTime gameTime)
	{
		int weaverType = ModContent.NPCType<StormWeaverHead>();
		if (StormWeaverHeadIndex < 0 || !Main.npc[StormWeaverHeadIndex].active || Main.npc[StormWeaverHeadIndex].type != weaverType)
		{
			StormWeaverHeadIndex = NPC.FindFirstNPC(weaverType);
		}
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		if (!(maxDepth < float.MaxValue) && Main.npc.IndexInRange(StormWeaverHeadIndex))
		{
			Texture2D white = TextureAssets.MagicPixel.Value;
			float lightningFlashPower = (Main.npc[StormWeaverHeadIndex].ModNPC as StormWeaverHead).lightning;
			Vector2 scale = default(Vector2);
			((Vector2)(ref scale))._002Ector((float)Main.screenWidth * 1.1f / (float)white.Width, (float)Main.screenHeight * 1.1f / (float)white.Height);
			Vector2 screenCenter = new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f;
			Color drawColor = Color.White * MathHelper.Lerp(0f, 0.88f, lightningFlashPower);
			Vector2 origin = white.Size() * 0.5f;
			for (int i = 0; i < 2; i++)
			{
				spriteBatch.Draw(white, screenCenter, (Rectangle?)null, drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
			}
		}
	}

	public override void Reset()
	{
	}

	public override void Activate(Vector2 position, params object[] args)
	{
	}

	public override void Deactivate(params object[] args)
	{
	}

	public override bool IsActive()
	{
		if (StormWeaverHeadIndex != -1)
		{
			return !Main.gameMenu;
		}
		return false;
	}
}
