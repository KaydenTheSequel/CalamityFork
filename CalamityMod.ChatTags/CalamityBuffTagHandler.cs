using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace CalamityMod.ChatTags;

public sealed class CalamityBuffTagHandler : AbstractTagHandler<CalamityBuffTagHandler>
{
	public sealed class Snippet(int buffId) : TextSnippet
	{
		private const float IconSize = 26f;

		public int BuffId => buffId;

		public bool DrawIcon => true;

		public override bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = default(Vector2), Color color = default(Color), float scale = 1f)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0108: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			size = new Vector2(GetStringLength(FontAssets.MouseText.Value), 26f);
			if (!justCheckingString && (((Color)(ref color)).R != 0 || ((Color)(ref color)).G != 0 || ((Color)(ref color)).B != 0))
			{
				if (DrawIcon)
				{
					if (Main.netMode != 2 && !Main.dedServ)
					{
						Asset<Texture2D> texture = TextureAssets.Buff[BuffId];
						spriteBatch.Draw(texture.Value, new Rectangle((int)position.X, (int)position.Y - 2, 26, 26), (Rectangle?)null, Color.White);
					}
					position.X += 26f;
				}
				Color buffColor = CalamityUtils.GetDebuffTooltipNameColor(buffId);
				string name = (DrawIcon ? " " : "") + Lang.GetBuffName(buffId);
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, name, position, buffColor, 0f, Vector2.Zero, new Vector2(scale));
			}
			return true;
		}

		public override float GetStringLength(DynamicSpriteFont font)
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			return (((!DrawIcon) ? 0f : (26f + font.MeasureString(" ").X)) + font.MeasureString(Lang.GetBuffName(buffId)).X) * Scale;
		}
	}

	protected override string[] TagNames { get; } = new string[1] { "cbuff" };

	public override TextSnippet Parse(string text, Color baseColor = default(Color), string options = null)
	{
		if (int.TryParse(text, out var buffId) && buffId >= 0 && buffId < BuffLoader.BuffCount)
		{
			return new Snippet(buffId);
		}
		if (BuffID.Search.TryGetId(text, ref buffId))
		{
			return new Snippet(buffId);
		}
		return new TextSnippet(text);
	}
}
