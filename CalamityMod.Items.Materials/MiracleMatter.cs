using System;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class MiracleMatter : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 122;
	}

	public override void SetDefaults()
	{
		base.Item.width = 70;
		base.Item.height = 80;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(1);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public void DrawBackAfterimage(SpriteBatch spriteBatch, Vector2 baseDrawPosition, Rectangle frame, Vector2 origin, float baseScale)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Item.velocity.X == 0f)
		{
			float pulse = (float)Math.Cos(1.618034f * Main.GlobalTimeWrappedHourly * 2f) + (float)Math.Cos(Math.E * (double)Main.GlobalTimeWrappedHourly * 1.7000000476837158);
			pulse = pulse * 0.25f + 0.5f;
			pulse = (float)Math.Pow(pulse, 3.0);
			float num = MathHelper.Lerp(-0.3f, 1.2f, pulse);
			Color drawColor = Color.Lerp(new Color(255, 218, 99), new Color(249, 134, 44), pulse);
			drawColor *= MathHelper.Lerp(0.35f, 0.67f, CalamityUtils.Convert01To010(pulse));
			((Color)(ref drawColor)).A = 25;
			float drawPositionOffset = num * baseScale * 8f;
			for (int i = 0; i < 8; i++)
			{
				Vector2 drawPosition = baseDrawPosition + ((float)Math.PI * 2f * (float)i / 8f).ToRotationVector2() * drawPositionOffset;
				spriteBatch.Draw(TextureAssets.Item[base.Type].Value, drawPosition, (Rectangle?)frame, drawColor, 0f, origin, baseScale, (SpriteEffects)0, 0f);
			}
		}
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		Rectangle frame = TextureAssets.Item[base.Type].Value.Frame();
		DrawBackAfterimage(spriteBatch, base.Item.position - Main.screenPosition, frame, Vector2.Zero, scale);
		return true;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		base.Item.velocity.X = 0f;
		DrawBackAfterimage(spriteBatch, position, frame, origin, scale);
		return true;
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		float brightness = Main.essScale * Main.rand.NextFloat(0.9f, 1.1f);
		Lighting.AddLight(base.Item.Center, 0.94f * brightness, 0.95f * brightness, 0.56f * brightness);
		if (Main.rand.NextBool(3))
		{
			Dust dust = Dust.NewDustDirect(base.Item.position, (int)((float)base.Item.width * base.Item.scale), (int)((float)base.Item.height * base.Item.scale * 0.6f), 6);
			dust.velocity = Vector2.Lerp(Main.rand.NextVector2Unit(), -Vector2.UnitY, 0.5f) * Main.rand.NextFloat(1.8f, 2.6f);
			dust.scale *= Main.rand.NextFloat(0.85f, 1.15f);
			dust.fadeIn = 0.9f;
			dust.noGravity = true;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AuricBar>(5).AddIngredient<ExoPrism>(5).AddIngredient<LifeAlloy>()
			.AddIngredient<AscendantSpiritEssence>()
			.AddIngredient<GalacticaSingularity>(3)
			.AddIngredient<CoreofCalamity>()
			.AddTile<DraedonsForge>()
			.Register();
	}
}
