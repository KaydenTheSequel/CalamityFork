using CalamityMod.BiomeManagers;
using CalamityMod.Items.Critters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DraedonLabThings;

public class Nanodroid : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		NPCID.Sets.CountsAsCritter[base.Type] = true;
		Main.npcCatchable[base.Type] = true;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "_Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.CloneDefaults(358);
		base.NPC.width = 16;
		base.NPC.height = 12;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath44;
		base.NPC.catchItem = (short)ModContent.ItemType<NanodroidItem>();
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<ArsenalLabBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Nanodroid")
		});
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.NPC.Center, 0.2f, 0.2f, 0.4f);
	}

	public override bool? CanBeHitByItem(Player player, Item item)
	{
		return null;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.spriteDirection = base.NPC.direction;
		base.NPC.frameCounter += 0.30000001192092896;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 226);
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		Texture2D critterTexture = TextureAssets.Npc[base.Type].Value;
		Texture2D glowmask = GlowTexture.Value;
		Vector2 drawPosition = base.NPC.Center - screenPos + Vector2.UnitY * base.NPC.gfxOffY;
		SpriteEffects direction = (SpriteEffects)(base.NPC.spriteDirection == 1);
		spriteBatch.Draw(critterTexture, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction, 0f);
		spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(Color.White), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction, 0f);
		return false;
	}
}
