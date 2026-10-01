using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.FurnitureDriftwood;
using CalamityMod.NPCs.Leviathan;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class NaiadsWarhorn : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle HornSound = new SoundStyle("CalamityMod/Sounds/Item/LeviathanHornSound")
	{
		Volume = 0.55f
	};

	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 12;
	}

	public override void SetDefaults()
	{
		base.Item.width = 108;
		base.Item.height = 68;
		base.Item.rare = 7;
		base.Item.useAnimation = 30;
		base.Item.useTime = 30;
		base.Item.useStyle = 5;
		base.Item.consumable = false;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(5f, 8f);
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ZoneBeach && !player.Calamity().ZoneSulphur && !NPC.AnyNPCs(ModContent.NPCType<Anahita>()) && !NPC.AnyNPCs(ModContent.NPCType<Leviathan>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		int posX = (int)player.position.X;
		int posY = (int)(player.position.Y - 700f);
		int bossToSpawn = ModContent.NPCType<Anahita>();
		CalamityUtils.SpawnBossOnPosUsingItem(player, bossToSpawn, posX, posY, new SoundStyle?(HornSound));
		return true;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(tex, base.Item.Center - Main.screenPosition + Vector2.UnitY * 20f, (Rectangle?)null, lightColor, rotation, tex.Size() / 2f, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Driftwood>(10).AddIngredient<AbyssGravel>(15).AddIngredient<Lumenyl>(10)
			.AddTile(134)
			.Register();
	}
}
