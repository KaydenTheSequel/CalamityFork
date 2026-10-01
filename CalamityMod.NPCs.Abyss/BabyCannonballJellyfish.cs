using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Critters;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class BabyCannonballJellyfish : ModNPC
{
	public bool hasBeenHit;

	public int explosionDamage;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		Main.npcCatchable[base.Type] = true;
		NPCID.Sets.CountsAsCritter[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 0.1f;
		base.NPC.noGravity = true;
		base.NPC.chaseable = false;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 0;
		explosionDamage = 30;
		base.NPC.width = 28;
		base.NPC.height = 36;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 5;
		base.NPC.knockBackResist = 1f;
		base.NPC.alpha = 100;
		base.NPC.HitSound = SoundID.NPCHit25;
		base.NPC.DeathSound = SoundID.NPCDeath28;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<BabyCannonballJellyfishBanner>();
		base.NPC.catchItem = (short)ModContent.ItemType<BabyCannonballJellyfishItem>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer1Biome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.BabyCannonballJellyfish")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.chaseable);
		writer.Write(hasBeenHit);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.chaseable = reader.ReadBoolean();
		hasBeenHit = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.localAI[0] == 0f && Main.netMode != 1)
		{
			base.NPC.localAI[0] = 1f;
			base.NPC.velocity.Y = -3f;
			base.NPC.netUpdate = true;
		}
		Lighting.AddLight(base.NPC.Center, (float)(67 - base.NPC.alpha) * 1f / 255f, (float)(218 - base.NPC.alpha) * 1f / 255f, (float)(166 - base.NPC.alpha) * 1f / 255f);
		if (base.NPC.wet)
		{
			base.NPC.noGravity = true;
			if (base.NPC.velocity.Y < 0f)
			{
				base.NPC.velocity.Y += 0.1f;
			}
			if (base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y = 0f;
			}
		}
		else
		{
			base.NPC.noGravity = false;
		}
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return hasBeenHit;
		}
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer1 && spawnInfo.Water)
		{
			return SpawnCondition.CaveJellyfish.Chance;
		}
		return 0f;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Vector2 val = new Vector2(base.NPC.Center.X, base.NPC.Center.Y);
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		Vector2 vector = val - screenPos;
		vector -= new Vector2((float)TextureAssets.Npc[base.Type].Value.Width, (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type])) * 1f / 2f;
		vector += halfSizeTexture * 1f + new Vector2(0f, 4f + base.NPC.gfxOffY);
		Color color = Utils.MultiplyRGBA(new Color(127 - base.NPC.alpha, 127 - base.NPC.alpha, 127 - base.NPC.alpha, 0), new Color(67, 218, 166));
		Main.spriteBatch.Draw(TextureAssets.Npc[base.Type].Value, vector, (Rectangle?)base.NPC.frame, color, base.NPC.rotation, halfSizeTexture, 1f, spriteEffects, 0f);
	}

	public override bool CheckDead()
	{
		Explode();
		return false;
	}

	private void Explode()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.position = base.NPC.Center;
		base.NPC.width = (int)((float)base.NPC.width * 3f);
		base.NPC.height = (int)((float)base.NPC.height * 3f);
		NPC nPC = base.NPC;
		nPC.position -= base.NPC.Size * 0.5f;
		SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			if (!player.dead)
			{
				Rectangle hitbox = base.NPC.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(player.Hitbox))
				{
					player.Hurt(PlayerDeathReason.ByNPC(base.NPC.whoAmI), explosionDamage, base.NPC.direction);
				}
			}
		}
		for (int k = 0; k < 8; k++)
		{
			int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 323, 0f, -1f);
			if (Main.dust.IndexInRange(dust))
			{
				Main.dust[dust].noGravity = true;
			}
		}
		base.NPC.active = false;
		base.NPC.NPCLoot();
		base.NPC.netUpdate = true;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 2; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 323, hit.HitDirection, -1f);
		}
	}
}
