using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems.Invasion;

[LegacyName(new string[] { "MartianDistressBeacon" })]
public class MartianDistressRemote : ModItem, ILocalizedModType, IModType
{
	public int frameCounter;

	public int frame;

	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 17;
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 52;
		base.Item.consumable = true;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.useAnimation = (base.Item.useTime = 10);
		base.Item.UseSound = SoundID.Zombie67;
		base.Item.useStyle = 4;
		base.Item.value = Item.buyPrice(0, 50);
		base.Item.rare = 8;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.EventItem;
	}

	public override bool CanUseItem(Player player)
	{
		if (Main.invasionType != 0)
		{
			return false;
		}
		if (!Main.player.Any((Player p) => p.active && p.ConsumedLifeCrystals >= 5))
		{
			return false;
		}
		return true;
	}

	public override bool? UseItem(Player player)
	{
		if (Main.netMode == 0)
		{
			Main.invasionDelay = 0;
			Main.StartInvasion(4);
			return true;
		}
		if (player.whoAmI == Main.myPlayer)
		{
			NetMessage.SendData(61, -1, -1, null, player.whoAmI, -7f);
			return true;
		}
		return true;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frameI, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/Invasion/MartianDistressRemote_Animated", (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, position, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 5, 12), Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/Invasion/MartianDistressRemote_Animated", (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 5, 12), lightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		return false;
	}
}
