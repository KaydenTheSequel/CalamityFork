using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class WillOWisp : ModItem, ILocalizedModType, IModType
{
	public int textureVariant;

	public static Asset<Texture2D> altTexture;

	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 60;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 26;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 1);
		base.Item.rare = 2;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Material;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ((textureVariant == 0) ? TextureAssets.Item[base.Type] : CalamityUtils.GetTextureEfficient(ref altTexture, "CalamityMod/Items/Materials/WillOWisp2"));
		float rotationAmount = 0.25f;
		float rotationSpeedMult = 0.033f;
		spriteBatch.Draw(tex.Value, base.Item.Center - Main.screenPosition, (Rectangle?)null, lightColor, rotation + rotationAmount * (float)Math.Sin((double)base.Item.whoAmI + Main.timeForVisualEffects * (double)rotationSpeedMult), tex.Size() / 2f, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (textureVariant == 0)
		{
			return true;
		}
		Asset<Texture2D> tex = CalamityUtils.GetTextureEfficient(ref altTexture, "CalamityMod/Items/Materials/WillOWisp2");
		spriteBatch.Draw(tex.Value, position, (Rectangle?)null, drawColor, 0f, tex.Size() / 2f, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnSpawn(IEntitySource source)
	{
		textureVariant = Main.rand.NextBool().ToInt();
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		if (Collision.SolidCollision(base.Item.position, base.Item.width, base.Item.height + 3.TilesToPixels(), acceptTopSurfaces: true))
		{
			gravity *= 0.2f;
		}
		maxFallSpeed *= 0.1f;
		if (Collision.SolidCollision(base.Item.position, base.Item.width, base.Item.height + 2.TilesToPixels(), acceptTopSurfaces: true))
		{
			gravity *= -1f;
		}
		base.Item.velocity.X /= 0.95f;
		base.Item.velocity.X *= 0.975f;
		if (Main.rand.NextBool(10))
		{
			Dust dust = Dust.NewDustDirect(base.Item.position, base.Item.width, base.Item.height, 37, 0f, 0f, 0, Color.Red, 0.9f);
			dust.velocity *= 1f;
			dust.noGravity = true;
		}
	}
}
