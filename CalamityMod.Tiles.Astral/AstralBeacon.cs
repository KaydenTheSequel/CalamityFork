using CalamityMod.Dusts;
using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Astral;

public class AstralBeacon : ModTile
{
	public const int Width = 5;

	public const int Height = 4;

	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Custom/AstralBeaconUse");

	public override void SetStaticDefaults()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileSpelunker[base.Type] = true;
		TileID.Sets.PreventsTileRemovalIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsTileHammeringIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsTileReplaceIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsSandfall[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(128, 128, 158), CalamityUtils.GetItemName<AstralBeaconItem>());
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		base.MinPick = 200;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = Utils.SelectRandom<int>(Main.rand, ModContent.DustType<AstralBlue>(), ModContent.DustType<AstralOrange>());
		return true;
	}

	public override bool RightClick(int i, int j)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Main.tile[i, j];
		int left = i - tile.TileFrameX / 18;
		int top = j - tile.TileFrameY / 18;
		if (!Main.LocalPlayer.HasItem(ModContent.ItemType<TitanHeart>()) && !Main.LocalPlayer.HasItem(ModContent.ItemType<Starcore>()))
		{
			return true;
		}
		if (NPC.AnyNPCs(ModContent.NPCType<AstrumDeusHead>()) || BossRushEvent.BossRushActive)
		{
			return true;
		}
		if (CalamityUtils.CountProjectiles(ModContent.ProjectileType<DeusRitualDrama>()) > 0)
		{
			return true;
		}
		bool usingStarcore = Main.LocalPlayer.HasItem(ModContent.ItemType<Starcore>());
		Vector2 ritualSpawnPosition = Utils.ToWorldCoordinates(new Vector2((float)(left + 2), (float)top), 8f, 8f);
		ritualSpawnPosition += new Vector2(0f, -24f);
		SoundEngine.PlaySound(in UseSound, ritualSpawnPosition);
		Projectile.NewProjectile(new EntitySource_WorldEvent(), ritualSpawnPosition, Vector2.Zero, ModContent.ProjectileType<DeusRitualDrama>(), 0, 0f, Main.myPlayer, 0f, usingStarcore.ToInt());
		if (!usingStarcore)
		{
			Main.LocalPlayer.ConsumeItem(ModContent.ItemType<TitanHeart>(), reverseOrder: true);
		}
		return true;
	}

	public override void MouseOver(int i, int j)
	{
		Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<TitanHeart>();
		if (Main.LocalPlayer.HasItem(ModContent.ItemType<Starcore>()))
		{
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<Starcore>();
		}
		Main.LocalPlayer.noThrow = 2;
		Main.LocalPlayer.cursorItemIconEnabled = true;
	}

	public override void MouseOverFar(int i, int j)
	{
		Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<TitanHeart>();
		if (Main.LocalPlayer.HasItem(ModContent.ItemType<Starcore>()))
		{
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<Starcore>();
		}
		Main.LocalPlayer.noThrow = 2;
		Main.LocalPlayer.cursorItemIconEnabled = true;
	}
}
