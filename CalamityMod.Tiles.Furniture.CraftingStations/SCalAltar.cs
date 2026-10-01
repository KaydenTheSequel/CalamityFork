using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.CraftingStations;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture.CraftingStations;

public class SCalAltar : ModTile
{
	public static readonly SoundStyle SummonSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalAltarSummon");

	public const int Width = 4;

	public const int Height = 3;

	public override void SetStaticDefaults()
	{
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		TileID.Sets.PreventsTileRemovalIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsTileHammeringIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsSandfall[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.Width = 4;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(1, 2);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(43, 19, 42), CalamityUtils.GetItemName<AltarOfTheAccursedItem>());
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		RegisterItemDrop(ModContent.ItemType<AltarOfTheAccursedItem>());
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = 60;
		return true;
	}

	public override bool RightClick(int i, int j)
	{
		return AttemptToSummonSCal(i, j);
	}

	public override void MouseOver(int i, int j)
	{
		HoverItemIcon(i, j);
	}

	public override void MouseOverFar(int i, int j)
	{
		HoverItemIcon(i, j);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == ModContent.ProjectileType<SCalAltarArenaVisual>())
			{
				p.Kill();
				break;
			}
		}
	}

	public static void HoverItemIcon(int i, int j)
	{
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<DeliciousMeat>() && Main.zenithWorld)
		{
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<DeliciousMeat>();
		}
		else if (Main.LocalPlayer.HasItem(ModContent.ItemType<CeremonialUrn>()))
		{
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<CeremonialUrn>();
		}
		else
		{
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<AshesofCalamity>();
		}
		Main.LocalPlayer.noThrow = 2;
		Main.LocalPlayer.cursorItemIconEnabled = true;
		if ((Main.LocalPlayer.builderAccStatus[0] == 0 || (Main.LocalPlayer.builderAccStatus[1] == 0 && Main.LocalPlayer.rulerGrid)) && !CalamityUtils.AnyProjectiles(ModContent.ProjectileType<SCalAltarArenaVisual>()) && !CalamityUtils.AnyProjectiles(ModContent.ProjectileType<SCalRitualDrama>()) && !NPC.AnyNPCs(ModContent.NPCType<SupremeCalamitas>()))
		{
			Tile t = Main.tile[i, j];
			Vector2 arenaCenter = Utils.ToWorldCoordinates(new Vector2((float)(i - t.TileFrameX / 18 + 2), (float)(j - t.TileFrameY / 18)), 8f, 8f) - Vector2.UnitY * 24f;
			Projectile.NewProjectile(new EntitySource_WorldEvent(), arenaCenter, Vector2.Zero, ModContent.ProjectileType<SCalAltarArenaVisual>(), 0, 0f, Main.myPlayer, CalamityWorld.death.ToInt());
		}
	}

	public static bool AttemptToSummonSCal(int i, int j)
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.LocalPlayer.HasItem(ModContent.ItemType<AshesofCalamity>()) && !Main.LocalPlayer.HasItem(ModContent.ItemType<CeremonialUrn>()) && (Main.LocalPlayer.HeldItem.type != ModContent.ItemType<DeliciousMeat>() || !Main.zenithWorld))
		{
			return true;
		}
		bool meat = Main.LocalPlayer.HeldItem.type == ModContent.ItemType<DeliciousMeat>() && Main.zenithWorld;
		if (NPC.AnyNPCs(ModContent.NPCType<SupremeCalamitas>()) || BossRushEvent.BossRushActive)
		{
			return true;
		}
		if (CalamityUtils.CountProjectiles(ModContent.ProjectileType<SCalRitualDrama>()) > 0)
		{
			return true;
		}
		bool usingSpecialItem = Main.LocalPlayer.HasItem(ModContent.ItemType<CeremonialUrn>());
		Tile tile = Main.tile[i, j];
		int num = i - tile.TileFrameX / 18;
		int top = j - tile.TileFrameY / 18;
		Vector2 ritualSpawnPosition = Utils.ToWorldCoordinates(new Vector2((float)(num + 2), (float)top), 8f, 8f);
		ritualSpawnPosition += new Vector2(0f, -24f);
		SoundEngine.PlaySound(in SummonSound, ritualSpawnPosition);
		Projectile.NewProjectile(new EntitySource_WorldEvent(), ritualSpawnPosition, Vector2.Zero, ModContent.ProjectileType<SCalRitualDrama>(), 0, 0f, Main.myPlayer, 0f, meat.ToInt());
		if (meat)
		{
			Main.LocalPlayer.ConsumeItem(ModContent.ItemType<DeliciousMeat>(), reverseOrder: true);
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.type == ModContent.NPCType<Archmage>())
				{
					n.active = false;
				}
			}
		}
		else if (!usingSpecialItem)
		{
			Main.LocalPlayer.ConsumeItem(ModContent.ItemType<AshesofCalamity>(), reverseOrder: true);
		}
		return true;
	}
}
