using System.Runtime.CompilerServices;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Fonts;

public sealed class FontAssetSystem : ModSystem
{
	[CompilerGenerated]
	private static Asset<DynamicSpriteFont> _003CCodebreakerDialog_003Ek__BackingField;

	[CompilerGenerated]
	private static Asset<DynamicSpriteFont> _003CImpact_003Ek__BackingField;

	[CompilerGenerated]
	private static Asset<DynamicSpriteFont> _003CFlexure_003Ek__BackingField;

	public static Asset<DynamicSpriteFont> MouseText => FontAssets.MouseText;

	public static Asset<DynamicSpriteFont> ItemStack => FontAssets.ItemStack;

	public static Asset<DynamicSpriteFont> DeathText => FontAssets.DeathText;

	public static Asset<DynamicSpriteFont> CombatText => FontAssets.CombatText[0];

	public static Asset<DynamicSpriteFont> CombatTextCrit => FontAssets.CombatText[1];

	public static Asset<DynamicSpriteFont> CodebreakerDialog => _003CCodebreakerDialog_003Ek__BackingField ?? (_003CCodebreakerDialog_003Ek__BackingField = GetFont("Fonts/CodebreakerDialog"));

	public static Asset<DynamicSpriteFont> Impact => _003CImpact_003Ek__BackingField ?? (_003CImpact_003Ek__BackingField = GetFont("Fonts/Impact"));

	public static Asset<DynamicSpriteFont> Flexure => _003CFlexure_003Ek__BackingField ?? (_003CFlexure_003Ek__BackingField = GetFont("Fonts/Flexure"));

	private static Asset<DynamicSpriteFont> GetFont(string path)
	{
		return ModContent.GetInstance<CalamityMod>().Assets.Request<DynamicSpriteFont>(path, (AssetRequestMode)1);
	}
}
