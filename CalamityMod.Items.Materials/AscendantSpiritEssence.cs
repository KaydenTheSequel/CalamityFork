using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class AscendantSpiritEssence : ModItem, ILocalizedModType, IModType
{
	public int frameCounter;

	public int frame;

	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 6));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.ItemNoGravity[base.Type] = true;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 118;
	}

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 54;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 6);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Item[base.Type].Value;
		spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 6, 6), lightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Materials/AscendantSpiritEssenceGlow", (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 6, 6, frameCounterUp: false), Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		float brightness = Main.essScale * Main.rand.NextFloat(0.9f, 1.1f);
		Lighting.AddLight(base.Item.Center, 1.2f * brightness, 0.4f * brightness, 0.8f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Necroplasm>(2).AddIngredient<NightmareFuel>(5).AddIngredient<EndothermicEnergy>(5)
			.AddIngredient<DarksunFragment>(2)
			.AddTile(134)
			.Register();
	}
}
