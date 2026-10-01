using CalamityMod.Items.Placeables.Banners;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Deconstructors;

public class BurrowerHitbox : BaseWormHitboxNPC
{
	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.Burrower.DisplayName");

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.width = 88;
		base.NPC.height = 88;
		base.NPC.lifeMax = 100;
		base.NPC.value = 0f;
		base.NPC.HitSound = ThanatosHead.ThanatosHitSoundClosed;
		base.NPC.DeathSound = CommonCalamitySounds.WulfrumNPCDeathSound;
		base.NPC.knockBackResist = 0f;
		base.NPC.behindTiles = true;
		base.NPC.noTileCollide = true;
		base.NPC.netAlways = true;
		base.NPC.SuperArmor = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToCold = false;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = false;
		base.NPC.chaseable = false;
		base.Banner = ModContent.NPCType<Burrower>();
		base.BannerItem = ModContent.ItemType<BurrowerBanner>();
		base.SetDefaults();
	}

	public override void AI()
	{
		base.AI();
		NPC headNPC = Main.npc[(int)base.NPC.ai[0]];
		base.NPC.Calamity().DR = headNPC.Calamity().DR;
		base.NPC.HitSound = headNPC.HitSound;
		base.NPC.width = 38;
		base.NPC.height = 38;
	}
}
