using CalamityMod.Items.Accessories;
using CalamityMod.Items.Placeables.FurnitureWulfrum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "WulfrumShard" })]
public class WulfrumMetalScrap : ModItem, ILocalizedModType, IModType
{
	public int textureVariant;

	public static Asset<Texture2D> altTexture;

	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		base.Item.width = 13;
		base.Item.height = 10;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 0, 10);
		base.Item.rare = 1;
		base.Item.ammo = base.Item.type;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Material;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (textureVariant == 0)
		{
			return true;
		}
		spriteBatch.Draw(altTexture.Value, base.Item.Center - Main.screenPosition, (Rectangle?)null, lightColor, rotation, altTexture.Size() / 2f, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (textureVariant == 0)
		{
			return true;
		}
		spriteBatch.Draw(altTexture.Value, position, (Rectangle?)null, drawColor, 0f, altTexture.Size() / 2f, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		if (!(source is EntitySource_Loot))
		{
			return;
		}
		textureVariant = Main.rand.NextBool().ToInt();
		if (Main.rand.NextBool())
		{
			return;
		}
		bool closePlayer = false;
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			Vector2 val = player.Center - base.Item.Center;
			if (((Vector2)(ref val)).Length() < 1200f && player.GetModPlayer<WulfrumBatteryPlayer>().battery)
			{
				closePlayer = true;
				break;
			}
		}
		if (closePlayer)
		{
			base.Item.stack++;
			SoundEngine.PlaySound(in WulfrumBattery.ExtraDropSound, base.Item.Center);
			int numDust = Main.rand.Next(3, 7);
			for (int i = 0; i < numDust; i++)
			{
				Vector2 position = base.Item.position;
				int width = base.Item.width;
				int height = base.Item.height;
				int type = (Main.rand.NextBool() ? 246 : 247);
				float scale = Main.rand.NextFloat(0.9f, 1f);
				Dust.NewDustDirect(position, width, height, type, 0f, -3f, 0, default(Color), scale);
			}
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumPlatform>(2).DisableDecraft().Register();
	}

	public override void Load()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		On_Item.CanFillEmptyAmmoSlot += new hook_CanFillEmptyAmmoSlot(AvoidDefaultingToAmmoSlot);
		altTexture = ModContent.Request<Texture2D>(Texture + "2", (AssetRequestMode)2);
	}

	private bool AvoidDefaultingToAmmoSlot(orig_CanFillEmptyAmmoSlot orig, Item self)
	{
		if (self.type == base.Type)
		{
			return false;
		}
		return orig.Invoke(self);
	}
}
