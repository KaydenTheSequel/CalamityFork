using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.Deconstructors;
using CalamityMod.NPCs.DraedonLabThings;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.NPCs.SulphurousSea;
using CalamityMod.NPCs.SunkenSea;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles;

public class MonsterBanner : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		TileID.Sets.MultiTileSway[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.PlanterBox, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.DrawYOffset = -2;
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.DrawYOffset = -10;
		TileObjectData.addAlternate(0);
		TileObjectData.addTile(base.Type);
		base.DustType = -1;
		AddMapEntry(new Color(13, 88, 130), Language.GetText("MapObject.Banner"));
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (closer)
		{
			return;
		}
		int style = Main.tile[i, j].TileFrameX / 18;
		int npc = GetBannerNPC(style);
		if (npc != -1)
		{
			int itemType = TileLoader.GetItemDropFromTypeAndStyle(base.Type, style);
			if (ItemID.Sets.BannerStrength.IndexInRange(itemType) && ItemID.Sets.BannerStrength[itemType].Enabled)
			{
				Main.SceneMetrics.NPCBannerBuff[npc] = true;
				Main.SceneMetrics.hasBanner = true;
			}
		}
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		return CalamityUtils.DrawSwayingMultiTile(i, j);
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY += 2;
	}

	public static int GetBannerNPC(int style)
	{
		int npc = -1;
		switch (style)
		{
		case 0:
			npc = ModContent.NPCType<RepairUnitCritter>();
			break;
		case 1:
			npc = ModContent.NPCType<Sulflounder>();
			break;
		case 2:
			npc = ModContent.NPCType<Gnasher>();
			break;
		case 3:
			npc = ModContent.NPCType<Trasher>();
			break;
		case 4:
			npc = ModContent.NPCType<Toxicatfish>();
			break;
		case 5:
			npc = ModContent.NPCType<SlabCrab>();
			break;
		case 6:
			npc = ModContent.NPCType<Androomba>();
			break;
		case 7:
			npc = ModContent.NPCType<AquaticUrchin>();
			break;
		case 8:
			npc = ModContent.NPCType<Frogfish>();
			break;
		case 9:
			npc = ModContent.NPCType<MantisShrimp>();
			break;
		case 10:
			npc = ModContent.NPCType<AuroraSpirit>();
			break;
		case 11:
			npc = ModContent.NPCType<WildBumblebirb>();
			break;
		case 13:
			npc = ModContent.NPCType<BoxJellyfish>();
			break;
		case 14:
			npc = ModContent.NPCType<MorayEel>();
			break;
		case 15:
			npc = ModContent.NPCType<DevilFish>();
			break;
		case 16:
			npc = ModContent.NPCType<Cuttlefish>();
			break;
		case 17:
			npc = ModContent.NPCType<ToxicMinnow>();
			break;
		case 18:
			npc = ModContent.NPCType<Viperfish>();
			break;
		case 19:
			npc = ModContent.NPCType<LuminousCorvina>();
			break;
		case 20:
			npc = ModContent.NPCType<GiantSquid>();
			break;
		case 21:
			npc = ModContent.NPCType<Laserfish>();
			break;
		case 22:
			npc = ModContent.NPCType<OarfishHead>();
			break;
		case 23:
			npc = ModContent.NPCType<ColossalSquid>();
			break;
		case 24:
			npc = ModContent.NPCType<MirageJelly>();
			break;
		case 25:
			npc = ModContent.NPCType<Eidolist>();
			break;
		case 26:
			npc = ModContent.NPCType<GulperEelHead>();
			break;
		case 27:
			npc = ModContent.NPCType<EidolonWyrmHead>();
			break;
		case 28:
			npc = ModContent.NPCType<Bloatfish>();
			break;
		case 29:
			npc = ModContent.NPCType<BobbitWormHead>();
			break;
		case 30:
			npc = ModContent.NPCType<ChaoticPuffer>();
			break;
		case 31:
			npc = ModContent.NPCType<AstralProbe>();
			break;
		case 32:
			npc = ModContent.NPCType<SightseerCollider>();
			break;
		case 33:
			npc = ModContent.NPCType<SightseerSpitter>();
			break;
		case 34:
			npc = ModContent.NPCType<Aries>();
			break;
		case 35:
			npc = ModContent.NPCType<AstralSlime>();
			break;
		case 36:
			npc = ModContent.NPCType<Atlas>();
			break;
		case 37:
			npc = ModContent.NPCType<Mantis>();
			break;
		case 38:
			npc = ModContent.NPCType<Nova>();
			break;
		case 39:
			npc = ModContent.NPCType<AstralachneaGround>();
			break;
		case 40:
			npc = ModContent.NPCType<Astraglomerate>();
			break;
		case 41:
			npc = ModContent.NPCType<StellarCulex>();
			break;
		case 42:
			npc = ModContent.NPCType<FusionFeeder>();
			break;
		case 43:
			npc = ModContent.NPCType<Hadarian>();
			break;
		case 44:
			npc = ModContent.NPCType<HeatSpirit>();
			break;
		case 45:
			npc = ModContent.NPCType<Scryllar>();
			break;
		case 46:
			npc = ModContent.NPCType<DespairStone>();
			break;
		case 47:
			npc = ModContent.NPCType<SoulSlurper>();
			break;
		case 48:
			npc = ModContent.NPCType<ImpiousImmolator>();
			break;
		case 49:
			npc = ModContent.NPCType<ScornEater>();
			break;
		case 50:
			npc = ModContent.NPCType<ProfanedEnergyBody>();
			break;
		case 51:
			npc = ModContent.NPCType<Shroomble>();
			break;
		case 52:
			npc = ModContent.NPCType<WulfrumDrone>();
			break;
		case 53:
			npc = ModContent.NPCType<Rotdog>();
			break;
		case 54:
			npc = ModContent.NPCType<CladCrab>();
			break;
		case 55:
			npc = ModContent.NPCType<CalamityEye>();
			break;
		case 56:
			npc = ModContent.NPCType<Sunskater>();
			break;
		case 57:
			npc = ModContent.NPCType<ShockstormShuttle>();
			break;
		case 58:
			npc = ModContent.NPCType<CloudElemental>();
			break;
		case 59:
			npc = ModContent.NPCType<Rimehound>();
			break;
		case 60:
			npc = ModContent.NPCType<Cryon>();
			break;
		case 61:
			npc = ModContent.NPCType<IceClasper>();
			break;
		case 62:
			npc = ModContent.NPCType<Stormlion>();
			break;
		case 63:
			npc = ModContent.NPCType<Cnidrion>();
			break;
		case 66:
			npc = ModContent.NPCType<CrawlerAmethyst>();
			break;
		case 67:
			npc = ModContent.NPCType<CrawlerTopaz>();
			break;
		case 68:
			npc = ModContent.NPCType<CrawlerSapphire>();
			break;
		case 69:
			npc = ModContent.NPCType<CrawlerEmerald>();
			break;
		case 70:
			npc = ModContent.NPCType<CrawlerRuby>();
			break;
		case 71:
			npc = ModContent.NPCType<CrawlerDiamond>();
			break;
		case 72:
			npc = ModContent.NPCType<CrawlerAmber>();
			break;
		case 73:
			npc = ModContent.NPCType<CrawlerCrystal>();
			break;
		case 76:
			npc = ModContent.NPCType<EarthElemental>();
			break;
		case 77:
			npc = ModContent.NPCType<Burrower>();
			break;
		case 78:
			npc = ModContent.NPCType<Melter>();
			break;
		case 79:
			npc = ModContent.NPCType<PestilentSlime>();
			break;
		case 80:
			npc = ModContent.NPCType<Plagueshell>();
			break;
		case 81:
			npc = ModContent.NPCType<PlagueCharger>();
			break;
		case 82:
			npc = ModContent.NPCType<Viruling>();
			break;
		case 83:
			npc = ModContent.NPCType<PlaguebringerMiniboss>();
			break;
		case 84:
			npc = ModContent.NPCType<PhantomSpirit>();
			break;
		case 85:
			npc = ModContent.NPCType<OverloadedSoldier>();
			break;
		case 87:
			npc = ModContent.NPCType<Bohldohr>();
			break;
		case 88:
			npc = ModContent.NPCType<EbonianBlightSlime>();
			break;
		case 89:
			npc = ModContent.NPCType<CrimulanBlightSlime>();
			break;
		case 90:
			npc = ModContent.NPCType<AeroSlime>();
			break;
		case 91:
			npc = ModContent.NPCType<CryoSlime>();
			break;
		case 92:
			npc = ModContent.NPCType<PerennialSlime>();
			break;
		case 93:
			npc = ModContent.NPCType<InfernalCongealment>();
			break;
		case 94:
			npc = ModContent.NPCType<BloomSlime>();
			break;
		case 95:
			npc = ModContent.NPCType<RenegadeWarlock>();
			break;
		case 96:
			npc = ModContent.NPCType<ReaperShark>();
			break;
		case 97:
			npc = ModContent.NPCType<IrradiatedSlime>();
			break;
		case 98:
			npc = ModContent.NPCType<PrismBack>();
			break;
		case 99:
			npc = ModContent.NPCType<Clam>();
			break;
		case 100:
			npc = ModContent.NPCType<EutrophicRay>();
			break;
		case 101:
			npc = ModContent.NPCType<GhostBell>();
			break;
		case 102:
			npc = ModContent.NPCType<BabyGhostBell>();
			break;
		case 103:
			npc = ModContent.NPCType<SeaFloaty>();
			break;
		case 104:
			npc = ModContent.NPCType<BlindedAngler>();
			break;
		case 105:
			npc = ModContent.NPCType<SeaMinnow>();
			break;
		case 106:
			npc = ModContent.NPCType<SeaSerpent1>();
			break;
		case 108:
			npc = ModContent.NPCType<Piggy>();
			break;
		case 109:
			npc = ModContent.NPCType<FearlessGoldfishWarrior>();
			break;
		case 110:
			npc = ModContent.NPCType<Radiator>();
			break;
		case 111:
			npc = ModContent.NPCType<Trilobite>();
			break;
		case 112:
			npc = ModContent.NPCType<Orthocera>();
			break;
		case 113:
			npc = ModContent.NPCType<Skyfin>();
			break;
		case 115:
			npc = ModContent.NPCType<AcidEel>();
			break;
		case 116:
			npc = ModContent.NPCType<NuclearToad>();
			break;
		case 117:
			npc = ModContent.NPCType<FlakCrab>();
			break;
		case 118:
			npc = ModContent.NPCType<SulphurousSkater>();
			break;
		case 119:
			npc = ModContent.NPCType<BabyFlakCrab>();
			break;
		case 120:
			npc = ModContent.NPCType<AnthozoanCrab>();
			break;
		case 121:
			npc = ModContent.NPCType<BelchingCoral>();
			break;
		case 122:
			npc = ModContent.NPCType<GammaSlime>();
			break;
		case 123:
			npc = ModContent.NPCType<WulfrumGyrator>();
			break;
		case 124:
			npc = ModContent.NPCType<WulfrumHovercraft>();
			break;
		case 125:
			npc = ModContent.NPCType<WulfrumRover>();
			break;
		case 126:
			npc = ModContent.NPCType<WulfrumAmplifier>();
			break;
		case 127:
			npc = ModContent.NPCType<CannonballJellyfish>();
			break;
		case 128:
			npc = ModContent.NPCType<BabyCannonballJellyfish>();
			break;
		}
		return npc;
	}
}
