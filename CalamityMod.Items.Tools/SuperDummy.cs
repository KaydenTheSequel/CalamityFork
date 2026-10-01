using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Packets;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class SuperDummy : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 30;
		base.Item.useAnimation = (base.Item.useTime = 15);
		base.Item.useStyle = 1;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
		base.Item.value = Item.sellPrice(0, 0, 1);
		base.Item.rare = 1;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public static void DeleteDummies()
	{
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (npc.type == ModContent.NPCType<SuperDummyNPC>())
			{
				npc.life = 0;
				npc.active = false;
				if (Main.dedServ)
				{
					NetMessage.SendData(23, -1, -1, null, npc.whoAmI);
				}
			}
		}
	}

	public override bool? UseItem(Player player)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			if (Main.myPlayer == player.whoAmI)
			{
				if (Main.netMode == 0)
				{
					DeleteDummies();
				}
				else
				{
					DeleteAllSuperDummiesPacket.Send();
				}
			}
		}
		else if (player.whoAmI == Main.myPlayer && NPC.CountNPCS(ModContent.NPCType<SuperDummyNPC>()) < 50)
		{
			int x = (int)Main.MouseWorld.X - 9;
			int y = (int)Main.MouseWorld.Y - 20;
			if (Main.netMode == 0)
			{
				NPC.NewNPC(new EntitySource_ItemUse(player, base.Item), x, y, ModContent.NPCType<SuperDummyNPC>());
			}
			else
			{
				SpawnSuperDummyPacket.Send(x, y);
			}
		}
		return true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void RightClick(Player player)
	{
		if (Main.netMode == 0)
		{
			DeleteDummies();
		}
		else
		{
			DeleteAllSuperDummiesPacket.Send();
		}
		base.Item.RestoreConsumedItemByRightClick();
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(3202);
		recipe.Register();
		Recipe recipe2 = Recipe.Create(3202);
		recipe2.AddIngredient<SuperDummy>();
		recipe2.Register();
		recipe2.SortAfterFirstRecipesOf(3202);
	}
}
